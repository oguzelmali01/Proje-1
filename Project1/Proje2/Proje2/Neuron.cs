using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje2
{
    public class Neuron
    {
        private double AgirlikCalismaSuresi { get; set; }
        private double AgirlikDerseDevamSuresi { get; set; }


        public double AgirlikCalismaSuresiProp { get { return AgirlikCalismaSuresi; } set { AgirlikCalismaSuresi = value; }  }
        public double AgirlikDerseDevamSuresiProp { get { return AgirlikDerseDevamSuresi; } set { AgirlikDerseDevamSuresi = value; } }

        private Random random { get; set; } = new Random();

        public const double ogrenmeKatsayisi = 0.025;
        //public const double ogrenmeKatsayisi = 0.01;
        //public const double ogrenmeKatsayisi = 0.05;

        public const int epochs = 10;
        //public const int epochs = 50;
        //public const int epochs = 100;

        public double[,] EgitimVerileri = {
            { 7.6, 11, 77 },
            { 8, 10, 70 },
            { 6.6, 8, 55 },
            { 8.4, 10, 78},
            { 8.8, 12, 95 },
            { 7.2, 10, 67 },
            { 8.1, 11, 80},
            { 9.5, 9, 87},
            { 7.3, 9, 60},
            { 8.9, 11, 88},
            { 7.5, 11, 72},
            { 7.6, 9, 58},
            { 7.9, 10, 70},
            { 8, 10, 76},
            { 7.2, 9, 58},
            { 8.8, 10, 81},
            { 7.6, 11, 74},
            { 7.5, 10, 67},
            { 9, 10, 82},
            { 7.7, 9, 62},
            {8.1 ,11 ,82 }
        };

        //public double[,] EgitimVerileri = {
        //    { 10, 1, 100 },
        //    { 8, 9, 90 },
        //    { 6, 7, 70 },
        //    { 3, 3, 45},
        //    { 8, 12, 95 }
        //};

        public double[] MSEdegerleri = new double[epochs];


        public Neuron()
        {
            AgirlikCalismaSuresi = random.NextDouble();
            AgirlikDerseDevamSuresi = random.NextDouble();

            NoronuEgit();
        }

        public double SinavSonucuHesapla(double CalismaSuresi, double DerseDevamSuresi)
        {
            return (CalismaSuresi * AgirlikCalismaSuresi) + (DerseDevamSuresi * AgirlikDerseDevamSuresi);
        }

        public void NoronuEgit()
        {
            double AgirlikCalismaSuresi_temp;
            double AgirlikDerseDevamSuresi_temp;


            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double MSEdegeri = 0;

                for (int i = 0; i < EgitimVerileri.GetLength(0); i++)
                {
                    double cSuresi = EgitimVerileri[i, 0] / 10;
                    double dSuresi = EgitimVerileri[i, 1] / 15;
                    double sSonuc = EgitimVerileri[i, 2] / 100;

                    AgirlikCalismaSuresi_temp = AgirlikCalismaSuresi + ogrenmeKatsayisi * (sSonuc - SinavSonucuHesapla(cSuresi, dSuresi)) * cSuresi;

                    AgirlikDerseDevamSuresi_temp = AgirlikDerseDevamSuresi + ogrenmeKatsayisi * (sSonuc - SinavSonucuHesapla(cSuresi, dSuresi)) * dSuresi ;

                    AgirlikCalismaSuresi = AgirlikCalismaSuresi_temp;
                    AgirlikDerseDevamSuresi = AgirlikDerseDevamSuresi_temp;

                    MSEdegeri += Math.Pow((sSonuc - SinavSonucuHesapla(cSuresi, dSuresi)), 2);
                }

                MSEdegerleri[epoch] = MSEdegeri / EgitimVerileri.GetLength(0);
            }
        }       

        public void MakinaTahminleriVeMSEtablsou()
        {

            Console.WriteLine($"Çalışma Süresi (saat) - Derse Devam (hafta) - Sınav Sonucu t (100) - Makina Tahmini");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < EgitimVerileri.GetLength(0); i++)
            {
                double cSuresi = EgitimVerileri[i, 0];
                double dSuresi = EgitimVerileri[i, 1];
                double sSonuc = EgitimVerileri[i, 2];

                Console.WriteLine($"         {cSuresi, -20}{dSuresi,-20}{sSonuc,-20}{SinavSonucuHesapla(cSuresi/10, dSuresi/15)*100: 0.00}");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"MSE (Mean Squared Error) Değerleri");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < MSEdegerleri.Length; i++)
            {
                Console.WriteLine($"{i+1}. Devir için MSE değeri: {MSEdegerleri[i]}");
                Console.WriteLine();
            }
        }
    }
}
