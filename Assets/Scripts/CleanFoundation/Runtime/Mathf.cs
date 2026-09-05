using System.Runtime.CompilerServices;
using UMathf = UnityEngine.Mathf;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Mathf の薄い Facade。
    /// 公開面は Unity の Mathf に近づけ、計算処理は UnityEngine.Mathf に委譲する。
    /// </summary>
    public static class Mathf
    {
        public const float PI = UMathf.PI;
        public const float Infinity = UMathf.Infinity;
        public const float NegativeInfinity = UMathf.NegativeInfinity;
        public const float Deg2Rad = UMathf.Deg2Rad;
        public const float Rad2Deg = UMathf.Rad2Deg;

        public static readonly float Epsilon = UMathf.Epsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GammaToLinearSpace(float value)
            => UMathf.GammaToLinearSpace(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LinearToGammaSpace(float value)
            => UMathf.LinearToGammaSpace(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color CorrelatedColorTemperatureToRGB(float kelvin)
            => UMathf.CorrelatedColorTemperatureToRGB(kelvin);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort FloatToHalf(float val)
            => UMathf.FloatToHalf(val);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float HalfToFloat(ushort val)
            => UMathf.HalfToFloat(val);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PerlinNoise(float x, float y)
            => UMathf.PerlinNoise(x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PerlinNoise1D(float x)
            => UMathf.PerlinNoise1D(x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float f)
            => UMathf.Sin(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float f)
            => UMathf.Cos(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float f)
            => UMathf.Tan(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float f)
            => UMathf.Asin(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float f)
            => UMathf.Acos(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float f)
            => UMathf.Atan(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
            => UMathf.Atan2(y, x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float f)
            => UMathf.Sqrt(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float f)
            => UMathf.Abs(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int value)
            => UMathf.Abs(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float a, float b)
            => UMathf.Min(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(params float[] values)
            => UMathf.Min(values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int a, int b)
            => UMathf.Min(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(params int[] values)
            => UMathf.Min(values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b)
            => UMathf.Max(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(params float[] values)
            => UMathf.Max(values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int a, int b)
            => UMathf.Max(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(params int[] values)
            => UMathf.Max(values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float f, float p)
            => UMathf.Pow(f, p);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float power)
            => UMathf.Exp(power);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float f, float p)
            => UMathf.Log(f, p);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float f)
            => UMathf.Log(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log10(float f)
            => UMathf.Log10(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceil(float f)
            => UMathf.Ceil(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float f)
            => UMathf.Floor(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float f)
            => UMathf.Round(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CeilToInt(float f)
            => UMathf.CeilToInt(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloorToInt(float f)
            => UMathf.FloorToInt(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(float f)
            => UMathf.RoundToInt(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sign(float f)
            => UMathf.Sign(f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
            => UMathf.Clamp(value, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max)
            => UMathf.Clamp(value, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(float value)
            => UMathf.Clamp01(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float a, float b, float t)
            => UMathf.Lerp(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpUnclamped(float a, float b, float t)
            => UMathf.LerpUnclamped(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpAngle(float a, float b, float t)
            => UMathf.LerpAngle(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MoveTowards(float current, float target, float maxDelta)
            => UMathf.MoveTowards(current, target, maxDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MoveTowardsAngle(float current, float target, float maxDelta)
            => UMathf.MoveTowardsAngle(current, target, maxDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothStep(float from, float to, float t)
            => UMathf.SmoothStep(from, to, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Gamma(float value, float absmax, float gamma)
            => UMathf.Gamma(value, absmax, gamma);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(float a, float b)
            => UMathf.Approximately(a, b);

        /// <summary>
        /// deltaTime を明示的に受け取る版。
        /// Unity版の省略オーバーロードは Time.deltaTime に暗黙依存するため、
        /// この Facade では意図的に公開しない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothDamp(
            float current,
            float target,
            ref float currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
            => UMathf.SmoothDamp(
                current,
                target,
                ref currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime);

        /// <summary>
        /// deltaTime を明示的に受け取る版。
        /// Unity版の省略オーバーロードは Time.deltaTime に暗黙依存するため、
        /// この Facade では意図的に公開しない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothDampAngle(
            float current,
            float target,
            ref float currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
            => UMathf.SmoothDampAngle(
                current,
                target,
                ref currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Repeat(float t, float length)
            => UMathf.Repeat(t, length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PingPong(float t, float length)
            => UMathf.PingPong(t, length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InverseLerp(float a, float b, float value)
            => UMathf.InverseLerp(a, b, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DeltaAngle(float current, float target)
            => UMathf.DeltaAngle(current, target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextPowerOfTwo(int value)
            => UMathf.NextPowerOfTwo(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClosestPowerOfTwo(int value)
            => UMathf.ClosestPowerOfTwo(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int value)
            => UMathf.IsPowerOfTwo(value);
    }
}
