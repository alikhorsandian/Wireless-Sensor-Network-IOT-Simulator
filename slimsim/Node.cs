using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace slimsim
{
    [Serializable]
    public class Node :Object
    {
        public int ID=0;
        static int counter = 0;
        public PointF position;
        private double Energy;
        public double capacity;
        public bool unlimitedEnergy=false; // it is for rovers when we are assuming they have unlimited amount of energy. 

        public Node()
        {
            ID = counter++;
        }
        public double energy 
        {
            get
            {
                if (!unlimitedEnergy)
                    return Energy;
                else
                    return capacity;
            }
            set
            {
                if (value < capacity)
                    Energy = value;
                else
                    Energy = capacity; 
            }
        }
    }
}
