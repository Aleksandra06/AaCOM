using Fractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_Jordan_Gauss_Method1///можно удалить
{
    public class Jordan_Gauss
    {
        public delegate void ExampleHandler(string message);
        public event ExampleHandler Notify;

        static MatrixFractionsMeneger _mFM = new MatrixFractionsMeneger();
        static SimpleFractionsMeneger _sFM = new SimpleFractionsMeneger();
        public bool Jordan_Gauss_Method(MatrixFractions mF)
        {
            if (!Rectangle_method(mF)) { if (Notify != null) Notify($"Невозможно решить (метод прямоугольников)! (Код 1)\n"); return false; }
            if (!Slu_decision(mF)) { if (Notify != null) Notify($"Невозможно решить (слу)! (Код 2)\n"); return false; }
            return true;
        }
        private bool Rectangle_method(MatrixFractions mF)
        {
            if (mF.M < mF.N) { if (Notify != null) Notify($"Строк больше столбцов! Невозможно решить! (Код 1.0)\n"); return false; }
            for (int j = 0; j < mF.N; j++)
            {
                if (_mFM.Norm(mF))
                    if (Notify != null) Notify("Сокращение:\n" + mF.toString() + "\n");
                if ((mF.Matrix[j, j].Numerator != 1 || mF.Matrix[j, j].Denominator != 1) && j != mF.N)
                {
                    ///Надо дописать какое-нибудь условие, чтоб строка не учитывалась, убиралась наверное, когда строка полностю зануляется
                    ///Иначе все ломается.
                    /*int ind = 1;
                    if (j == 0)
                    {
                        while (mF.Matrix[ind, j].Numerator == 0 && ind < mF.N - 1) ind++;
                        if (mF.Matrix[ind, j].Numerator == 0 && ind == mF.N - 1) { if (Notify != null) Notify($"Невозможно решить! (Код 1.1)\n"); return false; }
                    }
                    else
                    {
                        ind = j + 1;
                        while (mF.Matrix[ind, j].Numerator == 0 && ind < j - 1) ind++;
                        if (mF.Matrix[ind, j].Numerator == 0 && ind == j)
                        {
                            ind++;
                            while (mF.Matrix[ind, j].Numerator == 0 && ind < mF.N - 1) ind++;
                            if (mF.Matrix[ind, j].Numerator == 0 && (ind == mF.N - 1 || ind == mF.N))
                            {
                                if (Notify != null) Notify($"Невозможно решить! (Код 1.2)\n"); return false;
                            }
                        }
                    }*/
                    SimpleFractions kof = mF.Matrix[j, j];
                    //if (mF.Matrix[j, j].Numerator > 0)
                    //     kof = _sFM.Division(_sFM.Difference(mF.Matrix[j, j], new SimpleFractions(1, 1)), mF.Matrix[ind, j]);

                    for (int j1 = 0; j1 < mF.M; j1++)
                    {
                        mF.Matrix[j, j1] = _sFM.Division(mF.Matrix[j, j1], kof);
                    }
                    _mFM.Norm(mF);
                    //kof = _sFM.Division(_sFM.Difference(new SimpleFractions(1, 1), mF.Matrix[j, j]), mF.Matrix[ind, j]);
                    // _mFM.LineDifference(mF, j, ind, kof);

                    if (Notify != null) Notify($"*****({j + 1}) * {kof.toString()} \n");
                    if (Notify != null) Notify(mF.toString() + "\n");
                }
                for (int j1 = j + 1; j1 < mF.M; j1++)
                {
                    for (int i1 = 0; i1 < mF.N; i1++)
                    {
                        if (i1 != j)
                            mF.Matrix[i1, j1] =
                                _sFM.Difference(_sFM.Multiplication(mF.Matrix[j, j], mF.Matrix[i1, j1]),
                                                _sFM.Multiplication(mF.Matrix[j, j1], mF.Matrix[i1, j]));
                    }
                }
                for (int i = 0; i < mF.N; i++)
                {
                    if (i != j)
                    {
                        mF.Matrix[i, j].Denominator = 1;
                        mF.Matrix[i, j].Numerator = 0;
                    }
                }
                if (Notify != null) Notify(mF.toString() + "\n");
            }
            return true;
        }
        private bool Slu_decision(MatrixFractions matrix)
        {
            Print(matrix);
            bool flag1, flag2;
            for (int i = 0; i < matrix.N; i++)
            {
                flag1 = false;
                for (int j = 0; j < matrix.M - 1; j++)
                {
                    if (matrix.Matrix[i, j].Numerator == matrix.Matrix[i, j].Denominator) flag1 = true;
                }
                if(!flag1 && matrix.Matrix[i, matrix.M - 1].Numerator != 0) { if (Notify != null) Notify($"Невозможно решить! (Код 2.1)\n"); return false; }
            }
            return true;
        }

        private void Print(MatrixFractions matrix)
        {
            string str = "";
            for (int i = 0; i < matrix.N; i++)
            {
                for (int j = 0; j < matrix.M - 1; j++)
                {
                    if (matrix.Matrix[i, j].Numerator == 0) continue;
                    if (matrix.Matrix[i, j].Numerator == matrix.Matrix[i, j].Denominator)
                        str += "x" + (j + 1);
                    else
                        str += matrix.Matrix[i, j].toString() + " * x" + (j + 1);
                }
                str += " = " + matrix.Matrix[i, matrix.M - 1].toString() + "\n";
            }
            if (Notify != null) Notify(str + "\n");
        }
    }
}
