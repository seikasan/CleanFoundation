using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 角度。内部表現はラジアン。
    /// Unity API の degree / radian 混在を型で明示する。
    /// </summary>
    [Serializable]
    public readonly struct Angle
    {
        public float Radians { get; }

        public float Degrees => Radians * Mathf.Rad2Deg;

        public static Angle Zero => new(0f);

        private Angle(float radians)
        {
            Radians = radians;
        }

        public static Angle FromRadians(float radians)
            => new(radians);

        public static Angle FromDegrees(float degrees)
            => new(degrees * Mathf.Deg2Rad);

        public Angle Normalized360()
            => FromDegrees(Mathf.Repeat(Degrees, 360f));

        public Angle Normalized180()
        {
            float degrees = Mathf.Repeat(Degrees + 180f, 360f) - 180f;
            return FromDegrees(degrees);
        }

        public float Sin() => Mathf.Sin(Radians);
        public float Cos() => Mathf.Cos(Radians);
        public float Tan() => Mathf.Tan(Radians);

        public static Angle Delta(Angle from, Angle to)
            => FromDegrees(Mathf.DeltaAngle(from.Degrees, to.Degrees));

        public static Angle Lerp(Angle a, Angle b, float t)
            => FromDegrees(Mathf.LerpAngle(a.Degrees, b.Degrees, t));

        public static Angle operator +(Angle a, Angle b)
            => new(a.Radians + b.Radians);

        public static Angle operator -(Angle a, Angle b)
            => new(a.Radians - b.Radians);

        public static Angle operator -(Angle value)
            => new(-value.Radians);

        public static Angle operator *(Angle value, float scalar)
            => new(value.Radians * scalar);

        public static Angle operator *(float scalar, Angle value)
            => value * scalar;

        public static Angle operator /(Angle value, float scalar)
            => new(value.Radians / scalar);

        public static float operator /(Angle a, Angle b)
            => a.Radians / b.Radians;

        public static AngularSpeed operator /(Angle angle, Duration duration)
            => AngularSpeed.FromRadiansPerSecond(angle.Radians / duration.Seconds);

        public static bool operator <(Angle a, Angle b)
            => a.Radians < b.Radians;

        public static bool operator >(Angle a, Angle b)
            => a.Radians > b.Radians;

        public static bool operator <=(Angle a, Angle b)
            => a.Radians <= b.Radians;

        public static bool operator >=(Angle a, Angle b)
            => a.Radians >= b.Radians;

        public override string ToString()
            => $"{Degrees}°";
    }
}
