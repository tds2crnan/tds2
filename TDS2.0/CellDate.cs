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
    public partial class CellDate : UserControl
    {
        public CellDate(DateTime date)
        {
            InitializeComponent();
            switch( date.DayOfWeek ){
                case DayOfWeek.Monday :
                    this.nomJour.Text = "LUNDI";
                    break;
                case DayOfWeek.Tuesday :
                    this.nomJour.Text = "MARDI";
                    break;
                case DayOfWeek.Wednesday:
                    this.nomJour.Text = "MERCREDI";
                    break;
                case DayOfWeek.Thursday:
                    this.nomJour.Text = "JEUDI";
                    break;
                case DayOfWeek.Friday:
                    this.nomJour.Text = "VENDREDI";
                    break;
                case DayOfWeek.Saturday:
                    this.nomJour.Text = "SAMEDI";
                    break;
                case DayOfWeek.Sunday:
                    this.nomJour.Text = "DIMANCHE";
                    break;
            }
            this.date.Text = date.Day.ToString();
            this.moi.Text = date.Month.ToString();
        }

        private void DateTableau_Load(object sender, EventArgs e)
        {

        }
    }
}
