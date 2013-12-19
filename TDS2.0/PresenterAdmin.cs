using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Core
{
    public interface IViewAdmin
    {
        List<ICycle> ListCycleBdd { set; }
        List<string> ListCycleFactory { set; }
        List<string> ListTypeFactory { set; }
    }
    public class PresenterAdmin
    {
        IViewAdmin view;
        IModelAdmin model;
        public PresenterAdmin(IViewAdmin view, IModelAdmin model)
        {
            this.view = view;
            this.model = model;
            view.ListCycleBdd = model.ListCycleBdd;
            view.ListCycleFactory = model.ListCycleFactory;
            view.ListTypeFactory = model.ListTypeFactory;
        }
    }
    public interface IModelAdmin
    {
        List<ICycle> ListCycleBdd{ get; }
        List<string> ListCycleFactory { get; }
        List<string> ListTypeFactory { get; }
    }
    public class ModelAdmin : IModelAdmin
    {
        public List<ICycle> ListCycleBdd
        {
            get 
            { 
                return DaoCycle.findAll<ICycle>(); 
            }
        }
        public List<string> ListCycleFactory
        {
            get 
            {
                List<string> liste = new List<string>();
                Assembly assembly = Assembly.GetExecutingAssembly();
                foreach (Type type in assembly.GetTypes())
                {
                    if( type.IsSubclassOf( typeof(ICycle) ) )
                        liste.Add(type.FullName);
                }
                return liste;
            }
        }
        public List<string> ListTypeFactory
        {
            get
            {
                List<string> liste = new List<string>();
                Assembly assembly = Assembly.GetExecutingAssembly();
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(ITypeVacation)))
                        liste.Add(type.FullName);
                }
                return liste;
            }
        }
    }
}
