using Fractions;
using System;
using System.IO;

namespace lab2_Basic_solutions_sle_Jordan_Gauss
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Номер примера (1-10): ");
            var num = Console.ReadLine();
            string path = @"matrix\" + num + ".txt";
            var matrix = read(path);
            if (matrix == null) return;
            MatrixFractions matrixFractions = new MatrixFractions(matrix);
            matrixFractions.Print();

            Solutions_Jordan_Gauss jordan_Gauss = new Solutions_Jordan_Gauss();
            jordan_Gauss.Notify += Message;
            jordan_Gauss.Solutions_Jordan_Gauss_Metod(matrixFractions);
            // jordan_Gauss.Notify += Message;
            //jordan_Gauss.Rectangle(matrixFractions);
            //Rectangle rectangle = new Rectangle();
            //rectangle.Notify += Message;
            //rectangle.RectangleMetod(matrixFractions);
        }
        static int[,] read(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                int[,] num = new int[lines.Length, lines[0].Split(' ').Length];
                int i, j;
                for (i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (j = 0; j < temp.Length; j++)
                        num[i, j] = Convert.ToInt32(temp[j]);
                }
                return num;
            }
            catch
            {
                Console.WriteLine("Проблемы с чтением, проверьте входные данные\n");
                return null;
            }
        }

        static void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
