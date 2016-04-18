using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace slimsim
{
    public partial class Form4 : Form
    {
        Chart_Sim DistancePerRover;
        Chart_Sim Distance4All;
        Chart_Sim Lifetime;
        
        public Form4()
        {
            InitializeComponent();

            

            t1 = new Thread(runExperiment);

            DistancePerRover = new Chart_Sim(chart1);
            DistancePerRover.addFigure("auction",Color.Green);
            DistancePerRover.addFigure("inductive", Color.Blue);
            DistancePerRover.addFigure("multi", Color.Red);
            DistancePerRover.setTitle("Average travelled distance per rover");
            DistancePerRover.setAxes( "Number of rovers","Distance (cm/minute)");

            Distance4All = new Chart_Sim(chart2);
            Distance4All.addFigure("auction", Color.Green);
            Distance4All.addFigure("inductive", Color.Blue);
            Distance4All.addFigure("multi", Color.Red);
            Distance4All.setTitle("Travelled Distance for all rovers");
            Distance4All.setAxes("Number of rovers", "Distance (cm/minute)");

            Lifetime = new Chart_Sim(chart3);
            Lifetime.addFigure("auction", Color.Green);
            Lifetime.addFigure("inductive", Color.Blue);
            Lifetime.addFigure("multi", Color.Red);
            Lifetime.setTitle("Network lifetime");
            Lifetime.setAxes("Number of rovers", "Lifetime (sec)");

 
        }
        Form1 fm1;
        internal void giveMe(Form1 form1)
        {
            fm1 = form1;
            s = fm1.s;

            s.setGraphic(fm1.g);
            s.resetLiftime();
        }
        Simulator s;
        Thread t1;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!t1.IsAlive)
                t1.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (t1.IsAlive)
                t1.Abort();
        }
        public const double MAX_LIFETIME = 1000000;// in seconds
        public void runExperiment()
        {
            //reset chart
            //chart1.Series["auction"].Points.Clear();
            // run auction for different number of rovers until lifetime is unlimited
            bool isNetworkperpetual=false;
            //check if the environment is set propoerly
            if(s.roverNumber!=1)
                throw new Exception("set the environment for one rover");
            for (int j = 1; !isNetworkperpetual; j++)
            {
                //reset                
                s.stopnSave();
                s.resetLiftime();
                //run auction
                //if the network lifetime was unlimited give the distance after certain amount of lifetime.

                s.runAuction(MAX_LIFETIME);
                DistancePerRover.addPoint("auction", s.roverNumber, s.getAverageDistancePerRover());
                Distance4All.addPoint("auction", s.roverNumber, s.getAverageDistanceForAll());
                Lifetime.addPoint("auction", s.roverNumber, s.lifeTime);

                //chart1.Series["auction"].Points.AddXY(s.roverNumber, s.getAverageDistancePerRover());


                s.stopnSave();
                s.resetLiftime();

                s.runMulti(MAX_LIFETIME);
                DistancePerRover.addPoint("multi", s.roverNumber, s.getAverageDistancePerRover());
                Distance4All.addPoint("multi", s.roverNumber, s.getAverageDistanceForAll());
                Lifetime.addPoint("multi", s.roverNumber, s.lifeTime);

                s.stopnSave();
                s.resetLiftime();

                s.runInductive(MAX_LIFETIME);
                DistancePerRover.addPoint("inductive", s.roverNumber, s.getAverageDistancePerRover());
                Distance4All.addPoint("inductive", s.roverNumber, s.getAverageDistanceForAll());
                Lifetime.addPoint("inductive", s.roverNumber, s.lifeTime);

                s.stopnSave();
                s.resetLiftime();
                
                
                // add a new rover
                s.addOneRover();

            }
           
        }

       
       
    }
}
