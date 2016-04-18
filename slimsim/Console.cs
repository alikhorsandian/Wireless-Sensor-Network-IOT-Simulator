using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace slimsim
{
    class Console
    {

        bool lifetimeEnabled = false;
        TextBox console_Auction;
        TextBox console_Inductive;
        TextBox console_Multi;
        TextBox console_RoverNumber;

        TextBox console_AverageDistance;
        int counter = 1;

        public void print_intervally(string s)
        {
            if (lifetimeEnabled == false)
            {
                if (counter == 1)
                    console_Auction.Text += s + "\r\n";
                else if (counter == 2)
                    console_Multi.Text += s + "\r\n";
                else if (counter == 3)
                    console_Inductive.Text += s+"\r\n";

                counter++;
                if (counter == 4)
                    counter = 1;
            }

        }
        public void setConsole_AverageDistance(TextBox tb)
        {
            console_AverageDistance = tb;
        }
        public void setConsole_RoverNumber(TextBox tb)
        {
            console_RoverNumber = tb;
        }
        public void setConsole_Auction(TextBox tb)
        {
            console_Auction= tb;
        }
        public void setConsole_Inductive(TextBox tb)
        {
            console_Inductive = tb;
        }
        public void setConsole_Multi(TextBox tb)
        {
            console_Multi = tb;
        }

        public void print_AverageDistanceAuction(string s)
        {
            if (console_AverageDistance != null )
            {
                console_AverageDistance.Text += "Auction: " + s.ToString() + "\r\n";
            }
            console_AverageDistance.Refresh();
        }

        public void print_AverageDistanceMulti(string s)
        {
            if (console_AverageDistance != null )
            {
                console_AverageDistance.Text += "Multi: " + s.ToString() + "\r\n";
            }
            console_AverageDistance.Refresh();
        }

        public void print_AverageDistanceInductive(string s)
        {
            if (console_AverageDistance != null )
            {
                console_AverageDistance.Text += "Inductive: " + s.ToString() + "\r\n";
            }
            console_AverageDistance.Refresh();
        }
        public void print_RoverNumber(string s)
        {
            if (console_RoverNumber != null && lifetimeEnabled)
            {
                console_RoverNumber.Text += s.ToString() + "\r\n";
            }
        }
        public void print_Auction(string s)
        {
            if (console_Auction != null && lifetimeEnabled)
            {
                console_Auction.Text += s.ToString() + "\r\n";
            }
        }

        public void print_Inductive(string s)
        {
            if (console_Inductive != null && lifetimeEnabled)
            {
                console_Inductive.Text += s.ToString() + "\r\n";
            }
        }

        public void print_Multi(string s)
        {
            if (console_Multi != null && lifetimeEnabled)
            {
                console_Multi.Text += s.ToString() + "\r\n";
            }
        }
    }
}
