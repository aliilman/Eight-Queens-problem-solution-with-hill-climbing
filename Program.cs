using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace eightQueens
{
    
    class Program
    {   
        static Random  random = new Random();
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();  //işlem süresi hesaplama
            int[] satrançtahtası = new int[8];
            double[,] yerD_randomR_işlemS = new double[25, 3]; // Sırasıyla YerDeğiştirme - RandomRestart - İşlemSüre sinin tutan matris
            double[] toplamişlem = new double[3];

            for (int i = 0; i < 25; i++) //25 kere çözdürme 
            {
                watch.Start(); 
                Cozum(satrançtahtası, yerD_randomR_işlemS,i); //çözdürme fonksiyonu
                watch.Stop();
                yerD_randomR_işlemS[i, 2] = watch.ElapsedMilliseconds;  
                Console.Write("\n" +i + ":\t");
                for (int j = 0; j < 8; j++)
                    Console.Write(satrançtahtası[j]+" ");

            }
            //Yazdırma işlemleri
            Console.WriteLine("\n\t Yer Değiştirme \t Ramdom Restart \t İşlem süresi");
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(i+1 +":\t\t"+ yerD_randomR_işlemS[i,0] + "\t\t\t" + yerD_randomR_işlemS[i,1] + "\t\t\t" + yerD_randomR_işlemS[i, 2] + " ms\t\t");
                toplamişlem[0] += yerD_randomR_işlemS[i,0];
                toplamişlem[1] += yerD_randomR_işlemS[i, 1];
                toplamişlem[2] += yerD_randomR_işlemS[i, 2];

            }
            Console.WriteLine("Ortalama İşelem \t" + toplamişlem[0]/25 + "\t\t" + toplamişlem[1]/25 + "\t\t" + toplamişlem[2]/25);

            Console.ReadLine();

        }
        public static void Cozum(int[] satrançtahtası, double[,] yerD_randomR_işlemS, int No )
        {
          
            bool kontrol= false;// çözüm varlığı kontrolu

            for(int i = 0; i < 8; i++) //8 satırlık dizi tahta ilemi için döngü
            {
                
                int rnd = random.Next(8);
                yerD_randomR_işlemS[No, 0] += 1;

                satrançtahtası[i] = rnd;

                while (Cakısma_kontrol(satrançtahtası, i) == 1)//çakışma varsa yeni değer üretmek için gir
                {
                    int cakışansayısı = 0;           //çakışmayan değer var mı kontrolü
                   
                    for (int l = 0; l < 8; l++)     //kullanılabilecek değer varlığı kontrolu
                    {
                        satrançtahtası[i] = l;
                        if (Cakısma_kontrol(satrançtahtası, i) == 1) //tüm değerelerin çakışıyor olması durunuda 
                         cakışansayısı++;                            // random restart için konrol 0 lanır
                        if(cakışansayısı==8)        
                            kontrol = false;

                    }

                    if (!kontrol) //kontrol = false durumu için i=0 yaparak çözüme baştan başla
                    {
                        i = 0;
                        yerD_randomR_işlemS[No, 1] += 1;
                    }
                    
                     
                    satrançtahtası[i] = rnd;
                   
                    while ( Cakısma_kontrol(satrançtahtası,i)==1) //çakılmayan değerlerin varlığı durumunda o değeri arayan döngü
                    {
                        yerD_randomR_işlemS[No, 0] += 1;
                        rnd = random.Next(8);
                        satrançtahtası[i] = rnd;
 
                    }
                   
                }
                
                satrançtahtası[i] = rnd;
            }
 
        }
        public static int Cakısma_kontrol(int[] satrançtahtası, int a)  //çakışma kontol fonksiyonu
        {

            for (int i = a - 1; i >= 0; i--)
            {
                if (satrançtahtası[a] == satrançtahtası[i])         //aynı satıda çakışma
                    return 1;
                else if (satrançtahtası[a] + a == satrançtahtası[i] + i)    //x=y ekseninde çakışma
                    return 1;
                else if (satrançtahtası[a] - a == satrançtahtası[i] - i)    // x=-y ekseninde çakışma 
                    return 1;

            }
             return 0;//çakışma yok
        }

    }
}
