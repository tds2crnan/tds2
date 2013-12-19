using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core._0
{
    public partial class ctrlJ1J3N : UserControl, IViewCtrl_J1_J3_N
    {
        Presenter_J1_J3_N presenter;
        IModel_J1_J3_N model;
        public ctrlJ1J3N(IModel_J1_J3_N model_)
        {
            InitializeComponent();
            model = model_;
        }

        

        private void ctrlJ1J3N_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            presenter = new Presenter_J1_J3_N(this,model);
        }

        public string NomAgent
        {
            set { this.nom.Text = value; }
        }

        private void modifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifVacation fram = new ModifVacation();
            fram.nom = 
            if (fram.ShowDialog() == DialogResult.OK)
            {
                presenter.changeAgent(fram.nom);
            }
            else
            {
            }
        }
    }
    public interface IViewCtrl_J1_J3_N
    {
        String NomAgent { set; }
    }

}
