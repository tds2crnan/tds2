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
    public partial class ViewCarriere2013 : UserControl, IViewCarriere2013
    {
        PresenterCarriere2013 presenter;
        public ViewCarriere2013(PresenterAgent racine, IModelCarriere2013 model)
        {
            InitializeComponent();
            presenter = new PresenterCarriere2013(racine,this, model);
        }
//Equipe
        public List<MetierEquip> ListEquip
        {
            set
            {
                this.comboBoxEquip.DisplayMember = "Nom";
                this.comboBoxEquip.DataSource = value;
            }
        }
        public MetierEquip Equip
        {
            set 
            { 
                this.comboBoxEquip.SelectedItem = value; 
            }
        }
//Sub
        public List<MetierSub> ListSub
        {
            set
            {
                this.comboBoxSub.DisplayMember = "Nom";
                this.comboBoxSub.DataSource = value;
            }
        }
        public MetierSub Sub
        {
            set 
            {
                this.comboBoxSub.SelectedItem = value;
            }
        }
//date
        public DateTime DateDebut
        {
            set
            {
                this.dateTimePicker1.Value = value;
            }
        }
        public DateTime DateFin
        {
            set 
            { 
                this.dateTimePicker2.Value = value; 
            }
        }

        public UserControl getControl()
        {
            return this;
        }

 
    }
}
