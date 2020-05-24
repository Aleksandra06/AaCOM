using Fractions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fractions
{
    public class Basic_solutions_Jordan_Gauss
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;
        public bool Basic_solutions_Jordan_Gauss_Metod(MatrixFractions matrix)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Notify += Message;
            var mat = matrix.Copy();
            var flag = rectangle.RectangleMetod(matrix);
            for (int i = 0; i < matrix.N; i++)
            {
                bool flagNull = true;
                for (int j = 0; j < matrix.M; j++)
                {
                    if (matrix.Matrix[i, j].Numerator != 0) { flagNull = false; }
                }
                if (flagNull) matrix.N--;
            }
            if (!flag) { if (Notify != null) Notify($"Метод прямоугольников выполнен не успешно!\n"); return false; }
            var c = C(matrix.M - 1, matrix.N);
            if (Notify != null) Notify($"C = {c}\n");
            var kombi = Kombi(matrix.M - 1, matrix.N, c);
            List<List<SimpleFractions>> answer = new List<List<SimpleFractions>>();
            foreach (var k in kombi)
            {
                if (Notify != null) Notify($"***********************************************\n");
                var m = matrix.Copy();
                Rectangle(m, k);
                answer.Add(ListAnswer(m, k));
            }
            PrintAnswer(answer);
            return true;
        }

        private void PrintAnswer(List<List<SimpleFractions>> answer)
        {
            string str = "";
            if (Notify == null) return;
            foreach (var a in answer)
            {
                var flagO = true;
                str += "( ";
                foreach (var x in a)
                {
                    if (x != null)
                    {
                        str += x.toString() + ", ";
                        if ((x.Numerator < 0 && x.Denominator > 0) || (x.Numerator > 0 && x.Denominator < 0))
                        {
                            flagO = false;
                        }
                    }
                    else str += "0, ";
                }
                str += ")";
                if (flagO) str += " - опорное\n";
                else str += " - не опорное\n";
            }
            Notify(str);
        }

        private List<SimpleFractions> ListAnswer(MatrixFractions matrix, List<int> kombi)
        {
            List<SimpleFractions> answerList = new List<SimpleFractions>();
            for (int k = 0; k < kombi.Count; k++)
            {
                if (kombi[k] == 1)
                {
                    for (int i = 0; i < matrix.N; i++)
                    {
                        if (matrix.Matrix[i, k].Numerator == matrix.Matrix[i, k].Denominator)
                        {
                            answerList.Add(matrix.Matrix[i, matrix.M - 1]);
                        }
                    }
                }
                else
                {
                    answerList.Add(null);
                }
            }
            return answerList;
        }

        /// <summary>
        /// Метод прямоугольников для SimplexMethod
        /// </summary>
        private bool Rectangle(MatrixFractions matrix, List<int> basis)
        {
            List<Tuple<int, int>> obmen = new List<Tuple<int, int>>();
            int minus = 2;
            for (int b = 0; b < basis.Count; b++)
            {
                if (basis[b] == 1) continue;
                if (b > matrix.M) { if (Notify != null) Notify($"Ошибочные данные в базисе\n"); return false; }
                obmen.Add(new Tuple<int, int>(b, matrix.M - 2));
                ObmenColumn(matrix, b, matrix.M - minus, true);
                minus++;
            }
            if (Notify != null) Notify($"Оптимизация для метода прямоугольников:\n{matrix.toString()}\n");
            Rectangle rectangle = new Rectangle();
            rectangle.Notify += Message;
            var flag = rectangle.RectangleMetod(matrix);
            if (!flag) { if (Notify != null) Notify($"Ошибка в методе прямоугольников\n"); return false; }
            foreach (var o in obmen)
            {
                ObmenColumn(matrix, o.Item1, o.Item2, false);
            }
            if (Notify != null) Notify($"Обмен столбцов обратно:\n{matrix.toString()}\n");
            return true;
        }
        /// <summary>
        /// Обмен столбцов
        /// </summary>
        /// <param name="forward">В начало?</param>
        /// <returns></returns>
        private void ObmenColumn(MatrixFractions matrix, int stl1, int stl2, bool forward)
        {
            if (stl1 == stl2) return;
            SimpleFractions tmp;
            {
                for (int i = 0; i < matrix.N; i++)
                {
                    tmp = matrix.Matrix[i, stl2];
                    matrix.Matrix[i, stl2] = matrix.Matrix[i, stl1];
                    matrix.Matrix[i, stl1] = tmp;
                }
            }
        }
        /// <summary>
        /// Комбинации иксов. Пример 4,4: (1,1,1,0),(1,1,0,1),(1,0,1,1),(0,1,1,1)
        /// </summary>
        private List<List<int>> Kombi(int xCount, int strCount, int c)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= xCount; i++)
            {
                list.Add(i);
            }
            //for (int i = 0; i < c; i++)
            //{
            //    list.Add(new List<int>());
            //}
            //for (int x = 0; x < xCount; x++)
            //{
            //    for (int j = 0; j < c; j++)
            //    {
            //        if (j != c - x - 1) list[j].Add(1);
            //        else list[j].Add(0);
            //    }
            //}
            var listik = Allcombinations(list, new List<int>()).ToList();
            for (int j = 0; j < listik.Count; j++)
            {
                for (int i = 0; i < listik[j].Count; i++)
                {
                    if (listik[j][i] <= strCount) listik[j][ i] = 1;
                    else listik[j][i] = 0;
                }
                for(int k= 0; k< j; k++)
                {
                    var flag = true;
                    for (int i = 0; i < listik[j].Count; i++)
                    {
                        if (listik[k][i] != listik[j][i]) { flag = false; break; }
                    }
                    if (flag)
                    {
                        listik.Remove(listik[k]);
                        j--;
                        break;
                    }
                }
            }
            return listik;
        }
        private static IEnumerable<List<int>> Allcombinations(List<int> arg, List<int> awithout)
        {
            if (arg.Count == 1)
            {
                var result = new List<List<int>>();
                result.Add(new List<int>());
                result[0].Add(arg[0]);
                return result;
            }
            else
            {
                var result = new List<List<int>>();

                foreach (var first in arg)
                {
                    var others0 = new List<int>(arg.Except(new int[1] { first }));
                    awithout.Add(first);
                    var others = new List<int>(others0.Except(awithout));

                    var combinations = Allcombinations(others, awithout);
                    awithout.Remove(first);

                    foreach (var tail in combinations)
                    {
                        tail.Insert(0, first);
                        result.Add(tail);
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// Количество комбинаций
        /// </summary>
        public int C(int n, int m)
        {
            return Fact(n) / (Fact(m) * Fact(n - m));
        }
        /// <summary>
        /// Факториал
        /// </summary>
        public int Fact(int num)
        {
            int factorial = 1;
            for (int i = 1; i <= num; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
        private void Message(string message)
        {
            if (Notify != null) Notify(message);
        }
    }
}
