using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafovi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GrafTest graf = new GrafTest();
            Console.ReadLine();
        }
    }

    public class Graf
    {
        public Dictionary<int, Cvor> SviCvorovi = new Dictionary<int, Cvor>();

        public void Dajkstra(int pocetni)
        {
            int ukupanBrojCvorova = SviCvorovi.Count;

            string[] put = new string[100];
            int[] tezina = new int[100];

            Array.Fill(tezina, int.MaxValue);

            int[] osl = new int[100];

            tezina[pocetni] = 0;
            put[pocetni] = pocetni.ToString();

            for (int i = 0; i < ukupanBrojCvorova; i++)
            {
                int najmanji = -1;
                int minVrednost = int.MaxValue;

                foreach (var k in SviCvorovi)
                {
                    int id = k.Key;
                    if (osl[id] == 0 && tezina[id] < minVrednost)
                    {
                        minVrednost = tezina[id];
                        najmanji = id;
                    }
                }

                if (najmanji == -1) break;

                osl[najmanji] = 1;

                Cvor trenutni = SviCvorovi[najmanji];
                foreach (KeyValuePair<int, int> par in trenutni.susedi)
                {
                    int key = par.Key;
                    int tezina_grane = par.Value;

                    if (tezina[najmanji] != int.MaxValue && tezina[najmanji] + tezina_grane < tezina[key])
                    {
                        tezina[key] = tezina[najmanji] + tezina_grane;
                        put[key] = put[najmanji] + key;
                    }
                }
            }

            Console.WriteLine($"\nRezultati od cvora {pocetni}:");
            foreach (var cvor in SviCvorovi)
            {
                if (tezina[cvor.Key] == int.MaxValue)
                {
                    Console.WriteLine($"Do čvora {cvor.Key}: Ne postoji put");
                }
                else
                {
                    Console.WriteLine($"Do čvora {cvor.Key}: Težina = {tezina[cvor.Key]}, Put = {put[cvor.Key]}");
                }
            }
        }
    }

    public class Cvor
    {
        public int id = 0;
        public Dictionary<int, int> susedi = new Dictionary<int, int>();

        public Cvor(int id)
        {
            this.id = id;
            this.susedi = new Dictionary<int, int>();
        }

        public void grana(int id_prvog, int id_drugog, int tezina)
        {
            susedi[id_drugog] = tezina;
        }
    }

    public class GrafTest
    {
        public GrafTest()
        {
            Graf graf = new Graf();
            Console.Write("Unesite broj cvorova: ");
            int brCvorova = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < brCvorova; i++)
            {
                Cvor cvor = new Cvor(i);
                graf.SviCvorovi.Add(i, cvor);

                Console.WriteLine($"Unos grana za cvor {i}:");
                Console.Write("Koliko grana ima ovaj cvor? ");
                int brGrana = Convert.ToInt32(Console.ReadLine());

                for (int j = 0; j < brGrana; j++)
                {
                    Console.WriteLine("Grana " + (j + 1));
                    Console.WriteLine("Od cvora: " + i);
                    Console.Write("Ka cvoru: ");
                    int ka = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Tezina: ");
                    int tezina = Convert.ToInt32(Console.ReadLine());
                    cvor.grana(i, ka, tezina);
                }
            }
            Console.WriteLine("\nUnesite pocetni cvor: ");
            int x = Convert.ToInt32(Console.ReadLine());
            graf.Dajkstra(x);
        }
    }
}