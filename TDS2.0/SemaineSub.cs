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
    public partial class SemaineSub : UserControl, IViewSemaineSub
    {
        PresenterSemaineSub presenter;
        int nbJourAfficher = 13;
        public SemaineSub(IModelSemaineSub model)
        {
            InitializeComponent();
            presenter = new PresenterSemaineSub(this, model);
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            List<ITypeVacation> liste = presenter.getTypeVacation();

            this.tableLayoutPanel1.ColumnCount = nbJourAfficher+1;
            this.tableLayoutPanel1.RowCount = liste.Count+1;
            
            int iTypeVac = 1;
            foreach (ITypeVacation typeVac in liste)
            {
                this.tableLayoutPanel1.Controls.Add( typeVac.getCellType(), 0, iTypeVac );
                for ( int iDate = 1; iDate <= nbJourAfficher; ++iDate )
                {
                    this.tableLayoutPanel1.Controls.Add(new CellDate(dateAffichage), iDate, 0);
                    UserControl cell = typeVac.getVacation();
                    cell.Click += this.selection;
                    this.tableLayoutPanel1.Controls.Add( cell, iDate, iTypeVac);
                }
                ++iTypeVac;
            }
        }

        public event EventHandler changeDate;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (changeDate != null)
                changeDate(this, e);
        }

        public DateTime DateSelected
        {
            get { return this.dateTimePicker1.Value; }
        }
    }
}
