using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class DoeAsynchroon
    {
        public async Task LaatApenOntsnappen(Bos bos)
        {
                List<Task> tasks = new List<Task>();
               
                foreach (Aap aap in bos.Apen)
                {
                    tasks.Add(Task.Run(() => bos.StartOntsnappingAap(aap)));
                }
                Task.WaitAll(tasks.ToArray());
        }
        public async Task DatabankBitmapsBestandenDoen(List<Bos> bossen)
        {
            BitmapSchrijver bs = new BitmapSchrijver();
            BestandenPrinter bp = new BestandenPrinter();
            DataBeheer db = new DataBeheer("Data Source=DESKTOP-HT91N8R\\SQLEXPRESS;Initial Catalog=db_Apenbos;Integrated Security=True");
            List<Task> taken = new List<Task>();
            foreach (Bos bos in bossen)
            {
                taken.Add(Task.Run(() => db.VoegWoodRecordToe(bos)));
                taken.Add(Task.Run(() => bs.maakBitMap(bos, @"C:\Users\Sieglinde\OneDrive\Documenten\Programmeren\semester2\programmeren 4\Apenbos")));
                taken.Add(Task.Run(() => bp.printLogBestand(bos)));
                taken.Add(Task.Run(() => db.VoegMonkeyRecordToe(bos)));
                taken.Add(Task.Run(() => db.voegLogsToe(bos)));

            }
            Task.WaitAll(taken.ToArray());
        }
    }
}
