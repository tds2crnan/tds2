using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Core
{
    public interface IViewVacation2Person : IViewCellSemaineSub
    {
        UserControl vacation1 { set; }
        UserControl vacation2 { set; }
    }

    public class PresenterVacation2Person
    {
        IViewVacation2Person view;

        IModelVacation1Person model1;
        IModelVacation1Person model2;
        
        PresenterSemaineSub racine;
        public PresenterVacation2Person( PresenterSemaineSub racine, IViewVacation2Person view, IModelVacation1Person model1, IModelVacation1Person model2)
        {
            this.model1 = model1;
            this.model2 = model2;
            this.view = view;
            this.view.vacation1 = new ViewVacation1PersonSur2(racine,model1);
            this.view.vacation2 = new ViewVacation1PersonSur2(racine,model2);
            this.racine = racine;
 
        }
        void select()
        {
           // if( selectedVacation1 == true)

        }
        void unselect()
        {
        }
    }

    public interface IModelVacation2Pseron : IModelCellSemaineSub
    {

    }



    public class ModelVacation1PersonSur2<T> : IModelVacation1Person
        where T : ITypeVacation, new()
    {
        Vacation_D2_2013 vacation=null;

        public ModelVacation1PersonSur2(DateTime date, ICycle cycle, int numeroAgent)
        {
            List<Vacation_D2_2013> listVacation = DaoIVacation.find<Vacation_D2_2013,T>(date);
            foreach( Vacation_D2_2013 vac in listVacation )
            {
                if (vac.NumeroAgent == numeroAgent)
                {
                    vacation = vac;
                }
            }
            if (vacation == null)
                this.vacation = DaoIVacation.create<Vacation_D2_2013, int>(null, date, DaoTypes.findOne<T>(cycle), numeroAgent);
            this.vacation.PropertyChanged += notifyPropertyChanged;
        }

        public event PropertyChangedEventHandler ModelChanged;
        private void notifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ModelChanged(sender, e);
        }
        public string NomAgent
        {
            get
            {
                if (vacation.Agent == null)
                    return "pas d'agent";
                else
                    return vacation.Agent.Nom;
            }
        }

        public void actionDisponible(ListActionItem listAction, IViewSemaineSub view)
        {
            listAction.Add(new ModelModificationVacationOnSemaineSub(new ModelModificationVacation<Vacation_D2_2013>(vacation), view));
        }
    }
}
