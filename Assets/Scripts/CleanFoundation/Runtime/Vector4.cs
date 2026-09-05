using System;
using System.Runtime.CompilerServices;
using UVector2 = UnityEngine.Vector2;
using UVector3 = UnityEngine.Vector3;
using UVector4 = UnityEngine.Vector4;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Vector4 の薄い Facade。
    /// 公開面は Unity の Vector4 に近づけ、計算処理は UnityEngine.Vector4 に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Vector4 : IEquatable<Vector4>, IFormattable
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public const float kEpsilon = UVector4.kEpsilon;

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UVector4)this)[index];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }
        }

        public readonly Vector4 normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector4.Normalize((UVector4)this);
        }

        public readonly float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector4)this).magnitude;
        }

        public readonly float sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector4)this).sqrMagnitude;
        }

        public static Vector4 zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector4.zero;
        }

        public static Vector4 one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector4.one;
        }

        public static Vector4 positiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector4.positiveInfinity;
        }

        public static Vector4 negativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector4.negativeInfinity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            w = 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0f;
            w = 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float newX, float newY, float newZ, float newW)
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
            => UVector4.Lerp((UVector4)a, (UVector4)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(in Vector4 a, in Vector4 b, float t)
            => UVector4.Lerp((UVector4)a, (UVector4)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
            => UVector4.LerpUnclamped((UVector4)a, (UVector4)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(in Vector4 a, in Vector4 b, float t)
            => UVector4.LerpUnclamped((UVector4)a, (UVector4)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MoveTowards(
            Vector4 current,
            Vector4 target,
            float maxDistanceDelta)
            => UVector4.MoveTowards(
                (UVector4)current,
                (UVector4)target,
                maxDistanceDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MoveTowards(
            in Vector4 current,
            in Vector4 target,
            float maxDistanceDelta)
            => UVector4.MoveTowards(
                (UVector4)current,
                (UVector4)target,
                maxDistanceDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Scale(Vector4 a, Vector4 b)
            => UVector4.Scale((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Scale(in Vector4 a, in Vector4 b)
            => UVector4.Scale((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector4 scale)
        {
            UVector4 value = this;
            value.Scale((UVector4)scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(in Vector4 scale)
        {
            UVector4 value = this;
            value.Scale((UVector4)scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize(Vector4 a)
            => UVector4.Normalize((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize(in Vector4 a)
            => UVector4.Normalize((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
            => this = UVector4.Normalize((UVector4)this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector4 a, Vector4 b)
            => UVector4.Dot((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Vector4 a, in Vector4 b)
            => UVector4.Dot((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Project(Vector4 a, Vector4 b)
            => UVector4.Project((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Project(in Vector4 a, in Vector4 b)
            => UVector4.Project((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector4 a, Vector4 b)
            => UVector4.Distance((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector4 a, in Vector4 b)
            => UVector4.Distance((UVector4)a, (UVector4)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(Vector4 a)
            => UVector4.Magnitude((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(in Vector4 a)
            => UVector4.Magnitude((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(Vector4 a)
            => UVector4.SqrMagnitude((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(in Vector4 a)
            => UVector4.SqrMagnitude((UVector4)a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float SqrMagnitude()
            => ((UVector4)this).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Min(Vector4 lhs, Vector4 rhs)
            => UVector4.Min((UVector4)lhs, (UVector4)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Min(in Vector4 lhs, in Vector4 rhs)
            => UVector4.Min((UVector4)lhs, (UVector4)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Max(Vector4 lhs, Vector4 rhs)
            => UVector4.Max((UVector4)lhs, (UVector4)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Max(in Vector4 lhs, in Vector4 rhs)
            => UVector4.Max((UVector4)lhs, (UVector4)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UVector4)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Vector4 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Vector4 other)
            => ((UVector4)this).Equals((UVector4)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Vector4 other)
            => ((UVector4)this).Equals((UVector4)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UVector4)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UVector4)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(
            string format,
            IFormatProvider formatProvider)
            => ((UVector4)this).ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator +(Vector4 a, Vector4 b)
            => (UVector4)a + (UVector4)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(Vector4 a, Vector4 b)
            => (UVector4)a - (UVector4)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(Vector4 a)
            => -(UVector4)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(Vector4 a, float d)
            => (UVector4)a * d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(float d, Vector4 a)
            => d * (UVector4)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator /(Vector4 a, float d)
            => (UVector4)a / d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4 lhs, Vector4 rhs)
            => (UVector4)lhs == (UVector4)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4 lhs, Vector4 rhs)
            => (UVector4)lhs != (UVector4)rhs;

        /// <summary>
        /// Foundation の Vector3 / Vector4 間の変換。
        /// UnityEngine.Vector3 / Vector4 と同じ変換方向を提供する。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(Vector3 value)
            => new(value.x, value.y, value.z, 0f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Vector4 value)
            => new(value.x, value.y, value.z);

        /// <summary>
        /// Foundation の Vector2 / Vector4 間の変換。
        /// UnityEngine.Vector2 / Vector4 と同じ変換方向を提供する。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(Vector2 value)
            => new(value.x, value.y, 0f, 0f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(Vector4 value)
            => new(value.x, value.y);

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Vector4 を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UVector4(Vector4 value)
            => new(value.x, value.y, value.z, value.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(UVector4 value)
            => new(value.x, value.y, value.z, value.w);
    }
}
