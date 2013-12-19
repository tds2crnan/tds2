using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;

namespace Core
{
    public abstract class ITypeVacation
    {
        public abstract string getNom();
        public abstract int nbAgent();
        public abstract IVacation makeVacation(IModelDate date);
        public abstract DateTime debut();
        public abstract DateTime fin();
        public abstract IViewSelectable makeView(PresenterSemaineSub presenter) { return null; }
    }

    public abstract class IVacation
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public abstract IViewSelectable makeView(PresenterSemaineSub presenter, IModelDate date) { return null; }

        //public IVacation() { }
        //public void Sauvegarder(string filename)
        //{
        //    FileStream file = File.Open(filename, FileMode.OpenOrCreate);
        //    XmlSerializer serializer = new XmlSerializer(this.GetType());
        //    serializer.Serialize(file, this);
        //    file.Close();
        //}

        //public IVacation Charger(string filename)
        //{
        //    FileStream file = File.Open(filename, FileMode.Open);
        //    XmlSerializer serializer = new XmlSerializer(this.GetType());
        //    IVacation unCarnet = (IVacation)serializer.Deserialize(file);
        //    file.Close();
        //    return unCarnet;
        //}
    }
}
