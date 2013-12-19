using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace Core
{
    public interface ICompoModelAction
    {
        Type getType();
        void add(ICompoModelAction action);
        bool isValid(int nbAdd);
        void addActionOnView(int nbAdd);
        void removeActionOnView();
    }
    public abstract class CompositionModelAction<T> : ICompoModelAction
    {
        protected List<T> listeModel = new List<T>();
        public Type getType()
        {
            return typeof(T);
        }
        public void add(ICompoModelAction model)
        {
            CompositionModelAction<T> cm = model as CompositionModelAction<T>;
            if (cm != null)
                listeModel.Add(cm.listeModel.First());
            else
                throw new Exception("erreur");
        }
        abstract public bool isValid(int nbAdd);
        abstract public void addActionOnView(int nbAdd);
        abstract public void removeActionOnView();
    }

    public class ListActionItem
    {
        int nbAdd = 0;
        List<ICompoModelAction> listCompoAction = new List<ICompoModelAction>();
        public void Add(ICompoModelAction model)
        {
            nbAdd++;
            foreach (ICompoModelAction composition in listCompoAction)
            {
                if (composition.getType() == model.getType())
                {
                    composition.add(model);
                    return;
                }
            }
            listCompoAction.Add(model);
        }
        public void build()
        {
            foreach (ICompoModelAction composition in listCompoAction)
            {
                if (composition.isValid(nbAdd))
                    composition.addActionOnView(nbAdd);
            }
        }
    }

    public class ModelModificationVacationOnSemaineSub : CompoModificationVacation
    {
        IViewSemaineSub view;

        public ModelModificationVacationOnSemaineSub(IModelModificationVacation model, IViewSemaineSub view)
            : base(model)
        {
            this.view = view;
        }
        public override void addActionOnView(int nbAdd)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = "remplacement";
            item.Click += runDialog;
            view.addItem(item);
        }

        public override void removeActionOnView()
        {
        }
    }

    public class Outils
    {
        public static bool isFerie(DateTime date)
        {
            return false;
        }
        public static DateTime lastDayOfYear(DateTime date)
        {
            DateTime lastDay = new DateTime( date.Year, 12,31);
            return lastDay;
        }
        public static DateTime firstDayOfYear(DateTime date)
        {
            DateTime firstDay = new DateTime(date.Year, 1, 1);
            return firstDay;
        }

    }


#region D1

    public class Vacation_J1_D1_2013 : IVacation//, IModelJ1J3N
    {
        public Vacation_J1_D1_2013()
        {
        }
    }

    public class Type_J1_D1_2013 : ITypeVacation
    {
        public Type_J1_D1_2013()
        {
        }
        public override string getNom()
        {
            return "J1";
        }
        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            return new ViewVacation1Person(presenter, new ModelJ1J3N<Vacation_J1_D1_2013, Type_J1_D1_2013>(date, cycle));
        }

        public override string makePrint()
        {
            return "J1";
        }
    }

    public class Vacation_J3_D1_2013 : IVacation//, IModelJ1J3N
    {
        public Vacation_J3_D1_2013()
        {
        }
    }

    public class Type_J3_D1_2013 : ITypeVacation
    {
        public override string getNom()
        {
            return "J3";
        }
        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Tuesday || date.DayOfWeek == DayOfWeek.Thursday)
                return new ViewText("D4 :)");
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                return new ViewText("");
            if (Outils.isFerie(date))
                return new ViewText("ferie");
            else
                return new ViewVacation1Person(presenter, new ModelJ1J3N<Vacation_J3_D1_2013, Type_J3_D1_2013>(date, cycle));
        }

        public override string makePrint()
        {
            return "J3";
        }
    }

    public class Vacation_N_D1_2013 : IVacation//, IModelJ1J3N
    {
        public Vacation_N_D1_2013()
        {
        }
    }

    public class Type_N_D1_2013 : ITypeVacation
    {
        public override string getNom()
        {
            return "N";
        }

        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            return new ViewVacation1Person(presenter, new ModelJ1J3N<Vacation_N_D1_2013, Type_N_D1_2013>(date, cycle));
        }

        public override string makePrint()
        {
            return "N";
        }
    }

    public class Cycle_D1_2013 : ICycle
    {

        public override void peuplerCycle(MetierAgent agent, int iteration, DateTime date)
        {
            switch (iteration % 6)
            {
                case 0: //J3                    
                    if (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Wednesday || date.DayOfWeek == DayOfWeek.Friday)
                        if (!Outils.isFerie(date))
                            DaoIVacation.create<Vacation_J3_D1_2013>(agent, date, DaoTypes.findOne<Type_J3_D1_2013>(this));
                    break;
                case 1: //J1
                    DaoIVacation.create<Vacation_J1_D1_2013>(agent, date, DaoTypes.findOne<Type_J1_D1_2013>(this));
                    break;
                case 2: //N
                    DaoIVacation.create<Vacation_N_D1_2013>(agent, date, DaoTypes.findOne<Type_N_D1_2013>(this));
                    break;
            }
        }

        public override int dureeCycle()
        {
            return 7 * 6;
        }


        public override List<ITypeVacation> getListTypeVacation()
        {
            List<ITypeVacation> list = new List<ITypeVacation>();
            list.Add(DaoTypes.findOne<Type_J3_D1_2013>(this));
            list.Add(DaoTypes.findOne<Type_J1_D1_2013>(this));
            list.Add(DaoTypes.findOne<Type_N_D1_2013>(this));
            return list;
        }

        //public override IViewCellPeupler makeView(PresenterPeupler presenter, MetierSub sub, DateTime date)
        //{
        //    return new ViewCycleAnnee(presenter, new ModelCellPeupler(sub,date) );
        //}

        public override UserControl makeView(PresenterPeupler racine, MetierEquip equip, DateTime date)
        {
            return new ViewListCycle( racine, new ModelCycle_D1_2013<Cycle_D1_2013>(this, equip, date));
        }
    }

    #endregion

#region D2

    public class Vacation_D2_2013 : IVacation
    {
        int numeroAgent;
        public int NumeroAgent{
            set 
            {
                numeroAgent = value;
                DaoIVacation.update(this);
            }
            get
            {
                return numeroAgent;
            }
        }
        public Vacation_D2_2013()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }
        public Vacation_D2_2013(MetierAgent agent, DateTime date, ITypeVacation type, int numeroAgent)
            : this()
        {
            this.agent = agent;
            this.date = date;
            this.type = type;
            this.numeroAgent = numeroAgent;
        }
        private void loadFrBdd(Dictionary<string, object> row)
        {
            numeroAgent = loadTag<int>(row);
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            saveTag<int>(param,numeroAgent);
        }
    }

    public class Type_J3_D2_2013 : ITypeVacation
    {
        public Type_J3_D2_2013()
        {
        }
        public override string getNom()
        {
            return "J3";
        }

        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                return new ViewText("");
            if (Outils.isFerie(date))
                return new ViewText("ferie");
            else
                return new ViewVacation1Person(presenter, new ModelJ1J3N<Vacation_J3_D1_2013, Type_J3_D2_2013>(date, cycle));
        }

        public override string makePrint()
        {
            return "J3";
        }
    }

    //public class Vacation_J1_D2_2013 : IVacation
    //{
    //    int idPerso;
    //    public Vacation_J1_D2_2013()
    //    {
    //    }
    //    public Vacation_J1_D2_2013(MetierAgent agent, DateTime date, int idPerso)
    //        : this()
    //    {
    //    }
    //}

    public class Type_J1_D2_2013 : ITypeVacation
    {
        public override string getNom()
        {
            return "J1";
        }

        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            return new ViewVacation2Person(presenter, new ModelVacation1PersonSur2<Type_J1_D2_2013>(date, cycle, 1), new ModelVacation1PersonSur2<Type_J1_D2_2013>(date, cycle, 2));
        }

        public override string makePrint()
        {
            return "J1";
        }
    }

    //public class Vacation_N_D2_2013 : IVacation
    //{
    //    int idPerso;
    //    public Vacation_N_D2_2013()
    //    {
    //    }
    //    public Vacation_N_D2_2013(MetierAgent agent, DateTime date, int idPerso)
    //        : this()
    //    {
    //    }
    //}

    public class Type_N_D2_2013 : ITypeVacation
    {
        public override string getNom()
        {
            return "N";
        }

        public override IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date)
        {
            return new ViewVacation2Person(presenter, new ModelVacation1PersonSur2<Type_N_D2_2013>(date, cycle, 1), new ModelVacation1PersonSur2<Type_N_D2_2013>(date, cycle, 2));
        }

        public override string makePrint()
        {
            return "N";
        }
    }

    public class Cycle_D2_2013 : ICycle
    {
        bool isFerie(DateTime date)
        {
            return false;
        }

        public override void peuplerCycle(MetierAgent agent, int iteration, DateTime date)
        {
            int numeroAgent = 1;
            switch (iteration % 6)
            {
                case 0: //J3
                    if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                        if (!Outils.isFerie(date))
                        {
                            DaoIVacation.create<Vacation_D2_2013, int>(agent, date, DaoTypes.findOne<Type_J3_D2_2013>(this), numeroAgent);
                        }
                    break;
                case 1: //J1
                    {
                        DaoIVacation.create<Vacation_D2_2013, int>(agent, date, DaoTypes.findOne<Type_J1_D2_2013>(this), numeroAgent);
                    }
                    break;
                case 2: //N
                    {
                        DaoIVacation.create<Vacation_D2_2013, int>(agent, date, DaoTypes.findOne<Type_N_D2_2013>(this), numeroAgent);
                    }
                    break;
            }
        }

        public override int dureeCycle()
        {
            return 7 * 6;
        }

        public override List<ITypeVacation> getListTypeVacation()
        {
            List<ITypeVacation> list = new List<ITypeVacation>();
            list.Add(DaoTypes.findOne<Type_J3_D2_2013>(this));
            list.Add(DaoTypes.findOne<Type_J1_D2_2013>(this));
            list.Add(DaoTypes.findOne<Type_N_D2_2013>(this));
            return list;
        }

        public override UserControl makeView(PresenterPeupler racine, MetierEquip equip, DateTime date)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    //#region Cautra
    //class MO_12_22_D4 { }
    //class MO_07_13_D4 { }
    //class MO_ass_D4 { }

    //class vac_12_22_D4 { }
    //class vac_07_13_D4 { }
    //class vac_ass_D4 { }

    //class Cycle_D4_2013 : ICycle
    //{
    //    bool isFerie(DateTime date)
    //    {
    //        return false;
    //    }

    //    public override void peuplerCycle(int iteration, DateTime date)
    //    {
    //        switch (iteration)
    //        {
    //            case 0: //                 
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 1:
    //                if (isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 2: //
    //                if (!isFerie(date))
    //                    new vac_07_13_D4();
    //                break;
    //            case 3: //
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 4://asstreinte si ferie
    //                if (isFerie(date))
    //                    new vac_07_13_D4();
    //                break;
    //            case 5:
    //                new vac_matin_D4();
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 6:
    //                new vac_matin_D4();
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 7://asstreinte si ferie
    //                if (isFerie(date))
    //                    new vac_07_13_D4();
    //                break;
    //            case 8:
    //                if (!isFerie(date))
    //                    new vac_07_13_D4();
    //                break;
    //            case 9:
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 10://asstreinte si ferie
    //                if (isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 11:
    //                if (!isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 14:
    //                if (!isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 15:
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //            case 16://asstreinte si ferie
    //                if (isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 17:
    //                if (!isFerie(date))
    //                    new vac_matin_D4();
    //                break;
    //            case 18:
    //                new vac_soir_D4();
    //                new vac_astreinte_D4();
    //                break;
    //        }
    //    }

    //    public override int dureeCycle()
    //    {
    //        return 3 * 7;
    //    }

    //    public override List<ITypeVacation> getListTypeVacation()
    //    {
    //        List<ITypeVacation> list = new List<ITypeVacation>();
    //        list.Add(new MO_07_13_D4());
    //        list.Add(new MO_12_22_D4());
    //        list.Add(new MO_ass_D4());
    //        return list;
    //    }
    //}
    //#endregion
}
