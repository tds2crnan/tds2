using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewModificationVacation
    {
        string commentaire { get; }
        MetierAgent getAgent { get; }
        List<MetierAgent> ListAgent { set; }
        event EventHandler modifAgent;
    }

    public class PresenterModificationVacation
    {
        IViewModificationVacation view;
        IModelModificationVacation model;
        public PresenterModificationVacation(IViewModificationVacation view,IModelModificationVacation model)
        {
            this.view = view;
            this.model = model;
            this.view.ListAgent = this.model.ListAgent;
            this.view.modifAgent += modifAgent;
        }
        void modifAgent(object sender, EventArgs e)
        {
            model.updateVacation(this.view.getAgent);
        }
    }

    public interface IModelModificationVacation
    {
        List<MetierAgent> ListAgent { get; }
        void updateVacation(MetierAgent agent);
    }

    public abstract class CompoModificationVacation : CompositionModelAction<IModelModificationVacation>, IModelModificationVacation
    {
        public List<MetierAgent> ListAgent
        {
            get 
            {
                List<MetierAgent> listRef = new List<MetierAgent>();
                if (this.listeModel.Count != 0)
                    listRef = this.listeModel[0].ListAgent;
                foreach (IModelModificationVacation agentDisponible in this.listeModel)
                {
                    listRef = agentDisponible.ListAgent.Intersect(listRef).ToList();
                }
                return listRef;
            }
        }
        public CompoModificationVacation(IModelModificationVacation model)
        {
            this.listeModel.Add(model);
        }
        public override bool isValid(int nbAdd)
        {
            return this.listeModel.Count == nbAdd;
        }
        protected void runDialog(object sender, EventArgs e)
        {
            Form dialog = new ViewModificationVacation(this);
            dialog.ShowDialog();
        }

        //public override bool isValid(int nbAdd)
        //{
        //    throw new NotImplementedException();
        //}

        abstract public override void addActionOnView(int nbAdd);
        abstract public override void removeActionOnView();


        public void updateVacation(MetierAgent agent)
        {
            foreach(IModelModificationVacation model in this.listeModel)
                model.updateVacation(agent);
        }
    }

    public class ModelModificationVacation<T> : IModelModificationVacation
        where T : IVacation
    {
        List<MetierAgent> listAgent = new List<MetierAgent>();
        public List<MetierAgent> ListAgent
        {
            get 
            { 
                return this.listAgent;
            }
        }
        T vacation;
        public ModelModificationVacation(T vacation)
        {
            this.vacation = vacation;
            listAgent = DaoAgent.find(vacation);
        }
        public void updateVacation(MetierAgent agent)
        {
            vacation.Agent = agent;
            DaoIVacation.update<T>(vacation);
        }
    }

    //public class CompositeModelModificationVacation : IModelModificationVacation
    //{
    //    IModelModificationVacation model = null;
    //    public CompositeModelModificationVacation(IModelModificationVacation model)
    //    {
    //        this.model = model;
    //    }
    //    public List<MetierAgent> listAgent
    //    {
    //        get
    //        {
    //            if( model ==null)
    //                return 
    //            //List<MetierAgent> listRef = new List<MetierAgent>();
    //            //if( listModel.Count != 0)
    //            //     listRef= listModel[0].listAgent;
    //            //foreach( IModelModificationVacation agentDisponible in listModel )
    //            //{
    //            //    listRef = agentDisponible.listAgent.Intersect( listRef ).ToList();
    //            //}
    //            //return listRef;
    //        }
    //    }
    //}
}
