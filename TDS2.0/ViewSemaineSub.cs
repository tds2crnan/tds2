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
    public partial class ViewSemaineSub : UserControl, IViewSemaineSub
    {
        PresenterSemaineSub presenter;

        public List<List<UserControl>> tableau 
        {
            set
            {
                this.tableLayoutPanel1.SuspendLayout();
                this.tableLayoutPanel1.Controls.Clear();
                this.tableLayoutPanel1.ColumnStyles.Clear();
                this.tableLayoutPanel1.RowStyles.Clear();
                if (value != null)
                {
                    this.tableLayoutPanel1.ColumnCount = value.Count;
                    this.tableLayoutPanel1.RowCount = value[0].Count;
                    int i = 0;
                    foreach (List<UserControl> semaine in value)
                    {
                        int j = 0;
                        foreach (UserControl ctrl in semaine)
                        {
                            if (ctrl != null)
                                this.tableLayoutPanel1.Controls.Add(ctrl, i, j);
                            ++j;
                        }
                        ++i;
                    }
                }
                this.tableLayoutPanel1.ResumeLayout();
            }
        }

        public ViewSemaineSub(IModelSemaineSub model)
        {
            InitializeComponent();
            Type tp = tableLayoutPanel1.GetType().BaseType;
            System.Reflection.PropertyInfo pi =
                tp.GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic);            
            pi.SetValue(tableLayoutPanel1, true, null);

            presenter = new PresenterSemaineSub(this, model);

        }

        public event EventHandler changeDate;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (changeDate != null)
                changeDate(this, e);
        }

        public DateTime DateSelected
        {
            get { return this.dateTimePicker1.Value; }
            set { this.dateTimePicker1.Value = value; }
        }

        public void resetContext()
        {
            this.contextMenuStrip1.Items.Clear();
        }
        public void showContext(Point point)
        {
            this.contextMenuStrip1.Show(point);
        }


        public void addItem(ToolStripItem item)
        {
            this.contextMenuStrip1.Items.Add(item);
        }


        public event EventHandler changeSub;
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (changeSub != null)
                changeSub(sender, e);
        }

        public MetierSub SubSelected
        {
            get
            {
                return (MetierSub)this.comboBox1.SelectedItem;
            }
            set
            {
                this.comboBox1.SelectedItem = value;
            }
        }
        public List<MetierSub> ListSub
        {
            set 
            {
                this.comboBox1.DisplayMember = "Nom";
                this.comboBox1.DataSource = value;
            }
        }
    }
}
