using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace slimsim
{
    public class Simulator
    {
        /// <logging>
        /// //////////

        Console con = new Console();

        string logCloseLifetime = "";
        string logCloseAverageDistance = "";

        string logRNDLifetime = "";
        string logRNDAverageDistance = "";

        string logEnergyLifetime = "";
        string logEnergyDistance = "";
        /// </logging>
        

        private const int MAX = 2000; 
        Sensor[] sensorNodes= new Sensor[MAX];
        Rover[] rovers= new Rover[MAX];
        Graphics formGraph = null;
         //quantities 
        public  int sensorNumber=0;
        public  int roverNumber=0;
        TextBox txt;
        TextBox txt3;
        TextBox txt4;
        
        int cycle=1;
        Thread t1;
        Thread t;
        Thread t2;
        Thread t3;
        public Thread tAll;

        Form2 mn;
        public void setForm2(Form2 fm)
        {
            mn = fm;
        }
        public void drawOnForm2()
        {
            mn.SensorNumber = this.sensorNumber;
            for(int j=0;j<sensorNumber;j++)
            {
                mn.sensorNodes[j] = this.sensorNodes[j].clone();
            }
            
            mn.draw();
        }
        public void setTextbox3(TextBox t)
        {
            txt3 = t;
        }
        public void setTextbox4(TextBox t)
        {
            txt4 = t;
        }
        public Simulator()
        {
            t = new Thread(run);
            t1= new Thread(run1);
            t2 = new Thread(run2);
            t3 = new Thread(preRun3);
        }
        public void test3()
        {
            t3.Start();
        }
        public void stopTest3()
        {
            if (t3.IsAlive)
            {
                t3.Interrupt();
                t3.Abort();
            }
        }
        public void test2()
        {
            t2.Start();
        }
        public void stopTest2()
        {
            if (t2.IsAlive)
            {
                t2.Interrupt();
                t2.Abort();
            }
        }
        public void test()
        {
            t.Start();
        }
        public void stopTest()
        {
            if (t.IsAlive)
            {
                t.Interrupt();
                t.Abort();

            }
        }
        public void test1()
        {
            t1.Start();
        }
        public void stopTest1()
        {
            if (t1.IsAlive)
            {
                t1.Interrupt();
                t1.Abort();
              
            
               
            }
            log += "]";
            logmean += "]";
            m.writeInFile(log,"logweak");
            m.writeInFile(logmean, "logmean");

        }

        public void printAverageWhenitsPerpetual()
        {
            con.print_AverageDistanceAuction(getAverageDistancePerRover().ToString());
            con.print_intervally(getAverageDistanceForAll().ToString());
        }
        public void stopnSave()
        {
           

            if (tAll!=null && tAll.IsAlive)
            {
                tAll.Interrupt();
                tAll.Abort();



            }
             
            m.writeInFile(logInductive, "logInductive");
            m.writeInFile(logAuction, "logAuction");
            m.writeInFile(logMulti, "logMulti");

           
            //for (int cc = 0; chartAuctionLog[cc] > 0; cc++)
            //    ch1.Series["energy"].Points.AddY(chartAuctionLog[cc]);

            //ch1.Series["energy"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //ch1.Series["energy"].Color = Color.Red;

        }
        double[] chartAuctionLog= new double[1000000];
        public void addOneRover()
        {
            // load from the previous rnd.txt
                loadFromFile("rnd.txt");
            //create the new rover
                Random rnd= new Random();
                Rover r = new Rover();
                r.capacity=Parameters.roverEnergyCapacity;
                r.chargingEfficiency=Parameters.roverChargingEff;
                r.energy = Parameters.roverEnergyCapacity;
                r.energyConsumptionCharge=Parameters.roverEnergyConsumptionCharging;
                r.energyConsumptionMove=Parameters.roverEnergyConsumptionMove;
                //r.ID=ID++; not using id, it will be fixed later on. 
                r.speed=Parameters.roverSpeed; 
                
                int width = Parameters.environmentWidth;
                int length = Parameters.environmentLength;
                r.position = new PointF(rnd.Next(0, length), rnd.Next(0, width));

                rovers[roverNumber] = r;
                roverNumber++;
            // alter the file 
                saveToFile("rnd.txt");

        }
        
        public void giveRoversUnlimitedEnergy()
        {
            for (int i = 0; i < roverNumber; i++)
                rovers[i].unlimitedEnergy = true;
        }
        public double alpha = 0.5;
        public double betha = 0.5;

        public void preRun3()
        {
            //alpha and beta in minimizing the cost and maximizing lifeitme. 

            sensorNumber = 50;
            roverNumber = 6;
            init2(Parameters.roverEnergyCapacity); // initial
            postInit();
           


            for (double w = 0; w <= 100; w += 5)
            {
                txt2.Text += w.ToString() + " ";
                txt2.Refresh();
                alpha = 100-w;
                betha = w;
                mem.save(sensorNodes, rovers, sensorNumber, roverNumber);
               // giveRoversUnlimitedEnergy();
                run3();
                mem.load(ref sensorNodes, ref rovers, ref sensorNumber, ref roverNumber);
            }
        }
        public void run3()
        {
            postInit();

            //setting the monitor
            
            m.speedg = 50;
            m.setGraphics(formGraph);
            m.setNumbers(roverNumber, sensorNumber);
             
            

            double sum = 0;
            bool round = true;
            int stat = 0;
            while (round)
            {
                sink3();
                sum += averageDistance();
                stat = start2();
                if (stat == 0)
                    round = false;
                else if (stat == 1)
                    round = false;
                else if (stat == 2)
                    round = true;
                //log
            }

            txt.Text += lifeTime.ToString()+" ";
            txt.Refresh();

            txt3.Text += sum.ToString() + " ";
            txt3.Refresh();
            
        }
        public void run2()
        {
             
             

           //lifetime vs rover energy

            sensorNumber = 50;
            roverNumber = 14;
            init2(1); // initial
            postInit();

           
            m.speedg = 50;
            m.setGraphics(formGraph);
             
            m.setNumbers(roverNumber, sensorNumber);
           // m.draw(sensorNodes, rovers);
            
            mem.save(sensorNodes, rovers, sensorNumber, roverNumber);

            // loop start
            for (double energy = 10; energy < 129611; energy += Parameters.roverEnergyCapacity / 50)
            {
                
                setRoverEnergy(energy);
                mem.save(sensorNodes, rovers, sensorNumber, roverNumber);

                 
                double sum = 0;
                bool round = true;
                int stat = 0;
                while (round)
                {
                    sink();
                    sum += averageDistance();
                    stat = start2();
                    if (stat == 0)
                        round = false;
                    else if (stat == 1)
                        round = false;
                    else if (stat == 2)
                        round = true;
                    //log
                }
                txt.Text += energy.ToString()+ " ";
                txt.Refresh();
                txt2.Text += lifeTime.ToString()+" ";
                txt2.Refresh();
                mem.load(ref sensorNodes, ref rovers, ref sensorNumber, ref roverNumber);
                m.setNumbers(roverNumber, sensorNumber);
                postInit();
           }          
        }
        public void RI()
        {
            tAll = new Thread(()=>runInductive());
            tAll.Start();
        }
        public void RM()
        {
            tAll = new Thread(()=>runMulti());
            tAll.Start();
        }
        public void RA()
        {
            tAll = new Thread(()=>runAuction());
            tAll.Start();
        }
        public void RunInductive(double Max_lifetime=Parameters.MAXLIFETIME)
        {
            //init
            sensorNumber = 50;
            roverNumber = 12;
            init();
            
            
            inductive();
            //set monitor
           // m.disable = false;
            m.speedg = 5;
            m.setGraphics(formGraph);
            m.setNumbers(roverNumber, sensorNumber);
            //start simulation
            startInductive(false,Max_lifetime);
        }

        public void turnOffMonitor()
        {
            if (m != null)
                m.turnOffMonitor();
        }
        public void turnOnMonitor()
        {
            if (m != null)
                m.turnOnMonitor();
        }
        public void RunMulti(double maxlifetime=Parameters.MAXLIFETIME)
        {
            sensorNumber = 50;
            roverNumber = 12;
            init();


            MultiVehicleAssignment();
            //set monitor
            
            m.speedg = 5;
            m.setGraphics(formGraph);
            m.setNumbers(roverNumber, sensorNumber);
            //start simulation
            startInductive(true,maxlifetime);
        }
        public void MultiVehicleAssignment()
        {
            // init
            for (int i = 0; i < roverNumber; i++)
                for (int j = 0; j < sensorNumber; j++)
                    x1[i, j] = 0;
            // find the closeset rover for every sensor node and assinge them to the rover

            for (int j = 0; j < sensorNumber; j++)
            {
                int winnerRover = findClosestUnassignedRover(j);
                if (x1[winnerRover, j] == 0)
                    x1[winnerRover, j] = 1;
                else
                    throw new Exception("something wrong with the findclosestunassignedrover function");
            }


            // prioritize the list of sensor nodes for each rover

            for (int i = 0; i < roverNumber; i++)
            {
                // if the rover is assigned 
                int nofstc = numberOfSensorNodesToCharge(i);
                rovers[i].number_of_sensorNodes_toCharge = nofstc;
                double maxValue = -1;
                for (int c = 0; c < nofstc; c++)
                {
                    double minValue = 1000000000;
                    int minSensor = 100;
                    for (int j = 0; j < sensorNumber; j++)
                        if (x1[i, j] > 0)
                        {
                            if (minValue > inductiveValue(i, j))
                                if (inductiveValue(i, j) > maxValue)
                                {
                                    minValue = inductiveValue(i, j);
                                    minSensor = j;
                                }
                        }
                    x1[i, minSensor] = c + 1;
                    maxValue = minValue;
                }
            }
            // prioritize the list through the function. 
            

        }
        int[,] x1 = new int[MAX, MAX];
        public void inductive()
        {
            // init
            for(int i=0;i<roverNumber;i++)
                for(int j=0;j<sensorNumber;j++)
                    x1[i,j]=0;
             // find the closeset rover for every sensor node and assinge them to the rover

            for (int j = 0; j < sensorNumber; j++)
            {
                int winnerRover = findClosestUnassignedRover(j);
                if (x1[winnerRover, j] == 0)
                    x1[winnerRover, j] = 1;
                else
                    throw new Exception("something wrong with the findclosestunassignedrover function");
            }


            // prioritize the list of sensor nodes for each rover
           
            for (int i = 0; i < roverNumber; i++)
            {
                // if the rover is assigned 
                int nofstc=numberOfSensorNodesToCharge(i);
                rovers[i].number_of_sensorNodes_toCharge = nofstc;
                double maxValue = -1;
                for(int c=0;c<nofstc;c++)
                {
                    double minValue = 1000000000;
                    int minSensor = 100;
                    for(int j=0;j<sensorNumber;j++)
                        if (x1[i, j] > 0)
                        {
                            if (minValue > inductiveValue(i, j))
                                if (inductiveValue(i, j) > maxValue)
                                {
                                    minValue = inductiveValue(i, j);
                                    minSensor = j;
                                }
                        }
                    x1[i, minSensor] = c + 1;
                    maxValue = minValue;
                }
            }
            // prioritize the list through the function. 
            
        }

       
        public void startInductive(bool isiTMulti,double maxlifetime=Parameters.MAXLIFETIME)
        {
            
            //send updates to nodes
            //get the response from nodes
            //simulate the environment
            lifeTime = 0;


            // in a fast way...

            // move every rover toward their sensor nodes. 
             cycle = 1;

            for (cycle = 1; !isDead() && !isOverCharged() && cycle<=(maxlifetime/Parameters.cycleTime); cycle++)
            {
               //cycle
                if (isiTMulti)
                {
                    logMulti += meanValueOfMin().ToString() + " ";
                    if (cycle % 100 == 0 && ch1!=null)
                        ch1.Series["multi"].Points.AddXY(cycle * Parameters.cycleTime, weakestSensorEnergy());
                }
                else
                {
                    logInductive += meanValueOfMin().ToString() + " ";
                    if (cycle % 100 == 0 && ch1 != null)
                        ch1.Series["inductive"].Points.AddXY(cycle * Parameters.cycleTime, weakestSensorEnergy());
                }
               //

                updateSensorEnergy(Parameters.cycleTime);
                for (int i = 0; i < roverNumber; i++) // simulate all rovers behavours 
                {
                    if (rovers[i].number_of_sensorNodes_toCharge > 0)
                    {
                        // set the target
                        int target = -1;
                        int tgIndex = rovers[i].currentTarget();
                        for (int j = 0; j < sensorNumber; j++)
                            if (x1[i, j] == tgIndex)
                                target = j;


                        //move i to its target

                        // if is arount then charge else move there
                        if (target == -1) throw new Exception("didn't find any target");
                        if (isItThere(i, target))
                        {
                            // if its not charge charge it, else change target
                            if (sensorNodes[target].energy>=Parameters.sensorNodeEnergyCapacity-10)
                            {
                                rovers[i].nextTarget();                                
                            }
                            else
                            {
                                charge(i, target, Parameters.cycleTime);                               
                            }

                        }
                        else
                        {
                            move(i, target, Parameters.cycleTime);                            
                        }
                        
                    }
                }
                //cycle Space
                m.draw(sensorNodes, rovers);
                // you must set setform2 function somewhere before calling this. 
                mn.setChart();
                drawOnForm2();
                //cycle 
            }
            if (isDead())
                lifeTime = cycle * Parameters.cycleTime;
            else
                lifeTime = 20002;
        }
        public Double currentTime()
        {
            return cycle * Parameters.cycleTime;
        }
        public bool isItThere(int rov, int sen)
        {
            return isAround(rovers[rov].position, sensorNodes[sen].position);
        }
        public bool chargeFully(int i, int target, double cycletime1) // charge until it gets full, return 0 if its full. return 1 if rover is charging it yet
        {
            //txt2.Text += i.ToString() + "to fully charge" + target.ToString() + "\r\n";
            //txt2.Refresh();
            if (isFullyCharged(target))
                return false;
            charge(i, target, cycletime1);
            return true;
        }

        public bool isFullyCharged(int j)
        {
            if (sensorNodes[j].energy >= Parameters.sensorNodeEnergyCapacity)
                return true;
            return false;
        }

        public int numberOfSensorNodesToCharge(int i)
        {
            int result = 0;
            for (int j = 0; j < sensorNumber; j++)
                if (x1[i, j] > 0)
                    result++;
            return result; 
        }
        public int findClosestUnassignedRover(int j)
        {
            double minDistance = double.MaxValue;
            int minRover=-1;
            for (int i = 0; i < roverNumber; i++)
                if (Parameters.distance(sensorNodes[j], rovers[i]) < minDistance)
                {
                    minDistance = Parameters.distance(sensorNodes[j], rovers[i]);
                    minRover = i;
                }
            return minRover; 
        }
        public double inductiveValue(int i, int j)
        {
            double d1= Parameters.distance(sensorNodes[j], rovers[i]);
            double sensorEnergy = sensorNodes[j].energy;

            return Math.Log(d1) * sensorEnergy;
        }
        public double multiValue(int rov, int sen)
        {
         
            double td = Parameters.distance(rovers[rov], sensorNodes[sen]) / Parameters.roverSpeed;
            double ls = sensorNodes[sen].energy / Parameters.sensorNodeEnergyConsumptionRate;
            return td +   ls;
        }
        public void setRoverEnergy(double energy)
        {
            for (int i = 0; i < roverNumber; i++)
            {
                rovers[i].energy = energy;
            }
        }
        Memory mem = new Memory();
        public void run()
        {

            //1)  set the numbers of nodes. 
       
           // txt.Text = "[";
           // string result = "";
            sensorNumber = 50;
            roverNumber =0;
            init();
           // m.disable = false;
            m.speedg = 100;
            //mem.saveOnFile(sensorNodes, rovers, sensorNumber, roverNumber); // not ready yet
            mem.save(sensorNodes,rovers,sensorNumber,roverNumber);
            for (int i = 0; i < 50; i++)
            {
                m.setGraphics(formGraph);
                m.setNumbers(roverNumber, sensorNumber);
                // m.draw(sensorNodes, rovers);
                sink();
                logCloseAverageDistance += averageDistance().ToString() + " ";
                start();

                logCloseLifetime += lifeTime.ToString() + " ";
                //start2();
                //  result = lifeTime.ToString()+" ";
                // txt.Text += result;
                // txt.Refresh();

                mem.load(ref sensorNodes, ref rovers);
                giveRoversUnlimitedEnergy();
                addOneRover();

                
                mem.save(sensorNodes, rovers, sensorNumber, roverNumber);
                //int ays = 123;
            }
            m.writeInFile(logCloseAverageDistance, "logEnergyADistance");
            m.writeInFile(logCloseLifetime, "logEnergyLifetime");
          //  txt.Text += "]";
        }
        public double averageDistance()
        {
            double sum=0;
            for (int i = 0; i < roverNumber; i++)
                for (int j = 0; j < sensorNumber; j++)
                    if (x[i, j])
                        sum += Parameters.distance(rovers[i], sensorNodes[j]);
            return sum / roverNumber;
        }
        string logdistance = "";
        public void runMulti(double Max_lifetime = Parameters.MAXLIFETIME)
        {
            loadFromFile("rnd.txt");

            //set monitor
            
            m.speedg = 5;
            m.setNumbers(roverNumber, sensorNumber);
            m.setGraphics(formGraph);
            
            //prioritization 
            multiprio();
            //start simulation
            startInductive(true,Max_lifetime);//true means multi
            //txt.Text += " "+ lifeTime.ToString();
            //txt.Refresh();
            con.print_Multi(lifeTime.ToString());
            con.print_intervally(getAverageDistanceForAll().ToString());
            con.print_AverageDistanceMulti(getAverageDistancePerRover().ToString());
        }

        private void multiprio()
        {
            // init
            for (int i = 0; i < roverNumber; i++)
                for (int j = 0; j < sensorNumber; j++)
                    x1[i, j] = 0;
            // find the closeset rover for every sensor node and assinge them to the rover

            for (int j = 0; j < sensorNumber; j++)
            {
                int winnerRover = findClosestUnassignedRover(j);
                if (x1[winnerRover, j] == 0)
                    x1[winnerRover, j] = 1;
                else
                    throw new Exception("something wrong with the findclosestunassignedrover function");
            }


            // prioritize the list of sensor nodes for each rover

            for (int i = 0; i < roverNumber; i++)
            {
                // if the rover is assigned 
                int nofstc = numberOfSensorNodesToCharge(i);
                rovers[i].number_of_sensorNodes_toCharge = nofstc;
                double maxValue = -1;
                for (int c = 0; c < nofstc; c++)
                {
                    double minValue = 1000000000;
                    int minSensor = 100;
                    for (int j = 0; j < sensorNumber; j++)
                        if (x1[i, j] > 0)
                        {
                            if (minValue > multiValue(i, j))
                                if (multiValue(i, j) > maxValue)
                                {
                                    minValue = multiValue(i, j);
                                    minSensor = j;
                                }
                        }
                    x1[i, minSensor] = c + 1;
                    maxValue = minValue;
                }
            }
            // prioritize the list through the function. 
        }

        public void runInductive(double maxlifetime=Parameters.MAXLIFETIME)
        {

            loadFromFile("rnd.txt");
            
            //set monitor
            
            m.speedg = 10;
            m.setNumbers(roverNumber, sensorNumber);
            m.setGraphics(formGraph);

            inductive();
           
            
            //start simulation
            startInductive(false,maxlifetime);
            //txt.Text += " "+lifeTime.ToString();
            //txt.Refresh();
            con.print_Inductive(lifeTime.ToString());
            con.print_intervally(getAverageDistanceForAll().ToString());
            con.print_AverageDistanceInductive(getAverageDistancePerRover().ToString());
        }
        public void runAuction(double lifetime_Limit=Parameters.MAXLIFETIME)
        {
            loadFromFile("rnd.txt");
            m.setGraphics(formGraph);

           
            m.speedg = 10;
            
            m.setNumbers(roverNumber, sensorNumber);
            m.setGraphics(formGraph);
            // inti the chart
            mn.setChart();

       
            int stat;
            bool round = true;
            //double sum = 0;
            while (round && cycle<=(lifetime_Limit/Parameters.cycleTime))
            {
                sink();
                //sum += averageDistance();
                stat = start1(lifetime_Limit);
                if (stat == 0)
                    round = false;
                else if (stat == 1)
                    round = false;
                else if (stat == 2)
                    round = true;
            }
             
            //txt.Text += " " + lifeTime.ToString();
            //txt.Refresh();
           con.print_Auction(lifeTime.ToString());
           con.print_intervally(getAverageDistanceForAll().ToString());
           con.print_AverageDistanceAuction(getAverageDistancePerRover().ToString());
           // logdistance += sum.ToString() + " ";


        }
        public void run1() // assignment will be restarted when the assigned sensor nodes are fully charged. 
        {
            sensorNumber = 50;
            roverNumber = 9;
            init();
            mem.save(sensorNodes, rovers, sensorNumber, roverNumber);

            // set up the monitor
            m.setGraphics(formGraph);
           
            
            m.speedg = 20;

            for (int i = 49; i < 50; i++)
            {

                //m.speedg =(m.speedg+5) ;
                m.setNumbers(roverNumber, sensorNumber);
                postInit();

                

                string result = "";
               

                int stat;
                bool round = true;
                double sum = 0;
                while (round)
                {
                    sink();
                    //sum += averageDistance();
                    stat = start1();
                    if (stat == 0)
                        round = false;
                    else if (stat == 1)
                        round = false;
                    else if (stat == 2)
                        round = true;
                }
                result = lifeTime.ToString() + " ";
                txt.Text += result;
                txt.Refresh();
                logdistance += sum.ToString() + " ";

                mem.load(ref sensorNodes, ref rovers); 
                addOneRover();
                giveRoversUnlimitedEnergy();
                mem.save(sensorNodes, rovers, sensorNumber, roverNumber);
            }
           

        }

        int meanCounter=0;
        double sum = 0; 
        public double meanValueOfMin()
        {
            sum += weakestSensorEnergy();
            meanCounter++;
            return sum / meanCounter;
        }

        public void  postInit()
        {
            lifeTime = 0;
            cycle = 1; 
        }
        string log = "[";
        string logmean = "[";
        public void resetLiftime()
        {
            lifeTime = 0;
            cycle = 1;
        }
        public int start1(double lifetime_Limit = Parameters.MAXLIFETIME)
        {
            //send updates to nodes
            //get the response from nodes
            //simulate the environment
         


            // in a fast way...
            
            // move every rover toward their sensor nodes. 
            bool isde = false; bool arecharged = false; bool isoverch = false; bool areallvis = false; 

            for (; !(isde = isDead()) && !(arecharged = areTheSensorsCharged()) && (isoverch = !isOverCharged()) && (areallvis=!areAllVisited()) && cycle<=(Parameters.MAXLIFETIME/Parameters.cycleTime); cycle++)
            //for (; !(isde = isDead()); cycle++)
            {
                //updateSensorEnergy(cycletime);
                updateSensorEnergy(Parameters.cycleTime);
                for (int i = 0; i < roverNumber; i++) // simulate all rovers behavours 
                {
                    int target = -1;

                    for (int j = 0; j < sensorNumber; j++)
                        if (x[i, j])
                            target = j;


                    //move i to its target
                    if (!move(i, target, Parameters.cycleTime))
                        charge(i, target, Parameters.cycleTime);                   
                
                }


                /////////////////////cycle space

                m.draw(sensorNodes, rovers); // must be executed in every cycle. 
                logAuction += meanValueOfMin().ToString() + " ";
                chartAuctionLog[cycle] = meanValueOfMin();
                //if (cycle % 50 == 0)
                //{
                //    txt2.Text += sensorNodes[1].energy.ToString() + " ";
                //    txt2.Refresh();
                //}
                drawOnForm2();
                if (cycle % 100 == 0)
                {
                    //ch1.Series["auction"].Points.AddY(weakestSensorEnergy());
                    if(ch1!=null)
                        ch1.Series["auction"].Points.AddXY(cycle * Parameters.cycleTime, weakestSensorEnergy());
                }
                //ch1.Series["energy"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                //ch1.Series["energy"].Color = Color.Red;
               // logmean += meanValueOfMin().ToString() + " ";
                //print4mat(weakestSensorEnergy().ToString());
                
            }

            
            if (isDead() || isOverCharged() || areAllVisited())
            {
                if (areAllVisited())
                {
                    lifeTime = 20001;
                    return 1;
                }
                else
                {
                    lifeTime = cycle * Parameters.cycleTime;
                    return 0;
                }
               
            }
            else if (areTheSensorsCharged())
            {
                // update isVisited variable for the already seen sensor nodes. 
                for (int i = 0; i < roverNumber; i++)
                    for (int j = 0; j < sensorNumber; j++)
                        if (x[i, j])
                            sensorNodes[j].isVisited = true;

                if (areAllVisited())
                {
                    lifeTime = cycle * Parameters.cycleTime;
                    return 0;
                }
                else
                    return 2; /// there is a deadlock over here. that must be checked later. 
            }
            return 3; // this never happens

        }
        public void removeRover(int rvr)
        {
            for (int i = rvr; i < roverNumber; i++)
                rovers[i] = rovers[i + 1];
            roverNumber--;
            m.setNumbers(roverNumber, sensorNumber);
        }
        public void removeRvrIfIsDead()
        {
            for (int i = 0; i < roverNumber; i++)
                if (rovers[i].energy <= 0)
                    removeRover(i);                          
        }
        public int start2() 
        {
            //send updates to nodes
            //get the response from nodes
            //simulate the environment

     

            // in a fast way...
            
            // move every rover toward their sensor nodes. 

            bool isde = false; bool arecharged = false; bool isoverch = false; bool areallvis = false;
            if (roverNumber != 0)
                for (; roverNumber!=0 && !(isde = isDead()) && !(arecharged = areTheSensorsCharged()) && (isoverch = !isOverCharged2()) && (areallvis = !areAllVisited()); cycle++)                
                {
                    updateSensorEnergy(Parameters.cycleTime);

                    for (int i = 0; i < roverNumber; i++) // simulate all rovers behavours 
                    {
                        int target = -1;

                        for (int j = 0; j < sensorNumber; j++)
                            if (x[i, j])
                                target = j;


                        //move i to its target
                        if (!move(i, target, Parameters.cycleTime))
                            charge(i, target, Parameters.cycleTime);

                    }


                    /////////////////////cycle space

                    m.draw(sensorNodes, rovers); // must be executed in every cycle. 
                    log += weakestSensorEnergy().ToString() + " ";
                    logmean += meanValueOfMin().ToString() + " ";
                    //print4mat(weakestSensorEnergy().ToString());
                    //txt.Text = "";
                    //for (int i = 0; i < roverNumber; i++)
                    //    txt.Text += rovers[i].energy.ToString()+ " ";
                    //txt.Refresh();
                    removeRvrIfIsDead();
                }
            if(roverNumber==0)
            {
                for (; !(isde = isDead()); cycle++)
                {
                    updateSensorEnergy(Parameters.cycleTime);
                    //////cycle must be copy her
                    m.draw(sensorNodes, rovers); // must be executed in every cycle. 
                    log += weakestSensorEnergy().ToString() + " ";
                    logmean += meanValueOfMin().ToString() + " ";
                    //print4mat(weakestSensorEnergy().ToString());                                        
                }
            }
            if (isDead2() || isOverCharged() || areAllVisited())
            {
                if (areAllVisited())
                {
                    lifeTime = 20001;
                    return 1;
                }
                else
                {
                    lifeTime = cycle * Parameters.cycleTime;
                    return 0;
                }

            }
            else if (areTheSensorsCharged())
            {
                // update isVisited variable for the already seen sensor nodes. 
                for (int i = 0; i < roverNumber; i++)
                    for (int j = 0; j < sensorNumber; j++)
                        if (x[i, j])
                            sensorNodes[j].isVisited = true;

                if (areAllVisited())
                {
                    lifeTime = cycle * Parameters.cycleTime;
                    return 0;
                }
                else
                    return 2; /// there is a deadlock over here. that must be checked later. 
            }
            return 3; // this never happens
        }
       
        public bool areAllVisited()
        {
            //for (int j = 0; j < sensorNumber; j++)
            //    if (!sensorNodes[j].isVisited)
            //        return false;

            //return true; 
            return false;
        }
        public bool areTheSensorsCharged()
        {
            int num=0;
            for (int i = 0; i < roverNumber; i++)
                for (int j = 0; j < sensorNumber; j++)
                    if (x[i, j] && sensorNodes[j].energy >= Parameters.sensorNodeEnergyCapacity)
                        num++;
            if (num == roverNumber)
                return true;
            else
                return false; 
        }
       
        public void init2(double roverEnergy)
        {
            int ID = 0;
            Random rnd = new Random();
            GeneralRandom grnd = new GeneralRandom();
            // creating sensors 
            for (int i = 0; i < sensorNumber; i++)
            {
                //some of the definitions must go inside the object
                Sensor s = new Sensor(grnd);
                s.capacity = Parameters.sensorNodeEnergyCapacity;
                //s.consumptionRate = Parameters.sensorNodeEnergyConsumptionRate;
              //  s.energyConsumptionRate = Parameters.sensorNodeEnergyConsumptionRate;
                s.energy = rnd.Next((int)Parameters.sensorNodeEnergyCapacity / 2, (int)Parameters.sensorNodeEnergyCapacity);// random between half and full 
                //s.energy = Parameters.sensorNodeEnergyCapacity;

                int width = Parameters.environmentWidth;
                int length = Parameters.environmentLength;
                s.position = new PointF(rnd.Next(0, length), rnd.Next(0, width));
                sensorNodes[i] = s;

            }
            //creating rovers
            for (int i = 0; i < roverNumber; i++)
            {
                //some of the definitions must go inside the object
                Rover r = new Rover();
                r.capacity = Parameters.roverEnergyCapacity;
                r.chargingEfficiency = Parameters.roverChargingEff;
                r.energy = roverEnergy;// random between half and full ;
                r.energyConsumptionCharge = Parameters.roverEnergyConsumptionCharging;
                r.energyConsumptionMove = Parameters.roverEnergyConsumptionMove;
                r.ID = ID++;
                r.speed = Parameters.roverSpeed;

                int width = Parameters.environmentWidth;
                int length = Parameters.environmentLength;
                r.position = new PointF(rnd.Next(0, length), rnd.Next(0, width));
                rovers[i] = r;

            }
        }
        public void saveToFile(string fn)
        {
           
           
            var ser = new BinaryFormatter();
            using (var str = File.OpenWrite(fn))
            {
                ser.Serialize(str,rovers);
                ser.Serialize(str, sensorNodes);
                ser.Serialize(str, roverNumber);
                ser.Serialize(str, sensorNumber);
                
            }
        }
        public void loadFromFile(string fn)
        {
            var ser = new BinaryFormatter();
            using (var stream = File.OpenRead(fn))
            {
                rovers=(Rover[])ser.Deserialize(stream);
                sensorNodes = (Sensor[])ser.Deserialize(stream);
                roverNumber = (int)ser.Deserialize(stream);
                sensorNumber = (int)ser.Deserialize(stream);
                
            }
          
        }

        public void testSaveFileSystem()
        {
            sensorNumber = 10;
            roverNumber = 20;
            init();
            saveToFile("C:\\Users\\ak409\\Desktop\\testttttt.dat");
        }
        public void testLoadFileSystem()
        {
            loadFromFile("C:\\Users\\ak409\\Desktop\\testttttt.dat");
        }
        public void init()
        {
            GeneralRandom grnd = new GeneralRandom();
            int ID = 0;
            Random rnd = new Random();
            // creating sensors 
            for (int i = 0; i < sensorNumber; i++)
            {
                //some of the definitions must go inside the object
                Sensor s = new Sensor(grnd);
                s.capacity=Parameters.sensorNodeEnergyCapacity;
              //  s.consumptionRate=Parameters.sensorNodeEnergyConsumptionRate; // not necessary!!!
                //s.energyConsumptionRate = rnd.Next((int)Parameters.sensorNodeEnergyConsumptionRate / 2, (int)Parameters.sensorNodeEnergyConsumptionRate); 
                s.energy=rnd.Next((int)Parameters.sensorNodeEnergyCapacity/2,(int)Parameters.sensorNodeEnergyCapacity);// random between half and full 
                //s.ID = i;
               // s.energy = Parameters.sensorNodeEnergyCapacity/2;
               
                int width= Parameters.environmentWidth;
                int length=Parameters.environmentLength;
                s.position= new PointF(rnd.Next(0,length),rnd.Next(0,width));
                sensorNodes[i]=s;


            }
            //creating rovers
            for (int i = 0; i < roverNumber; i++)
            {
                //some of the definitions must go inside the object
                Rover r = new Rover();
                r.capacity=Parameters.roverEnergyCapacity;
                r.chargingEfficiency=Parameters.roverChargingEff;
                r.energy = Parameters.roverEnergyCapacity;
                //r.energy = rnd.Next((int)Parameters.roverEnergyCapacity / 2, (int)Parameters.roverEnergyCapacity);// random between half and full ;
                r.energyConsumptionCharge=Parameters.roverEnergyConsumptionCharging;
                r.energyConsumptionMove=Parameters.roverEnergyConsumptionMove;
                r.ID=ID++;
                r.speed=Parameters.roverSpeed;
                
                int width = Parameters.environmentWidth;
                int length = Parameters.environmentLength;
                r.position = new PointF(rnd.Next(0, length), rnd.Next(0, width));
                rovers[i]=r;
                
            }
            //setting the monitor
            
            m.setNumbers(roverNumber, sensorNumber);
            m.setGraphics(formGraph);
            m.ForceDraw(sensorNodes, rovers);
           
            
        }
        
        bool[,] x = new bool[MAX, MAX]; // decision variable
        public double test(int i, int j)
        {
            double[,] result = new double[,] { { 12, 32, 13 }, { 34, 22, 33 }, { 16, 42, 9 } };
            return result[i, j];
        }
        public bool isAssigned(int rover)
        {
            for (int j = 0; j < sensorNumber; j++)
                if (x[rover, j] == true)
                    return true;
            return false; 
        }
        public void printarray(double[,] a)
        {
            int num = 0;
            string result = "\r\n";
            for(int i=0;i<roverNumber;i++)
            {
                for (int j = 0; j < sensorNumber; j++)
                {
                    num = (int)a[i, j];
                    result += num.ToString() + " ";
                }

                result += "\r\n";
            }
            txt.Text = result;
            txt.Refresh();
        }
        public double calculatebids_closestDistance(int rvr, int sns)
        {
            return 500-Parameters.distance(rovers[rvr], sensorNodes[sns]);
        }
        public double calculatebids_rnd(int rvr, int sns)
        {
            Random rn = new Random(rvr + sns);
            return rn.Next(0, 500);
        }
        public void sink3()
        {
            if (roverNumber == 0) return;
            double[] y = new double[MAX];
            double[,] c = new double[MAX, MAX];
            int[,] h = new int[MAX, MAX];

            // init variables 
            for (int j = 0; j < sensorNumber; j++)
            {
                y[j] = 0;
                for (int i = 0; i < roverNumber; i++)
                {
                    h[i, j] = 0;
                    x[i, j] = false;
                    //energy
                    c[i, j] = calculateBids3(Parameters.distance(rovers[i], sensorNodes[j]), sensorNodes[j].energy, rovers[i].energy);
                    //distance
                    //c[i, j] = calculatebids_closestDistance(i, j);
                    //random
                    // c[i, j] = calculatebids_rnd(i, j);
                    //test
                    //c[i, j] = test(i, j);
                }
            }
            //printarray(c);
            // algorithm loop
            while (!allRoversAreAssigned())
            {
                bool flag = false;
                for (int i = 0; i < roverNumber; i++)
                {
                    if (!isAssigned(i))
                    {
                        flag = false;
                        for (int j = 0; j < sensorNumber; j++)
                        {

                            h[i, j] = 0;
                            if (c[i, j] > y[j])
                            {
                                h[i, j] = 1;
                                flag = true;
                            }

                        }
                        if (flag)
                        {
                            int winner = 0;
                            double highestBid = 0;
                            for (int j = 0; j < sensorNumber; j++)
                            {
                                if (h[i, j] * c[i, j] > highestBid)
                                {
                                    highestBid = h[i, j] * c[i, j];
                                    winner = j;
                                }
                            }


                            for (int ii = 0; ii < roverNumber; ii++)
                                x[ii, winner] = false;

                            x[i, winner] = true;
                            y[winner] = highestBid;
                            flag = false;
                        }
                    }
                }
            }
        }
        System.Windows.Forms.DataVisualization.Charting.Chart ch1;

        public void setChart(System.Windows.Forms.DataVisualization.Charting.Chart ch)
        {
            ch1 = ch;
        }
        public double calculateBidslinkeMulti(int rov, int sen)
        {
            double alpha = rovers[rov].energy / Parameters.roverEnergyCapacity;
            double td = Parameters.distance(rovers[rov], sensorNodes[sen])/Parameters.roverSpeed;
            double ls = sensorNodes[sen].energy / Parameters.sensorNodeEnergyConsumptionRate;
            //return 1/((1-alpha)*td + (alpha)*ls);
            return 1/(10*td+ls);
        }

        public double alf=Parameters.alpha;
        public double bet=Parameters.betha;

        public void setalphbet(double a, double b)
        {
            alf = a;
            bet = b;
        }
        public double findBestCoeficients(int rov, int sen)
        {
            double td = Parameters.distance(rovers[rov], sensorNodes[sen]) / Parameters.roverSpeed;
            double ls = sensorNodes[sen].energy / sensorNodes[sen].energyConsumption();
            return 1 / ((alf*td) + (bet*ls));
        }

        public int ActiveRoversNumber()
        {
            return roverNumber; 
        }
        public void sink()
        {
            if (roverNumber == 0) return; 
            double[] y = new double[MAX];
            double[,] c = new double[MAX, MAX];
            int[,] h = new int[MAX, MAX];

            // init variables 
            for (int j = 0; j < sensorNumber; j++)
            {
                y[j] = 0;
                for (int i = 0; i < roverNumber; i++)
                {
                    h[i, j] = 0;
                    x[i, j] = false;
                    //find alpha betha test
                    c[i, j] = findBestCoeficients(i, j);
                    //multi vehicle function
                    //c[i, j] = calculateBidslinkeMulti(i, j);
                    //energy
                   //c[i, j] = calculateBids(Parameters.distance(rovers[i],sensorNodes[j]),sensorNodes[j].energy,rovers[i].energy);
                    //distance
                   //c[i, j] = calculatebids_closestDistance(i, j);
                    //random
                   // c[i, j] = calculatebids_rnd(i, j);
                    //test
                    //c[i, j] = test(i, j);
                }
            }
            //printarray(c);
            // algorithm loop
            while (!allRoversAreAssigned())
            {
                bool flag=false;
                for(int i=0;i<roverNumber;i++)
                {
                    if (!isAssigned(i))
                    {
                        flag = false;
                        for (int j = 0; j < sensorNumber; j++)
                        {

                            h[i, j] = 0;
                            if (c[i, j] > y[j])
                            {
                                h[i, j] = 1;
                                flag = true;
                            }

                        }
                        if (flag)
                        {
                            int winner = 0;
                            double highestBid = 0;
                            for (int j = 0; j < sensorNumber; j++)
                            {
                                if (h[i, j] * c[i, j] > highestBid)
                                {
                                    highestBid = h[i, j] * c[i, j];
                                    winner = j;
                                }
                            }


                            for (int ii = 0; ii < roverNumber; ii++)
                                x[ii, winner] = false;

                            x[i, winner] = true;
                            y[winner] = highestBid;
                            flag = false;
                        }
                    }
                }
            }

        }



        bool allRoversAreAssigned()
        {
            int assigned=0;
            for (int i = 0; i < roverNumber; i++)
                for (int j = 0; j < sensorNumber; j++)
                    assigned += 1 * ((x[i, j])?1:0);

            if (assigned == roverNumber)
                return true;            
            return false;
        }
        public double calculateBids3(double distance, double sensorEnergy, double roverEnergy)
        {
            //using possible transferable energy as value for bidding.
            double p = Parameters.sensorNodeEnergyCapacity - (sensorEnergy - (distance / Parameters.roverSpeed) * Parameters.sensorNodeEnergyConsumptionRate);
            double a = roverEnergy - (distance / Parameters.roverSpeed) * Parameters.roverEnergyConsumptionMove;
            double profit= Math.Min(Parameters.roverChargingEff * a, p);
           // double profit = Parameters.sensorNodeEnergyCapacity - sensorEnergy; // simply it consideres the empty space which can be charged as a profit. 
            double loss = (distance / Parameters.roverSpeed) * Parameters.roverEnergyConsumptionMove;

            double netProfit = (alpha * profit - betha * loss) ;//+500 to avoid negative numbers
            return netProfit;
        }
        double calculateBids(double distance, double sensorEnergy,double roverEnergy)
        {
            //using possible transferable energy as value for bidding.
            double p = Parameters.sensorNodeEnergyCapacity - (sensorEnergy - (distance / Parameters.roverSpeed) * Parameters.sensorNodeEnergyConsumptionRate);
            double a = roverEnergy-(distance / Parameters.roverSpeed) * Parameters.roverEnergyConsumptionMove;
            return Math.Min(Parameters.roverChargingEff * a, p);
        }
        Monitor m = new Monitor();
               
        public double lifeTime = 0;
        

        
        public void start()
        {
            //send updates to nodes
            //get the response from nodes
            //simulate the environment
            lifeTime = 0;


            // in a fast way...
            
            // move every rover toward their sensor nodes. 
            int cycle = 1;
           
            for (cycle = 1; !isDead() && !isOverCharged(); cycle++)
            {
                updateSensorEnergy(Parameters.cycleTime);
                for (int i = 0; i < roverNumber; i++) // simulate all rovers behavours 
                {
                    int target = -1;
                  
                    for (int j=0; j < sensorNumber; j++)
                        if (x[i, j])
                            target = j;


                    //move i to its target
                    if (!move(i, target, Parameters.cycleTime))
                        charge(i, target, Parameters.cycleTime);

                }
                m.draw(sensorNodes,rovers);
            }
            if (isDead())
                lifeTime = cycle * Parameters.cycleTime;
            else
                lifeTime = 20002; 

        }
        public bool isOverCharged2()
        {
            int numberOfOverCharged = 0;
            for (int j = 0; j < sensorNumber; j++)
                if (sensorNodes[j].energy > Parameters.sensorNodeEnergyCapacity )
                    numberOfOverCharged++;
            if (numberOfOverCharged == sensorNumber)
                return true;
            else
                return false;
        }
        public bool isOverCharged()
        {
            int numberOfOverCharged=0;
            for (int j = 0; j < sensorNumber; j++)
                if (sensorNodes[j].energy > Parameters.sensorNodeEnergyCapacity)
                    numberOfOverCharged++;
            if (numberOfOverCharged == sensorNumber)
                return true;
            else
                return false; 
        }

        public double dyanmicDurationTime=50;// 50sec    
        public double dynamicVariancePercentage = 200;//10%
        public double currentConsumptionRate = Parameters.sensorNodeEnergyConsumptionRate;

        public void updateSensorEnergy(double cycletime)
        {
            //if (!dynamic)
            //{
            //    for (int j = 0; j < sensorNumber; j++)
            //        sensorNodes[j].energy -= Parameters.sensorNodeEnergyConsumptionRate * cycletime;
            //}
            //else
            //{
            //    double durationCycle = 0;
            //    Random rnd = new Random();
            //    if (currentCycle % durationCycle == 0)
            //    {
            //        //update the currentConsumptionRate
            //        double min = Parameters.sensorNodeEnergyConsumptionRate - (dynamicVariancePercentage / 100) * Parameters.sensorNodeEnergyConsumptionRate;
            //        double max = Parameters.sensorNodeEnergyConsumptionRate + (dynamicVariancePercentage / 100) * Parameters.sensorNodeEnergyConsumptionRate;

            //        if (min < 0) throw new Exception("wrong calculation in dynamic consumptionRate");
            //        currentConsumptionRate = rnd.Next((int)min, (int)max);
            //    }
            //    for (int j = 0; j < sensorNumber; j++) // I want to have different energy consumption for every sensor node first. so i need to define it in their class first. 
            //        sensorNodes[j].energy -= currentConsumptionRate * cycletime;
            //}

            //for (int j = 0; j < sensorNumber; j++)
            //    sensorNodes[j].energy -= sensorNodes[j].energyConsumptionRate * cycletime;

            for (int j = 0; j < sensorNumber; j++)
                sensorNodes[j].UpdateEnergy(cycletime); 
            //for (int j = 0; j < sensorNumber; j++)
            //    sensorNodes[j].energy -= Parameters.sensorNodeEnergyConsumptionRate * cycletime;
        }

        public double weakestSensorEnergy()
        {
            double min=Parameters.sensorNodeEnergyCapacity+10;
            for (int j = 0; j < sensorNumber; j++)
                if (sensorNodes[j].energy < min)
                    min = sensorNodes[j].energy;
            return min;
        }
        
        public bool isDead()
        {
            for (int j = 0; j < sensorNumber; j++)
                if (sensorNodes[j].energy <= 0)
                    return true; 
            return false; 
        }

        public bool isDead2() // consider rovers as well. 
        {
           
            for (int j = 0; j < sensorNumber; j++)
                if (sensorNodes[j].energy <= 0)
                    return true;


            for (int i = 0; i < roverNumber; i++)
                if (rovers[i].energy <= 0)
                    return true;

            return false; 
           
        }
        public void setGraphic(Graphics graph)
        {
            formGraph = graph; 
        }
        public void createRoverAt(float x, float y)
        {
            rovers[roverNumber++] = new Rover();
            rovers[roverNumber - 1].position.X = x;
            rovers[roverNumber - 1].position.Y = y;
        }
        public double testCycleTime=1;
        public void moveRoverTo(int rover, PointF destination)
        {
            m.speedg = 1;
            m.setNumbers(roverNumber, sensorNumber);
            m.setGraphics(formGraph);
            m.draw(sensorNodes,rovers);

            for(int cycle= 1; moveTest(rover, destination);cycle++)
            {
                m.draw(sensorNodes, rovers);
                fm3.setTime(cycle * testCycleTime);
                if (cycle % 5 == 0)
                {
                    fm3.Refresh();
                }
            }
            
        }
        Form3 fm3;
        public void giveForm3(Form3 fmD)
        {
            fm3 = fmD;
        }
        
        public bool moveTest(int rover, PointF destination)
        {
            Rover rvr = rovers[rover];
            // sensor position
            double x2 =  destination.X;
            double y2 =  destination.Y;
            double xn = -1;
            double y = -1;
            if (isAround(rvr.position, destination))
                return false;

            if (rvr.energy < 0)
                return false;

            if (rvr.energy > 0)
            {
                double xx1 = rvr.position.X;
                double y1 = rvr.position.Y;
                double R = Math.Sqrt(Math.Pow(x2 - xx1, 2) + Math.Pow(y2 - y1, 2)); //R^2=(x2-x1)^2+(y2-y1)^2
                double DX = Parameters.roverSpeed * testCycleTime * (x2 - xx1) / R;
                double DY = Parameters.roverSpeed * testCycleTime * (y2 - y1) / R;
                xn = xx1 + DX;
                y = y1 + DY;
            }


            //m.draw(sensorNodes, rovers);
            if (isAround(rvr.position, destination))
                return false; // if rover near sensor node
            else
            {
                //update position
                rovers[rover].position.X = (float)xn;
                rovers[rover].position.Y = (float)y;
                //update energy
                rovers[rover].energy -= Parameters.roverEnergyConsumptionMove * testCycleTime;
                return true;
            }
        }
        public bool move(int rover, int sensor, double cycletime)
        {
            Rover rvr = rovers[rover];
            // sensor position
            double x2= sensorNodes[sensor].position.X;
            double y2 = sensorNodes[sensor].position.Y;
            double xn = -1;
            double y = -1;
            if (isAround(rvr.position, sensorNodes[sensor].position))
                return false;

            if (rvr.energy < 0)
                return false; 

            if (rvr.energy > 0)
            {
                double x1 = rvr.position.X;
                double y1 = rvr.position.Y;
                double R = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); //R^2=(x2-x1)^2+(y2-y1)^2
                double DX = Parameters.roverSpeed * cycletime * (x2 - x1) / R;
                double DY = Parameters.roverSpeed * cycletime * (y2 - y1) / R;
                xn = x1 + DX;
                y = y1 + DY;
            }

            
            //m.draw(sensorNodes, rovers);
            if (isAround(rvr.position, sensorNodes[sensor].position))
                return false; // if rover near sensor node
            else
            {
                // measure the distance
                rovers[rover].travelledDistance+=Parameters.distance(rovers[rover].position, new PointF((float)xn, (float)y));
                //update position
                rovers[rover].position.X = (float)xn;
                rovers[rover].position.Y = (float)y;
                //update energy
                rovers[rover].energy -= Parameters.roverEnergyConsumptionMove * cycletime;
                return true; 
            }
        }
        public double getAverageDistancePerRover() //centimeter per rover per minute
        {
            double s=0;
            for (int i = 0; i < roverNumber; i++)
            {
                s += rovers[i].travelledDistance;
            }

            
            double lifetimeinMinute = ((lifeTime==0)?(cycle*Parameters.cycleTime):lifeTime) / 60;
            double distanceinCentimeter = s * 100;
            return distanceinCentimeter / (roverNumber * lifetimeinMinute);
        }
        public double getAverageDistanceForAll()
        {
            return getAverageDistancePerRover() * roverNumber;
        }
        private bool isAround(PointF A, PointF B) // should move it to the parameters in the tools part later. 
        {
            double error = 0.5;
            if (A.X <= B.X + error && A.X >= B.X - error)
                if (A.Y <= B.Y + error && A.Y >= B.Y - error)
                    return true;
            return false;
        }
        public bool charge(int rover, int sensor, double cycletime)
        {
            if (rovers[rover].energy < 0)
                return false;
            //first check if they are in range and then charge it 
            if (isAround(rovers[rover].position, sensorNodes[sensor].position))
            {
                //txt.Text += rover.ToString() + " is charging " + sensor.ToString() + "\r\n";
                //txt.Refresh();
                //reduce the rover energy
                rovers[rover].energy -= Parameters.roverEnergyConsumptionCharging * cycletime;
                //add to the sensor energy
                double energyRate=Parameters.roverEnergyConsumptionCharging*Parameters.roverChargingEff;
                sensorNodes[sensor].energy += energyRate * cycletime;
            }
            return true; 
        }

        TextBox txt2 = null;
        internal void setTextBox2(TextBox textBox2)
        {
            txt2 = textBox2;
        }

        public string logInductive { get; set; }
        public string logAuction { get; set; }
        public string logMulti { get; set; }

        public int MAXCYCLE = 100000;

        internal void createSensorAt(float x, float y)
        {
            GeneralRandom grnd = new GeneralRandom();
            sensorNodes[sensorNumber++] = new Sensor(grnd);
            sensorNodes[sensorNumber - 1].position.X = x;
            sensorNodes[sensorNumber - 1].position.Y = y;
        }

        internal void setConsole(Console c)
        {
            con = c ;
        }
    }
}
