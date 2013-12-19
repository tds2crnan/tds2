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

    public interface IViewPeuplerSub : IViewCellPeupler
    {
        List<Tuple<UserControl, RowStyle>> Tabeau { set; }

    }

    public class PresenterPeuplerSub : IPresenterCellPeupler
    {
        IViewPeuplerSub view;
        public override IViewCellPeupler View{get{return view;}}
        IModelPeuplerSub model;
        public override IModelCellPeupler Model{get{return model;}}
        PresenterPeupler racine;
        public PresenterPeuplerSub(PresenterPeupler racine, IViewPeuplerSub view, IModelPeuplerSub model)
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
                    
                    //calcule de la place du cycle
                    RowStyle style;
                    int nbJourCycle=1;
                    if (cycle.DateFin < Outils.lastDayOfYear(racine.Date))
                    {
                        if (cycle.DateDebut > Outils.firstDayOfYear(racine.Date))
                            nbJourCycle = (cycle.DateFin - cycle.DateDebut).Days;
                        else
                            nbJourCycle = (cycle.DateFin - Outils.firstDayOfYear(racine.Date)).Days;
                        //TODO decalage par rapport a la duree 
                    }
                    else
                    {
                        if (cycle.DateDebut > Outils.firstDayOfYear(racine.Date))
                            nbJourCycle = (Outils.lastDayOfYear(racine.Date) - cycle.DateDebut).Days;
                        else
                            nbJourCycle = (Outils.lastDayOfYear(racine.Date) - Outils.firstDayOfYear(racine.Date)).Days;
                    }
                    if (DateTime.IsLeapYear(racine.Date.Year))
                        style = new RowStyle(SizeType.Percent, (float)(nbJourCycle/366) );
                    else
                        style = new RowStyle(SizeType.Percent, (float)(nbJourCycle / 365));

                    //
                    listCtrl.Add(new Tuple<UserControl, RowStyle>(cycle.makeView(racine,racine.Equipe, racine.Date), style));
                }
                return listCtrl;
            }
        }
    }

    public interface IModelPeuplerSub : IModelCellPeupler
    {
        List<ICycle> ListCycle { get; }
    }

    public class ModelCycleAnneeSub : IModelPeuplerSub
    {
        MetierEquip equip;
        MetierSub sub;
        DateTime date;

        public ModelCycleAnneeSub(MetierSub sub, MetierEquip equip, DateTime date)
        {
            this.sub = sub;
            this.equip = equip;
            this.date = date;
        }
        public List<ICycle> ListCycle { 
            get 
            {
                return DaoCycle.findAnnee<ICycle>(sub, equip, date);
            } 
        }
    }
}
