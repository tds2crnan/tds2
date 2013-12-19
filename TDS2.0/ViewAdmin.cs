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
    public partial class ViewAdmin : UserControl, IViewAdmin
    {
        PresenterAdmin presenter;
        public ViewAdmin(IModelAdmin model)
        {
            InitializeComponent();
            presenter = new PresenterAdmin(this, model);
        }

        public List<ICycle> ListCycleBdd
        {
            set { this.listCycle.DataSource = value; }
        }

        public List<string> ListCycleFactory
        {
            set { this.listFactCycle.DataSource = value; }
        }


        public List<string> ListTypeFactory
        {
            set { this.listTypes.DataSource = value; }
        }
    }
}
