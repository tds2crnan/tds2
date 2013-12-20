using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;



namespace Core
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form dialog = new FormImpressionAnnuel();
            dialog.ShowDialog();
        }

        private void buttonImpHebdo_Click(object sender, EventArgs e)
        {

        }

        private void buttoniCal_Click(object sender, EventArgs e)
        {
            //je fixe le premier jour de l'année
            DateTime premier_jour = new DateTime(2013, 1, 1);


            // je créé le dictionnaire des vacs pour l'agent donné et l'année donnée


            List<IVacation> liste = DaoIVacation.findVacAnnee<IVacation>(60, premier_jour); // 60 = id de toto2
            Dictionary<DateTime, IVacation> dico = new Dictionary<DateTime, IVacation>();
            foreach (IVacation vacation in liste)
            {
                dico[vacation.Date] = vacation;
            }

            // Create a new calendar
            IICalendar iCal = new iCalendar();

            // Add the local time zone to the calendar
            ITimeZone local = iCal.AddLocalTimeZone();



            // boucle de douze mois
            int i = 0;
            for (i = 0; i < 12; i++)
            {
                // boucle pour chaque jour du mois concerné
                int nbDays = DateTime.DaysInMonth(2013, i + 1);

                int j = 0;
                for (j = 0; j < nbDays; j++)
                {

                    //j'écris dans un fichier .ics
                    if (dico.ContainsKey(premier_jour) == true)
                    {
                        string chaine_vac = dico[premier_jour].Type.makePrint();

                        // Create a new event in the calendar
                        // that uses our local time zone
                        IEvent evt = iCal.Create<Event>();
                        evt.Summary = chaine_vac;
                        evt.Start = new iCalDateTime(premier_jour);
                        evt.End = new iCalDateTime(premier_jour.AddHours(12));
                        //evt.IsAllDay = true;
                        evt.Location = "Athis-Mons CRNA-N 1609";
                        //evt.Start = iCalDateTime.Today.AddHours(8).SetTimeZone(local);
                        //evt.Duration = TimeSpan.FromHours(12);
                    }

                    //j'incrémente le jour
                    premier_jour = premier_jour.AddDays(1);

                }
            }

            // je récupère le iCal pour l'exporter en .ics

            if (iCal.Events.Count > 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.DefaultExt = ".ics";
                saveFileDialog1.FileName = DateTime.Now.ToString("yyyy_MM_dd") + "_AgendaMO";
                saveFileDialog1.Filter = "iCal Calendar (ICS)|*.ics";
                saveFileDialog1.Title = "Exporter son calendrier personnel";
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string resultat = new iCalendarSerializer().SerializeToString(iCal);
                    System.IO.File.WriteAllText(@saveFileDialog1.FileName, resultat);
                    MessageBox.Show("Exportation de " + iCal.Events.Count + " évènements Ok");
                }   
            }
        }
    }
}
