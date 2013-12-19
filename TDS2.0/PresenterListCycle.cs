using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewInstanceCycle
    {
    }

    public abstract class IPresenterInstanceCycle
    {
    }

    public interface IModelInstanceCycle
    {
    }

    public interface IViewListCycle : IViewCellCycle
    {
        List<UserControl> Tableau {set;}
    }

    class PresenterListCycle : IPresenterCellCycle
    {
        PresenterPeupler racine;
        IViewListCycle view;
        IModelListCycle model;

        List<UserControl> Tableau 
        {
            get
            {
                List<UserControl> liste = new List<UserControl>();
                DateTime dateFin = (model.Cycle.DateFin < Outils.lastDayOfYear(model.Date)) ? model.Cycle.DateFin : Outils.lastDayOfYear(model.Date);
                DateTime dateDebut = (model.Cycle.DateDebut > Outils.firstDayOfYear(model.Date)) ? model.Cycle.DateDebut : Outils.firstDayOfYear(model.Date);
                int nbJourDebut = (dateDebut - model.Equip.DateStart).Days;
                int nbJourCycleAnneePrecedente = nbJourDebut % model.Cycle.dureeCycle();
                int sizeFirstInstance = model.Cycle.dureeCycle() - nbJourCycleAnneePrecedente;
                int nbInstance = ( dateFin - dateDebut.AddDays(sizeFirstInstance) ).Days / model.Cycle.dureeCycle();
                int sizeLastInstance = (dateFin - dateDebut.AddDays(sizeFirstInstance)).Days % model.Cycle.dureeCycle();
                if (sizeLastInstance != 0)
                    nbInstance++;
                for( int i= 0; i < nbInstance; ++i)
                    liste.Add(new ViewCycleJ3J1N(new ModelCycleJ1J3N(dateDebut.AddDays(sizeFirstInstance + i * model.Cycle.dureeCycle()))));
                return liste;
            }
        }

        public PresenterListCycle(PresenterPeupler racine, IViewListCycle view, IModelListCycle model)
            : base(/*racine,view,model*/)
        {
            this.racine = racine;
            this.view = view;
            this.model = model;
            view.Tableau = Tableau;
        }
    }

    public interface IModelListCycle : IModelCellCycle
    {
        ICycle Cycle { get; }
        MetierEquip Equip { get; }
        DateTime Date { get; }
    }

    class ModelCycle_D1_2013<T> : IModelListCycle
        where T : ICycle
    {
        T cycle;
        public ICycle Cycle
        {
            get { return cycle; }
        }
        MetierEquip equip;
        public MetierEquip Equip
        {
            get { return equip; }
        }
        DateTime date;
        public DateTime Date
        {
            get { return date; }
        }
        public ModelCycle_D1_2013(T cycle, MetierEquip equip, DateTime date)
        {
            this.cycle =  cycle;
            this.equip = equip;
            this.date = date;
        }
    }
}
