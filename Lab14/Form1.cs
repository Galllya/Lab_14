using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Lab14
{
    public partial class Form1 : Form
    {
        Statistica stat = new Statistica();
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.ChartAreas[0].Axes[1].Maximum = 1.1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stat.SumGen((int)numericUpDown3.Value, numericUpDown1.Value, numericUpDown2.Value);
            decimal[] Freq = stat.GetStat();
            chart1.Series[0].Points.Clear();
            double ai = stat.a;
            chart1.ChartAreas[0].Axes[0].Interval = ((double)(stat.b - stat.a)) / 5;
            for (int i = 0; i < Freq.Length; i++)
            {
                chart1.Series[0].Points.AddXY(ai + ((double)(stat.b - stat.a)) / 10, Math.Round((double)Freq[i], 3));
                ai += ((double)(stat.b - stat.a)) / 5;
            }
            chart1.ChartAreas[0].Axes[0].Maximum = stat.b;
            stat.MeanAvailable();
            label4.Text = "Average: " + stat.E + " (error = " + stat.EErr + " %)";
            label5.Text = "Variance: " + stat.D + " (error = " + stat.DErr + " %)";
            label7.Text = stat.ChiCheck();
            distr(numericUpDown1.Value, numericUpDown2.Value);
        }

        private void distr(decimal E, decimal D)
        {
            chart1.Series[1].Points.Clear();
            double ai = stat.a;
            for (int i = 0; i < 5; i++)
            {
                chart1.Series[1].Points.AddXY(ai + ((double)(stat.b - stat.a)) / 10, stat.p(ai + ((double)(stat.b - stat.a)) / 10));
                ai += ((double)(stat.b - stat.a)) / 5;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}