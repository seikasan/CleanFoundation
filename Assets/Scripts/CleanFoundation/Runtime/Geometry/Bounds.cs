using System;
using System.Runtime.CompilerServices;
using UBounds = UnityEngine.Bounds;

namespace CleanFoundation.Geometry
{
    /// <summary>
    /// UnityEngine.Bounds の薄い Facade。
    /// </summary>
    [Serializable]
    public struct Bounds : IEquatable<Bounds>, IFormattable
    {
        [UnityEngine.SerializeField]
        private UBounds _value;

        public Vector3 center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.center;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.center = value;
        }

        public Vector3 size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.size;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.size = value;
        }

        public Vector3 extents
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.extents;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.extents = value;
        }

        public Vector3 min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.min;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.min = value;
        }

        public Vector3 max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.max;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bounds(Vector3 center, Vector3 size)
        {
            _value = new UBounds(center, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Bounds(UBounds value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinMax(Vector3 min, Vector3 max)
            => _value.SetMinMax(min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(Vector3 point)
            => _value.Encapsulate(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encapsulate(Bounds bounds)
            => _value.Encapsulate(bounds._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Expand(float amount)
            => _value.Expand(amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Expand(Vector3 amount)
            => _value.Expand(amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Contains(Vector3 point)
            => _value.Contains(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Intersects(Bounds bounds)
            => _value.Intersects(bounds._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float SqrDistance(Vector3 point)
            => _value.SqrDistance(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 ClosestPoint(Vector3 point)
            => _value.ClosestPoint(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IntersectRay(Ray ray)
            => _value.IntersectRay(ray);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IntersectRay(Ray ray, out float distance)
            => _value.IntersectRay(ray, out distance);

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
            => obj is Bounds other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Bounds other)
            => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Bounds lhs, Bounds rhs)
            => lhs._value == rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Bounds lhs, Bounds rhs)
            => lhs._value != rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBounds(Bounds value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Bounds(UBounds value)
            => new(value);
    }
}
