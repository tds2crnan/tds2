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
    public partial class CellTypeVacation : UserControl
    {
        public CellTypeVacation(IVacation vacation)
        {
            InitializeComponent();
            this.nom.Text = vacation.getNom();
        }
    }
}
