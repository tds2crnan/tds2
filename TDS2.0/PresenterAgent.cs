using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public interface IViewCellCarriere
    {
        UserControl getControl();
    }

    public class IPresenterCellCarriere
    {
    }

    public interface IModelCellCarriere
    {
    }

    public interface IViewAgent
    {
        List<MetierAgent> ListAgent { set; }
        event EventHandler agentSelected;
        MetierAgent getAgentSelected();

        List<ICarriere> ListCarriere { set; }
        event EventHandler carriereSelected;
        ICarriere getCarriereSelected();

        UserControl Carriere { set; }
    }

    public class PresenterAgent
    {
        IViewAgent view;
        IModelAgent model;
        public PresenterAgent(IViewAgent view, IModelAgent model)
        {
            this.view = view;
            this.model = model;
            view.ListAgent = model.ListAgent;
            view.agentSelected += selectedAgent;
            view.carriereSelected += selectedCarriere;
        }
        private void selectedAgent(object sender, EventArgs e)
        {
            MetierAgent agent = view.getAgentSelected();
            List<ICarriere> listCarriere = DaoICarriere.find<ICarriere>(agent);
            view.ListCarriere = listCarriere;
            view.Carriere = null;
        }
        private void selectedCarriere(object sender, EventArgs e)
        {
            ICarriere carriere = view.getCarriereSelected();
            if (carriere != null)
                view.Carriere = carriere.makeView(this).getControl();
            else
                view.Carriere = null;
        }
    }

    public interface IModelAgent
    {
        List<MetierAgent> ListAgent{get;}
    }

    public class ModelAgent : IModelAgent
    {
        public List<MetierAgent> ListAgent
        {
            get 
            {
                return DaoAgent.findAll();
            }
        }
    }
}
