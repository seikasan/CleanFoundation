using System;
using System.Runtime.CompilerServices;
using URect = UnityEngine.Rect;
using UVector2 = UnityEngine.Vector2;
using UVector3 = UnityEngine.Vector3;

namespace CleanFoundation.Geometry
{
    /// <summary>
    /// UnityEngine.Rect の薄い Facade。
    /// </summary>
    [Serializable]
    public struct Rect : IEquatable<Rect>, IFormattable
    {
        [UnityEngine.SerializeField]
        private URect _value;

        public static Rect zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => URect.zero;
        }

        public float x
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.x;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.x = value;
        }

        public float y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.y;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.y = value;
        }

        public float width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.width;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.width = value;
        }

        public float height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.height;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.height = value;
        }

        public Vector2 position
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.position;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.position = value;
        }

        public Vector2 center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.center;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.center = value;
        }

        public Vector2 size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.size;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.size = value;
        }

        public Vector2 min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.min;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.min = value;
        }

        public Vector2 max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.max;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.max = value;
        }

        public float xMin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.xMin;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.xMin = value;
        }

        public float yMin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.yMin;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.yMin = value;
        }

        public float xMax
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.xMax;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.xMax = value;
        }

        public float yMax
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.yMax;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.yMax = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rect(float x, float y, float width, float height)
        {
            _value = new URect(x, y, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Rect(Vector2 position, Vector2 size)
        {
            _value = new URect(position, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Rect(URect value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect MinMaxRect(
            float xmin,
            float ymin,
            float xmax,
            float ymax)
            => URect.MinMaxRect(xmin, ymin, xmax, ymax);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float x, float y, float width, float height)
            => _value.Set(x, y, width, height);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector2 point)
            => _value.Contains((UVector2)point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector3 point)
            => _value.Contains((UVector3)point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector3 point, bool allowInverse)
            => _value.Contains((UVector3)point, allowInverse);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Overlaps(Rect other)
            => _value.Overlaps(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Overlaps(Rect other, bool allowInverse)
            => _value.Overlaps(other._value, allowInverse);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NormalizedToPoint(
            Rect rectangle,
            Vector2 normalizedRectCoordinates)
            => URect.NormalizedToPoint(
                rectangle._value,
                (UVector2)normalizedRectCoordinates);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 PointToNormalized(
            Rect rectangle,
            Vector2 point)
            => URect.PointToNormalized(
                rectangle._value,
                (UVector2)point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => _value.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => _value.ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(
            string format,
            IFormatProvider formatProvider)
            => _value.ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => _value.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Rect other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Rect other)
            => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rect lhs, Rect rhs)
            => lhs._value == rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rect lhs, Rect rhs)
            => lhs._value != rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator URect(Rect value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Rect(URect value)
            => new(value);
    }
}
