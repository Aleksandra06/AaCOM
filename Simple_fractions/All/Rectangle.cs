using System.Collections.Generic;
using System.Linq;

namespace Fractions
{
    public class Rectangle
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;

        private MatrixFractionsMeneger _mFM = new MatrixFractionsMeneger();
        private SimpleFractionsMeneger _sFM = new SimpleFractionsMeneger();
        public bool RectangleMetod(MatrixFractions matrix)
        {
            List<int> banIndexStrList = new List<int>();
            if (matrix.M < matrix.N) { if (Notify != null) Notify($"Строк больше столбцов! Невозможно решить! (Код 1.0)\n"); return false; }
            for (int nowStr = 0, nowCol = 0; nowStr < matrix.N || nowCol < matrix.N; nowStr++, nowCol++)
            {
                if (_mFM.Norm(matrix))
                    if (Notify != null) Notify("Сокращение:\n" + matrix.toString() + "\n");
                //блок проверки
                var check = CheckExceptions(matrix);
                if (check == null) { if (Notify != null) Notify($"Ошибка! \n"); return false; }
                if (check.SingleOrDefault() == -1) { if (Notify != null) Notify($"Противоречие! \n"); return false; }
                if (check.Count > 0)
                {
                    foreach (var ch in check)
                    {
                        banIndexStrList.Add(ch);
                    }
                }
                if (Notify != null) Notify($"Пока что все идет по плану... наверное! \n");
                //проверка банлиста
                int strNullCount = CheckBanlist(check, nowStr, matrix.N);
                if (strNullCount > 0)
                {
                    nowStr += strNullCount - 1;
                    if (nowStr >= matrix.N)
                    {
                        if (Notify != null) Notify("Зануленная строка!\n" + matrix.toString() + "\n");
                    }
                }
                if (nowStr >= matrix.N)
                {
                    return true;
                }
                //проверка на 0-вой элемент
                if (matrix.Matrix[nowStr, nowCol].Numerator == 0)
                {
                    int Str = nowStr;
                    Str++;
                    if (Str >= matrix.N) { if (Notify != null) Notify($"Ведущий элемент 0, а строки закончились. Упс... \n"); return true; }
                    while (matrix.Matrix[Str, nowCol].Numerator == 0)
                    {
                        Str++;
                        if (Str >= matrix.N) { if (Notify != null) Notify($"Ведущий элемент 0, а строки закончились. Упс... \n"); return true; }
                    }
                    ObmenStrok(matrix, Str, nowStr);
                    if (Notify != null) { Notify($"Обмен: ({nowStr} и {Str})\n"); Notify(matrix.toString() + "\n"); }
                }
                //приводим к 1
                if (matrix.Matrix[nowStr, nowCol].Numerator != matrix.Matrix[nowStr, nowCol].Denominator)
                {
                    SimpleFractions kof = matrix.Matrix[nowStr, nowCol];
                    for (int j1 = 0; j1 < matrix.M; j1++)
                    {
                        matrix.Matrix[nowStr, j1] = _sFM.Division(matrix.Matrix[nowStr, j1], kof);
                    }
                    if (Notify != null) { Notify($"Приведение: ({nowStr + 1}) : {kof.toString()} \n"); Notify(matrix.toString() + "\n"); }
                }
                //прямоугольники
                for (int j1 = nowStr + 1; j1 < matrix.M; j1++)
                {
                    for (int i1 = 0; i1 < matrix.N; i1++)
                    {
                        if (i1 != nowCol)
                            matrix.Matrix[i1, j1] =
                                _sFM.Difference(matrix.Matrix[i1, j1],
                                                _sFM.Multiplication(matrix.Matrix[nowStr, j1], matrix.Matrix[i1, nowCol]));
                    }
                }
                for (int i = 0; i < matrix.N; i++)
                {
                    if (i != nowStr)
                    {
                        matrix.Matrix[i, nowCol].Denominator = 1;
                        matrix.Matrix[i, nowCol].Numerator = 0;
                    }
                }
                if (Notify != null) Notify("После прямоугольников: \n" + matrix.toString() + "\n");
            }
            return true;
        }
        /// <summary>
        /// Сколько впереди зануленных строк
        /// </summary>
        private int CheckBanlist(List<int> checklist, int nowStr, int allStr)
        {
            int strNull = 0;
            while (checklist.FindIndex(i => i == (nowStr + strNull)) >= 0)
            {
                strNull++;
                if (nowStr + strNull >= allStr)
                {
                    if (Notify != null) Notify("Зануленная строка!\n");
                }
            }
            return strNull;
        }
        /// <summary>
        /// Проверка на то, чтобы не было противоречий: 0*x=8 и занулений строк
        /// Возвращает индексы зануленных строк, -1 в случае, когда возникает исключение, если null, то все плохо
        /// </summary>
        private List<int> CheckExceptions(MatrixFractions matrix)
        {
            if (matrix.Matrix == null) { if (Notify != null) Notify($"Матрицы нет! См. CheckExceptions \n"); return null; }
            List<int> indexMas = new List<int>();
            for (int nowStr = 0; nowStr < matrix.N; nowStr++)
            {
                int countNull = 0;
                for (int nowCol = 0; nowCol < matrix.M; nowCol++)
                {
                    if (matrix.Matrix[nowStr, nowCol].Numerator == 0) countNull++;
                    else break;
                }
                if (countNull == matrix.M) indexMas.Add(nowStr);
                else if (countNull == matrix.M - 1)
                {
                    if (Notify != null) Notify($"Система не имеет решений \n");
                    return new List<int>() { -1 };
                }
            }
            return indexMas;
        }

        private bool ObmenStrok(MatrixFractions matrix, int a, int b)
        {
            if (a >= matrix.N || b >= matrix.N)
            {
                return false;
            }
            SimpleFractions tmp = new SimpleFractions();
            for (int i = 0; i < matrix.M; i++)
            {
                tmp = matrix.Matrix[a, i];
                matrix.Matrix[a, i] = matrix.Matrix[b, i];
                matrix.Matrix[b, i] = tmp;
            }
            return true;
        }
    }
}
