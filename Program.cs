using System.Runtime.ConstrainedExecution;
using System;

namespace DiophantineEquations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calcultion calcultion = new Calcultion();

            calcultion.CalcEqation();

            calcultion.WriteMatrix();
        }
    }

    class Calcultion
    {
        public Calcultion() 
        { 
            MakeMatrix(); 
        }
        public int N, c;
        private List<List<int>> x;
        private int d, d_index, count_not_null;

        private void MakeMatrix()
        {
            Console.Write("Enter count of uknowns: ");
            N = Convert.ToInt32(Console.ReadLine());
            x = new List<List<int>>();

            for (int i = 0; i <= N; i++)
            {
                x.Add(new List<int>());
                for (int j = 0; j < N; j++)
                    if ((i - 1) == j) x[i].Add(1);
                    else x[i].Add(0);
            }

            for (int i = 0; i < N; i++)
            {
                Console.Write("Enter a" + i.ToString() + ": ");
                x[0][i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Enter c: ");
            c = Convert.ToInt32(Console.ReadLine());

        }
        private void CalcRQ(int a, int b, out int r, out int q)
        {
            r = a % b;
            q = a / b;

            if (r < 0 && a * b < 0)
            {
                q--;
                r = a - q * b;
            }
            if (r < 0 && a * b > 0)
            {
                q++;
                r = a - q * b;
            }
        }
        private bool SearchMin()
        {

            d_index = 0;
            d = 0;

            if (N == 1)
                d = x[0][0];

            for (int i = 1; i < N; i++)
            {
                if (x[0][i] != 0)
                    if (Math.Abs(x[0][i]) < Math.Abs(x[0][d_index]))
                    { 
                        d_index = i;
                        d = x[0][i];
                    }
                    else
                    {
                        d = x[0][d_index];

                    }
            }

            if (d == 0)
                return false;
            else
                return true;
        }
        private void CalcMatrix()
        {
            int r = 0, q = 0;
            count_not_null = 0;
            while (count_not_null != 1)
            {
                if (!SearchMin())
                {
                    Console.WriteLine("Error!!! All elements are 0");
                    break;
                }
                count_not_null = 1;

                for (int j = 0; j < N; j++)
                {
                    if (x[0][j] != 0 && j != d_index)
                    {
                        CalcRQ(x[0][j], d, out r, out q);
                        x[0][j] = r;

                        for (int i = 1; i <= N; i++)
                        {
                            x[i][j] -= q * x[i][d_index];
                        }

                        if (r != 0) count_not_null++;
                    }
                }
            }
        }
        private void WriteResult()
        {
            if (c % d == 0)
            {
                for (int i = 1; i <= N; i++)
                {
                    int t = 0;
                    Console.Write("x" + (i - 1).ToString() + " = " + (x[i][d_index] * c / d).ToString());
                    for (int j = 0; j < N; j++)
                    {
                        if (j != d_index)
                        {
                            Console.Write(" + " + x[i][j].ToString() + "*t" + t.ToString());
                            t++;
                        }
                    }
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("This equation has no solutions in integers!!!");

        }
        public void CalcEqation()
        {
            CalcMatrix();
            WriteResult();
        }
        public void WriteMatrix()
        {
            Console.WriteLine();

            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(x[i][j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }
    }
}