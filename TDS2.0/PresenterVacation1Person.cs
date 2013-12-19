using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Core
{
    public interface IViewVacation1Person : IViewCellSemaineSub
    {
        event MouseEventHandler clickSouris;
        void selected();
        void unselected();
        String NomAgent { set; }
    }

    public class PresenterVacation1Person : IPresenterCellSemaineSub
    {
        IViewVacation1Person view;
        IModelVacation1Person model;
        bool selected = false;
        public PresenterVacation1Person(PresenterSemaineSub racine, IViewVacation1Person view, IModelVacation1Person model)
            : base(racine, view, model)
        {
            this.view = view;
            this.model = model;
            view.NomAgent = model.NomAgent;
            model.ModelChanged += notifyUpdateModel;
            view.clickSouris += clickSouris;
            this.actionSelect += select;
            this.actionUnselect += unselect;
        }
        public override IViewCellSemaineSub getView() { return (IViewCellSemaineSub)view; }
        public override IModelCellSemaineSub getModel() { return model; }

        void notifyUpdateModel(object sender, PropertyChangedEventArgs e)
        {
            view.NomAgent = model.NomAgent;
        }
        protected void clickSouris(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selected == true)
                    this.actionUnselect();
                else
                    this.actionSelect();
            }
            if (e.Button == MouseButtons.Right)
            {
                base.clickSouris(sender, e);
            }
        }
        void select()
        {
            selected = true;
        }
        void unselect()
        {
            selected = false;
        }
    }

    public interface IModelVacation1Person : IModelCellSemaineSub
    {
        String NomAgent { get; }
        event PropertyChangedEventHandler ModelChanged;
    }

    public class ModelJ1J3N<T, J> : IModelVacation1Person
        where T : IVacation, new()
        where J : ITypeVacation, new()
    {
        T vacation;

        public ModelJ1J3N(DateTime date, ICycle cycle)
        {
            this.vacation = DaoIVacation.findOne<T>(date);
            if (this.vacation == null)
                this.vacation = DaoIVacation.create<T>(null, date, DaoTypes.findOne<J>(cycle));
            this.vacation.PropertyChanged += notifyPropertyChanged;
        }

        public event PropertyChangedEventHandler ModelChanged;
        private void notifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ModelChanged(sender,e);
        }
        public string NomAgent
        {
            get
            {
                if (vacation.Agent == null)
                    return "pas d'agent";
                else
                    return vacation.Agent.Nom;
            }
        }

   //     public event PropertyChangedEventHandler PropertyChanged;

        public void actionDisponible( ListActionItem listAction, IViewSemaineSub view)
        {
            listAction.Add( new ModelModificationVacationOnSemaineSub( new ModelModificationVacation<T>(vacation), view ) );
        }
    }

    //public class ModelJ1 : IModelJ1J3N
    //{
    //    IModelDate date;
    //    Vacation_J1_D1_2013 vacation=null;
    //    string NomAgent { 
    //        get 
    //        {
    //            string nom = "";
                
    //            return nom;
    //        }
    //    }

    //    public ModelJ1(IModelDate date)
    //    {
    //        this.date = date;
    //        date.PropertyChanged += notifyDateChange;
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void notifyDateChange(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(sender, e);
    //        }
    //    }
    //}
    //public class ModelJ3 : IModelJ1J3N
    //{
    //    IModelDate date;
    //    Vacation_J3_D1_2013 vacation=null;
    //    string NomAgent { 
    //        get 
    //        {
    //            string nom = "";
                
    //            return nom;
    //        }
    //    }

    //    public ModelJ3(IModelDate date)
    //    {
    //        this.date = date;
    //        date.PropertyChanged += notifyDateChange;
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void notifyDateChange(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(sender, e);
    //        }
    //    }
    //}
    //public class ModelN : IModelJ1J3N
    //{
    //    IModelDate date;
    //    Vacation_N_D1_2013 vacation=null;
    //    string NomAgent {
    //        get 
    //        {
    //            string nom = "";
                
    //            return nom;
    //        }
    //    }

    //    public ModelN(IModelDate date)
    //    {
    //        this.date = date;
    //        date.PropertyChanged += notifyDateChange;
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    void notifyDateChange(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(sender, e);
    //        }
    //    }
    //}
}
