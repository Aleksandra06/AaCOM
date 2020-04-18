using System;
namespace Fractions
{
    public class MatrixFractions
    {
        /// <summary>
        /// Строк
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Столбцов
        /// </summary>
        public int M { get; set; }
        public SimpleFractions[,] Matrix { get; set; }

        public MatrixFractions(int[,] matrInt)
        {
            N = matrInt.GetLength(0);
            M = matrInt.GetLength(1);
            Matrix = new SimpleFractions[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Matrix[i, j] = new SimpleFractions();
                    Matrix[i, j].Numerator = matrInt[i, j];
                    Matrix[i, j].Denominator = 1;
                }
            }
        }
        public void Print()
        {
            for (int i = 0; i < N; i++)
            {
                string line = "";
                for (int j = 0; j < M; j++)
                {
                    line += " " + Matrix[i, j].toString() + "\t";
                }
                Console.WriteLine(line);
            }
        }
        public string toString()
        {
            string str = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    str += Matrix[i, j].toString() + "\t";
                }
                str += "\n";
            }
            return str;
        }
    }
}
