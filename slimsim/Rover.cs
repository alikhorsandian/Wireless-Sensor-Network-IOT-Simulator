using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace slimsim
{
    [Serializable]
   public  class Rover :Node
    {
        public double speed;
        public double energyConsumptionMove;
        public double energyConsumptionCharge;
        public double chargingEfficiency;
        public double travelledDistance = 0;

        public Rover()
        {
            capacity = Parameters.roverEnergyCapacity;
            this.energy = Parameters.roverEnergyCapacity;
            this.unlimitedEnergy = Parameters.doesRoverHasUnlimitedEnergy;
        }

       // for the inductive target selection
        public int counter = 0;
        public int number_of_sensorNodes_toCharge = 0;
        public void nextTarget()
        {
            counter++;
        }
        public int currentTarget()
        {

            return (counter % number_of_sensorNodes_toCharge) +1;
        }
        public Rover clone()
        {
            Rover result = new Rover();

            result.capacity=this.capacity;
            result.chargingEfficiency = this.chargingEfficiency;
            result.energy = this.energy;
            result.energyConsumptionCharge = this.energyConsumptionCharge;
            result.energyConsumptionMove = this.energyConsumptionMove;
            result.ID = this.ID;
            result.position = this.position;
            result.speed = this.speed;
            return result;
           
        }
    }
}
