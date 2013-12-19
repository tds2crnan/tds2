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
    public partial class ctrlMCAE : UserControl, ICtrlMCAE
    {
        private Presenter_MCAE presenter;
        public ctrlMCAE(IModelJ1J3N model_)
        {
            InitializeComponent();
        }

        private void ctrlMCAE_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            presenter = new Presenter_MCAE(this);
        }
    }

    public interface ICtrlMCAE
    {
    }
}
