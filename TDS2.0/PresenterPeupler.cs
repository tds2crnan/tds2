using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewCellPeupler
    {
        //event MouseEventHandler clickSouris;
        //void selected();
        //void unselected();
        UserControl getControl();
    }

    public abstract class IPresenterCellPeupler
    {
        PresenterPeupler racine;
        //bool selected = false;
        public abstract IViewCellPeupler View {get;}
        public abstract IModelCellPeupler Model { get; }
        public IPresenterCellPeupler(PresenterPeupler racine, IViewCellPeupler view, IModelCellPeupler model)
        {
            this.racine = racine;
        }
    }

    public interface IModelCellPeupler
    {
        //void actionDisponible(ListActionItem listAction, IViewPeupler view);
    }

/********************************************************************************************************************************************/

    public interface IViewPeupler
    {
        event EventHandler changeDate;
        DateTime DateSelected { get; set; }

        event EventHandler changeEquip;
        List<MetierEquip> ListEquip { set; }
        MetierEquip EquipSelected { get;}
        
        List<UserControl> tableau { set; }
    }
    public class PresenterPeupler
    {
        IViewPeupler view;
        IModelPeupler model;
        public DateTime Date
        {
            get
            {
                return this.view.DateSelected;
            }
        }
        public MetierEquip Equipe
        {
            get
            {
                return this.view.EquipSelected;
            }
        }
        public PresenterPeupler(IViewPeupler view, IModelPeupler model)
        {
            this.view = view;
            this.model = model;
            this.view.DateSelected = DateTime.Now;
            this.view.ListEquip = model.getListEquip(this.view.DateSelected);
            this.view.changeDate += dateUpdate;
            this.view.changeEquip += equipUpdate;
        }
        private void dateUpdate(object sender, EventArgs e)
        {
            this.view.ListEquip = model.getListEquip(this.view.DateSelected);
        }
        private void equipUpdate(object sender, EventArgs e)
        {
            List<UserControl> listCtrl = new List<UserControl>();            
            List<MetierSub> listSub = model.getListSub(Date, this.view.EquipSelected);
            foreach ( MetierSub sub in listSub)
            {
                listCtrl.Add(new ViewPeuplerSub(this, new ModelCycleAnneeSub(sub, this.view.EquipSelected, Date)));
            }
            this.view.tableau = listCtrl;
        }
    }
    public interface IModelPeupler
    {
        List<MetierEquip> getListEquip(DateTime date);
        List<MetierSub> getListSub(DateTime date, MetierEquip equip);
    }

    public class ModelPeupler : IModelPeupler
    {
        public ModelPeupler()
        {
        }
        public List<MetierEquip> getListEquip(DateTime date)
        {
            return DaoEquip.find(date);
        }
        //public List<ICycle> getListCycle(DateTime date, MetierEquip equip)
        //{
        //    return DaoCycle.find<ICycle>(equip, date);
        //}
        public List<MetierSub> getListSub(DateTime date, MetierEquip equip)
        {
            return DaoSub.findAnnee(date, equip);
        }
    }
}
