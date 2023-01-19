using System;

namespace com.sharmas4.MentalHealthDisorder
{
    public static class Tweens
    {

        private const float PI = (float)Math.PI;
        private const float EASE_OUT_ELASTIC_MULTIPLIER = (2 * PI) / 3f;
        private const float EASE_OUT_BOUNCE_MULTIPLIER_N = 7.5625f;
        private const float EASE_OUT_BOUNCE_MULTIPLIER_D = 2.75f;
        //private float EASE_OUT_BOUNCE_MULTIPLIER_N;


        public static float EaseOutElastic(float currentTime)
        {
            if (currentTime == 0) return 0;
            else if (currentTime == 1) return 1;
            else return (float)Math.Pow(2, -10 * currentTime) * (float)Math.Sin((currentTime * 10 - 0.75) * EASE_OUT_ELASTIC_MULTIPLIER) + 1;
        }

        public static float EaseOutBounce(float currentTime)
        {
            if (currentTime < 1f / EASE_OUT_BOUNCE_MULTIPLIER_D) return EASE_OUT_BOUNCE_MULTIPLIER_N * currentTime * currentTime;
            else if (currentTime < 2f / EASE_OUT_BOUNCE_MULTIPLIER_D) return EASE_OUT_BOUNCE_MULTIPLIER_N * (currentTime -= 1.5f / EASE_OUT_BOUNCE_MULTIPLIER_D) * currentTime + 0.75f;
            else if (currentTime < 2.5f / EASE_OUT_BOUNCE_MULTIPLIER_D) return EASE_OUT_BOUNCE_MULTIPLIER_N * (currentTime -= 2.25f / EASE_OUT_BOUNCE_MULTIPLIER_D) * currentTime + 0.9375f;
            else return EASE_OUT_BOUNCE_MULTIPLIER_N * (currentTime -= 2.625f / EASE_OUT_BOUNCE_MULTIPLIER_D) * currentTime + 0.984375f;
        }

        // Slow to fast
        public static float EaseInExponential(float currentTime)
        {
            if (currentTime == 0) return 0;
            else return (float)Math.Pow(2, 10 * (currentTime - 1));
        }

        // Fast to slow
        public static float EaseOutExponential(float currentTime)
        {
            if (currentTime == 1) return 1;
            else return (1 - (float)Math.Pow(2, -10 * currentTime));
        }

        public static float EaseInQuadratic(float currentTime)
        {
            return currentTime * currentTime;
        }

        public static float EaseOutQuadratic(float currentTime)
        {
            return -currentTime * (currentTime - 2);
        }

        public static float EaseInCubic(float currentTime)
        {
            return currentTime * currentTime * currentTime;
        }

        public static float EaseOutCubic(float currentTime)
        {
            currentTime = currentTime - 1;
            return (currentTime * currentTime * currentTime + 1);
        }

        public static float Linear(float currentTime)
        {
            return currentTime;
        }
    }
}