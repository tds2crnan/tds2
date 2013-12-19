namespace Core
{
    partial class ViewDate
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.nomJour = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.Label();
            this.moi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nomJour
            // 
            this.nomJour.AutoSize = true;
            this.nomJour.Location = new System.Drawing.Point(3, 0);
            this.nomJour.Name = "nomJour";
            this.nomJour.Size = new System.Drawing.Size(29, 13);
            this.nomJour.TabIndex = 0;
            this.nomJour.Text = "lundi";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Location = new System.Drawing.Point(3, 13);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(19, 13);
            this.date.TabIndex = 1;
            this.date.Text = "12";
            // 
            // moi
            // 
            this.moi.AutoSize = true;
            this.moi.Location = new System.Drawing.Point(3, 26);
            this.moi.Name = "moi";
            this.moi.Size = new System.Drawing.Size(38, 13);
            this.moi.TabIndex = 2;
            this.moi.Text = "janvier";
            // 
            // ViewDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.moi);
            this.Controls.Add(this.date);
            this.Controls.Add(this.nomJour);
            this.Name = "ViewDate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nomJour;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.Label moi;
    }
}
