using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class MesafeSehir
    {

        private string IlAdi;
        private int IleOlanMesafe;
        public string IlAdiProp { get { return IlAdi; } set { IlAdi = value; } }
        public int IleOlanMesafeProp { get { return IleOlanMesafe; } set { IleOlanMesafe = value; } }

        public MesafeSehir(string IlAdi, int IleOlanMesafe)
        {
            this.IlAdi = IlAdi;
            this.IleOlanMesafe = IleOlanMesafe;
        }
    }
}
