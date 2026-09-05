using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 2D変位。各成分の単位は Unity world unit。
    /// </summary>
    [Serializable]
    public readonly struct Displacement2
    {
        public Vector2 Value { get; }

        public Distance Magnitude => Distance.FromUnits(Value.magnitude);

        public static Displacement2 Zero => new(Vector2.zero);

        public Displacement2(Vector2 value)
        {
            Value = value;
        }

        public static Displacement2 operator +(Displacement2 a, Displacement2 b)
            => new(a.Value + b.Value);

        public static Displacement2 operator -(Displacement2 a, Displacement2 b)
            => new(a.Value - b.Value);

        public static Displacement2 operator -(Displacement2 value)
            => new(-value.Value);

        public static Displacement2 operator *(Displacement2 value, float scalar)
            => new(value.Value * scalar);

        public static Displacement2 operator *(float scalar, Displacement2 value)
            => value * scalar;

        public static Displacement2 operator /(Displacement2 value, float scalar)
            => new(value.Value / scalar);

        public static Velocity2 operator /(Displacement2 displacement, Duration duration)
            => new(displacement.Value / duration.Seconds);

        public override string ToString()
            => $"{Value}u";
    }
}
