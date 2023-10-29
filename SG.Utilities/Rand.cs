using System;

namespace SG.Utilities
{
    public static class Rand
    {
        private static readonly Random s_random = new((int)DateTime.UtcNow.Ticks);

        public static bool FlipCoin()
        {
            double d = s_random.NextDouble();
            return (d >= 0.5);
        }
    }
}
