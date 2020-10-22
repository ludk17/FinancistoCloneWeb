using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb
{

    class Main2
    {
        public Main2()
        {
            int a = 1;
            string b = "2";
            MyClass c = new MyClass();
            IMyInterface i = new MyClass();
            var r = i.Multiplicar(1, 2);
        }
    }

    public interface IMyInterface
    {
        int Multiplicar(int a, int b);
        void OtherMethod(int a);
    }

    public class MyClass: IMyInterface
    {
        public int Multiplicar(int a, int b)
        {
            return a * b;
        }

        public void OtherMethod(int a)
        {

        }
    }

    public class MyOtherClass : IMyInterface
    {
        public int Multiplicar(int a, int b)
        {
            var resultado = 0;
            for (var i = 0; i < b; i++)
                resultado += a;
            
            return resultado;
        }

        public void OtherMethod(int a)
        {

        }
    }
}
