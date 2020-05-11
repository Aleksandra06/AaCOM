using Fractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"matrix\1.txt";
            var matrix = read(path);
            if (matrix == null) return;
            MatrixFractions matrixFractions = new MatrixFractions(matrix);
            matrixFractions.Print();
            List<SimpleFractions> F = new List<SimpleFractions>() { new SimpleFractions(9, 1), new SimpleFractions(2, 1) };
            List<int> basis = new List<int>() { 0, 1, 4 };
            var simplex_method = new Simplex_method();
            simplex_method.Notify += Message;
            simplex_method.SimplexMethod(matrixFractions, basis, F, true);
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
