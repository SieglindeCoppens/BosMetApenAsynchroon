using System;
using System.Collections.Generic;
using System.Text;

namespace BosMetApenAsynchroon
{
    class Aap
    {
        public Aap(string naam)
        {
            Id = IDGenerator.GenerateAapId();
            Naam = naam;
            bezochteBomen = new List<Boom>();
        }
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Boom> bezochteBomen { get; set; }
    }
}
