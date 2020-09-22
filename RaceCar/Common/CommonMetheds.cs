using System;

namespace RaceCar
{
    public static class CommonMetheds
    {
        private static Random random = new Random();
        private static short Xpoint;

        public static short NewPoints()
        {
        Here: Xpoint = (short)random.Next(30, 360);
            while (Xpoint < 235 && Xpoint > 215)
                goto Here;
            return Xpoint;
        }
    }
}