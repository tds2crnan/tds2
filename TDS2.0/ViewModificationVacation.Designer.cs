namespace Core
{
    partial class ViewModificationVacation
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.listAgent = new System.Windows.Forms.ListBox();
            this.action_OK = new System.Windows.Forms.Button();
            this.action_Cancel = new System.Windows.Forms.Button();
            this.richTextCommentaire = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listAgent
            // 
            this.listAgent.FormattingEnabled = true;
            this.listAgent.Location = new System.Drawing.Point(16, 22);
            this.listAgent.Name = "listAgent";
            this.listAgent.Size = new System.Drawing.Size(247, 368);
            this.listAgent.TabIndex = 0;
            // 
            // action_OK
            // 
            this.action_OK.Location = new System.Drawing.Point(286, 365);
            this.action_OK.Name = "action_OK";
            this.action_OK.Size = new System.Drawing.Size(174, 24);
            this.action_OK.TabIndex = 1;
            this.action_OK.Text = "OK";
            this.action_OK.UseVisualStyleBackColor = true;
            this.action_OK.Click += new System.EventHandler(this.action_OK_Click);
            // 
            // action_Cancel
            // 
            this.action_Cancel.Location = new System.Drawing.Point(486, 366);
            this.action_Cancel.Name = "action_Cancel";
            this.action_Cancel.Size = new System.Drawing.Size(174, 24);
            this.action_Cancel.TabIndex = 1;
            this.action_Cancel.Text = "Annuler";
            this.action_Cancel.UseVisualStyleBackColor = true;
            this.action_Cancel.Click += new System.EventHandler(this.action_Cancel_Click);
            // 
            // richTextCommentaire
            // 
            this.richTextCommentaire.Location = new System.Drawing.Point(286, 211);
            this.richTextCommentaire.Name = "richTextCommentaire";
            this.richTextCommentaire.Size = new System.Drawing.Size(373, 141);
            this.richTextCommentaire.TabIndex = 2;
            this.richTextCommentaire.Text = "";
            // 
            // ViewModificationVacation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 428);
            this.Controls.Add(this.richTextCommentaire);
            this.Controls.Add(this.action_Cancel);
            this.Controls.Add(this.action_OK);
            this.Controls.Add(this.listAgent);
            this.Name = "ViewModificationVacation";
            this.Text = "ModifVacation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listAgent;
        private System.Windows.Forms.Button action_OK;
        private System.Windows.Forms.Button action_Cancel;
        private System.Windows.Forms.RichTextBox richTextCommentaire;
    }
}