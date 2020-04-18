using System;
using System.Collections.Generic;

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
                long nok = NOK(Convert.ToInt32(a.Denominator), Convert.ToInt32(b.Denominator));
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
            var nod = NOD(new List<long> { a.Numerator, a.Denominator });
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
            public long Number { get; set; }
            public int Quantity { get; set; }
        }
        /// <summary>
        /// Разложение числа на простые множители
        /// </summary>
        public List<FactorizationModel> Factorization(long m)
        {
            long n = m;
            List<FactorizationModel> otv = new List<FactorizationModel>();
            long div = 2;
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
        /// Наименьшее общее кратное, не работает с большими числами, как не иронично для long
        /// </summary>
        private long NOK(long n, long m)
        {
            long nok = 0;
            for (long i = Math.Max(n,m); i < (n * m + 1); i++)
            {
                if (i % n == 0 && i % m == 0)
                {
                    nok = i;
                }
            }
            return nok;
        }

        /// <summary>
        /// Наибольший общий делитель
        /// </summary>
        static long NOD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        /// <summary>
        /// Наибольший общий делитель
        /// </summary>
        public long NOD(List<long> list)
        {
            if (list.Count == 0) return 0;
            int i;
            long gcd = list[0];
            for (i = 0; i < list.Count - 1; i++)
                gcd = NOD(gcd, list[i + 1]);
            return gcd;
            //list.Sort();
            //List<FactorizationModel>[] fact = new List<FactorizationModel>[list.Count];
            ////long i = 0;
            //long nod = 1;
            //bool flag;
            //for (long i = 2; i <= list[list.Count-1]; i++)
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
            if (fractions.Numerator == 0) fractions.Denominator = 1;
            fractions = Reduction(fractions);
            if (NOD(new List<long> { fractions.Numerator, fractions.Denominator }) != 0)
            {
                long nod = NOD(new List<long> { fractions.Numerator, fractions.Denominator });
                fractions.Numerator /= nod;
                fractions.Denominator /= nod;
            }
            if(fractions.Denominator < 0)
            {
                fractions.Numerator *= -1;
                fractions.Denominator *= -1;
            }
            return fractions;
        }
    }
}
