using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 角速度。内部表現は radians / second。
    /// </summary>
    [Serializable]
    public readonly struct AngularSpeed
    {
        public float RadiansPerSecond { get; }

        public float DegreesPerSecond => RadiansPerSecond * Mathf.Rad2Deg;

        public static AngularSpeed Zero => new(0f);

        private AngularSpeed(float radiansPerSecond)
        {
            RadiansPerSecond = radiansPerSecond;
        }

        public static AngularSpeed FromRadiansPerSecond(float value)
            => new(value);

        public static AngularSpeed FromDegreesPerSecond(float value)
            => new(value * Mathf.Deg2Rad);

        public static AngularSpeed operator +(AngularSpeed a, AngularSpeed b)
            => new(a.RadiansPerSecond + b.RadiansPerSecond);

        public static AngularSpeed operator -(AngularSpeed a, AngularSpeed b)
            => new(a.RadiansPerSecond - b.RadiansPerSecond);

        public static AngularSpeed operator -(AngularSpeed value)
            => new(-value.RadiansPerSecond);

        public static AngularSpeed operator *(AngularSpeed value, float scalar)
            => new(value.RadiansPerSecond * scalar);

        public static AngularSpeed operator *(float scalar, AngularSpeed value)
            => value * scalar;

        public static AngularSpeed operator /(AngularSpeed value, float scalar)
            => new(value.RadiansPerSecond / scalar);

        public static Angle operator *(AngularSpeed speed, Duration duration)
            => Angle.FromRadians(speed.RadiansPerSecond * duration.Seconds);

        public static Angle operator *(Duration duration, AngularSpeed speed)
            => speed * duration;

        public override string ToString()
            => $"{DegreesPerSecond}°/s";
    }
}
