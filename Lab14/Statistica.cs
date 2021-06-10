using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Lab14
{
    class Statistica
    {
        public Stopwatch sw = new Stopwatch();
        public decimal E, D;
        public double EErr, DErr;
        public int a, b;
        public decimal[] Values, Probabilities;
        decimal[] Frequency = new decimal[5];
        decimal E0, D0, Chi;
        int[] Statistics = new int[5];
        Random rnd = new Random();
        public decimal[] GetStat()
        {
            decimal min = Values[0], max = Values[0];
            int j;
            for (int i = 1; i < Values.Length; i++)
            {
                if (Values[i] < min) min = Values[i];
                if (Values[i] > max) max = Values[i];
            }
            a = (int)Math.Floor(min); b = (int)Math.Ceiling(max);
            for (int i = 0; i < 5; i++)
                Statistics[i] = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                j = 0;
                while (a + j * ((decimal)(b - a)) / 5 >= Values[i] || a + (j + 1) * ((decimal)(b - a)) / 5 < Values[i]) j++;
                Statistics[j]++;
            }
            for (int i = 0; i < 5; i++) Frequency[i] = (decimal)Statistics[i] / Values.Length;
            return Frequency;
        }
        public void MeanAvailable()
        {
            Chi = E = D = 0;
            Probabilities = new decimal[5];
            for (int i = 0; i < 5; i++)
            {
                Probabilities[i] = (decimal)((b - a) * p((2 * a + i * ((double)(b - a)) / 5 + (i + 1) * ((double)(b - a)) / 5) / 2) / 5);
                Chi += Statistics[i] * Statistics[i] / (Probabilities[i] * Values.Length);
            }
            Chi -= Values.Length;
            for (int i = 0; i < Values.Length; i++)
            {
                E += Values[i];
                D += Values[i] * Values[i];
            }
            E /= Values.Length;
            D /= Values.Length;
            D -= E * E;
            E = Math.Round(E, 3);
            D = Math.Round(D, 3);
            EErr = Math.Round(Math.Abs((double)(E - E0)) * 100 / Math.Abs((double)E0), 3);
            DErr = Math.Round(Math.Abs((double)(D - D0)) * 100 / Math.Abs((double)D0), 3);
        }
        public void SumGen(int n, decimal e0, decimal d0)
        {
            E0 = e0; D0 = d0;
            Values = new decimal[n];
            sw.Restart();
            double x;
            for (int i = 0; i < n; i++)
            {
                x = 0;
                for (int j = 0; j < 12; j++)
                    x += rnd.NextDouble();
                Values[i] = (decimal)((x - 6) * Math.Sqrt((double)D0)) + E0;
            }
            sw.Stop();
        }


        public string ChiCheck()
        {
            if ((double)Chi < 11.07) return "Chi-squared: " + Math.Round((double)Chi, 3) + " < 11.07 true";
            return "Chi-squared: " + Math.Round((double)Chi, 3) + " > 11.07 не false";
        }
        public double p(double x)
        {
            return Math.Exp(-(x - (double)E0) * (x - (double)E0) / (2 * (double)D0)) / Math.Sqrt(2 * Math.PI * (double)D0);
        }
    }
}

