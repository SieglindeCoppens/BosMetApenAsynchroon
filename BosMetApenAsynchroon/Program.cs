using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Bos bos1 = new Bos(50, 50, 1000);
            Bos bos2 = new Bos(50, 50, 500);
            List<Bos> bossen = new List<Bos>() { bos1, bos2 };

            //Apen maken 
            Aap Tijs = new Aap("Tijs");
            Aap Bart = new Aap("Bart");
            Aap Bram = new Aap("Bram");
            Aap Gust = new Aap("Gust");
            Aap Charel = new Aap("Charel");
            List<Aap> apen1 = new List<Aap>{ Tijs, Bart, Bram };
            List<Aap> apen2 = new List<Aap> { Gust, Charel };

            //Apen aan het bos toevoegen
            bos1.VoegApenToe(apen1);
            bos2.VoegApenToe(apen2);


            //Alle apen ontsnappen asynchroon
            DoeAsynchroon da = new DoeAsynchroon();
            List<Task> ontsnappingTasks = new List<Task>();
            foreach(Bos bos in bossen)
            {
                ontsnappingTasks.Add(da.LaatApenOntsnappen(bos));
            }
            Task.WaitAll(ontsnappingTasks.ToArray());

            //Alle data die je nodig hebt voor de rest is geschreven, de rest kan allemaal asynchroon. 
            await da.DatabankBitmapsBestandenDoen(bossen);
            stopwatch.Stop();
            Console.WriteLine($"Time elamsed : {stopwatch.Elapsed}");
        }
    }
}
