using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class SehirlerMesafe
    {
        private string sehir1;
        private string sehir2;
        private int aralarindakiMesafe;
        public string sehir1Prop { get { return sehir1; } set { sehir1 = value; } }
        public string sehir2Prop { get { return sehir2; } set { sehir2 = value; } }
        public int aralarindakiMesafeProp { get { return aralarindakiMesafe; } set { aralarindakiMesafe = value; } }


        public SehirlerMesafe()
        {           
        }
        public SehirlerMesafe(string sehir1, string sehir2, int aralarindakiMesafe)
        {
            this.sehir1 = sehir1;
            this.sehir2 = sehir2;
            this.aralarindakiMesafe = aralarindakiMesafe;
        }
    }
}
