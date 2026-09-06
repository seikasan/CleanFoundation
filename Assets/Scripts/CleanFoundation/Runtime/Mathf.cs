using System;

using System.Runtime.CompilerServices;
namespace CleanFoundation
{
    public static class Mathf
    {
        public const float PI = 3.1415927f;
        public const float Infinity = float.PositiveInfinity;
        public const float NegativeInfinity = float.NegativeInfinity;
        public const float Deg2Rad = 0.017453292f;
        public const float Rad2Deg = 57.29578f;
        private const float FloatMinNormal = 1.17549435E-38f;
        private static readonly float FloatMinDenormal = float.Epsilon;
        public static readonly float Epsilon = FloatMinDenormal == 0f ? FloatMinNormal : FloatMinDenormal;

        public static float GammaToLinearSpace(float value)
        {
            if (value <= 0.04045f) return value / 12.92f;
            if (value < 1f) return Pow((value + 0.055f) / 1.055f, 2.4f);
            return Pow(value, 2.2f);
        }

        public static float LinearToGammaSpace(float value)
        {
            if (value <= 0f) return 0f;
            if (value <= 0.0031308f) return 12.92f * value;
            if (value < 1f) return 1.055f * Pow(value, 0.41666667f) - 0.055f;
            return Pow(value, 0.45454545f);
        }

        public static Color CorrelatedColorTemperatureToRGB(float kelvin)
        {
            kelvin = Clamp(kelvin, 1000f, 40000f) / 100f;
            double r, g, b;
            if (kelvin <= 66.0)
            {
                r = 255.0;
                g = 99.4708025861 * Math.Log(kelvin) - 161.1195681661;
                b = kelvin <= 19.0 ? 0.0 : 138.5177312231 * Math.Log(kelvin - 10.0) - 305.0447927307;
            }
            else
            {
                r = 329.698727446 * Math.Pow(kelvin - 60.0, -0.1332047592);
                g = 288.1221695283 * Math.Pow(kelvin - 60.0, -0.0755148492);
                b = 255.0;
            }
            return new Color(Clamp((float)r / 255f, 0f, 1f), Clamp((float)g / 255f, 0f, 1f), Clamp((float)b / 255f, 0f, 1f), 1f);
        }

        public static ushort FloatToHalf(float value)
        {
            uint bits = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            uint sign = (bits >> 16) & 0x8000u;
            uint exponent = (bits >> 23) & 0xffu;
            uint mantissa = bits & 0x7fffffu;

            if (exponent == 255u)
                return (ushort)(sign | (mantissa == 0 ? 0x7c00u : 0x7e00u));

            int halfExp = (int)exponent - 127 + 15;
            if (halfExp >= 31) return (ushort)(sign | 0x7c00u);
            if (halfExp <= 0)
            {
                if (halfExp < -10) return (ushort)sign;
                mantissa |= 0x800000u;
                int shift = 14 - halfExp;
                uint halfMantissa = mantissa >> shift;
                uint roundBit = 1u << (shift - 1);
                if ((mantissa & roundBit) != 0 && ((mantissa & (roundBit - 1)) != 0 || (halfMantissa & 1) != 0)) halfMantissa++;
                return (ushort)(sign | halfMantissa);
            }

            uint result = sign | ((uint)halfExp << 10) | (mantissa >> 13);
            uint remainder = mantissa & 0x1fffu;
            if (remainder > 0x1000u || (remainder == 0x1000u && (result & 1u) != 0)) result++;
            return (ushort)result;
        }

        public static float HalfToFloat(ushort value)
        {
            uint sign = (uint)(value & 0x8000) << 16;
            uint exponent = (uint)(value >> 10) & 0x1fu;
            uint mantissa = (uint)value & 0x3ffu;
            uint bits;
            if (exponent == 0)
            {
                if (mantissa == 0) bits = sign;
                else
                {
                    int e = -14;
                    while ((mantissa & 0x400u) == 0) { mantissa <<= 1; e--; }
                    mantissa &= 0x3ffu;
                    bits = sign | (uint)(e + 127) << 23 | mantissa << 13;
                }
            }
            else if (exponent == 31)
                bits = sign | 0x7f800000u | mantissa << 13;
            else
                bits = sign | (exponent + 112u) << 23 | mantissa << 13;
            return BitConverter.ToSingle(BitConverter.GetBytes(bits), 0);
        }

        private static readonly int[] PerlinPermutation =
        {
            151,160,137,91,90,15,131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190,6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,88,237,149,56,87,174,20,
            125,136,171,168,68,175,74,165,71,134,139,48,27,166,77,146,158,231,83,111,229,122,60,211,133,230,220,
            105,92,41,55,46,245,40,244,102,143,54,65,25,63,161,1,216,80,73,209,76,132,187,208,89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,226,250,124,123,5,202,38,147,118,126,255,
            82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,223,183,170,213,119,248,152,2,44,154,163,70,221,
            153,101,155,167,43,172,9,129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,235,249,14,239,107,49,192,214,31,181,199,
            106,157,184,84,204,176,115,121,50,45,127,4,150,254,138,236,205,93,222,114,67,29,24,72,243,141,128,
            195,78,66,215,61,156,180
        };

        public static float PerlinNoise(float x, float y)
        {
            int xi = FloorToInt(x) & 255;
            int yi = FloorToInt(y) & 255;
            float xf = x - Floor(x);
            float yf = y - Floor(y);
            float u = Fade(xf);
            float v = Fade(yf);
            int aa = Perm(Perm(xi) + yi);
            int ab = Perm(Perm(xi) + yi + 1);
            int ba = Perm(Perm(xi + 1) + yi);
            int bb = Perm(Perm(xi + 1) + yi + 1);
            float x1 = LerpUnclamped(Grad(aa, xf, yf), Grad(ba, xf - 1f, yf), u);
            float x2 = LerpUnclamped(Grad(ab, xf, yf - 1f), Grad(bb, xf - 1f, yf - 1f), u);
            return LerpUnclamped(x1, x2, v) * 0.5f + 0.5f;
        }

        public static float PerlinNoise1D(float x) => PerlinNoise(x, 0f);
        private static int Perm(int i) => PerlinPermutation[i & 255];
        private static float Fade(float t) => t * t * t * (t * (t * 6f - 15f) + 10f);
        private static float Grad(int hash, float x, float y)
        {
            switch (hash & 7)
            {
                case 0: return x + y;
                case 1: return -x + y;
                case 2: return x - y;
                case 3: return -x - y;
                case 4: return x;
                case 5: return -x;
                case 6: return y;
                default: return -y;
            }
        }

        public static float Sin(float f) => (float)Math.Sin(f);
        public static float Cos(float f) => (float)Math.Cos(f);
        public static float Tan(float f) => (float)Math.Tan(f);
        public static float Asin(float f) => (float)Math.Asin(f);
        public static float Acos(float f) => (float)Math.Acos(f);
        public static float Atan(float f) => (float)Math.Atan(f);
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float Sqrt(float f) => (float)Math.Sqrt(f);
        public static float Abs(float f) => Math.Abs(f);
        public static int Abs(int value) => Math.Abs(value);
        public static float Min(float a, float b) => a < b ? a : b;
        public static float Min(params float[] values) { if (values.Length == 0) return 0f; float r = values[0]; for (int i = 1; i < values.Length; i++) if (values[i] < r) r = values[i]; return r; }
        public static int Min(int a, int b) => a < b ? a : b;
        public static int Min(params int[] values) { if (values.Length == 0) return 0; int r = values[0]; for (int i = 1; i < values.Length; i++) if (values[i] < r) r = values[i]; return r; }
        public static float Max(float a, float b) => a > b ? a : b;
        public static float Max(params float[] values) { if (values.Length == 0) return 0f; float r = values[0]; for (int i = 1; i < values.Length; i++) if (values[i] > r) r = values[i]; return r; }
        public static int Max(int a, int b) => a > b ? a : b;
        public static int Max(params int[] values) { if (values.Length == 0) return 0; int r = values[0]; for (int i = 1; i < values.Length; i++) if (values[i] > r) r = values[i]; return r; }
        public static float Pow(float f, float p) => (float)Math.Pow(f, p);
        public static float Exp(float power) => (float)Math.Exp(power);
        public static float Log(float f, float p) => (float)Math.Log(f, p);
        public static float Log(float f) => (float)Math.Log(f);
        public static float Log10(float f) => (float)Math.Log10(f);
        public static float Ceil(float f) => (float)Math.Ceiling(f);
        public static float Floor(float f) => (float)Math.Floor(f);
        public static float Round(float f) => (float)Math.Round(f, MidpointRounding.ToEven);
        public static int CeilToInt(float f) => (int)Math.Ceiling(f);
        public static int FloorToInt(float f) => (int)Math.Floor(f);
        public static int RoundToInt(float f) => (int)Math.Round(f, MidpointRounding.ToEven);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Sign(float f) => f >= 0f ? 1f : -1f;
        public static float Clamp(float value, float min, float max) { if (value < min) return min; if (value > max) return max; return value; }
        public static int Clamp(int value, int min, int max) { if (value < min) return min; if (value > max) return max; return value; }
        public static float Clamp01(float value) { if (value < 0f) return 0f; if (value > 1f) return 1f; return value; }
        public static float Lerp(float a, float b, float t) => a + (b - a) * Clamp01(t);
        public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;
        public static float LerpAngle(float a, float b, float t) { float delta = Repeat(b - a, 360f); if (delta > 180f) delta -= 360f; return a + delta * Clamp01(t); }
        public static float MoveTowards(float current, float target, float maxDelta) { if (Abs(target - current) <= maxDelta) return target; return current + Sign(target - current) * maxDelta; }
        public static float MoveTowardsAngle(float current, float target, float maxDelta) { float delta = DeltaAngle(current, target); if (-maxDelta < delta && delta < maxDelta) return target; target = current + delta; return MoveTowards(current, target, maxDelta); }
        public static float SmoothStep(float from, float to, float t) { t = Clamp01(t); t = -2f * t * t * t + 3f * t * t; return to * t + from * (1f - t); }
        public static float Gamma(float value, float absmax, float gamma) { bool negative = value < 0f; float abs = Abs(value); if (abs > absmax) return negative ? -abs : abs; float result = Pow(abs / absmax, gamma) * absmax; return negative ? -result : result; }
        public static bool Approximately(float a, float b) => Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Max(0.0001f, smoothTime);
            float omega = 2f / smoothTime;
            float x = omega * deltaTime;
            float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
            float change = current - target;
            float originalTarget = target;
            float maxChange = maxSpeed * smoothTime;
            change = Clamp(change, -maxChange, maxChange);
            target = current - change;
            float temp = (currentVelocity + omega * change) * deltaTime;
            currentVelocity = (currentVelocity - omega * temp) * exp;
            float output = target + (change + temp) * exp;
            if ((originalTarget - current > 0f) == (output > originalTarget))
            {
                output = originalTarget;
                currentVelocity = (output - originalTarget) / deltaTime;
            }
            return output;
        }

        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            target = current + DeltaAngle(current, target);
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static float Repeat(float t, float length) => Clamp(t - Floor(t / length) * length, 0f, length);
        public static float PingPong(float t, float length) { t = Repeat(t, length * 2f); return length - Abs(t - length); }
        public static float InverseLerp(float a, float b, float value) => a != b ? Clamp01((value - a) / (b - a)) : 0f;
        public static float DeltaAngle(float current, float target) { float delta = Repeat(target - current, 360f); if (delta > 180f) delta -= 360f; return delta; }
        public static int NextPowerOfTwo(int value) { value--; value |= value >> 16; value |= value >> 8; value |= value >> 4; value |= value >> 2; value |= value >> 1; return value + 1; }
        public static int ClosestPowerOfTwo(int value) { int next = NextPowerOfTwo(value); int prev = next >> 1; return value - prev < next - value ? prev : next; }
        public static bool IsPowerOfTwo(int value) => (value & (value - 1)) == 0;
    }
}
