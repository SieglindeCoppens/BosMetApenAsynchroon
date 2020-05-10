using System;
using System.Collections.Generic;
using System.Text;

namespace BosMetApenAsynchroon
{
    class Boom
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Boom(int x, int y)
        {
            Id = IDGenerator.GenerateBoomId();
            X = x;
            Y = y;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                Boom boom = (Boom)obj;
                return (X == boom.X) && (Y == boom.Y);
            }
        }
        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

    }
}
