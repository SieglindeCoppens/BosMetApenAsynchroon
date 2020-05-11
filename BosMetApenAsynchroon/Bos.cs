using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class Bos
    {
        public int BosId { get; set; }
        public int Xmax { get; set; }
        public int Ymax { get; set; }
        public List<Boom> Bomen {get ; set;}
        public List<Aap> Apen { get; set; }
        public Bos(int xmax, int ymax, int aantalBomen)
        {
            if(aantalBomen > (xmax - 2) * (ymax - 2))
            {
                throw new ArgumentException($"Too many trees!!");
            }
            Xmax = xmax;
            Ymax = ymax;
            BosId = IDGenerator.GenerateBosId();

            Random random = new Random();
            Bomen = new List<Boom>(aantalBomen);

            int bomenTeller = 0;
            while(bomenTeller < aantalBomen)
            {
                int x = random.Next(1, Xmax);
                int y = random.Next(1, Ymax);
                //Is het hier logisch om nutteloze objecten aan te maken om te kunnen vergelijken? Ik kan ook een dictionary aanmaken die voor elke x de gecombineerde y's bijhoudt!! 
                Boom nieuweBoom = new Boom(x, y);

                //Er mogen geen 2 bomen op dezelfde plaats staan. Contains werkt met equals en die is overschreven. 
                if (!Bomen.Contains(nieuweBoom))
                {
                    Bomen.Add(nieuweBoom);
                    bomenTeller++;
                }
            }
        }

        public void VoegApenToe(List<Aap> apen)
        {
            Apen = apen;
            //apen op startbomen zetten 
            Random random = new Random();
            List<Boom> startBomen = new List<Boom>();
            foreach (Aap aap in apen)
            {
                int index = random.Next(Bomen.Count);
                while(startBomen.Contains(Bomen[index]))
                {
                    index = random.Next(Bomen.Count);
                }
                startBomen.Add(Bomen[index]);
                aap.bezochteBomen.Add(Bomen[index]);
            }
        }

        public async Task StartOntsnappingAap(Aap aap)
        {
            Console.WriteLine($"{aap.Naam} start ontsnapping");
            bool ontsnapt = false;
            Boom huidigeBoom = aap.bezochteBomen[0];
            Boom dichtsteBoom = null;
            double minimum = Xmax * Ymax;
            while (!ontsnapt)
            {
                //zoeken naar dichtsbijzijnde boom
                //Alle bomen overlopen, minimum vervangen door nieuw minimum. 
                for(int teller = 0; teller < Bomen.Count; teller++)
                {
                    if (!aap.bezochteBomen.Contains(Bomen[teller]))
                    {
                        double afstandTotBoom = Math.Sqrt(Math.Pow(huidigeBoom.X - Bomen[teller].X, 2) + Math.Pow(huidigeBoom.Y - Bomen[teller].Y, 2));
                        if (afstandTotBoom < minimum)
                        {
                            minimum = afstandTotBoom;
                            dichtsteBoom = Bomen[teller];
                        }
                        //Als de boom niet dichter staat dan het minimum wordt hij geskipped

                    }
                    //Als de aap wel al op deze boom is geweest wordt deze boom geskipped! 
                }

                double afstandTotRand = (new List<double>() { Ymax - huidigeBoom.Y, Xmax - huidigeBoom.X, huidigeBoom.Y - 0, huidigeBoom.X - 0 }).Min();
                if (afstandTotRand < minimum)
                {
                    ontsnapt = true;
                }
                else
                {
                    huidigeBoom = dichtsteBoom;
                    aap.bezochteBomen.Add(huidigeBoom);
                    minimum = Xmax * Ymax;
                }
            }
            Console.WriteLine($"{aap.Naam} is ontsnapt");
        }
    }
}
