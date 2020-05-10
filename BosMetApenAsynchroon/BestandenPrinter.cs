using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class BestandenPrinter
    {
        public async Task printLogBestand(Bos bos)
        {
            int maxAantalStappen = bos.Apen[0].bezochteBomen.Count;
            for(int x = 1; x < bos.Apen.Count; x++)
            {
                if(bos.Apen[x].bezochteBomen.Count > maxAantalStappen)
                {
                    maxAantalStappen = bos.Apen[x].bezochteBomen.Count;
                }
            }

            using StreamWriter writer = File.CreateText(@"C:\Users\Sieglinde\OneDrive\Documenten\Programmeren\semester2\programmeren 4\apenbos\logbestand" + $"{bos.BosId}");

            var gesorteerdeApen = bos.Apen.OrderBy(x => x.Naam);
            for(int y = 0; y < maxAantalStappen; y++)
            {
                foreach(Aap aap in gesorteerdeApen)
                {
                    if(aap.bezochteBomen.Count > y)
                    {
                        writer.WriteLine($"{aap.Naam} is in tree {aap.bezochteBomen[y].Id} at ({aap.bezochteBomen[y].X},{aap.bezochteBomen[y].Y})");
                    }             
                }
            }
        }
    }
}
