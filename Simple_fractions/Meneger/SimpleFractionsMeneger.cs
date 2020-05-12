using System;
using System.Collections.Generic;
using System.Linq;

namespace Fractions
{
    public class SimpleFractionsMeneger
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public SimpleFractions Sum(SimpleFractions a, SimpleFractions b)
        {
            SimpleFractions otv = new SimpleFractions();
            if (a.Denominator == b.Denominator)
            {
                otv.Denominator = a.Denominator;
                otv.Numerator = a.Numerator + b.Numerator;
            }
            else
            {
                int nok = NOK(Convert.ToInt32(a.Denominator), Convert.ToInt32(b.Denominator));
                otv.Denominator = nok;
                otv.Numerator = a.Numerator * (nok / a.Denominator) + b.Numerator * (nok / b.Denominator);
            }
            return Norm(otv);
        }
        /// <summary>
        /// Разность
        /// </summary>
        public SimpleFractions Difference(SimpleFractions A, SimpleFractions B)
        {
            if (A.Numerator == 0) return Multiplication(Norm(B), new SimpleFractions(-1, 1));
            else if (B.Numerator == 0) return Norm(A);
            SimpleFractions otv = new SimpleFractions();
            SimpleFractions a = Norm(A), b = Norm(B);
            if (a.Denominator == b.Denominator)
            {
                otv.Denominator = a.Denominator;
                otv.Numerator = a.Numerator - b.Numerator;
            }
            else
            {
                int nok = NOK(Convert.ToInt32(a.Denominator), Convert.ToInt32(b.Denominator));
                otv.Denominator = nok;
                otv.Numerator = a.Numerator * (nok / a.Denominator) - b.Numerator * (nok / b.Denominator);
            }
            return Norm(otv);
        }
        /// <summary>
        /// НОК. Хз как, но работает
        /// </summary>
        public int NOK(int a, int b) { return (a * b) / gcd(a, b); }
        public int gcd(int a, int b) { return a != 0 ? gcd(b % a, a) : b; }
        /// <summary>
        /// Произведение
        /// </summary>
        public SimpleFractions Multiplication(SimpleFractions a, SimpleFractions b)
        {
            SimpleFractions otv = new SimpleFractions();
            otv.Denominator = a.Denominator * b.Denominator;
            otv.Numerator = a.Numerator * b.Numerator;
            return otv;
        }
        /// <summary>
        /// Деление
        /// </summary>
        public SimpleFractions Division(SimpleFractions a, SimpleFractions b)
        {
            SimpleFractions otv = new SimpleFractions();
            otv.Denominator = a.Denominator * b.Numerator;
            otv.Numerator = a.Numerator * b.Denominator;
            return Norm(otv);
        }
        /// <summary>
        /// Cокращение дроби
        /// </summary>
        public SimpleFractions Reduction(SimpleFractions simpleFractions)
        {
            SimpleFractions a = simpleFractions;
            if ((simpleFractions.Denominator < 0 && simpleFractions.Numerator >= 0) || (simpleFractions.Numerator < 0 && simpleFractions.Denominator < 0))
            {
                simpleFractions.Numerator *= -1;
                simpleFractions.Denominator *= -1;
            }
            var nod = NOD(new List<int> { a.Numerator, a.Denominator });
            if (nod != 1)
            {
                a.Denominator /= nod;
                a.Numerator /= nod;
            }
            //if (nod != 0)
            //{
            //    a.Denominator /= nod;
            //    a.Numerator /= nod;
            //    nod = NOD(a.Numerator, a.Denominator);
            //}
            return a;
        }
        /// <summary>
        /// Модель разложения числа на простые множители
        /// </summary>
        public class FactorizationModel
        {
            public int Number { get; set; }
            public int Quantity { get; set; }
        }
        /// <summary>
        /// Разложение числа на простые множители
        /// </summary>
        public List<FactorizationModel> Factorization(int m)
        {
            int n = m;
            List<FactorizationModel> otv = new List<FactorizationModel>();
            int div = 2;
            int num;
            if (n < 0)
            {
                otv.Add(new FactorizationModel { Number = -1, Quantity = 1 });
                n *= -1;
            }
            while (n > 1)
            {
                while (n % div == 0)
                {
                    num = otv.FindIndex(i => i.Number == div);
                    if (num == -1)
                    {
                        otv.Add(new FactorizationModel() { Number = div, Quantity = 1 });
                    }
                    else
                    {
                        otv[num].Quantity++;
                    }
                    n = n / div;
                }
                if (div == 2) div++;
                else div += 2;
            }
            return otv;
        }
        /// <summary>
        /// Наименьшее общее кратное, не работает с большими числами, как не иронично для int
        /// </summary>
        //private int NOK(int n, int m)
        //{
        //    int nok = 0;
        //    for (int i = Math.Max(n,m); i < (n * m + 1); i++)
        //    {
        //        if (i % n == 0 && i % m == 0)
        //        {
        //            nok = i;
        //        }
        //    }
        //    return nok;
        //}

        /// <summary>
        /// Наибольший общий делитель
        /// </summary>
        static int NOD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        /// <summary>
        /// Наибольший общий делитель
        /// </summary>
        public int NOD(List<int> list)
        {
            if (list.Count == 0) return 0;
            int i;
            int gcd = list[0];
            for (i = 0; i < list.Count - 1; i++)
                gcd = NOD(gcd, list[i + 1]);
            return gcd;
            //list.Sort();
            //List<FactorizationModel>[] fact = new List<FactorizationModel>[list.Count];
            ////int i = 0;
            //int nod = 1;
            //bool flag;
            //for (int i = 2; i <= list[list.Count-1]; i++)
            //{
            //    flag = true;
            //    foreach(var a in list)
            //    {
            //        if(a%i != 0)
            //        {
            //            flag = false;
            //        }
            //    }
            //    if(flag) nod = i;
            //}
            //return nod;
        }
        /// <summary>
        /// Модуль
        /// </summary>
        public SimpleFractions Abs(SimpleFractions fractions)
        {
            SimpleFractions simpleFractions = Norm(fractions);
            if (simpleFractions.Denominator < 0) simpleFractions.Denominator *= -1;
            if (simpleFractions.Numerator < 0) simpleFractions.Numerator *= -1;
            return simpleFractions;
        }
        /// <summary>
        /// Максимальный элемент (первый попавшийся)
        /// </summary>
        public SimpleFractions Max(SimpleFractions a, SimpleFractions b)
        {
            SimpleFractions a1 = Norm(a);
            SimpleFractions b1 = Norm(b);
            if (Difference(a1, b1).Numerator < 0) return b1;
            else return a1;
        }
        /// <summary>
        /// Приведение дроби к нормальному виду (сокращение)
        /// </summary>
        public SimpleFractions Norm(SimpleFractions simpleFractions)
        {
            SimpleFractions fractions = simpleFractions;
            if (fractions.Numerator == 0) { fractions.Denominator = 1; return fractions; }
            fractions = Reduction(fractions);
            if (NOD(new List<int> { fractions.Numerator, fractions.Denominator }) != 0)
            {
                int nod = NOD(new List<int> { fractions.Numerator, fractions.Denominator });
                fractions.Numerator /= nod;
                fractions.Denominator /= nod;
            }
            if (fractions.Denominator < 0)
            {
                fractions.Numerator *= -1;
                fractions.Denominator *= -1;
            }
            return fractions;
        }
        /// <summary>
        /// Модуль
        /// </summary>
        public SimpleFractions Mod(SimpleFractions fractions)
        {
            SimpleFractions f = new SimpleFractions();
            if (fractions.Numerator < 0) f.Numerator = fractions.Numerator * -1;
            if (fractions.Denominator < 0) f.Denominator = fractions.Denominator * -1;
            return f;
        }
        /// <summary>
        /// Поиск индекса минимального по модулю элемента. В случае пустого листа возвращается -1.
        /// </summary>
        public int SearchMax(List<SimpleFractions> list)
        {
            if (list == null || list.Count < 2) return -1;
            int idmax = 0;
            while (list[idmax] == null) { idmax++; if (list.Count == idmax) return -1; }
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] == null) continue;
                if (Comparisons(list[idmax], list[i]))
                {
                    idmax = i;
                }
            }
            return idmax;
        }
        /// <summary>
        /// Поиск индекса минимального по модулю элемента. В случае пустого листа возвращается -1.
        /// </summary>
        public int SearchMin(List<SimpleFractions> list)
        {
            if (list == null || list.Count < 2) return -1;
            int idmin = 0;
            while (list[idmin] == null) { idmin++; if (list.Count == idmin) return -1; }
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] == null) continue;
                if (!Comparisons(Mod(list[idmin]), Mod(list[i])))
                {
                    idmin = i;
                }
            }
            return idmin;
        }
        /// <summary>
        /// Если a < b, то true, иначе false
        /// </summary>
        public bool Comparisons(SimpleFractions a, SimpleFractions b)
        {
            var c = Difference(b, a);
            if ((c.Numerator > 0 && c.Denominator > 0) || (c.Denominator < 0 && c.Numerator < 0))
            {
                return true;
            }
            return false;
        }
    }
}
