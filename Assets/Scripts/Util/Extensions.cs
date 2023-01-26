namespace UnityEngine
{
    public static class Extensions
    {
        public static Sign ToSign(this int value)
        {
            return value == 0 ? Sign.Zero : value < 0 ? Sign.Negative : Sign.Positive;
        }
    }
}
