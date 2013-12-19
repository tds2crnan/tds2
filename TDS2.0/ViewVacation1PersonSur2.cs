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
    public partial class ViewVacation1PersonSur2 : UserControl, IViewVacation1Person
    {
        PresenterVacation1Person presenter;
        public ViewVacation1PersonSur2(PresenterSemaineSub racine, IModelVacation1Person model)
        {
            InitializeComponent();
            presenter = new PresenterVacation1Person(racine, this, model);
            this.presenter.actionSelect += selected;
            this.presenter.actionUnselect += unselected;
        }

        public event MouseEventHandler clickSouris;

        public void selected()
        {
            this.BackColor = Color.AliceBlue;
        }

        public void unselected()
        {
            this.BackColor = Color.LightGray;
        }

        public string NomAgent
        {
            set { this.nom.Text = value; }
        }

        public UserControl getControl()
        {
            return this;
        }

        private void ViewVacation1PersonSur2_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }

        private void nom_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickSouris != null)
                clickSouris(sender, e);
        }
    }
}
