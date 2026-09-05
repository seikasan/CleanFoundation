using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 2D加速度。各成分の単位は Unity world unit / second²。
    /// </summary>
    [Serializable]
    public readonly struct Acceleration2
    {
        public Vector2 Value { get; }

        public float Magnitude => Value.magnitude;

        public static Acceleration2 Zero => new(Vector2.zero);

        public Acceleration2(Vector2 value)
        {
            Value = value;
        }

        public static Acceleration2 operator +(Acceleration2 a, Acceleration2 b)
            => new(a.Value + b.Value);

        public static Acceleration2 operator -(Acceleration2 a, Acceleration2 b)
            => new(a.Value - b.Value);

        public static Acceleration2 operator -(Acceleration2 value)
            => new(-value.Value);

        public static Acceleration2 operator *(Acceleration2 value, float scalar)
            => new(value.Value * scalar);

        public static Acceleration2 operator *(float scalar, Acceleration2 value)
            => value * scalar;

        public static Acceleration2 operator /(Acceleration2 value, float scalar)
            => new(value.Value / scalar);

        public static Velocity2 operator *(Acceleration2 acceleration, Duration duration)
            => new(acceleration.Value * duration.Seconds);

        public static Velocity2 operator *(Duration duration, Acceleration2 acceleration)
            => acceleration * duration;

        public override string ToString()
            => $"{Value}u/s²";
    }
}
