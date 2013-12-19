using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core
{
    //public interface IViewDate
    //{
    //    DateTime Date {set;}
    //}

    //public class PresenterDate
    //{
    //    IViewDate view;
    //    IModelDate model;

    //    public PresenterDate(IViewDate view, IModelDate model)
    //    {
    //        this.view = view;
    //        this.model = model;
    //        model.PropertyChanged += update;
    //        view.Date = model.Date;
    //    }
    //    void update(object sender, PropertyChangedEventArgs e)
    //    {
    //        view.Date = model.Date;
    //    }
    //}

    //public interface IModelDate : INotifyPropertyChanged
    //{
    //    DateTime Date { get; }
    //    event PropertyChangedEventHandler PropertyChanged;
    //}

    //class ModelDate : IModelDate
    //{
    //    int shift;
    //    DateTime date;

    //    public ModelDate(DateTime date, int shift)
    //    {
    //        this.date = date;
    //        this.shift = shift;
    //    }

    //    public DateTime Date
    //    {
    //        get
    //        {
    //            return date.AddDays(shift);
    //        }
    //        set
    //        {
    //            date = value;
    //            NotifyPropertyChanged("date");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private void NotifyPropertyChanged(String propertyName)
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //}
}
