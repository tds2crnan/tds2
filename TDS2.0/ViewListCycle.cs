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
    public partial class ViewListCycle : UserControl, IViewListCycle
    {
        PresenterListCycle presenter;
        public ViewListCycle( PresenterPeupler racine, IModelListCycle model)
        {
            InitializeComponent();
            presenter = new PresenterListCycle(racine, this, model);
        }

        public List<UserControl> Tableau
        {
            set 
            {
                this.tableLayoutPanel1.SuspendLayout();
                this.tableLayoutPanel1.Controls.Clear();
                this.tableLayoutPanel1.ColumnStyles.Clear();
                this.tableLayoutPanel1.RowStyles.Clear();

                this.tableLayoutPanel1.ColumnCount = 1;
                this.tableLayoutPanel1.RowCount = value.Count;
                foreach (UserControl ctrl in value)
                {
                    this.tableLayoutPanel1.Controls.Add(ctrl);
                }
                this.tableLayoutPanel1.ResumeLayout();           
            }
        }

        public UserControl getControl()
        {
            return this;
        }
    }
}
