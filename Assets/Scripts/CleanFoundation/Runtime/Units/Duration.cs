using System;

namespace CleanFoundation.Units
{
    /// <summary>
    /// 時間間隔。内部単位は秒。
    /// </summary>
    [Serializable]
    public readonly struct Duration
    {
        public float Seconds { get; }

        public float Milliseconds => Seconds * 1000f;

        public static Duration Zero => new(0f);

        private Duration(float seconds)
        {
            Seconds = seconds;
        }

        public static Duration FromSeconds(float seconds)
            => new(seconds);

        public static Duration FromMilliseconds(float milliseconds)
            => new(milliseconds / 1000f);

        public static Duration FromTimeSpan(TimeSpan value)
            => new((float)value.TotalSeconds);

        public TimeSpan ToTimeSpan()
            => TimeSpan.FromSeconds(Seconds);

        public static Duration operator +(Duration a, Duration b)
            => new(a.Seconds + b.Seconds);

        public static Duration operator -(Duration a, Duration b)
            => new(a.Seconds - b.Seconds);

        public static Duration operator -(Duration value)
            => new(-value.Seconds);

        public static Duration operator *(Duration value, float scalar)
            => new(value.Seconds * scalar);

        public static Duration operator *(float scalar, Duration value)
            => value * scalar;

        public static Duration operator /(Duration value, float scalar)
            => new(value.Seconds / scalar);

        public static float operator /(Duration a, Duration b)
            => a.Seconds / b.Seconds;

        public static bool operator <(Duration a, Duration b)
            => a.Seconds < b.Seconds;

        public static bool operator >(Duration a, Duration b)
            => a.Seconds > b.Seconds;

        public static bool operator <=(Duration a, Duration b)
            => a.Seconds <= b.Seconds;

        public static bool operator >=(Duration a, Duration b)
            => a.Seconds >= b.Seconds;

        public override string ToString()
            => $"{Seconds}s";
    }
}
