using System;
using System.Collections.Generic;
using System.Text;

namespace Fractions
{
    public class MatrixFractionsMeneger
    {

        /// <summary>
        /// Сложение строк. True - удачно, False - невозможно
        /// </summary>
        /// <param name="num1">Строка, к которой прибалять и в которую записывается ответ</param>
        /// <returns></returns>
        public bool LineDifference(MatrixFractions matrix, int num1, int num2, SimpleFractions koeff)
        {
            MatrixFractions matrixFractions = matrix;
            if (num1 > matrixFractions.N || num2 > matrixFractions.N || num1 < 0 || num2 < 0) return false;
            SimpleFractionsMeneger sFM = new SimpleFractionsMeneger();
            for (int j = 0; j < matrixFractions.M; j++)
            {
                matrixFractions.Matrix[num1, j] = sFM.Sum(matrixFractions.Matrix[num1, j],
                    sFM.Multiplication(koeff, matrixFractions.Matrix[num2, j]));
            }
            matrix = matrixFractions;
            return true;
        }
        /// <summary>
        /// Упрощение матрицы
        /// </summary>
        public bool Norm(MatrixFractions matrix)
        {
            bool flag = false;
            SimpleFractionsMeneger sFM = new SimpleFractionsMeneger();
            List<long> list = new List<long>();
            for (int i = 0; i < matrix.N; i++)
            {
                //числитель
                for (int j = 0; j < matrix.M; j++)
                {
                    matrix.Matrix[i, j] = sFM.Norm(matrix.Matrix[i, j]);
                    list.Add(matrix.Matrix[i, j].Numerator);
                }
                list.RemoveAll(a => a == 0);
                long nod = sFM.NOD(list);
                if (nod == 0) break;
                if (nod != 1)
                {
                    for (int j = 0; j < matrix.M; j++)
                    {
                        matrix.Matrix[i, j].Numerator /= nod;
                        flag = true;
                    }
                }
                list.Clear();
                //знаменатель
                for (int j = 0; j < matrix.M; j++)
                {
                    matrix.Matrix[i, j] = sFM.Norm(matrix.Matrix[i, j]);
                    if (matrix.Matrix[i, j].Numerator != 0) list.Add(matrix.Matrix[i, j].Denominator);
                }
                //list.RemoveAll(a => a == 0);
                nod = sFM.NOD(list);
                if (nod == 0) break;
                if (nod != 1)
                {
                    for (int j = 0; j < matrix.M; j++)
                    {
                        matrix.Matrix[i, j].Denominator /= nod;
                        flag = true;
                    }
                }
                list.Clear();
            }
            return flag;
        }
    }
}
