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
    public partial class ViewPeupler : UserControl, IViewPeupler
    {
        PresenterPeupler presenter;
        public ViewPeupler(IModelPeupler model)
        {
            InitializeComponent();
            presenter = new PresenterPeupler(this, model);
        }

        public event EventHandler changeDate;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (changeDate != null)
                changeDate(sender, e);
        }
        public DateTime DateSelected
        {
            get
            {
                return this.dateTimePicker1.Value;
            }
            set
            {
                this.dateTimePicker1.Value = value;
            }
        }

        public event EventHandler changeEquip;
        private void comboEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeEquip != null)
                changeEquip(sender, e);
        }
        public MetierEquip EquipSelected
        {
            get
            {
                return (MetierEquip)this.comboEquip.SelectedItem;
            }
        }
        public List<MetierEquip> ListEquip
        {
            set 
            {
                this.comboEquip.DisplayMember = "Nom";
                this.comboEquip.DataSource = value; 
            }
        }

        public List<UserControl> tableau
        {
            set 
            {
                this.tableLayoutPanel1.SuspendLayout();
                this.tableLayoutPanel1.Controls.Clear();
                this.tableLayoutPanel1.ColumnStyles.Clear();
                this.tableLayoutPanel1.RowStyles.Clear();

                this.tableLayoutPanel1.ColumnCount = value.Count;
                this.tableLayoutPanel1.RowCount = 1;
                int i = 0;
                foreach (UserControl annee in value)
                {
                    this.tableLayoutPanel1.Controls.Add(annee, 0, i);
                    i++;
                }
                this.tableLayoutPanel1.ResumeLayout();
            }
        }
    }
}
