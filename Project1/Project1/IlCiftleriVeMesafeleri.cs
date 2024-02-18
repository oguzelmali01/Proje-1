using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class IlCiftleriVeMesafeleri
    {
        private string BirinciIl { get; set; }
        private string IkinciIl { get; set; }
        private int IllerArasiMesafe { get; set; }

        public string BirinciIlProp { get { return BirinciIl; } set { BirinciIl = value; } }
        public string IkinciIlProp { get { return IkinciIl; } set { IkinciIl = value; } }
        public int IllerArasiMesafeProp { get { return IllerArasiMesafe; } set { IllerArasiMesafe = value; } }

        public IlCiftleriVeMesafeleri()
        {
            
        }

        public IlCiftleriVeMesafeleri(string BirinciIl, string IkinciIl, int IllerArasiMesafe)
        {
            this.BirinciIl = BirinciIl;
            this.IkinciIl = IkinciIl;
            this.IllerArasiMesafe = IllerArasiMesafe;
        }
    }
}
