using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace slimsim
{
    class Memory
    {
        Sensor[] lastSensors= new Sensor[Parameters.MAX];
        Rover[] lastRovers= new Rover[Parameters.MAX];

        int last_SN;
        int last_RN;
        internal void save(Sensor[] sensorNodes, Rover[] rovers, int sensorNumber, int roverNumber)
        {
            for(int i=0;i<roverNumber;i++)
                 lastRovers[i]=rovers[i].clone();
            for (int j = 0; j < sensorNumber; j++)                               
                 lastSensors[j]=sensorNodes[j].clone();

            last_RN = roverNumber;
            last_SN = sensorNumber;
                
        }
        internal void saveOnFile(Sensor[] sensorNodes, Rover[] rovers, int sensorNumber, int roverNumber)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(
                    @"C:\Users\ak409\Desktop\rover.xml");

            System.Xml.Serialization.XmlSerializer writer =
                   new System.Xml.Serialization.XmlSerializer(typeof(Rover));

            for (int i = 0; i < roverNumber; i++)
            {

                writer.Serialize(file, rovers[i]);

            }

            file.Close();

            System.IO.StreamWriter file2 = new System.IO.StreamWriter(
                   @"C:\Users\ak409\Desktop\sensor.xml");

            System.Xml.Serialization.XmlSerializer writer2 =
                   new System.Xml.Serialization.XmlSerializer(typeof(Sensor));

            for (int j = 0; j < sensorNumber; j++)
            {

                writer2.Serialize(file2, sensorNodes[j]);

            }

            file2.Close();



        }

        internal void load(ref Sensor[] sensorNodes, ref Rover[] rovers)
        {
            for (int i = 0; i < last_RN; i++)
                rovers[i] = lastRovers[i];
            for (int j = 0; j < last_SN; j++)
                sensorNodes[j] = lastSensors[j];
        }

        internal void load(ref Sensor[] sensorNodes, ref Rover[] rovers, ref int sensorNumber, ref int roverNumber)
        {
            for (int i = 0; i < last_RN; i++)
                rovers[i] = lastRovers[i];

            for (int j = 0; j < last_SN; j++)
                sensorNodes[j] = lastSensors[j];

            sensorNumber = last_SN;
            roverNumber = last_RN;
        }
    }
}
