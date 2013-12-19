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
    public partial class ViewPeuplerSub : UserControl, IViewPeuplerSub
    {
        PresenterPeuplerSub presenter;
        public ViewPeuplerSub(PresenterPeupler racine, IModelPeuplerSub model)
        {
            InitializeComponent();
            presenter = new PresenterPeuplerSub(racine, this, model);
        }

        public UserControl getControl()
        {
            return this;
        }

        public List<Tuple<UserControl, RowStyle>> Tabeau
        {
            set {
                this.tableLayoutPanel1.SuspendLayout();
                this.tableLayoutPanel1.Controls.Clear();
                this.tableLayoutPanel1.ColumnStyles.Clear();
                this.tableLayoutPanel1.RowStyles.Clear();

                this.tableLayoutPanel1.ColumnCount = 1;
                this.tableLayoutPanel1.RowCount = value.Count;
                foreach (Tuple<UserControl, RowStyle> pair in value)
                {
                    this.tableLayoutPanel1.RowStyles.Add(pair.Item2);
                    this.tableLayoutPanel1.Controls.Add(pair.Item1);
                    pair.Item1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
                this.tableLayoutPanel1.ResumeLayout();
            }
        }
    }
}
