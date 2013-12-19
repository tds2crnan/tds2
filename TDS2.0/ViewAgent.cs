using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public partial class ViewAgent : UserControl, IViewAgent
    {
        PresenterAgent presenter;
        public ViewAgent(IModelAgent model)
        {
            InitializeComponent();
            Type tp = this.tlpCarrieres.GetType().BaseType;
            System.Reflection.PropertyInfo pi =
                tp.GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic);
            pi.SetValue(this.tlpCarrieres, true, null);

            presenter = new PresenterAgent(this, model);
        }

        public List<MetierAgent> ListAgent
        {
            set
            {
                this.listAgent.DataSource = null;
                this.listAgent.DisplayMember = "Nom";
                //this.listAgent.ValueMember = "";
                this.listAgent.DataSource = value;
                this.listAgent.ClearSelected();
            }
        }

        public MetierAgent getAgentSelected()
        {
            return (MetierAgent)this.listAgent.SelectedItem;
        }

        public event EventHandler agentSelected;
        private void listAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }


        public List<ICarriere> ListCarriere 
        {
            set
            {
                this.listCarriere.SuspendLayout();
                this.listCarriere.SelectionMode = SelectionMode.None;
                this.listCarriere.DataSource = null;
                this.listCarriere.DisplayMember = "DateDebut";
                this.listCarriere.DataSource = value;
                this.listCarriere.ClearSelected();
                this.listCarriere.SelectionMode = SelectionMode.One;
                this.listCarriere.ResumeLayout();
            }
        }
        public event EventHandler carriereSelected;
        public ICarriere getCarriereSelected()
        {
            return (ICarriere)this.listCarriere.SelectedItem;
        }

        private void listCarriere_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public UserControl Carriere
        {
            set
            {
                this.tlpCarrieres.SuspendLayout();
                this.tlpCarrieres.Controls.Clear();
                this.tlpCarrieres.ColumnStyles.Clear();
                this.tlpCarrieres.RowStyles.Clear();

                this.tlpCarrieres.ColumnCount = 1;
                this.tlpCarrieres.RowCount = 1;
                if( value != null )
                    this.tlpCarrieres.Controls.Add(value, 0, 0);
                this.tlpCarrieres.ResumeLayout();
            }
        }

        private void listAgent_SelectedValueChanged(object sender, EventArgs e)
        {
            if (agentSelected != null)
                agentSelected(sender, e);
        }

        private void listCarriere_SelectedValueChanged(object sender, EventArgs e)
        {
            if (carriereSelected != null)
                carriereSelected(sender, e);
        }


    }
}
