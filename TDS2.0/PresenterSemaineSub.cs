using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core
{
    public interface IViewCellSemaineSub
    {
        UserControl getControl();
    }

    public abstract class IPresenterCellSemaineSub
    {
        PresenterSemaineSub racine;
        public abstract IViewCellSemaineSub getView();
        public abstract IModelCellSemaineSub getModel();
        public IPresenterCellSemaineSub(PresenterSemaineSub racine,IViewCellSemaineSub view, IModelCellSemaineSub model)
        {
            this.racine = racine;
            //view.clickSouris += clickSouris;
            this.actionSelect += actionSelected;
            this.actionUnselect += actionUnselected;
            //actionUnselect();
        }
        protected void clickSouris(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right )
            {
                //presenter.addListSelection(this);
                Point screenPoint = Cursor.Position;
                racine.makeContextMenu(screenPoint);
            }
        }
        public delegate void actionSelectionCell();
        public actionSelectionCell actionSelect;
        public actionSelectionCell actionUnselect;


        public void actionSelected()
        {
            if( (System.Windows.Forms.Control.ModifierKeys & Keys.Control) != Keys.Control )
                racine.clearListSelection();
            racine.addListSelection(this);
        }
        public void actionUnselected()
        {
            racine.removeListSelection(this);
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                racine.clearListSelection();
                //this.actionSelect();
            }
        }
    }

    public interface IModelCellSemaineSub
    {
        void actionDisponible(ListActionItem listAction, IViewSemaineSub view);
    }



    public abstract class MultiSelection<T>
    {
        public delegate void actionSelectionCell();
        public actionSelectionCell actionSelect;
        public actionSelectionCell actionUnselect;
        GestionSelection<T> gestion;
        public abstract T data { get; }
        public MultiSelection(GestionSelection<T> gestion)
        {
            this.gestion = gestion;
            this.actionSelect += selected;
            this.actionUnselect += unselected;
        }
        public void selected()
        {
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != Keys.Control)
                gestion.clearListSelection();
            gestion.addListSelection(this);
        }
        public void unselected()
        {
            gestion.removeListSelection(this);
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                gestion.clearListSelection();
                //this.actionSelect();
            }
        }
    }

    

    public class GestionSelection<T>
    {
        public List<T> ListSelection 
        {
            get
            {
                List<T> liste = new List<T>();
                foreach (MultiSelection<T> select in listSelection)
                    liste.Add(select.data);
                return liste;
            }
        }
        bool clearAll = false;
        List<MultiSelection<T>> listSelection = new List<MultiSelection<T>>();
        public void clearListSelection()
        {
            if (clearAll == false)
            {
                clearAll = true;
                MultiSelection<T>[] tabDelete = listSelection.ToArray();
                foreach (MultiSelection<T> cellPre in tabDelete)
                {
                    cellPre.actionUnselect();
                }
                clearAll = false;
            }
        }
        public void addListSelection(MultiSelection<T> cell)
        {
            listSelection.Add(cell);
        }
        public void removeListSelection(MultiSelection<T> cell)
        {
            listSelection.Remove(cell);
        }
        //public void makeContextMenu(Point point)
        //{
        //    ListActionItem listAction = new ListActionItem();
        //    foreach (IPresenterCellSemaineSub cell in listSelection)
        //    {
        //        cell.getModel().actionDisponible(listAction, this.view);
        //    }
        //    view.resetContext();
        //    listAction.build();
        //    view.showContext(point);
        //}
    }

/***************************************************************************************************************************************************/


    public interface IViewSemaineSub
    {
    //date
        event EventHandler changeDate;
        DateTime DateSelected { get; set; }
    //tableau
        List<List<UserControl>> tableau{set;}
    //sub
        event EventHandler changeSub;
        List<MetierSub> ListSub { set; }
        MetierSub SubSelected { get; set; }
    //menu contextuel
        void resetContext();
        void addItem(ToolStripItem item);
        void showContext(Point point);
    }

    public class PresenterSemaineSub
    {
        IViewSemaineSub view;
        IModelSemaineSub model;
        
        List<List<UserControl>> tableauView = new List<List<UserControl>>();

        public List<List<UserControl>> Tableau{
            get{
                tableauView.Clear();
                List<ICycle> listCycle = model.getListCycle(model.Date, model.Date.AddDays(10));
                {
                    List<UserControl> column = new List<UserControl>();
                    column.Add(null);
                    foreach (ICycle cycle in listCycle)
                    {
                        List<ITypeVacation> tabTypeVacation = cycle.getListTypeVacation();
                        foreach (ITypeVacation typeVacation in tabTypeVacation)
                        {
                            column.Add(new ViewTypeVacation(typeVacation));
                        }
                    }
                    tableauView.Add(column);
                }
                for( int iDay=0; iDay <10; ++iDay)
                {
                    DateTime dt = model.Date.AddDays(iDay);
                    List<UserControl> column = new List<UserControl>();
                    column.Add(new ViewDate(dt));
                    foreach (ICycle cycle in listCycle)
                    {
                        List<ITypeVacation> tabTypeVacation = cycle.getListTypeVacation();                                 
                        foreach (ITypeVacation typeVacation in tabTypeVacation)
                        {
                            if( cycle.DateFin > dt && dt > cycle.DateDebut )
                                column.Add(typeVacation.makeView(this, dt).getControl());
                            else
                                column.Add(null);
                        }
                    }
                    tableauView.Add(column);
                }
                return tableauView;
            }
        }
        public PresenterSemaineSub(IViewSemaineSub view, IModelSemaineSub model)
        {
            this.view = view;
            this.model = model;            
            view.changeDate += changeDate;
            view.DateSelected = model.Date;            
            view.ListSub = model.getListSub(model.Date,model.Date.AddDays(10));
            model.Sub = view.SubSelected;
            view.changeSub += changeSub;
            view.tableau = this.Tableau;
        }

        void changeDate(object sender, EventArgs e)
        {
            model.Date = view.DateSelected;
            MetierSub sub = view.SubSelected;
            List<MetierSub> listSub = model.getListSub(model.Date, model.Date.AddDays(10));
            view.ListSub = listSub;
            if (listSub.Contains(sub))
                view.SubSelected = sub;
            view.tableau = this.Tableau;
        }
        void changeSub(object sender, EventArgs e)
        {
            model.Sub = view.SubSelected;
            view.tableau = this.Tableau;
        }
//selection multiple
        bool controlKeyPressed = false;        
        public bool ControlKeyPressed { get { return controlKeyPressed; } }
        public void keyControlDown()
        {
            controlKeyPressed = true;
        }
        public void keyControlUp()
        {
            controlKeyPressed = false;
        }
        

//gestion des cell
        bool clearAll = false;
        List<IPresenterCellSemaineSub> listSelection = new List<IPresenterCellSemaineSub>();
        public void clearListSelection()
        {
            if (clearAll == false)
            {
                clearAll = true;
                IPresenterCellSemaineSub[] tabDelete = listSelection.ToArray();
                foreach (IPresenterCellSemaineSub cellPre in tabDelete)
                {
                    cellPre.actionUnselect();
                }
                clearAll = false;
            }
        }
        public void addListSelection(IPresenterCellSemaineSub cell)
        {
            listSelection.Add(cell);
        }
        public void removeListSelection(IPresenterCellSemaineSub cell)
        {
            listSelection.Remove(cell);
        }
        public void makeContextMenu(Point point)
        {
            ListActionItem listAction = new ListActionItem();
            foreach (IPresenterCellSemaineSub cell in listSelection)
            {
                cell.getModel().actionDisponible(listAction, this.view);
            }
            view.resetContext();
            listAction.build();
            view.showContext(point);
        }
    }

    public interface IModelSemaineSub
    {
        List<ICycle> getListCycle(DateTime dateDebut, DateTime dateFin);
        DateTime Date { get; set; }
        MetierSub Sub { set; }
        List<MetierSub> getListSub(DateTime dateDebut, DateTime dateFin);
    }

    public class ModelSemaineSub : IModelSemaineSub
    {
        MetierSub sub = null;
        DateTime date = DateTime.Now;
        
        public ModelSemaineSub(int nbJour)
        {
        }
        public List<ICycle> getListCycle(DateTime dateDebut, DateTime dateFin)
        {
            if (sub == null)
                return new List<ICycle>();
            return DaoCycle.find<ICycle>(sub, dateDebut, dateFin);
        }


        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public MetierSub Sub
        {
            set { sub = value; }
        }

        public List<MetierSub> getListSub(DateTime dateDebut, DateTime dateFin)
        {
            return DaoSub.find(dateDebut, dateFin);
        }
    }
}
