using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewCellCycle
    {
        UserControl getControl();
    }

    public abstract class IPresenterCellCycle
    {

    }

    public interface IModelCellCycle
    {
    }

    public interface IViewCycleAnnee : IViewCellPeupler
    {
        List<Tuple<UserControl, RowStyle>> Tabeau { set; }

    }

    public class PresenterCycleAnnee : IPresenterCellPeupler
    {
        IViewCycleAnnee view;
        public override IViewCellPeupler View{get{return view;}}
        IModelCycleAnnee model;
        public override IModelCellPeupler Model{get{return model;}}
        PresenterPeupler racine;
        public PresenterCycleAnnee(PresenterPeupler racine, IViewCycleAnnee view, IModelCycleAnnee model)
            :base(racine, view,model)
        {
            this.racine = racine;
            this.view = view;
            this.model = model;
            this.view.Tabeau = this.Tableau;
        }
        List<Tuple<UserControl, RowStyle>> Tableau
        {
            get
            {
                List< Tuple<UserControl,RowStyle> > listCtrl = new List<Tuple<UserControl,RowStyle>>();
                List<ICycle> listcycle = model.ListCycle;
                foreach (ICycle cycle in listcycle)
                {
                    listCtrl.Add( cycle.makeView(racine.Equipe,racine.Date)) ;
                }
                return listCtrl;
            }
        }
    }

    public interface IModelCycleAnnee : IModelCellPeupler
    {
        List<ICycle> ListCycle { get; }
    }

    public class ModelCycleAnnee : IModelCycleAnnee
    {
        MetierSub sub;
        DateTime date;

        public ModelCycleAnnee(MetierSub sub, DateTime date)
        {
            this.sub = sub;
            this.date = date;
        }
        public List<ICycle> ListCycle { 
            get 
            {
                return null;
            } 
        }
    }
}
