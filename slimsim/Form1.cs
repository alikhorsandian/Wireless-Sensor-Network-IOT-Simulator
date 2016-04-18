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
    public partial class Form1 : Form
    {
        Thread t;
        //Thread t2;
        Form2 mon;
        public Form1()
        {
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //
            g = panel1.CreateGraphics();
            chart1.Series["auction"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["auction"].Color = Color.Red;
            chart1.Series["multi"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
          
            chart1.Series["inductive"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;


            mon = new Form2();
          

            mon.setTextbox1("hi Ali");
            s = new Simulator();
            s.setForm2(mon);

            c.setConsole_Auction(textBox1);
            c.setConsole_Inductive(textBox2);
            c.setConsole_Multi(textBox3);
            c.setConsole_RoverNumber(textBox4);
            c.setConsole_AverageDistance(textBox5);
            s.setConsole(c);
          

            t = new Thread(UpdateNumberOfRovers);
            t.Start();

            //t2 = new Thread(displayTime);
            //t2.Start();
           

            //t2.Tick += new EventHandler(displayTime);
            //t2.Interval = 1000; // in miliseconds
            //t2.Start();

            manageMonitor();
            fm4 = new Form4();
        }

        public void displayTime()
        {
            while (true)
            {
                if (s != null)
                {
                    label6.Text = s.currentTime().ToString();
                    label6.Refresh();
                }
            }
        }
        public void manageMonitor()
        {


            if (s != null)
            {
                if (checkBox1.Checked)
                    s.turnOnMonitor();
                else
                    s.turnOffMonitor();
            }
        }
        public void UpdateNumberOfRovers()
        {
            while (true)
            {
                if (s != null)
                    label4.Text = s.ActiveRoversNumber().ToString();
            }
        }
        public Graphics g; 
       public Simulator s;
        private void button1_Click(object sender, EventArgs e)
        {
            s.setGraphic(g);
            //s.setTextBox(textBox1);
            s.test();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s.setGraphic(g);
           // s.setTextBox(textBox1);
            s.test1();
           // s.run1();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            s.stopTest1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            s.stopTest();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            s.setGraphic(g);
            //s.setTextBox(textBox1);
            //s.setTextBox2(textBox2);
            s.test2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            s.stopTest2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            s.setGraphic(g);
            //s.setTextBox(textBox1);
            //s.setTextBox2(textBox2);
            //s.setTextbox3(textBox3);
            //s.setTextbox4(textBox4);

            s.test3();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            s.stopTest3();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            s.setGraphic(g);
            //s.setTextBox(textBox1);
            //s.setTextBox2(textBox2);
            s.RunInductive();
          
        }

        
        private void button10_Click(object sender, EventArgs e)
        {

            chart1.Series["auction"].Points.Clear();
            chart1.Series["inductive"].Points.Clear();
            chart1.Series["multi"].Points.Clear();

            s.setGraphic(g);
            int SN = int.Parse(sen.Text);
            int RN = int.Parse(rov.Text);
            s.roverNumber = RN;
            s.sensorNumber = SN;
            s.init();
            s.saveToFile("rnd.txt");
        }
        Console c = new Console();
        private void button11_Click(object sender, EventArgs e)
        {
            s.stopnSave();
           

            mon.BringToFront();
            s.resetLiftime();
            chart1.Series["auction"].Points.Clear();

            s.setChart(chart1);
            s.setGraphic(g);

            
            
           //
           //s.setm(false);

            s.RA();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            s.stopnSave();
            s.resetLiftime();
            chart1.Series["inductive"].Points.Clear();
            s.setChart(chart1);
            s.setGraphic(g);
       
            s.setForm2(mon);
            s.RI();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            s.stopnSave();
            s.resetLiftime();
            chart1.Series["multi"].Points.Clear();
            s.setChart(chart1);
            s.setGraphic(g);
           
            s.setForm2(mon);
            s.RM();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            s.printAverageWhenitsPerpetual();
            s.stopnSave();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void sen_TextChanged(object sender, EventArgs e)
        {

        }

      
        private void button15_Click(object sender, EventArgs e)
        {
            for(double alpha=1;alpha<=10;alpha+=1)
                for (double betha = 1; betha <= 10; betha += 1)
                {
                    //model
                    s.setalphbet(alpha, betha);


                    s.resetLiftime();
                    

                    s.setChart(chart1);
                    s.setGraphic(g);
                    //s.setTextBox(textBox1);
                    //s.setTextBox2(textBox2);
                    s.setForm2(mon);
                    //s.RA();
                    s.runAuction();

                    //view 
                    chart1.Series["auction"].Points.Clear();
                    //textBox2.Text += alpha.ToString() + " ";
                    //textBox3.Text += betha.ToString() + " ";
                    //textBox4.Text += s.lifeTime.ToString() + " ";
                    //textBox2.Refresh();
                    //textBox3.Refresh();
                    //textBox4.Refresh();

                    s.stopnSave();
                }
        }
        Form3 fm3;
        private void button16_Click(object sender, EventArgs e)
        {
            fm3 = new Form3();
            fm3.giveMe(this);
            fm3.Show();
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (s == null)            
                s = new Simulator();
            c.print_RoverNumber(s.ActiveRoversNumber().ToString());

            s.addOneRover();

            
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            manageMonitor();
        }

        private void button19_Click(object sender, EventArgs e)
        {
         if(mon.Visible)
             mon.Hide();
         else
            mon.Show();

           
        }
        Form4 fm4;
        private void button20_Click(object sender, EventArgs e)
        {
            fm4.giveMe(this);
            fm4.Show();
        }
        
    }
}
