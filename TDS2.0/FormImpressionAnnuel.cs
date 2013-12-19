using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public partial class FormImpressionAnnuel : Form
    {
        public FormImpressionAnnuel()
        {
            InitializeComponent();
            this.comboBoxAgent.DisplayMember = "Nom";
            this.comboBoxAgent.ValueMember = "id";
            this.comboBoxAgent.DataSource = DaoAgent.findAll();
            
        }

        private void comboBoxAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           // MessageBox.Show(this.comboBoxAgent.SelectedValue.ToString(), "Valeur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonImprimer_Click(object sender, EventArgs e)
        {
            // je fixe certaines constantes
            printDocument1.DocumentName = "Calendrier annuel" + this.dateTimePicker1.Text + " de " + this.comboBoxAgent.Text + "_" + DateTime.Now.ToShortDateString();
            printDocument1.DefaultPageSettings.Landscape = true;
            //PrintDialog associate with PrintDocument;
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
                this.Close();
                this.Dispose();
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Point;
            // on dessine le cartouche
            e.Graphics.DrawString("CALENDRIER " + this.dateTimePicker1.Text + " de " + this.comboBoxAgent.Text, new Font("Arial", 18), Brushes.Black, 80, 20);
            e.Graphics.DrawString("Logiciel TDS 2.0 - Athis-Mons - " + DateTime.Now.ToShortDateString(), new Font("Arial", 14), Brushes.Black, 550, 20);
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(80, 50), new Point(500, 50));


            // quelques variables
            int interligne = 43;
            int marge_gauche = 40;
            Font police_10_gras = new Font("Arial", 10, FontStyle.Bold);
            Font police_6_regular = new Font("Arial", 6);

            //je fixe le premier jour de l'année
            DateTime premier_jour = new DateTime(this.dateTimePicker1.Value.Year, 1, 1);


            //chaine pour chaque mois
            string[] chaine = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };


            // je créé le dictionnaire des vacs pour l'agent donné et l'année donnée


            List<IVacation> liste = DaoIVacation.findVacAnnee<IVacation>(60, premier_jour);
            Dictionary<DateTime, IVacation> dico = new Dictionary<DateTime,IVacation>();
            foreach (IVacation vacation in liste)
            {
                dico[vacation.Date] = vacation;
            }

            // boucle de douze mois
            int i = 0;
            for (i = 0; i < chaine.Length; i++)
            {
                e.Graphics.DrawString(chaine[i], police_10_gras, Brushes.Black, marge_gauche - 20, 70 + (i * interligne));
                // boucle pour chaque jour du mois concerné
                int nbDays = DateTime.DaysInMonth(this.dateTimePicker1.Value.Year, i + 1);

                int j = 0;
                for (j = 0; j < nbDays; j++)
                {
                    //je dessine la barre devant
                    e.Graphics.DrawLine(new Pen(Brushes.Black, 0.5F), new Point(marge_gauche - 16 + (26 * j), 85 + (i * interligne)), new Point(marge_gauche - 16 + (26 * j), 110 + (i * interligne)));

                    //je dessine les trois premières lettre du jour
                    //je passe la première lettre en majuscule
                    string oldstring = String.Format("{0:ddd}", premier_jour);
                    string newstring = oldstring[0].ToString().ToUpper() + oldstring.Substring(1).ToLower();
                    e.Graphics.DrawString(newstring, police_6_regular, Brushes.Blue, marge_gauche - 15 + (26 * j), 85 + (i * interligne));


                    //je dessine le numéro de jour
                    e.Graphics.DrawString((j + 1).ToString(), police_6_regular, Brushes.Blue, marge_gauche + (26 * j), 85 + (i * interligne));

                    //je grise les dimanche
                    if (premier_jour.DayOfWeek == DayOfWeek.Sunday)
                    {
                        e.Graphics.FillRectangle(Brushes.Gray, marge_gauche - 16 + (26 * j), 93 + (i * interligne), 26, interligne - 26);

                    }

                    //je grise les 11 jours fériés
                    if (bolIsWorkingDay(premier_jour) == false)
                    {
                        e.Graphics.FillRectangle(Brushes.LightGray, marge_gauche - 16 + (26 * j), 93 + (i * interligne), 26, interligne - 26);

                    }

                    //je dessine la vac

                    if (dico.ContainsKey(premier_jour) == true)
                    {
                        string chaine_vac = dico[premier_jour].Type.makePrint();
                        e.Graphics.DrawString(chaine_vac,police_10_gras,Brushes.Red,marge_gauche + (26 * j)-10, 94 + (i * interligne));
                    }

                    
                    //j'incrémente le jour
                    premier_jour = premier_jour.AddDays(1);

                }
            }
        }
        private bool bolIsWorkingDay(DateTime dtDate)
        {
            bool bolWorkingDay = true;
            Array arrDateFerie = Array.CreateInstance(typeof(DateTime), 8);
            // 01 Janvier
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 1, 1), 0);
            // 01 Mai
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 5, 1), 1);
            // 08 Mai
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 5, 8), 2);
            // 14 Juillet
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 7, 14), 3);
            // 15 Aout
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 8, 15), 4);
            // 01 Novembre
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 11, 1), 5);
            // 11 Novembre
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 11, 11), 6);
            // Noël
            arrDateFerie.SetValue(new DateTime(dtDate.Year, 12, 25), 7);

            // Dimanche ou jour férié
            //bolWorkingDay = !((dtDate.DayOfWeek == DayOfWeek.Sunday) || (Array.BinarySearch(arrDateFerie, dtDate) >= 0));

            if ((Array.BinarySearch(arrDateFerie, dtDate) >= 0) == true)
            {
                bolWorkingDay = false;
            }
            else
            {
                bolWorkingDay = true;
            }


            if (bolWorkingDay)
            {


                int Y = dtDate.Year;             // Annee
                int golden;                      // Nombre d'or
                int solar;                       // Correction solaire
                int lunar;                       // Correction lunaire
                int pfm;                         // Pleine lune de paques
                int dom;                         // Nombre dominical
                int easter;                      // jour de paques
                int tmp;


                DateTime Paques, Ascension, Pentecote;


                // Nombre d'or
                golden = (Y % 19) + 1;
                if (Y <= 1752)            // Calendrier Julien
                {
                    // Nombre dominical
                    dom = (Y + (int)(Y / 4) + 5) % 7;
                    if (dom < 0) dom += 7;
                    // Date non corrigee de la pleine lune de paques
                    pfm = (3 - (11 * golden) - 7) % 30;
                    if (pfm < 0) pfm += 30;
                }
                else                       // Calendrier Gregorien
                {
                    // Nombre dominical
                    dom = (Y + (int)(Y / 4) - (int)(Y / 100) + (int)(Y / 400)) % 7;
                    if (dom < 0) dom += 7;
                    // Correction solaire et lunaire
                    solar = (int)(Y - 1600) / 100 - (int)(Y - 1600) / 400;
                    lunar = (int)(((int)(Y - 1400) / 100) * 8) / 25;
                    // Date non corrigee de la pleine lune de paques
                    pfm = (3 - (11 * golden) + solar - lunar) % 30;
                    if (pfm < 0) pfm += 30;
                }
                // Date corrige de la pleine lune de paques :
                // jours apres le 21 mars (equinoxe de printemps)
                if ((pfm == 29) || (pfm == 28 && golden > 11)) pfm--;

                tmp = (4 - pfm - dom) % 7;
                if (tmp < 0) tmp += 7;

                // Paques en nombre de jour apres le 21 mars
                easter = pfm + tmp + 1;

                if (easter < 11)
                {
                    Paques = DateTime.Parse((easter + 21) + "/3/" + Y);
                }
                else
                {
                    Paques = DateTime.Parse((easter - 10) + "/4/" + Y);
                }

                Ascension = Paques.AddDays(39);
                Pentecote = Paques.AddDays(50);
                // Lundi de paques
                Paques = Paques.AddDays(1);

                bolWorkingDay = !((DateTime.Compare(Paques, dtDate) == 0) || (DateTime.Compare(Ascension, dtDate) == 0)
                || (DateTime.Compare(Pentecote, dtDate) == 0));



                // autre méthode mais qui foire pour 2013
                //// Calcul du jour de pâques (algorithme de Oudin (1940))
                ////Calcul du nombre d'or - 1
                //int intGoldNumber = (int)(dtDate.Year % 19);
                //// Année divisé par cent
                //int intAnneeDiv100 = (int)(dtDate.Year / 100);
                //// intEpacte est = 23 - Epacte (modulo 30)
                //int intEpacte = (int)((intAnneeDiv100 - intAnneeDiv100 / 4 - (8 * intAnneeDiv100 + 13) / 25 + (
                //19 * intGoldNumber) + 15) % 30);
                ////Le nombre de jours à partir du 21 mars pour atteindre la pleine lune Pascale
                //int intDaysEquinoxeToMoonFull = (int)(intEpacte - (intEpacte / 28) * (1 - (intEpacte / 28) * (29 / (intEpacte + 1)) * ((21 - intGoldNumber) / 11)));
                ////Jour de la semaine pour la pleine lune Pascale (0=dimanche)
                //int intWeekDayMoonFull = (int)((dtDate.Year + dtDate.Year / 4 + intDaysEquinoxeToMoonFull +
                //      2 - intAnneeDiv100 + intAnneeDiv100 / 4) % 7);
                //// Nombre de jours du 21 mars jusqu'au dimanche de ou 
                //// avant la pleine lune Pascale (un nombre entre -6 et 28)
                //int intDaysEquinoxeBeforeFullMoon = intDaysEquinoxeToMoonFull - intWeekDayMoonFull;
                //// mois de pâques
                //int intMonthPaques = (int)(3 + (intDaysEquinoxeBeforeFullMoon + 40) / 44);
                //// jour de pâques
                //int intDayPaques = (int)(intDaysEquinoxeBeforeFullMoon + 28 - 31 * (intMonthPaques / 4));
                //// lundi de pâques
                //DateTime dtMondayPaques = new DateTime(dtDate.Year, intMonthPaques, intDayPaques + 1);
                //// Ascension
                //DateTime dtAscension = dtMondayPaques.AddDays(38);
                ////Pentecote
                //DateTime dtMondayPentecote = dtMondayPaques.AddDays(49);
                //bolWorkingDay = !((DateTime.Compare(dtMondayPaques, dtDate) == 0) || (DateTime.Compare(dtAscension, dtDate) == 0)
                //|| (DateTime.Compare(dtMondayPentecote, dtDate) == 0));
            }
            return bolWorkingDay;
        }

        private void buttonpreview_Click(object sender, EventArgs e)
        {
            // je fixe certaines constantes
            printDocument1.DefaultPageSettings.Landscape = true;
            //Associate PrintPreviewDialog with PrintDocument.
            printPreviewDialog1.Document = printDocument1;
            // Show PrintPreview Dialog
            printPreviewDialog1.ShowDialog();
        }
    }
}
