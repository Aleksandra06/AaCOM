using Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var flag = rectangle.RectangleMetod(matrix);
            if (!flag) { if (Notify != null) Notify($"Метод прямоугольников выполнен не успешно!\n"); return false; }

            return true;
        }
        private void Message(string message)
        {
            if (Notify != null) Notify(message);
        }

        //static MatrixFractionsMeneger _mFM = new MatrixFractionsMeneger();
        //static SimpleFractionsMeneger _sFM = new SimpleFractionsMeneger();

        //public bool Rectangle(MatrixFractions matrix)
        //{
        //    if (matrix.M < matrix.N) { if (Notify != null) Notify($"Строк больше столбцов! Невозможно решить! (Код 1.0)\n"); return false; }
        //    for (int nowStr = 0; nowStr < matrix.N; nowStr++)
        //    {
        //        if (_mFM.Norm(matrix))
        //            if (Notify != null) Notify("Сокращение:\n" + matrix.toString() + "\n");
        //        //блок проверки
        //        var check = CheckExceptions(matrix);
        //        if (check == null) { if (Notify != null) Notify($"Ошибка! \n"); return false; }
        //        if (check.SingleOrDefault() == -1) { if (Notify != null) Notify($"Противоречие! \n"); return true; }//дописать что делать, когда противоречие
        //        if (check.Count == 0) { if (Notify != null) Notify($"Пока что все идет по плану... наверное! \n"); }
        //        //приводим к 1
        //        SimpleFractions kof = matrix.Matrix[nowStr, nowStr];
        //        for (int j1 = 0; j1 < matrix.M; j1++)
        //        {
        //            matrix.Matrix[nowStr, j1] = _sFM.Division(matrix.Matrix[nowStr, j1], kof);
        //        }
        //        if (Notify != null) { Notify($"Сокращение: ({nowStr + 1}) * {kof.toString()} \n"); Notify(matrix.toString() + "\n"); }
        //        //прямоугольники
        //        for (int j1 = nowStr + 1; j1 < matrix.M; j1++)
        //        {
        //            for (int i1 = 0; i1 < matrix.N; i1++)
        //            {
        //                if (i1 != nowStr)
        //                    matrix.Matrix[i1, j1] =
        //                        _sFM.Difference(matrix.Matrix[i1, j1],
        //                                        _sFM.Multiplication(matrix.Matrix[nowStr, j1], matrix.Matrix[i1, nowStr]));
        //            }
        //        }
        //        for (int i = 0; i < matrix.N; i++)
        //        {
        //            if (i != nowStr)
        //            {
        //                matrix.Matrix[i, nowStr].Denominator = 1;
        //                matrix.Matrix[i, nowStr].Numerator = 0;
        //            }
        //        }
        //        if (Notify != null) Notify("После прямоугольников: \n" + matrix.toString() + "\n");
        //    }
        //    return true;
        //}
        ///// <summary>
        ///// Проверка на то, чтобы не было противоречий: 0*x=8 и занулений строк
        ///// Возвращает индексы зануленных строк, -1 в случае, когда возникает исключение, если null, то все плохо
        ///// </summary>
        //private List<int> CheckExceptions(MatrixFractions matrix)
        //{
        //    if (matrix.Matrix == null) { if (Notify != null) Notify($"Матрицы нет! См. CheckExceptions \n"); return null; }
        //    List<int> indexMas = new List<int>();
        //    for (int nowStr = 0; nowStr < matrix.N; nowStr++)
        //    {
        //        int countNull = 0;
        //        for (int nowCol = 0; nowCol < matrix.M; nowCol++)
        //        {
        //            if (matrix.Matrix[nowStr, nowCol].Numerator == 0) countNull++;
        //            else break;
        //        }
        //        if (countNull == matrix.M) indexMas.Add(nowStr);//строка занулилась
        //        else if (countNull == matrix.M - 1)//противоречие типа 0*x=8
        //        {
        //            if (Notify != null) Notify($"Противоречие типа 0=const \n");
        //            return new List<int>() { -1 };
        //        }
        //    }
        //    return indexMas;
        //}
    }
}
