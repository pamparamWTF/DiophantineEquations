using System.Runtime.ConstrainedExecution;
using System;
using System.Reflection;

namespace DiophantineEquations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CalcEqual equal = new CalcEqual();
            CalcSystemOfEquals calcSystemOfEquals = new CalcSystemOfEquals();

            calcSystemOfEquals.CalcEqation();

            //equal.CalcEqation();

            //equal.WriteMatrix();
        }
    }

    class CalcEqual
    {
        public CalcEqual() 
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
                        d = x[0][d_index];
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

    class CalcSystemOfEquals
    {
        public CalcSystemOfEquals()
        {
            MakeMatrix();
        }

        public int Ns, Nx, c;
        private List<List<int>> matrix;
        private int d, d_index, count_not_null;

        private void MakeMatrix()
        {
            Console.Write("Enter count of equals: ");
            Ns = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter count of uknowns: ");
            Nx = Convert.ToInt32(Console.ReadLine());
            matrix = new List<List<int>>();


            for (int i = 0; i < Ns + Nx; i++)
            {
                matrix.Add(new List<int>());

                for (int j = 0; j <= Nx; j++)
                    if ((Ns + j) == i) matrix[i].Add(1);
                    else matrix[i].Add(0);
            }

            for (int i = 0; i < Ns; i++)
            {
                for (int j = 0; j < Nx; j++)
                {
                    Console.Write("Enter a" + i.ToString() + j.ToString() + ": ");
                    matrix[i][j] = Convert.ToInt32(Console.ReadLine());
                }
                Console.Write("Enter c" + i.ToString() + ": ");
                matrix[i][Nx] = -Convert.ToInt32(Console.ReadLine());
            }
        }
        private bool SearchMin(int index)
        {
            //index - номер строки в которой необходимо искать / номер столбца с которого идет поиск
            int not_null_index;
            bool flag = false;

            d_index = index;
            d = matrix[index][index];

            for (not_null_index = index; not_null_index < Nx; not_null_index++)
                if (matrix[index][not_null_index] != 0)
                {
                    flag = true; break;
                }

            if (!flag) return false;

            d_index = not_null_index;

            for (int i = not_null_index + 1; i < Nx; i++)
                if (matrix[index][i] != 0)
                    if (Math.Abs(matrix[index][i]) < Math.Abs(matrix[index][d_index]))
                        d_index = i;

            d = matrix[index][d_index];

            return true;
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
        private void ReplaceRow(int ind_from, int ind_to)
        {
            List<int> row = new List<int>();

            for (int i = 0; i < Nx + Ns; i++)
            {
                row.Add(matrix[i][ind_from]);
                matrix[i][ind_from] = matrix[i][ind_to];
                matrix[i][ind_to] = row[i];
            }

            row.Clear();
        }
        private bool MakeLastRow(int index)
        {
            if (matrix[index][index] == 0 && matrix[index][Nx] != 0)
                return false;
            
            if (matrix[index][index] != 0)
                if (matrix[index][Nx] % matrix[index][index] != 0)
                    return false;

            if (matrix[index][index] == 0 && matrix[index][Nx] == 0)
                return true;

            int r = 0, q = 0;
            CalcRQ(matrix[index][Nx], matrix[index][index], out r, out q);

            matrix[index][Nx] = r;

            for (int i = index + 1; i < Nx + Ns; i++)
            {
                matrix[i][Nx] -= q * matrix[i][index];
            }
            return true;
        }
        private void CalcString(int index)
        {
            //index - номер строки в которой необходимо рассчитать

            int r = 0, q = 0;
            count_not_null = 0;

            while (count_not_null != 1)
            {
                if (!SearchMin(index))
                {
                    break;
                }
                count_not_null = 1;

                for (int j = index; j < Nx; j++)
                {
                    if (matrix[index][j] != 0 && j != d_index)
                    {
                        CalcRQ(matrix[index][j], d, out r, out q);
                        matrix[index][j] = r;

                        for (int i = index + 1; i < Nx + Ns; i++)
                        {
                            matrix[i][j] -= q * matrix[i][d_index];
                        }

                        if (r != 0) count_not_null++;
                    }
                }
            }

            
            if (d_index != index) ReplaceRow(d_index, index);
        }
        public void CalcEqation()
        {
            bool flag = true;
            for (int i = 0; i < Ns; i++)
            {
                CalcString(i);
                flag = MakeLastRow(i);

                //WriteMatrix();

                if (!flag)
                    break;
            }

            WriteResult(flag);
        }

        public void WriteResult(bool flag) 
        {
            WriteMatrix();
            if (!flag)
                Console.WriteLine("This system of equation has no solutions in integers!!!");
            else
            {
                int index = 0;
                while (matrix[index][index] != 0 && index < Ns)
                    index++;

                for (int i = Ns; i < Ns + Nx; i++)
                {
                    int t = 0;
                    Console.Write("x" + (i - Ns).ToString() + " = " + matrix[i][Nx].ToString());
                    for (int j = index; j < Nx; j++)
                    {
                        if (matrix[i][j] !=0)
                            Console.Write(" + " + matrix[i][j].ToString() + "*t" + t.ToString());
                        t++;
                    }
                    Console.WriteLine();
                }
            }
        }
        public void WriteMatrix()
        {
            Console.WriteLine();

            for (int i = 0; i < Ns + Nx; i++)
            {
                for (int j = 0; j <= Nx; j++)
                {
                    Console.Write(matrix[i][j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}