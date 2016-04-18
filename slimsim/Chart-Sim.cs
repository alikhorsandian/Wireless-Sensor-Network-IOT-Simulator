using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace slimsim
{
    class Chart_Sim
    {
        private Chart chart;

        public Chart_Sim(Chart chart1)
        {
            // TODO: Complete member initialization
            this.chart = chart1;
        }

        internal void addFigure(string p, System.Drawing.Color color)
        {
            chart.Series.Add(p);
            chart.Series[p].Color = color;
            chart.Series[p].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            
            
       

        }

        internal void addPoint(string p, int p_2, double p_3)
        {
            chart.Series[p].Points.AddXY(p_2, p_3);
        }
        public void setTitle(string title)
        {
            chart.Titles.Add(title);
        }

        public void setAxes(string x, string y)
        {
            chart.ChartAreas[0].AxisX.Title = x;
            chart.ChartAreas[0].AxisY.Title = y;
        }
    }
}
