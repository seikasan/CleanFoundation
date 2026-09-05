using System;
using System.Runtime.CompilerServices;
using UVector2 = UnityEngine.Vector2;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Vector2 の薄い Facade。
    /// 公開面は Unity の Vector2 に近づけ、計算処理は UnityEngine.Vector2 に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public float x;
        public float y;

        public const float kEpsilon = UVector2.kEpsilon;
        public const float kEpsilonNormalSqrt = UVector2.kEpsilonNormalSqrt;

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UVector2)this)[index];

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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        public readonly float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector2)this).magnitude;
        }

        public readonly float sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector2)this).sqrMagnitude;
        }

        public readonly Vector2 normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector2)this).normalized;
        }

        public static Vector2 zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.zero;
        }

        public static Vector2 one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.one;
        }

        public static Vector2 up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.up;
        }

        public static Vector2 down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.down;
        }

        public static Vector2 left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.left;
        }

        public static Vector2 right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.right;
        }

        public static Vector2 positiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.positiveInfinity;
        }

        public static Vector2 negativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector2.negativeInfinity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float newX, float newY)
        {
            x = newX;
            y = newY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
            => UVector2.Lerp(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(in Vector2 a, in Vector2 b, float t)
            => UVector2.Lerp(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
            => UVector2.LerpUnclamped(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(in Vector2 a, in Vector2 b, float t)
            => UVector2.LerpUnclamped(a, b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
            => UVector2.MoveTowards(current, target, maxDistanceDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 MoveTowards(in Vector2 current, in Vector2 target, float maxDistanceDelta)
            => UVector2.MoveTowards(current, target, maxDistanceDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Scale(Vector2 a, Vector2 b)
            => UVector2.Scale(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Scale(in Vector2 a, in Vector2 b)
            => UVector2.Scale(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector2 scale)
        {
            UVector2 value = this;
            value.Scale(scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(in Vector2 scale)
        {
            UVector2 value = this;
            value.Scale(scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 value)
            => UVector2.Normalize(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(in Vector2 value)
            => UVector2.Normalize(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            UVector2 value = this;
            value.Normalize();
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
            => UVector2.Reflect(inDirection, inNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(in Vector2 inDirection, in Vector2 inNormal)
            => UVector2.Reflect(inDirection, inNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Perpendicular(Vector2 inDirection)
            => UVector2.Perpendicular(inDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Perpendicular(in Vector2 inDirection)
            => UVector2.Perpendicular(inDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 lhs, Vector2 rhs)
            => UVector2.Dot(lhs, rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Vector2 lhs, in Vector2 rhs)
            => UVector2.Dot(lhs, rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(Vector2 from, Vector2 to)
            => UVector2.Angle(from, to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(in Vector2 from, in Vector2 to)
            => UVector2.Angle(from, to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(Vector2 from, Vector2 to)
            => UVector2.SignedAngle(from, to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(in Vector2 from, in Vector2 to)
            => UVector2.SignedAngle(from, to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 a, Vector2 b)
            => UVector2.Distance(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector2 a, in Vector2 b)
            => UVector2.Distance(a, b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
            => UVector2.ClampMagnitude(vector, maxLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitude(in Vector2 vector, float maxLength)
            => UVector2.ClampMagnitude(vector, maxLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(Vector2 a)
            => ((UVector2)a).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(in Vector2 a)
            => ((UVector2)a).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float SqrMagnitude()
            => ((UVector2)this).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
            => UVector2.Min(lhs, rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(in Vector2 lhs, in Vector2 rhs)
            => UVector2.Min(lhs, rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
            => UVector2.Max(lhs, rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(in Vector2 lhs, in Vector2 rhs)
            => UVector2.Max(lhs, rhs);

        /// <summary>
        /// deltaTime を明示的に受け取る版。
        /// Unity版の deltaTime 省略オーバーロードは Time.deltaTime に暗黙依存するため、
        /// この Facade では意図的に公開しない。
        /// </summary>
        public static Vector2 SmoothDamp(
            Vector2 current,
            Vector2 target,
            ref Vector2 currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
        {
            UVector2 velocity = currentVelocity;
            UVector2 result = UVector2.SmoothDamp(
                current,
                target,
                ref velocity,
                smoothTime,
                maxSpeed,
                deltaTime);

            currentVelocity = velocity;
            return result;
        }

        public static Vector2 SmoothDamp(
            in Vector2 current,
            in Vector2 target,
            ref Vector2 currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
        {
            UVector2 velocity = currentVelocity;
            UVector2 result = UVector2.SmoothDamp(
                current,
                target,
                ref velocity,
                smoothTime,
                maxSpeed,
                deltaTime);

            currentVelocity = velocity;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UVector2)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UVector2)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
            => ((UVector2)this).ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UVector2)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Vector2 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Vector2 other)
            => ((UVector2)this).Equals((UVector2)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Vector2 other)
            => ((UVector2)this).Equals((UVector2)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 a, Vector2 b)
            => (UVector2)a + (UVector2)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 a, Vector2 b)
            => (UVector2)a - (UVector2)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 a, Vector2 b)
            => (UVector2)a * (UVector2)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 a, Vector2 b)
            => (UVector2)a / (UVector2)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 a)
            => -(UVector2)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 a, float d)
            => (UVector2)a * d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float d, Vector2 a)
            => d * (UVector2)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 a, float d)
            => (UVector2)a / d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
            => (UVector2)lhs == (UVector2)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
            => (UVector2)lhs != (UVector2)rhs;

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Vector2 を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UVector2(Vector2 value)
            => new(value.x, value.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(UVector2 value)
            => new(value.x, value.y);
    }
}
