using System.Collections;

namespace Project1
{
    public class Program
    {
        public static List<string> gecilenIller = new List<string>();

        public static List<List<string>> tumGecilenIller = new List<List<string>>();

        public static List<IlCiftleriVeMesafeleri> cikarilanIlCiftleri = new List<IlCiftleriVeMesafeleri>();

        public static void Main(string[] args)
        {
            int[][] mesafeler = mesafelerDizisi();

            string[] iller = illerDizisi();

            List<IlCiftleriVeMesafeleri> dizi = TumIllerArasiMesafeDizisi();

            // Soru A
            //SoruA(mesafeler, iller);

            //Soru B
            //ileMesafeVeBilgiler(mesafeler, iller);

            //Soru C
            //enYakinVeEnUzakIller(mesafeler, iller);

            //Soru D
            //SoruD(dizi, iller);

            // Soru E
            random5IlMatrisi();
        }
        //a
        public static int[][] mesafelerDizisi()
        {
            int[][] jaggedArray = new int[81][];

            string mesafelerDosyaYolu = "mesafeler.txt";

            // Dosya okuması ve listeye ekleme işlemleri bu kısımda yapılıyor 
            using (StreamReader reader = new StreamReader(mesafelerDosyaYolu))
            {
                string satir;
                int satirSirasi = 0;

                while ((satir = reader.ReadLine()) != null)
                {
                    string[] satirParcalarDizisi = satir.Split(",");
                    jaggedArray[satirSirasi] = new int[satirSirasi + 1];

                    for (int i = 0; i < satirParcalarDizisi.Length; i++)
                    {
                        string parca = satirParcalarDizisi[i].Trim();

                        if (parca == "nan")
                        {
                            jaggedArray[satirSirasi][i] = 0;
                            break;
                        }
                        else
                        {
                            jaggedArray[satirSirasi][i] = Convert.ToInt32(parca);
                        }
                    }

                    satirSirasi++;
                }
            }
            return jaggedArray;
        }
        public static string[] illerDizisi()
        {
            string[] dizi = new string[81];

            string illerDosyaYolu = "il_plaka.txt";

            // Dosya okuması burada yapılır, ve illerin isimlerini içeren liste oluşturulur.
            using (StreamReader reader = new StreamReader(illerDosyaYolu))
            {
                string satir;
                int satirSirasi = 0;


                while ((satir = reader.ReadLine()) != null)
                {

                    string[] satirParcalarDizisi = satir.Split(",");

                    dizi[satirSirasi] = satirParcalarDizisi[1].Trim();

                    satirSirasi++;
                }

            }

            return dizi;
        }
        public static void SoruA(int[][] mesafeler, string[] iller)
        {
            //Mesafeleri tutan jagged array'i yazdırır.
            foreach (int[] dizi in mesafeler)
            {
                foreach (int mesafe in dizi)
                {
                    if (mesafe == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write(mesafe + " ");
                    }
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------");

            // İller dizisini yazdırır
            for (int i = 0; i < iller.Length; i++)
            {
                Console.WriteLine($"{i + 1}. il : {iller[i]}");
            }
        }

        //b
        public static void ileMesafeVeBilgiler(int[][] mesafeler, string[] iller)
        {
            Console.WriteLine("İl plakasını giriniz:");
            int IlinPlakasi = Convert.ToInt32(Console.ReadLine());

            // Girilen ilin plakasını iller dizisindeki ilin indeksine eşitler.
            IlinPlakasi -= 1;

            Console.WriteLine("Mesafeyi giriniz:");
            int girilenMesafe = Convert.ToInt32(Console.ReadLine());

            int mesafeIciIlSayisi = 0;

            // MesafeSehir sınıfı girilen mesafe içindeki illerin adını ve ona olan mesafeyi tutar.
            // Buradaki liste bu sınıfı tutar.
            List<MesafeSehir> uygunSehirler = new List<MesafeSehir>();

            for (int i = 0; i < 81; i++)
            {
                // Jagged arrayi tarar uygun illerin nesnesini oluşturup listeye ekler.
                if (i < IlinPlakasi)
                {

                    if (girilenMesafe >= mesafeler[IlinPlakasi][i])
                    {
                        uygunSehirler.Add(new MesafeSehir(iller[i], mesafeler[IlinPlakasi][i]));
                        mesafeIciIlSayisi++;
                    }

                }
                else if (i > IlinPlakasi)
                {
                    if (girilenMesafe >= mesafeler[i][IlinPlakasi])
                    {
                        uygunSehirler.Add(new MesafeSehir(iller[i], mesafeler[i][IlinPlakasi]));
                        mesafeIciIlSayisi++;
                    }
                }
                else { continue; }
            }

            Console.WriteLine($"Seçilen il adı: {iller[IlinPlakasi]}");
            Console.WriteLine($"Mesafe içi il sayısı: {mesafeIciIlSayisi}\n");
            foreach (var eleman in uygunSehirler)
            {
                Console.WriteLine($"İl adı: {eleman.IlAdiProp}, Uzaklık: {eleman.IleOlanMesafeProp} KM");
            }

        }

        //c
        public static void enYakinVeEnUzakIller(int[][] mesafeler, string[] iller)
        {
            int enKisaMesafe = 99999;
            int enUzunMesafe = 0;

            SehirlerMesafe enUzakSehirler = new SehirlerMesafe();
            SehirlerMesafe enYakinSehirler = new SehirlerMesafe();

            for (int i = 80; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    // En uzak şehirleri bulmak için
                    if (mesafeler[i][j] > enUzunMesafe)
                    {
                        enUzunMesafe = mesafeler[i][j];

                        enUzakSehirler.sehir1Prop = iller[i];
                        enUzakSehirler.sehir2Prop = iller[j];
                        enUzakSehirler.aralarindakiMesafeProp = mesafeler[i][j];

                    }
                    // En yakın şehirleri bulmak için
                    if (mesafeler[i][j] < enKisaMesafe)
                    {
                        enKisaMesafe = mesafeler[i][j];

                        enYakinSehirler.sehir1Prop = iller[i];
                        enYakinSehirler.sehir2Prop = iller[j];
                        enYakinSehirler.aralarindakiMesafeProp = mesafeler[i][j];
                    }
                }
            }

            Console.WriteLine($"En uzak şehirler: {enUzakSehirler.sehir1Prop}-{enUzakSehirler.sehir2Prop}, Aralarındaki mesafe: {enUzakSehirler.aralarindakiMesafeProp} KM");
            Console.WriteLine($"En yakın şehirler: {enYakinSehirler.sehir1Prop}-{enYakinSehirler.sehir2Prop}, Aralarındaki mesafe: {enYakinSehirler.aralarindakiMesafeProp} KM");
        }

        //d
        public static void enFazlaIlDolasMesafe(List<IlCiftleriVeMesafeleri> tumIllerMesafeDizisi, string aramaYapilacakIl, int aramaYapilacakMesafe)
        {

            List<IlCiftleriVeMesafeleri> UygunSehirler = tumIllerMesafeDizisi.FindAll(x => x.BirinciIlProp == aramaYapilacakIl && x.IllerArasiMesafeProp < aramaYapilacakMesafe);

            if (UygunSehirler.Count == 0)
            {
                gecilenIller.Add(aramaYapilacakIl);

                List<string> tempListe = new List<string>();

                foreach (var e in gecilenIller)
                {
                    tempListe.Add(e);
                }
                tumGecilenIller.Add(tempListe);
                
                tumIllerMesafeDizisi.AddRange(cikarilanIlCiftleri);
                cikarilanIlCiftleri.Clear();

            }

            else
            {
                foreach (var eleman in UygunSehirler)
                {

                    int kalanMesafe = aramaYapilacakMesafe - eleman.IllerArasiMesafeProp;

                    gecilenIller.Add(eleman.BirinciIlProp);
                    int bitisIndexi = gecilenIller.IndexOf(eleman.BirinciIlProp);
                    gecilenIller = gecilenIller.GetRange(0, bitisIndexi + 1);

                    foreach(var il in gecilenIller)
                    {
                        tumIllerMesafeDizisi.RemoveAll(x =>
                        {
                            if (x.IkinciIlProp == il && x.BirinciIlProp == eleman.IkinciIlProp)
                            {
                                cikarilanIlCiftleri.Add(x); 
                                return true;  //öğeyi kaldır
                            }
                            return false; //öğeyi kaldırma
                        });

                    }

                    enFazlaIlDolasMesafe(tumIllerMesafeDizisi, eleman.IkinciIlProp, kalanMesafe);

                }
            }
        }
        public static List<IlCiftleriVeMesafeleri> TumIllerArasiMesafeDizisi()
        {
            List<IlCiftleriVeMesafeleri> dizi = new List<IlCiftleriVeMesafeleri> ();

            for(int i = 0; i < 81;  i++)
            {
                for (int j = 0; j < 81; j++)
                {

                    if (i < j)
                    {
                        dizi.Add(new IlCiftleriVeMesafeleri(illerDizisi()[i], illerDizisi()[j], mesafelerDizisi()[j][i]));

                    }
                    else if (i > j)
                    {
                        dizi.Add(new IlCiftleriVeMesafeleri(illerDizisi()[i], illerDizisi()[j], mesafelerDizisi()[i][j]));

                    }
                    else { continue; }
                }
            }
            return dizi;
        }

        public static void SoruD(List<IlCiftleriVeMesafeleri> TumIlCiftleriDizisi, string[] iller)
        {

            Console.WriteLine("Rotayı başlatmak istediğiniz ilin plakasını giriniz: ");
            int IlPlakasi = Convert.ToInt32(Console.ReadLine());
            string IlAdi = iller[IlPlakasi - 1];

            Console.WriteLine("Rota mesafesini giriniz: ");
            int mesafe = Convert.ToInt32(Console.ReadLine());

            enFazlaIlDolasMesafe(TumIlCiftleriDizisi, IlAdi, mesafe);

            int gecilenIlSayisi = 0;
            int katedilenMesafe = 0;

            List<string> yakalananListe = new List<string>();
            foreach (var eleman in tumGecilenIller)
            {
                if (eleman.Count() > gecilenIlSayisi)
                {
                    gecilenIlSayisi = eleman.Count();
                    yakalananListe = eleman;
                }
            }
            Console.WriteLine($"Seçilen il adı: {iller[IlPlakasi-1]}");
            Console.WriteLine($"Dolaşılan il sayısı : {gecilenIlSayisi}");
            Console.WriteLine();

            for (int i = 0; i < yakalananListe.Count; i++)
            {
                string il1 = yakalananListe[i];
 
                if (i != yakalananListe.Count - 1)
                {
                    string il2 = yakalananListe[i + 1];

                    IlCiftleriVeMesafeleri ilcifti = TumIlCiftleriDizisi.Find(x => x.BirinciIlProp == il1 && x.IkinciIlProp == il2);

                    katedilenMesafe += ilcifti.IllerArasiMesafeProp;
                }

                Console.WriteLine(il1);
            }
            Console.WriteLine();
            Console.WriteLine($"Katedilen mesafe: {katedilenMesafe} KM");
            Console.WriteLine();
        }

        //e
        public static void random5IlMatrisi()
        {
            int[,] IllerArasiUzaklıkMatrisi = new int[5,5];

            Random random = new Random();
            int[] RandomIlPlakalari = new int[5];
            List<int> sayiKontrolcu = new List<int>();

            bool kontrolcu = true;
            int sayac = 0;
            while (kontrolcu)
            {
                int randomSayi = random.Next(0,81);
                if(sayac == 5)
                {
                    kontrolcu = false;
                }
                else
                {
                    if (sayiKontrolcu.Contains(randomSayi))
                    {
                        continue;
                    }
                    else
                    {
                        RandomIlPlakalari[sayac] = randomSayi;
                        sayac++;
                        sayiKontrolcu.Add(randomSayi);
                    }
                }
                

            }

           
            string[] illerListesi = new string[5];
            for(int i = 0; i < 5; i++)
            {
                
                illerListesi[i] = illerDizisi()[RandomIlPlakalari[i]];
            }


            for(int i = 0; i < 5 ; i++)
            {
                for(int j = 0; j < 5 ; j++)
                {
                    int birinciIlPlaka = RandomIlPlakalari[i];
                    int ikinciIlPlaka = RandomIlPlakalari[j];

                    if(i == j)
                    {
                        IllerArasiUzaklıkMatrisi[i, j] = 0;
                    }
                    else
                    {
                        if (birinciIlPlaka > ikinciIlPlaka)
                        {
                            IllerArasiUzaklıkMatrisi[i, j] = mesafelerDizisi()[birinciIlPlaka][ikinciIlPlaka];

                        }
                        else if (birinciIlPlaka < ikinciIlPlaka)
                        {
                            IllerArasiUzaklıkMatrisi[i, j] = mesafelerDizisi()[ikinciIlPlaka][birinciIlPlaka];
                        }
                    }
                }
            }

            Console.WriteLine($"            {illerListesi[0]}  {illerListesi[1]}  {illerListesi[2]}  {illerListesi[3]}  {illerListesi[4]}");
            Console.WriteLine("------------------------------------------------------");
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"{illerListesi[i],-15}");
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{IllerArasiUzaklıkMatrisi[i, j],-8}");
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------");
            }
        }

    }
}