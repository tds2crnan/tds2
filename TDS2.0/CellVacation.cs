using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDS2._0
{
    public partial class CellVacation : UserControl
    {
        public CellVacation(IVacation vacation)
        {
            InitializeComponent();
            this.nom.Text = vacation.getNom();
        }
    }
}
