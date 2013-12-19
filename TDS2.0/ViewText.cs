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
    public partial class ViewText : UserControl, IViewCellSemaineSub
    {
        public ViewText( string text)
        {
            InitializeComponent();
            this.label1.Text = text;
        }

        public event MouseEventHandler clickSouris;
        public void selected()
        {
        }
        public void unselected()
        {
        }
        public UserControl getControl()
        {
            return this;
        }
    }
}
