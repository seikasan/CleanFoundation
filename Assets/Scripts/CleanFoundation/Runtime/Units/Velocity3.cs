using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 3D速度。各成分の単位は Unity world unit / second。
    /// </summary>
    [Serializable]
    public readonly struct Velocity3
    {
        public Vector3 Value { get; }

        public Speed Speed => Speed.FromUnitsPerSecond(Value.magnitude);

        public static Velocity3 Zero => new(Vector3.zero);

        public Velocity3(Vector3 value)
        {
            Value = value;
        }

        public static Velocity3 FromDirection(Vector3 direction, Speed speed)
            => new(direction.normalized * speed.UnitsPerSecond);

        public static Velocity3 operator +(Velocity3 a, Velocity3 b)
            => new(a.Value + b.Value);

        public static Velocity3 operator -(Velocity3 a, Velocity3 b)
            => new(a.Value - b.Value);

        public static Velocity3 operator -(Velocity3 value)
            => new(-value.Value);

        public static Velocity3 operator *(Velocity3 value, float scalar)
            => new(value.Value * scalar);

        public static Velocity3 operator *(float scalar, Velocity3 value)
            => value * scalar;

        public static Velocity3 operator /(Velocity3 value, float scalar)
            => new(value.Value / scalar);

        public static Displacement3 operator *(Velocity3 velocity, Duration duration)
            => new(velocity.Value * duration.Seconds);

        public static Displacement3 operator *(Duration duration, Velocity3 velocity)
            => velocity * duration;

        public static Acceleration3 operator /(Velocity3 velocity, Duration duration)
            => new(velocity.Value / duration.Seconds);

        public override string ToString()
            => $"{Value}u/s";
    }
}
