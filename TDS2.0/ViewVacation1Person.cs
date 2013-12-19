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
    public partial class ViewVacation1Person : UserControl, IViewVacation1Person
    {
        PresenterVacation1Person presenter;

        public ViewVacation1Person(PresenterSemaineSub racine, IModelVacation1Person model)
            : base()
        {
            InitializeComponent();
            this.presenter = new PresenterVacation1Person(racine,this, model);
            this.presenter.actionSelect += selected;
            this.presenter.actionUnselect += unselected;
        }


        public string NomAgent
        {
            set { this.nom.Text = value; }
        }

        public event MouseEventHandler clickSouris;
        private void ViewJ1J3N_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }

        public void selected()
        {
            this.BackColor = Color.AliceBlue;
        }
        public void unselected()
        {
            this.BackColor = Color.LightGray;
        }

        public UserControl getControl()
        {
            return this;
        }

        private void nom_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }

    }

}
