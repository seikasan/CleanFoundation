using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 3D変位。各成分の単位は Unity world unit。
    /// </summary>
    [Serializable]
    public readonly struct Displacement3
    {
        public Vector3 Value { get; }

        public Distance Magnitude => Distance.FromUnits(Value.magnitude);

        public static Displacement3 Zero => new(Vector3.zero);

        public Displacement3(Vector3 value)
        {
            Value = value;
        }

        public static Displacement3 operator +(Displacement3 a, Displacement3 b)
            => new(a.Value + b.Value);

        public static Displacement3 operator -(Displacement3 a, Displacement3 b)
            => new(a.Value - b.Value);

        public static Displacement3 operator -(Displacement3 value)
            => new(-value.Value);

        public static Displacement3 operator *(Displacement3 value, float scalar)
            => new(value.Value * scalar);

        public static Displacement3 operator *(float scalar, Displacement3 value)
            => value * scalar;

        public static Displacement3 operator /(Displacement3 value, float scalar)
            => new(value.Value / scalar);

        public static Velocity3 operator /(Displacement3 displacement, Duration duration)
            => new(displacement.Value / duration.Seconds);

        public override string ToString()
            => $"{Value}u";
    }
}
