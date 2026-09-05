using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 3D加速度。各成分の単位は Unity world unit / second²。
    /// </summary>
    [Serializable]
    public readonly struct Acceleration3
    {
        public Vector3 Value { get; }

        public float Magnitude => Value.magnitude;

        public static Acceleration3 Zero => new(Vector3.zero);

        public Acceleration3(Vector3 value)
        {
            Value = value;
        }

        public static Acceleration3 operator +(Acceleration3 a, Acceleration3 b)
            => new(a.Value + b.Value);

        public static Acceleration3 operator -(Acceleration3 a, Acceleration3 b)
            => new(a.Value - b.Value);

        public static Acceleration3 operator -(Acceleration3 value)
            => new(-value.Value);

        public static Acceleration3 operator *(Acceleration3 value, float scalar)
            => new(value.Value * scalar);

        public static Acceleration3 operator *(float scalar, Acceleration3 value)
            => value * scalar;

        public static Acceleration3 operator /(Acceleration3 value, float scalar)
            => new(value.Value / scalar);

        public static Velocity3 operator *(Acceleration3 acceleration, Duration duration)
            => new(acceleration.Value * duration.Seconds);

        public static Velocity3 operator *(Duration duration, Acceleration3 acceleration)
            => acceleration * duration;

        public override string ToString()
            => $"{Value}u/s²";
    }
}
