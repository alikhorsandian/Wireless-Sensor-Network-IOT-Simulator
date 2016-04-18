using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace slimsim
{
    public partial class Form2 : Form
    {
        const int MAX = 200;

        public Form2()
        {
            InitializeComponent();
            rbtn_Energy.Checked = true;

        }
        public void setTextbox1(string txt)
        {
            //textBox1.Text = txt;
        }
        public int SensorNumber;
        public Sensor[] sensorNodes = new Sensor[MAX];

        public void setChart()
        {
            if (isFirst)
            {
                chart1.Titles.Add("Sensor Nodes Energy");
                //chart1.ChartAreas[0].AxisY.Maximum = 1000;
                //chart1.ChartAreas[0].AxisX.Interval = 2;
                s = new Series("Sensor Nodes");
                chart1.Series.Add(s);
                chart1.ChartAreas[0].AxisX.Title = "Sensor IDs";
                chart1.ChartAreas[0].AxisX.Interval = 1;
                
                isFirst = false;
            }
        }
        bool isFirst = true;
        Series s;
        int counter = 0;
        int referesh = 200;
        public void draw()
        {
            if (counter++ % referesh == 0)
            {
                s.Points.Clear();
                if (rbtn_Consumption.Checked)
                {
                    for (int j = 0; j < SensorNumber; j++)
                    {
                        chart1.ChartAreas[0].AxisY.Title = "Energy Consumption Rate (j/s)";
                        s.Points.Add(sensorNodes[j].energyConsumption());
                    }
                }
                else if (rbtn_Energy.Checked)
                {
                    for (int j = 0; j < SensorNumber; j++)
                    {
                        chart1.ChartAreas[0].AxisY.Title = "Residual Energy (j)";
                        s.Points.Add(sensorNodes[j].energy);
                    }
                }
            }

        }
    }
}
