namespace Core
{
    partial class ViewCycleJ3J1N
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
            this.label1 = new System.Windows.Forms.Label();
            this.nomAgent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "dateDebutCycle";
            this.label1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseClick);
            // 
            // nomAgent
            // 
            this.nomAgent.AutoSize = true;
            this.nomAgent.Location = new System.Drawing.Point(156, 10);
            this.nomAgent.Name = "nomAgent";
            this.nomAgent.Size = new System.Drawing.Size(55, 13);
            this.nomAgent.TabIndex = 1;
            this.nomAgent.Text = "nomAgent";
            this.nomAgent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nomAgent_MouseClick);
            // 
            // ViewCycleJ3J1N
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nomAgent);
            this.Controls.Add(this.label1);
            this.Name = "ViewCycleJ3J1N";
            this.Size = new System.Drawing.Size(321, 43);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ViewCycleJ3J1N_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nomAgent;
    }
}
