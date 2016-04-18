using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slimsim
{
    [Serializable]
   public  class GeneralRandom
    {
        Random rnd = new Random();
        public int Next(int min, int max)
        {
            return rnd.Next(min, max);
        }

    }
}
