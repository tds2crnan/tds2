namespace Core
{
    partial class ViewAdmin
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
            this.listTypes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listSubs = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listEquips = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listCycle = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listFactCycle = new System.Windows.Forms.ListBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // listTypes
            // 
            this.listTypes.FormattingEnabled = true;
            this.listTypes.Location = new System.Drawing.Point(388, 141);
            this.listTypes.Name = "listTypes";
            this.listTypes.Size = new System.Drawing.Size(135, 342);
            this.listTypes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Types:";
            // 
            // listSubs
            // 
            this.listSubs.FormattingEnabled = true;
            this.listSubs.Location = new System.Drawing.Point(547, 141);
            this.listSubs.Name = "listSubs";
            this.listSubs.Size = new System.Drawing.Size(135, 342);
            this.listSubs.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subdivisions:";
            // 
            // listEquips
            // 
            this.listEquips.FormattingEnabled = true;
            this.listEquips.Location = new System.Drawing.Point(706, 141);
            this.listEquips.Name = "listEquips";
            this.listEquips.Size = new System.Drawing.Size(135, 342);
            this.listEquips.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(703, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Equipe:";
            // 
            // listCycle
            // 
            this.listCycle.FormattingEnabled = true;
            this.listCycle.Location = new System.Drawing.Point(18, 141);
            this.listCycle.Name = "listCycle";
            this.listCycle.Size = new System.Drawing.Size(139, 342);
            this.listCycle.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cycles:";
            // 
            // listFactCycle
            // 
            this.listFactCycle.FormattingEnabled = true;
            this.listFactCycle.Location = new System.Drawing.Point(235, 141);
            this.listFactCycle.Name = "listFactCycle";
            this.listFactCycle.Size = new System.Drawing.Size(139, 342);
            this.listFactCycle.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(167, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(167, 68);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // ViewAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.listFactCycle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listCycle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listEquips);
            this.Controls.Add(this.listSubs);
            this.Controls.Add(this.listTypes);
            this.Name = "ViewAdmin";
            this.Size = new System.Drawing.Size(862, 507);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listSubs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listEquips;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listCycle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listFactCycle;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}
