namespace Fractions
{
    public class SimpleFractions
    {
        /// <summary>
        /// Числитель
        /// </summary>
        public long Numerator { get; set; }
        /// <summary>
        /// Знаменатель
        /// </summary>
        public long Denominator { get; set; }

        public SimpleFractions()
        {
            Numerator = 0;
            Denominator = 1;
        }
        public SimpleFractions(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public string toString()
        {
            if (Numerator == 0)
            {
                return "0";
            }
            if (Denominator == 1) return Numerator.ToString();
            return Numerator + "/" + Denominator;
        }
        public double GetValue()
        {
            if (Denominator != 0)
            {
                return Numerator / Denominator;
            }
            else
            {
                return double.MaxValue;
            }
        }
    }
}
