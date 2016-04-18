using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slimsim
{
    class Parameters
    {
         

        //system
        public const double MAXLIFETIME = 200000;
        
        
        //clock
        public const double cycleTime = 10; //in seconds

        public const int MAX = 2000;
        


        //auction algorithm coeficients

        public const double alpha = 1;
        public const double betha = 0.4;
       
        // sensor nodes
        
        public const double sensorNodeEnergyConsumptionRate = 0.05; //10 mw after test change it to 0.01
        public const double sensorNodeEnergyCapacity = 1000;// 1000J
        public const bool isSensorNodesSimilar = false;
        public const bool isSensorNodesDynamic = false;
        // rovers
        public const double roverChargingEff = 0.3; //60 mw after test turn it to 0.06, 300 w was 0.3
        public const double roverEnergyConsumptionMove = 0.03; //30 mw
        public const bool doesRoverHasUnlimitedEnergy = true; 
        public const double roverEnergyConsumptionCharging = 3; //3 w
        public const double roverEnergyCapacity = 2700000; //270,000J
        public const double roverSpeed = 0.1; // 0.1 m/s

        // environment       
        public const int environmentLength = 250; // 10000cm
        public const int environmentWidth = 175; // 10000cm 
      

        //tools
        public static double distance(Node a, Node b)
        {
            double l = Math.Pow(a.position.X - b.position.X, 2);
            double w = Math.Pow(a.position.Y - b.position.Y, 2);
            return Math.Sqrt(l + w);
        }




        internal static double distance(System.Drawing.PointF a, System.Drawing.PointF b)
        {
            double l = Math.Pow(a.X - b.X, 2);
            double w = Math.Pow(a.Y - b.Y, 2);
            return Math.Sqrt(l + w);
        }
    }
}
 