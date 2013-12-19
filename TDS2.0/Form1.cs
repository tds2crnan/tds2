using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Core
{
    public partial class Form1 : Form
    {
        //DateTime date = DateTime.Now;
        //int nbJourAfficher = 13;
        //int nbVacation = 3;

        //List<IVacation> listVacationSelected = new List<IVacation>();

        public Form1()
        {
            InitializeComponent();
            this.tabPage2.Controls.Add(new ViewAgent(new ModelAgent()));
            this.tabPage1.Controls.Add(new ViewSemaineSub(new ModelSemaineSub(10)));
            this.tabPage3.Controls.Add(new ViewPeupler(new ModelPeupler()));
            this.tabPage4.Controls.Add(new ViewAdmin(new ModelAdmin()));
            this.tabPage5.Controls.Add(new StartPage());
            //EntityAgent agent1 = DaoAgent.create("toto1");
            //EntityAgent agent2 = DaoAgent.create("toto2");
            //DaoIVacation.create<Vacation_D2_J1>(agent1, DateTime.Now);
            //DaoIVacation.create<Vacation_J1_D1_2013>(agent1, DateTime.Now);
            //DaoIVacation.create<Vacation_J3_D1_2013>(agent1, DateTime.Now);
            //DaoIVacation.create<Vacation_J3_D1_2013>(agent1, DateTime.Now);
            //List<IVacation> list = DaoIVacation.findAll<IVacation>();
        }

        //void unselect(object sender, EventArgs e)
        //{
        //}

        //void selection(object sender, EventArgs e)
        //{
        //    ((UserControl)sender).BackColor = Color.Aqua;
        //    foreach( IVacation vac in this.listVacationSelected ){
        //        var item = new ToolStripMenuItem();
        //        item.Text = "test"+vac.GetHashCode().ToString();
        //        this.contextMenuStrip1.Items.Add(item);
        //    }
        //    this.contextMenuStrip1.Show( ((MouseEventArgs )e).Location );
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{
            //   // CycleTest test = new CycleTest();
            //    Type_J1_D1_2013 vac = new Type_J1_D1_2013();
            //    string nom = vac.GetType().ToString();
            //    System.Type t = vac.GetType();
                
            //    Assembly assembly = Assembly.GetExecutingAssembly();
            //    IVacation v = (IVacation)assembly.CreateInstance("TDS2._0.Vacation_J1_2013");

            //    vac.Sauvegarder("vac.xml");
            //    //Bdd.Instance.test();
            //    List<ICycle> cycle = Bdd.Instance.select<ICycle>("select * from agents", null);
                
   
            //}
            //catch(Exception ex){
            //    int a = 10;
            //}
            ////bdd.test();
            //this.tabPage1.Controls.Remove(this.tableLayoutPanel1);
            //this.tableLayoutPanel1 = majTable();
            //this.tabPage1.Controls.Add(this.tableLayoutPanel1);
        }

        //private TableLayoutPanel majTable()
        //{
        //    TableLayoutPanel panel = new TableLayoutPanel();
        //    panel.ColumnCount = nbJourAfficher + 1;
        //    panel.RowCount = nbVacation + 1;
        //    panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        //    | System.Windows.Forms.AnchorStyles.Left)
        //    | System.Windows.Forms.AnchorStyles.Right)));
        //    panel.AutoScroll = true;
        //    panel.AutoSize = true;
        //    panel.Location = new System.Drawing.Point(32, 83);
        //    panel.Name = "tableLayoutPanel1";
        //    panel.Size = new System.Drawing.Size(732, 451);
        //    panel.TabIndex = 0;
        //    DateTime dateAffichage = this.date;
        //    for (int i = 1; i <= nbJourAfficher; ++i, dateAffichage = dateAffichage.AddDays(1))
        //    {
        //        panel.Controls.Add(new CellDate(dateAffichage), i, 0);
        //        ViewJ1J3N cell = new ViewJ1J3N();
        //        cell.Click += this.selection;
        //        panel.Controls.Add(cell, i, 1);
        //        cell = new ViewJ1J3N();
        //        cell.Click += this.selection;
        //        panel.Controls.Add(cell, i, 2);
        //        cell = new ViewJ1J3N();
        //        cell.Click += this.selection;
        //        panel.Controls.Add(cell, i, 3);
        //    }
        //    panel.Controls.Add(new CellTypeVacation(new J3_2013_D1()), 0, 1);
        //    panel.Controls.Add(new CellTypeVacation(new J1_D1_2013()), 0, 2);
        //    panel.Controls.Add(new CellTypeVacation(new N_D1_2013()), 0, 3);
        //    return panel;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //this.date = date.AddDays(-1);
            //this.tabPage1.Controls.Remove(this.tableLayoutPanel1);
            //this.tableLayoutPanel1 = majTable();
            //this.tabPage1.Controls.Add(this.tableLayoutPanel1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.date = date.AddDays(1);
            //this.tabPage1.Controls.Remove(this.tableLayoutPanel1);
            //this.tableLayoutPanel1 = majTable();
            //this.tabPage1.Controls.Add(this.tableLayoutPanel1);

        }

    }
}
