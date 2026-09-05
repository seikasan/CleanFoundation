using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// スカラー距離。単位は Unity world unit。
    /// </summary>
    [Serializable]
    public readonly struct Distance
    {
        public float Units { get; }

        public static Distance Zero => new(0f);

        private Distance(float units)
        {
            Units = units;
        }

        public static Distance FromUnits(float units)
            => new(units);

        public static Distance Abs(Distance value)
            => new(Mathf.Abs(value.Units));

        public static Distance Min(Distance a, Distance b)
            => new(Mathf.Min(a.Units, b.Units));

        public static Distance Max(Distance a, Distance b)
            => new(Mathf.Max(a.Units, b.Units));

        public static Distance operator +(Distance a, Distance b)
            => new(a.Units + b.Units);

        public static Distance operator -(Distance a, Distance b)
            => new(a.Units - b.Units);

        public static Distance operator -(Distance value)
            => new(-value.Units);

        public static Distance operator *(Distance value, float scalar)
            => new(value.Units * scalar);

        public static Distance operator *(float scalar, Distance value)
            => value * scalar;

        public static Distance operator /(Distance value, float scalar)
            => new(value.Units / scalar);

        public static float operator /(Distance a, Distance b)
            => a.Units / b.Units;

        public static Speed operator /(Distance distance, Duration duration)
            => Speed.FromUnitsPerSecond(distance.Units / duration.Seconds);

        public static Duration operator /(Distance distance, Speed speed)
            => Duration.FromSeconds(distance.Units / speed.UnitsPerSecond);

        public static bool operator <(Distance a, Distance b)
            => a.Units < b.Units;

        public static bool operator >(Distance a, Distance b)
            => a.Units > b.Units;

        public static bool operator <=(Distance a, Distance b)
            => a.Units <= b.Units;

        public static bool operator >=(Distance a, Distance b)
            => a.Units >= b.Units;

        public override string ToString()
            => $"{Units}u";
    }
}
