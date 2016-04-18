using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slimsim
{
    [Serializable]
   public  class Sensor :Node
    {
        //public double consumptionRate;
        public bool isVisited;
         double energyConsumptionRate;

         GeneralRandom rnd;

         public double energyConsumption()
         {
             if (Parameters.isSensorNodesDynamic) return energyConsumptionRateDynamic;
             else return energyConsumptionRate;
         }
        public Sensor(GeneralRandom r)
        {
            rnd = r; 
           

            isVisited = false;
            if (Parameters.isSensorNodesSimilar)
            {
                energyConsumptionRate = Parameters.sensorNodeEnergyConsumptionRate;
                
            }
            else
            {
                double varient = 10; //percents
                //Random Econ = new Random(ID + int.Parse(DateTime.Now.ToString("ss")));
                double centre1000=(Parameters.sensorNodeEnergyConsumptionRate*1000);
                int min = (int)(centre1000 - (varient / 100) * centre1000);
                int max = (int)(centre1000 + (varient / 100) * centre1000);
                energyConsumptionRate = (double)rnd.Next(min, max) / 1000.0;
                
            }
            energy = Parameters.sensorNodeEnergyCapacity; // charging to the full unless the value is over writed. 

            energyConsumptionRateDynamic = energyConsumptionRate;
        }
        public Sensor clone()
        {
            Sensor result = new Sensor(rnd);
            result.capacity= this.capacity;
          //  result.consumptionRate = this.consumptionRate;
            result.energy = this.energy;
            result.energyConsumptionRate = this.energyConsumptionRate;
            result.ID = this.ID;
            result.isVisited = this.isVisited;
            result.position = this.position;
            return result; 
        }

        int Dynamiccounter = 0;
        double DynamicDuration = 1000;
        double Dynamicvarrient = 50;// in percentage


        double energyConsumptionRateDynamic;
        
      public void UpdateEnergy(double cycletime)
        {

            if (Parameters.isSensorNodesDynamic)
            {
                if (Dynamiccounter++ % DynamicDuration == 0)
                {
                    double min = energyConsumptionRate - (Dynamicvarrient / 100) * energyConsumptionRate;
                    double max = energyConsumptionRate + (Dynamicvarrient / 100) * energyConsumptionRate;
                    min *= 1000;
                    max *= 1000;
                    if (min < 0) min = 0;
                    energyConsumptionRateDynamic = rnd.Next((int)min, (int)max);
                    energyConsumptionRateDynamic /= 1000;

                }

                if (energyConsumptionRate < 0) throw new Exception("energy consumption rate is negative ???");

                this.energy -= energyConsumptionRateDynamic * cycletime;
            }
            else
                this.energy -= energyConsumptionRate * cycletime;
        }
    }
}
