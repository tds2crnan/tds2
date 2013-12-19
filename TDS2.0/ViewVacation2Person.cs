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
    public partial class ViewVacation2Person : UserControl, IViewVacation2Person
    {
        PresenterVacation2Person presenter;

        public ViewVacation2Person(PresenterSemaineSub racine, IModelVacation1Person model1, IModelVacation1Person model2)
        {
            InitializeComponent();
            presenter = new PresenterVacation2Person(racine, this, model1,model2);
        }

        public UserControl vacation1
        {
            set { this.tableLayoutPanel1.Controls.Add(value,0,0); }
        }

        public UserControl vacation2
        {
            set { this.tableLayoutPanel1.Controls.Add(value, 0, 1); }
        }

        public UserControl getControl()
        {
            return this;
        }
    }
}
