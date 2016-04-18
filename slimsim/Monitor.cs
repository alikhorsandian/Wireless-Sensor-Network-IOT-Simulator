using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace slimsim
{
    class Monitor
    {
        TextBox txt; 
        Graphics g = null;
        int roverNumber;
        int sensorNumber;
        int counterg = 1;
        int countert = 1;
       public int speedg = 20;
        public int speedt = 10;
        StreamWriter sw;

        bool isOn = true; 
        public void turnOffMonitor()
        {
            isOn = false;
        }
        public void turnOnMonitor()
        {
            isOn = true;
        }
        public void writeInFile(string t,string fileName)
        {
            //string path = @"c:\users\ak409\desktop\"+filename+".txt";
            //using (sw = file.createtext(path))
            //{
            //    sw.writeline(t);
            //}
            //sw.close();
        }
        public void setTextBox(TextBox t)
        {
            txt = t; 
        }
        int scale = 4;
        public void setNumbers(int rovernumbers, int sensornodenumbers)
        {
            roverNumber = rovernumbers;
            sensorNumber = sensornodenumbers;
        }
        public void setGraphics(Graphics graph)
        {
            g = graph;
        }
        public void print(string t)
        {
            txt.Text = t;
            txt.Refresh();
        }
        public void printLine(string t)
        {
            if (countert % speedt == 0)
            {
                txt.Text += t + "\r\n";
                txt.Refresh();
            }
            else
                countert++;

        }
        public void print4mat(string t)
        {
            if (countert % speedt == 0)
            {
                txt.Text += t + " ";
                txt.Refresh();
            }
            else
                countert++;
        }

        public void draw(Sensor[] sensors, Rover[] rovers)
        {
            if (isOn)
            {
                if (counterg % speedg == 0)
                {
                    clear();
                    for (int i = 0; i < roverNumber; i++)
                        drawRover(rovers[i].position, Color.FromArgb(255 - (int)(((rovers[i].energy / Parameters.roverEnergyCapacity)) * 255), 0,(int)(((rovers[i].energy / Parameters.roverEnergyCapacity)) * 255)));
                    for (int j = 0; j < sensorNumber; j++)
                        drawSensorNode(sensors[j].position, Color.FromArgb(255 - (int)(((sensors[j].energy / Parameters.sensorNodeEnergyCapacity)) * 255), (int)(((sensors[j].energy / Parameters.sensorNodeEnergyCapacity)) * 255), 0));
                    counterg = 1;
                }
                else
                    counterg++;
            }

        }
        public void ForceDraw(Sensor[] sensors, Rover[] rovers)
        {
            clear();
            for (int i = 0; i < roverNumber; i++)
                drawRover(rovers[i].position, Color.FromArgb(255 - (int)(((rovers[i].energy / Parameters.roverEnergyCapacity)) * 255), 0, (int)(((rovers[i].energy / Parameters.roverEnergyCapacity)) * 255)));
            for (int j = 0; j < sensorNumber; j++)
                drawSensorNode(sensors[j].position, Color.FromArgb(255 - (int)(((sensors[j].energy / Parameters.sensorNodeEnergyCapacity)) * 255), (int)(((sensors[j].energy / Parameters.sensorNodeEnergyCapacity)) * 255), 0));
        }
        private void clear()
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, Parameters.environmentLength * 4, Parameters.environmentWidth * 4);
            //test
            //g.FillEllipse(new SolidBrush(Color.Red), (float)(1000 - 50 / 2), (float)(700 - 50 / 2), (float)50, (float)50);
        }

        private void drawSensorNode(PointF position, Color cl)
        {
            double xm = position.X;
            double ym = position.Y;
           
            
            
            double x = xm*scale;
            double y = ym * scale;
            double size = 5*scale;
            g.FillEllipse(new SolidBrush(cl), (float)(x - size / 2), (float)(y - size / 2), (float)size, (float)size);

        }

        private void drawRover(PointF position, Color cl)
        {
            double xm = position.X;
            double ym = position.Y;
            
            double x = xm * scale;
            double y = ym * scale;
            double size = 10 * scale;
            g.FillEllipse(new SolidBrush(cl), (float)(x - size / 2), (float)(y - size / 2), (float)size, (float)size);

        }
        private void drawRoverOncharge(double xm, double ym)
        {
            double x = xm * scale;
            double y = ym * scale;
            double size = 10 * scale;

            size *= 2;
            g.DrawEllipse(new Pen(Color.White), (float)(x - size / 2), (float)(y - size / 2), (float)size, (float)size);

        }

        internal void load(ref Sensor[] sensorNodes, ref Rover[] rovers)
        {
            throw new NotImplementedException();
        }
    }
}
