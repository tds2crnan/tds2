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
    public partial class ViewCycleJ3J1N : UserControl, IViewCycleJ3J1N
    {
        PresenterCycleJ3J1N presenter;
        public ViewCycleJ3J1N(IModelCycleJ3J1N model)
        {
            InitializeComponent();
            presenter = new PresenterCycleJ3J1N(this, model);
            presenter.actionSelect += selected;
            presenter.actionUnselect += unselected;
        }

        DateTime IViewCycleJ3J1N.dateDebut
        {
            set { this.label1.Text = value.ToString(); }
        }

        public MetierAgent agent
        {
            set { this.nomAgent.Text = value.Nom; }
        }


        public void selected()
        {
            this.BackColor = Color.AliceBlue;
        }
        public void unselected()
        {
            this.BackColor = Color.LightGray;
        }

        public event MouseEventHandler clickSouris;
        private void ViewCycleJ3J1N_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }
        private void nomAgent_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }
    }
}
