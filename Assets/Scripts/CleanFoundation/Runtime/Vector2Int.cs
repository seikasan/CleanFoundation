using System;
using System.Runtime.CompilerServices;
using UVector2Int = UnityEngine.Vector2Int;

namespace CleanFoundation
{
    [Serializable]
    public partial struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        private UVector2Int _value;

        public int x
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.x;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.x = value;
        }

        public int y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.y;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.y = value;
        }

        public int this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value[index] = value;
        }

        public readonly float magnitude => _value.magnitude;
        public readonly int sqrMagnitude => _value.sqrMagnitude;

        public static Vector2Int zero => UVector2Int.zero;
        public static Vector2Int one => UVector2Int.one;
        public static Vector2Int up => UVector2Int.up;
        public static Vector2Int down => UVector2Int.down;
        public static Vector2Int left => UVector2Int.left;
        public static Vector2Int right => UVector2Int.right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int(int x, int y)
        {
            _value = new UVector2Int(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector2Int(UVector2Int value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int x, int y) => _value.Set(x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2Int a, Vector2Int b)
            => UVector2Int.Distance(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector2Int a, in Vector2Int b)
            => UVector2Int.Distance(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
            => UVector2Int.Min(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Min(in Vector2Int lhs, in Vector2Int rhs)
            => UVector2Int.Min(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
            => UVector2Int.Max(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Max(in Vector2Int lhs, in Vector2Int rhs)
            => UVector2Int.Max(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Scale(Vector2Int a, Vector2Int b)
            => UVector2Int.Scale(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Scale(in Vector2Int a, in Vector2Int b)
            => UVector2Int.Scale(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector2Int scale) => _value.Scale(scale._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(in Vector2Int scale) => _value.Scale(scale._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clamp(Vector2Int min, Vector2Int max)
            => _value.Clamp(min._value, max._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clamp(in Vector2Int min, in Vector2Int max)
            => _value.Clamp(min._value, max._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int FloorToInt(Vector2 v)
            => UVector2Int.FloorToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int FloorToInt(in Vector2 v)
            => UVector2Int.FloorToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int CeilToInt(Vector2 v)
            => UVector2Int.CeilToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int CeilToInt(in Vector2 v)
            => UVector2Int.CeilToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RoundToInt(Vector2 v)
            => UVector2Int.RoundToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RoundToInt(in Vector2 v)
            => UVector2Int.RoundToInt((UnityEngine.Vector2)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator -(Vector2Int v) => -v._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => a._value + b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => a._value - b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator *(Vector2Int a, Vector2Int b) => a._value * b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator *(int a, Vector2Int b) => a * b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator *(Vector2Int a, int b) => a._value * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int operator /(Vector2Int a, int b) => a._value / b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2Int lhs, Vector2Int rhs) => lhs._value == rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2Int lhs, Vector2Int rhs) => lhs._value != rhs._value;

        public override readonly bool Equals(object obj) => obj is Vector2Int other && Equals(other);
        public readonly bool Equals(Vector2Int other) => _value.Equals(other._value);
        public readonly bool Equals(in Vector2Int other) => _value.Equals(other._value);
        public override readonly int GetHashCode() => _value.GetHashCode();

        public override readonly string ToString() => _value.ToString();
        public readonly string ToString(string format) => _value.ToString(format);

        public readonly string ToString(string format, IFormatProvider formatProvider)
            => _value.ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(Vector2Int value)
            => new(value.x, value.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UVector2Int(Vector2Int value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2Int(UVector2Int value)
            => new(value);
    }
}
