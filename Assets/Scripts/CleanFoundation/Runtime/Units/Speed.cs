using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// スカラーの速さ。単位は Unity world unit / second。
    /// </summary>
    [Serializable]
    public readonly struct Speed
    {
        public float UnitsPerSecond { get; }

        public static Speed Zero => new(0f);

        private Speed(float unitsPerSecond)
        {
            UnitsPerSecond = unitsPerSecond;
        }

        public static Speed FromUnitsPerSecond(float unitsPerSecond)
            => new(unitsPerSecond);

        public static Speed Abs(Speed value)
            => new(Mathf.Abs(value.UnitsPerSecond));

        public static Speed operator +(Speed a, Speed b)
            => new(a.UnitsPerSecond + b.UnitsPerSecond);

        public static Speed operator -(Speed a, Speed b)
            => new(a.UnitsPerSecond - b.UnitsPerSecond);

        public static Speed operator -(Speed value)
            => new(-value.UnitsPerSecond);

        public static Speed operator *(Speed value, float scalar)
            => new(value.UnitsPerSecond * scalar);

        public static Speed operator *(float scalar, Speed value)
            => value * scalar;

        public static Speed operator /(Speed value, float scalar)
            => new(value.UnitsPerSecond / scalar);

        public static float operator /(Speed a, Speed b)
            => a.UnitsPerSecond / b.UnitsPerSecond;

        public static Distance operator *(Speed speed, Duration duration)
            => Distance.FromUnits(speed.UnitsPerSecond * duration.Seconds);

        public static Distance operator *(Duration duration, Speed speed)
            => speed * duration;

        public static bool operator <(Speed a, Speed b)
            => a.UnitsPerSecond < b.UnitsPerSecond;

        public static bool operator >(Speed a, Speed b)
            => a.UnitsPerSecond > b.UnitsPerSecond;

        public static bool operator <=(Speed a, Speed b)
            => a.UnitsPerSecond <= b.UnitsPerSecond;

        public static bool operator >=(Speed a, Speed b)
            => a.UnitsPerSecond >= b.UnitsPerSecond;

        public override string ToString()
            => $"{UnitsPerSecond}u/s";
    }
}
