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

            Bos bos = new Bos(50, 50, 1000);
            Bos bos2 = new Bos(50, 50, 1500);

            //Apen maken 
            Aap Tijs = new Aap("Tijs");
            Aap Bart = new Aap("Bart");
            Aap Bram = new Aap("Bram");
            Aap Gust = new Aap("Gust");
            Aap Charel = new Aap("Charel");
            List<Aap> apen1 = new List<Aap>{ Tijs, Bart, Bram };
            List<Aap> apen2 = new List<Aap> { Gust, Charel };

            //Apen aan het bos toevoegen
            bos.VoegApenToe(apen1);
            bos2.VoegApenToe(apen2);


            //Er worden gebruik gemaakt van meerdere threads om de apen te laten verspringen. 
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => bos.StartOntsnappingAap(Tijs)));
            tasks.Add(Task.Run(() => bos.StartOntsnappingAap(Bart)));
            tasks.Add(Task.Run(() => bos.StartOntsnappingAap(Bram)));
            tasks.Add(Task.Run(() => bos2.StartOntsnappingAap(Gust)));
            tasks.Add(Task.Run(() => bos2.StartOntsnappingAap(Charel)));
            Task.WaitAll(tasks.ToArray());

            BitmapSchrijver bs = new BitmapSchrijver();

            BestandenPrinter bp = new BestandenPrinter();

            //Alles naar databank schrijven. Is het nadelig dat ik met de list werk? Kunnen connections asynchroon gemaakt worden? 
            List<Bos> bossen = new List<Bos>() { bos, bos2};
            DataBeheer db = new DataBeheer("Data Source=DESKTOP-HT91N8R\\SQLEXPRESS;Initial Catalog=db_Apenbos;Integrated Security=True");
            List<Task> databankTasks = new List<Task>();
            databankTasks.Add(Task.Run(() => db.VoegWoodRecordToe(bossen)));
            databankTasks.Add(Task.Run(() => bs.maakBitMap(bossen, @"C:\Users\Sieglinde\OneDrive\Documenten\Programmeren\semester2\programmeren 4\Apenbos")));
            databankTasks.Add(Task.Run(() => bp.printLogBestand(bos)));
            databankTasks.Add(Task.Run(() => db.VoegMonkeyRecordToe(bossen)));
            databankTasks.Add(Task.Run(() => db.voegLogsToe(bossen)));
            Task.WaitAll(databankTasks.ToArray());
            stopwatch.Stop();
            Console.WriteLine($"Time elamsed : {stopwatch.Elapsed}");
        }
    }
}
