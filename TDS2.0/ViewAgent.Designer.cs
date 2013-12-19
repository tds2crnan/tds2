namespace Core
{
    partial class ViewAgent
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
            this.listAgent = new System.Windows.Forms.ListBox();
            this.listCarriere = new System.Windows.Forms.ListBox();
            this.tlpCarrieres = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // listAgent
            // 
            this.listAgent.FormattingEnabled = true;
            this.listAgent.Location = new System.Drawing.Point(17, 11);
            this.listAgent.Name = "listAgent";
            this.listAgent.Size = new System.Drawing.Size(174, 589);
            this.listAgent.TabIndex = 0;
            this.listAgent.SelectedIndexChanged += new System.EventHandler(this.listAgent_SelectedIndexChanged);
            this.listAgent.SelectedValueChanged += new System.EventHandler(this.listAgent_SelectedValueChanged);
            // 
            // listCarriere
            // 
            this.listCarriere.FormattingEnabled = true;
            this.listCarriere.Location = new System.Drawing.Point(197, 143);
            this.listCarriere.Name = "listCarriere";
            this.listCarriere.Size = new System.Drawing.Size(170, 459);
            this.listCarriere.TabIndex = 2;
            this.listCarriere.SelectedIndexChanged += new System.EventHandler(this.listCarriere_SelectedIndexChanged);
            this.listCarriere.SelectedValueChanged += new System.EventHandler(this.listCarriere_SelectedValueChanged);
            // 
            // tlpCarrieres
            // 
            this.tlpCarrieres.ColumnCount = 2;
            this.tlpCarrieres.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCarrieres.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCarrieres.Location = new System.Drawing.Point(373, 146);
            this.tlpCarrieres.Name = "tlpCarrieres";
            this.tlpCarrieres.RowCount = 2;
            this.tlpCarrieres.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCarrieres.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCarrieres.Size = new System.Drawing.Size(634, 454);
            this.tlpCarrieres.TabIndex = 1;
            // 
            // ViewAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listCarriere);
            this.Controls.Add(this.tlpCarrieres);
            this.Controls.Add(this.listAgent);
            this.Name = "ViewAgent";
            this.Size = new System.Drawing.Size(1027, 618);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listAgent;
        private System.Windows.Forms.ListBox listCarriere;
        private System.Windows.Forms.TableLayoutPanel tlpCarrieres;
    }
}
