using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace slimsim
{
    public partial class Form3 : Form
    {
        Form1 fm1;
        Simulator s;
        public Form3()
        {
            InitializeComponent();
            lbl_time.Text = "0";
        }

        internal void giveMe(Form1 form1)
        {
            fm1 = form1;
            s = fm1.s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Parameters.cycleTime = 10;
            s.testCycleTime =180;
            fm1.chart1.Series["auction"].Points.Clear();

            s.setChart(fm1.chart1);
          
            //s.setTextBox(fm1.textBox1);
            //s.setTextBox2(fm1.textBox2);
            

            s.setGraphic(fm1.g);
            s.createRoverAt(10, 10);
            s.giveForm3(this);
            //s.createSensorAt(50, 10);
          
            s.moveRoverTo(0, new PointF(100, 10)); 
            
        }
        public void setTime(double time)
        {
            lbl_time.Text = time/s.testCycleTime + " " +time.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s.stopnSave();
        }

       

       
    }
}
