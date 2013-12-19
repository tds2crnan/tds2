using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IViewCarriere2013 : IViewCellCarriere
    {
        List<MetierEquip> ListEquip { set; }
        MetierEquip Equip{set;}
        List<MetierSub> ListSub { set; }
        MetierSub Sub{set;}
        DateTime DateDebut { set; }
        DateTime DateFin { set; }
    }
    public class PresenterCarriere2013 : IPresenterCellCarriere
    {
        IViewCarriere2013 view;
        IModelCarriere2013 model;
        PresenterAgent racine;
        public PresenterCarriere2013(PresenterAgent racine, IViewCarriere2013 view, IModelCarriere2013 model)
        {
            this.view = view;
            this.model = model;
            this.racine = racine;
            view.ListEquip = model.ListEquip;
            view.ListSub = model.ListSub;
            view.Equip = model.Equip;
            view.Sub = model.Sub;
            view.DateDebut = model.DateDebut;
            view.DateFin = model.DateFin;
        }
    }
    public interface IModelCarriere2013 : IModelCellCarriere
    {
        List<MetierEquip> ListEquip { get; }
        List<MetierSub> ListSub { get; }
        DateTime DateDebut { get; }
        DateTime DateFin { get; }
        MetierEquip Equip { get; }
        MetierSub Sub { get; }
        void set(MetierEquip equip, MetierSub sub, DateTime date);
    }

    public class ModelCarriere<T> : IModelCarriere2013
        where T : ICarriere
    {
        T carriere;
        public List<MetierEquip> ListEquip
        { 
            get 
            {
                return DaoEquip.findAll();
            } 
        }
        public MetierEquip Equip
        {
            get 
            {
                return carriere.Equip;
            }
        }
        public List<MetierSub> ListSub
        {
            get
            {
                return DaoSub.findAll();
            }
        }
        public MetierSub Sub
        {
            get
            { 
                return carriere.Sub;
            }
        }

        public DateTime DateDebut
        {
            get 
            { 
                return carriere.DateDebut; 
            }
        }

        public DateTime DateFin
        {
            get 
            { 
                return carriere.DateFin; 
            }
        }
        public ModelCarriere(T carriere)
        {
            this.carriere = carriere;
        }
        public void set(MetierEquip equip, MetierSub sub, DateTime date)
        {
        }
        //public override IViewCellCarriere makeView(PresenterAgent presenter)
        //{
        //    return new ViewCarriere2013(presenter, this);
        //}
    }

    public class CarriereD1 : ICarriere
    {
        public CarriereD1()
        {
        }
        public void set(MetierEquip equip, MetierSub sub, DateTime date)
        {
        }
        public override IViewCellCarriere makeView(PresenterAgent presenter)
        {
            return new ViewCarriere2013(presenter, new ModelCarriere<CarriereD1>(this) );
        }
    }
}
