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
    public partial class ViewModificationVacation : Form, IViewModificationVacation
    {
        public string commentaire { get { return this.richTextCommentaire.Text; } }
        public MetierAgent getAgent { get { return (MetierAgent)this.listAgent.SelectedItem; } }

        PresenterModificationVacation presenter;
        public ViewModificationVacation(IModelModificationVacation model)
        {
            InitializeComponent();
            this.presenter = new PresenterModificationVacation(this, model);
        }

        public event EventHandler modifAgent;
        private void action_OK_Click(object sender, EventArgs e)
        {
            if (modifAgent != null)
                modifAgent(sender, e);
            this.DialogResult = DialogResult.OK;
        }

        private void action_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        public List<MetierAgent> ListAgent
        {
            set 
            {
                this.listAgent.DisplayMember = "Nom";
                this.listAgent.DataSource = value; 
            }
        }
    }
}
