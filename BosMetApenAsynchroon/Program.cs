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

            //Bos maken, als ik asynchroon werk duurt deze 25.63s
            //Duurt maar 11.31 seconden bij asynchroon! 
            //Bos bos = new Bos(100, 100,9000);

            Bos bos = new Bos(50, 50, 1500);

            //Apen maken 
            Aap Jos = new Aap("Jos");
            Aap Bart = new Aap("Bart");
            List<Aap> apen = new List<Aap>{ Jos, Bart };

            //Apen aan het bos toevoegen??
            bos.VoegApenToe(apen);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => bos.StartOntsnappingAap(Jos)));
            tasks.Add(Task.Run(() => bos.StartOntsnappingAap(Bart)));
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            Console.WriteLine($"Time elamsed : {stopwatch.Elapsed}");

            //Alles naar databank schrijven. Is het nadelig dat ik met de list werk? Kunnen connections asynchroon gemaakt worden? 
            //List<Bos> bossen = new List<Bos>() { bos };
            //DataBeheer db = new DataBeheer("Data Source=DESKTOP-HT91N8R\\SQLEXPRESS;Initial Catalog=db_Apenbos;Integrated Security=True");
            //List<Task> databankTasks = new List<Task>();
            //databankTasks.Add(db.VoegWoodRecordToe(bossen));
            //databankTasks.Add(db.VoegMonkeyRecordToe(bossen));
            //databankTasks.Add(db.voegLogsToe(bossen));
            //Task.WaitAll(databankTasks.ToArray());

            BitmapSchrijver bs = new BitmapSchrijver();
            bs.maakBitMap(bos, @"C:\Users\Sieglinde\OneDrive\Documenten\Programmeren\semester2\programmeren 4\Apenbos");

            BestandenPrinter bp = new BestandenPrinter();
            bp.printLogBestand(bos);
            
        }
    }
}
