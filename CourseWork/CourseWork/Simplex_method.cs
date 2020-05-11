using Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork
{
    class Simplex_method
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;

        SimpleFractionsMeneger sFM = new SimpleFractionsMeneger();
        public bool SimplexMethod(MatrixFractions matrix, List<int> basis, List<SimpleFractions> F, bool min)
        {
            var flag = Rectangle(matrix, basis);
            if (!flag) return false;
            //создание таблички
            var table = CreateTable(matrix, basis, F);
            PrintTable(table, basis);
            List<List<List<SimpleFractions>>> megaList = new List<List<List<SimpleFractions>>>();


            return true;
        }

        private void PrintTable(List<List<SimpleFractions>> list, List<int> basis)
        {
            if (Notify == null) return;
            string str = "";
            str += "------------------------------------------------------------------\n";
            str += "бп\t| 1\t| x1\t| x2\t| x3\t| x4\t| x5\t| co\n";
            str += "------------------------------------------------------------------\n";
            int num = 0;
            foreach (var listik in list)
            {
                if (num < basis.Count) str += "x" + (basis[num] + 1).ToString() + "\t|";
                else str += "Z\t|";
                foreach (var l in listik)
                {
                    str += l.toString() + "\t|";
                }
                str += "\n";
                str += "------------------------------------------------------------------\n";
                num++;
            }
            Notify(str);
        }
        /// <summary>
        /// Создание первой таблицы
        /// </summary>
        private List<List<SimpleFractions>> CreateTable(MatrixFractions matrix, List<int> basis, List<SimpleFractions> F)
        {
            try
            {
                List<List<SimpleFractions>> list = new List<List<SimpleFractions>>();
                for (int i = 0; i < matrix.N; i++)
                {
                    list.Add(new List<SimpleFractions>());
                    list.LastOrDefault().Add(matrix.Matrix[i, matrix.M - 1]);
                    for (int j = 0; j < matrix.M - 1; j++)
                    {
                        list.LastOrDefault().Add(matrix.Matrix[i, j]);
                    }
                }
                list.Add(new List<SimpleFractions>());
                //list.LastOrDefault().Add(new SimpleFractions(0, 1));
                for (int j = 0; j < matrix.M; j++)
                {
                    SimpleFractions fractions;
                    fractions = sFM.Multiplication(list[0][j], F[0]);
                    for (int i = 1; i < matrix.N; i++)
                    {
                        fractions = sFM.Sum(sFM.Multiplication(list[i][j], F[i]), fractions);
                    }
                    if (j != 0) fractions = sFM.Difference(fractions, F[j - 1]);
                    else fractions = sFM.Difference(fractions, F[F.Count - 1]);
                    list.LastOrDefault().Add(fractions);
                }
                return list;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// Метод прямоугольников для SimplexMethod
        /// </summary>
        private bool Rectangle(MatrixFractions matrix, List<int> basis)
        {
            //обмен столбцов, чтоб базис был в начале матрицы, дабы метод прямоугольников не надо было переписывать и все и так работало правильно
            List<Tuple<int, int>> obmen = new List<Tuple<int, int>>();
            int numStr = 0;
            basis.Sort();
            foreach (var b in basis)
            {
                if (b > matrix.M) { if (Notify != null) Notify($"Ошибочные данные в базисе\n"); return false; }
                if (numStr == b)
                {
                    numStr++; continue;
                }
                obmen.Add(new Tuple<int, int>(numStr, b));
                ObmenColumn(matrix, numStr, b, true);
                numStr++;
            }
            //прямоугольник
            if (Notify != null) Notify($"Оптимизация для метода прямоугольников:\n{matrix.toString()}\n");
            Rectangle rectangle = new Rectangle();
            rectangle.Notify += Message;
            var flag = rectangle.RectangleMetod(matrix);
            if (!flag) { if (Notify != null) Notify($"Ошибка в методе прямоугольников\n"); return false; }
            //обмен столбцов обратно
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
            if (forward)
            {
                if (stl1 > stl2) { var tmpint = stl1; stl1 = stl2; stl2 = tmpint; }
                while (stl1 < stl2)
                {
                    for (int i = 0; i < matrix.N; i++)
                    {
                        tmp = matrix.Matrix[i, stl2];
                        matrix.Matrix[i, stl2] = matrix.Matrix[i, stl1];
                        matrix.Matrix[i, stl1] = tmp;
                    }
                    stl1++;
                }
            }
            else
            {
                if (stl1 < stl2) { var tmpint = stl1; stl1 = stl2; stl2 = tmpint; }
                while (stl1 > stl2)
                {
                    for (int i = 0; i < matrix.N; i++)
                    {
                        tmp = matrix.Matrix[i, stl2];
                        matrix.Matrix[i, stl2] = matrix.Matrix[i, stl1];
                        matrix.Matrix[i, stl1] = tmp;
                    }
                    stl1--;
                }
            }
        }
        private void Message(string message)
        {
            if (Notify != null) Notify(message);
        }
    }
}
