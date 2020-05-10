using System;
using System.Collections.Generic;
using System.Text;

namespace BosMetApenAsynchroon
{
    class IDGenerator
    {
        private static int boomId = 1;
        private static int bosId = 1;
        private static int aapId = 1;

        public static int GenerateBoomId()
        {
            return boomId++;
        }
        public static int GenerateBosId()
        {
            return bosId++;
        }
        public static int GenerateAapId()
        {
            return aapId++;
        }
    }
}
