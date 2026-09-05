using System;
using System.Runtime.CompilerServices;
using UVector3Int = UnityEngine.Vector3Int;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Vector3Int の薄い Facade。
    /// 公開面は Unity の Vector3Int に近づけ、計算処理は UnityEngine.Vector3Int に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Vector3Int : IEquatable<Vector3Int>, IFormattable
    {
        private UVector3Int _value;

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

        public int z
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.z;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.z = value;
        }

        public int this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value[index];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value[index] = value;
        }

        public readonly float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _value.magnitude;
        }

        public readonly int sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _value.sqrMagnitude;
        }

        public static Vector3Int zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.zero;
        }

        public static Vector3Int one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.one;
        }

        public static Vector3Int up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.up;
        }

        public static Vector3Int down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.down;
        }

        public static Vector3Int left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.left;
        }

        public static Vector3Int right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.right;
        }

        public static Vector3Int forward
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.forward;
        }

        public static Vector3Int back
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3Int.back;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int(int x, int y)
        {
            _value = new UVector3Int(x, y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int(int x, int y, int z)
        {
            _value = new UVector3Int(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3Int(UVector3Int value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int x, int y, int z)
            => _value.Set(x, y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3Int a, Vector3Int b)
            => UVector3Int.Distance(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector3Int a, in Vector3Int b)
            => UVector3Int.Distance(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
            => UVector3Int.Min(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Min(in Vector3Int lhs, in Vector3Int rhs)
            => UVector3Int.Min(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
            => UVector3Int.Max(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Max(in Vector3Int lhs, in Vector3Int rhs)
            => UVector3Int.Max(lhs._value, rhs._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Scale(Vector3Int a, Vector3Int b)
            => UVector3Int.Scale(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Scale(in Vector3Int a, in Vector3Int b)
            => UVector3Int.Scale(a._value, b._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector3Int scale)
            => _value.Scale(scale._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(in Vector3Int scale)
            => _value.Scale(scale._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clamp(Vector3Int min, Vector3Int max)
            => _value.Clamp(min._value, max._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clamp(in Vector3Int min, in Vector3Int max)
            => _value.Clamp(min._value, max._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int FloorToInt(Vector3 v)
            => UVector3Int.FloorToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int FloorToInt(in Vector3 v)
            => UVector3Int.FloorToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int CeilToInt(Vector3 v)
            => UVector3Int.CeilToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int CeilToInt(in Vector3 v)
            => UVector3Int.CeilToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int RoundToInt(Vector3 v)
            => UVector3Int.RoundToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int RoundToInt(in Vector3 v)
            => UVector3Int.RoundToInt((UnityEngine.Vector3)v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator +(Vector3Int a, Vector3Int b)
            => a._value + b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator -(Vector3Int a, Vector3Int b)
            => a._value - b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator *(Vector3Int a, Vector3Int b)
            => a._value * b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator -(Vector3Int a)
            => -a._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator *(Vector3Int a, int b)
            => a._value * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator *(int a, Vector3Int b)
            => a * b._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int operator /(Vector3Int a, int b)
            => a._value / b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
            => lhs._value == rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
            => lhs._value != rhs._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Vector3Int other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Vector3Int other)
            => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Vector3Int other)
            => _value.Equals(other._value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => _value.GetHashCode();

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

        /// <summary>
        /// Foundation の Vector3 への変換。
        /// UnityEngine.Vector3Int → UnityEngine.Vector3 と同じく implicit。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Vector3Int value)
            => new(value.x, value.y, value.z);

        /// <summary>
        /// Foundation の Vector2Int への変換。
        /// UnityEngine.Vector3Int → UnityEngine.Vector2Int と同じく explicit。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2Int(Vector3Int value)
            => new(value.x, value.y);

        /// <summary>
        /// Foundation の Vector2Int からの変換。
        /// UnityEngine.Vector2Int → UnityEngine.Vector3Int と同じく explicit。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3Int(Vector2Int value)
            => new(value.x, value.y, 0);

        /// <summary>
        /// Unity 境界との相互変換。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UVector3Int(Vector3Int value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3Int(UVector3Int value)
            => new(value);
    }
}
