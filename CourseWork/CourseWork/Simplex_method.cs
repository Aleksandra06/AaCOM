using Fractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class Simplex_method
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;
        public bool SimplexMethod(MatrixFractions matrix, List<int> basis, List<SimpleFractions> F, bool min)
        {
            SimpleFractions tmp;
            int num = 0;
            basis.Sort();
            foreach (var b in basis)
            {
                if(b > matrix.M) { if (Notify != null) Notify($"Ошибочные данные в базисе\n"); return false; }
                int stl = num;
                if (num == b)
                {
                    num++; continue;
                }
                while (stl < b)
                {
                    for (int i = 0; i < matrix.N; i++)
                    {
                        tmp = matrix.Matrix[i, b];
                        matrix.Matrix[i, b] = matrix.Matrix[i, stl];
                        matrix.Matrix[i, stl] = tmp;
                    }
                    stl++;
                }
                num++;
            }
            if (Notify != null) Notify($"Оптимизация для метода прямоугольников:\n{matrix.toString()}\n");
            Rectangle rectangle = new Rectangle();
            rectangle.Notify += Message;
            var flag = rectangle.RectangleMetod(matrix);
            if(!flag) { if (Notify != null) Notify($"Ошибка в методе прямоугольников\n"); return false; }
            //придумать, как вернуть на место столбцы!
            return true;
        }

        private void Message(string message)
        {
            if (Notify != null) Notify(message);
        }
    }
}
