﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core
{
    public partial class ViewTypeVacation : UserControl
    {
        public ViewTypeVacation(ITypeVacation typeVacation)
        {
            InitializeComponent();
            this.nom.Text = typeVacation.getNom();
        }
    }
}
