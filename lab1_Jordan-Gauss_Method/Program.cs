using Fractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab1_Jordan_Gauss_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Номер примера (1-6): ");
            var num = Console.ReadLine();
            string path = @"matrix\" + num + ".txt";
            //StreamReader file = new StreamReader(path, Encoding.Default);
            //string str = file.ReadToEnd();
            //Console.Write(str);
            var matrix = read(path);
            if (matrix == null) return;
            MatrixFractions matrixFractions = new MatrixFractions(matrix);
            matrixFractions.Print();

            Solutions_Jordan_Gauss jordan_Gauss = new Solutions_Jordan_Gauss();
            jordan_Gauss.Notify += Message;
            jordan_Gauss.Solutions_Jordan_Gauss_Metod(matrixFractions);
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
