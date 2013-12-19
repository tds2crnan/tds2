using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewCycleJ3J1N
    {
        DateTime dateDebut { set; }
        MetierAgent agent { set; }
        event MouseEventHandler clickSouris;
    }

    class PresenterCycleJ3J1N
    {
        IViewCycleJ3J1N view;
        IModelCycleJ3J1N model;
        bool selected = false;
        public PresenterCycleJ3J1N(IViewCycleJ3J1N view, IModelCycleJ3J1N model)
        {
            this.view = view;
            this.model = model;
            view.dateDebut = model.DateDebut;
            this.actionSelect += select;
            this.actionUnselect += unselect;
            view.clickSouris += clickSouris;
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
            //if (e.Button == MouseButtons.Right)
            //{
            //    base.clickSouris(sender, e);
            //}
        }
        void select()
        {
            selected = true;
        }
        void unselect()
        {
            selected = false;
        }
        public delegate void actionSelectionCell();
        public actionSelectionCell actionSelect;
        public actionSelectionCell actionUnselect;
    }

    public interface IModelCycleJ3J1N
    {
        DateTime DateDebut { get; }
        MetierAgent agent { get; }
    }

    public class ModelCycleJ1J3N : IModelCycleJ3J1N
    {
        DateTime dateDebut;
        public DateTime DateDebut { get { return dateDebut; } }

        public ModelCycleJ1J3N(DateTime dateDebut)
        {
            this.dateDebut = dateDebut;
        }
        public MetierAgent agent
        {
            get { throw new NotImplementedException(); }
        }
    }
}
