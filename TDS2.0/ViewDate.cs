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
    public partial class ViewDate : UserControl//, IViewDate
    {
        //PresenterDate presenter;
        static string[] mois = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };
        DateTime Date
        {
            set
            {
                switch (value.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        this.nomJour.Text = "LUNDI";
                        break;
                    case DayOfWeek.Tuesday:
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
                this.date.Text = value.Day.ToString();
                this.moi.Text = mois[ value.Month-1 ];
            }
        }

        public ViewDate(DateTime date)
        {
            InitializeComponent();
            this.Date = date;
            //presenter = new PresenterDate(this, date);
        }
    }
}
