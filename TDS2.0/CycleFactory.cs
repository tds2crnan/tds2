using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Core
{
    //public class RegisterFactory<T>
    //    where T : ICycle, new()
    //{
    //    public RegisterFactory()
    //    {
    //        CycleFactory.add(this.GetType().ToString(), new RealFactoryImpl<T>());
    //    }
    //}

    //public interface RealFactory
    //{
    //    ICycle build();
    //}

    //public class RealFactoryImpl<T> : RealFactory
    //    where T : ICycle, new()
    //{
    //    public ICycle build()
    //    {
    //        return new T();
    //    }
    //}

    //public abstract class ICycle2<T> : ICycle
    //    where T : ICycle, new()
    //{
    //    private static RegisterFactory<T> reg = new RegisterFactory<T>();

    //}

    //class CycleFactory
    //{
    //    static Dictionary<string, RealFactory> dico = new Dictionary<string, RealFactory>();
    //    static public void add(string nom, RealFactory factory)
    //    {
    //        dico[nom] = factory;
    //    }
    //    static public ICycle get(string nom)
    //    {
    //        return dico[nom].build();
    //    }
    //}

    //class CycleTest : ICycle2<CycleTest>
    //{
    //    public override IVacation peuplerCycle(int iteration, DateTime date)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override int dureeCycle()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override List<IVacation> getListTypeVacation()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


}
