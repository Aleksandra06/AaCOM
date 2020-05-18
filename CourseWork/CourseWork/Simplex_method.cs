using Fractions;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<SimpleFractions> co = new List<SimpleFractions>();
            var table = CreateTable(matrix, basis, F);
            var search = Search(table, basis, co);
            PrintTable(table, basis, co);
            while (CheckF(table, min))
            {
                table = NextTable(table, basis, search, co);
                if (!CheckF(table, min)) break;
                search = Search(table, basis, co);
                PrintTable(table, basis, co);
            }
            PrintTable(table, basis, null);
            PrintAnswer(table, basis, F);
            return true;
        }
        /// <summary>
        /// Вывод ответа
        /// </summary>
        private List<SimpleFractions> PrintAnswer(List<List<SimpleFractions>> table, List<int> basis, List<SimpleFractions> F)
        {
            List<SimpleFractions> answer = new List<SimpleFractions>();
            string str = "";
            for (int j = 1; j < table[0].Count; j++)
            {
                str += "x" + j + " = ";
                var id = basis.FindIndex(a => a == j - 1);
                if (id >= 0)
                {
                    str += table[id][0].toString() + "\n";
                    answer.Add(table[id][0]);
                }
                else
                {
                    str += "0\n";
                    answer.Add(new SimpleFractions(0, 1));
                }
            }
            SimpleFractions fAnswer = new SimpleFractions(0, 1);
            for (int i = 0; i < answer.Count; i++)
            {
                fAnswer = sFM.Sum(fAnswer, sFM.Multiplication(answer[i], F[i]));
            }
             fAnswer = sFM.Sum(fAnswer, F[F.Count - 1]);
            answer.Add(fAnswer);
            str += "Z = " + fAnswer.toString();
            if (Notify != null) Notify(str);
            return answer;
        }
        /// <summary>
        /// Поиск ведущего элемента
        /// </summary>
        private List<List<SimpleFractions>> NextTable(List<List<SimpleFractions>> table, List<int> basis, Tuple<int, int> search, List<SimpleFractions> co)
        {
            List<List<SimpleFractions>> newTable = new List<List<SimpleFractions>>();
            for (int i = 0; i < table.Count; i++)
            {
                newTable.Add(new List<SimpleFractions>());
                if (i == search.Item2)
                {
                    for (int j = 0; j < table[i].Count; j++)
                    {
                        newTable.LastOrDefault().Add(sFM.Division(table[i][j], table[search.Item2][search.Item1]));
                    }
                }
                else
                {
                    for (int j = 0; j < table[i].Count; j++)
                    {
                        newTable.LastOrDefault().Add(sFM.Difference(table[i][j], sFM.Division(sFM.Multiplication(table[search.Item2][j], table[i][search.Item1]), table[search.Item2][search.Item1])));
                    }
                }
            }
            basis[search.Item2] = search.Item1 - 1;
            return newTable;
        }
        /// <summary>
        /// Поиск ведущего элемента
        /// </summary>
        private Tuple<int, int> Search(List<List<SimpleFractions>> table, List<int> basis, List<SimpleFractions> co)
        {
            List<SimpleFractions> list = new List<SimpleFractions>();
            list = table[table.Count - 1].ToList();
            list.Remove(list[0]);
            var indexh = sFM.SearchMax(list);
            indexh++;
            co.Clear();
            for (int i = 0; i < table.Count - 1; i++)
            {
                if (table[i][indexh].Numerator != 0 && (table[i][indexh].Numerator > 0 && table[i][indexh].Denominator > 0)
                    || (table[i][indexh].Numerator < 0 && table[i][indexh].Denominator < 0))
                    co.Add(sFM.Division(table[i][0], table[i][indexh]));
                else
                    co.Add(null);
            }
            var indexV = sFM.SearchMin(co);
            return new Tuple<int, int>(indexh, indexV);
        }
        /// <summary>
        /// Проверка, продолжаем считать, или все
        /// </summary>
        private bool CheckF(List<List<SimpleFractions>> table, bool min)
        {
            for (int i = 1; i < table[table.Count - 1].Count; i++)
            {
                if ((table[table.Count - 1][i].Numerator > 0 && table[table.Count - 1][i].Denominator > 0) ||
                    (table[table.Count - 1][i].Denominator < 0 && table[table.Count - 1][i].Numerator < 0)) return true;
            }
            return false;
        }
        /// <summary>
        /// Вывод таблицы
        /// </summary>
        private void PrintTable(List<List<SimpleFractions>> list, List<int> basis, List<SimpleFractions> co)
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
                if (co != null && num < co.Count && co[num] != null)
                {
                    str += co[num].toString();
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
