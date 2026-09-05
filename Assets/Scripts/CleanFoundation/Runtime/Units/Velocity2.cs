using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 2D速度。各成分の単位は Unity world unit / second。
    /// </summary>
    [Serializable]
    public readonly struct Velocity2
    {
        public Vector2 Value { get; }

        public Speed Speed => Speed.FromUnitsPerSecond(Value.magnitude);

        public static Velocity2 Zero => new(Vector2.zero);

        public Velocity2(Vector2 value)
        {
            Value = value;
        }

        public static Velocity2 FromDirection(Vector2 direction, Speed speed)
            => new(direction.normalized * speed.UnitsPerSecond);

        public static Velocity2 operator +(Velocity2 a, Velocity2 b)
            => new(a.Value + b.Value);

        public static Velocity2 operator -(Velocity2 a, Velocity2 b)
            => new(a.Value - b.Value);

        public static Velocity2 operator -(Velocity2 value)
            => new(-value.Value);

        public static Velocity2 operator *(Velocity2 value, float scalar)
            => new(value.Value * scalar);

        public static Velocity2 operator *(float scalar, Velocity2 value)
            => value * scalar;

        public static Velocity2 operator /(Velocity2 value, float scalar)
            => new(value.Value / scalar);

        public static Displacement2 operator *(Velocity2 velocity, Duration duration)
            => new(velocity.Value * duration.Seconds);

        public static Displacement2 operator *(Duration duration, Velocity2 velocity)
            => velocity * duration;

        public static Acceleration2 operator /(Velocity2 velocity, Duration duration)
            => new(velocity.Value / duration.Seconds);

        public override string ToString()
            => $"{Value}u/s";
    }
}
