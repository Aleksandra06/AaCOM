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
            if (Denominator == 1) return Numerator.ToString();
            return Numerator + "/" + Denominator;
        }
    }
}
