using System;
using System.Runtime.CompilerServices;
using UVector3 = UnityEngine.Vector3;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Vector3 の薄い Facade。
    /// 公開面は Unity の Vector3 に近づけ、計算処理は UnityEngine.Vector3 に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public float x;
        public float y;
        public float z;

        public const float kEpsilon = UVector3.kEpsilon;
        public const float kEpsilonNormalSqrt = UVector3.kEpsilonNormalSqrt;

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UVector3)this)[index];

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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public readonly float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector3)this).magnitude;
        }

        public readonly float sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector3)this).sqrMagnitude;
        }

        public readonly Vector3 normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UVector3)this).normalized;
        }

        public static Vector3 zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.zero;
        }

        public static Vector3 one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.one;
        }

        public static Vector3 forward
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.forward;
        }

        public static Vector3 back
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.back;
        }

        public static Vector3 up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.up;
        }

        public static Vector3 down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.down;
        }

        public static Vector3 left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.left;
        }

        public static Vector3 right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.right;
        }

        public static Vector3 positiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.positiveInfinity;
        }

        public static Vector3 negativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UVector3.negativeInfinity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
            => UVector3.Slerp((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Slerp(in Vector3 a, in Vector3 b, float t)
            => UVector3.Slerp((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
            => UVector3.SlerpUnclamped((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SlerpUnclamped(in Vector3 a, in Vector3 b, float t)
            => UVector3.SlerpUnclamped((UVector3)a, (UVector3)b, t);

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            UVector3 unityNormal = normal;
            UVector3 unityTangent = tangent;

            UVector3.OrthoNormalize(ref unityNormal, ref unityTangent);

            normal = unityNormal;
            tangent = unityTangent;
        }

        public static void OrthoNormalize(
            ref Vector3 normal,
            ref Vector3 tangent,
            ref Vector3 binormal)
        {
            UVector3 unityNormal = normal;
            UVector3 unityTangent = tangent;
            UVector3 unityBinormal = binormal;

            UVector3.OrthoNormalize(
                ref unityNormal,
                ref unityTangent,
                ref unityBinormal);

            normal = unityNormal;
            tangent = unityTangent;
            binormal = unityBinormal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RotateTowards(
            Vector3 current,
            Vector3 target,
            float maxRadiansDelta,
            float maxMagnitudeDelta)
            => UVector3.RotateTowards(
                (UVector3)current,
                (UVector3)target,
                maxRadiansDelta,
                maxMagnitudeDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RotateTowards(
            in Vector3 current,
            in Vector3 target,
            float maxRadiansDelta,
            float maxMagnitudeDelta)
            => UVector3.RotateTowards(
                (UVector3)current,
                (UVector3)target,
                maxRadiansDelta,
                maxMagnitudeDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
            => UVector3.Lerp((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(in Vector3 a, in Vector3 b, float t)
            => UVector3.Lerp((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
            => UVector3.LerpUnclamped((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(in Vector3 a, in Vector3 b, float t)
            => UVector3.LerpUnclamped((UVector3)a, (UVector3)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 MoveTowards(
            Vector3 current,
            Vector3 target,
            float maxDistanceDelta)
            => UVector3.MoveTowards(
                (UVector3)current,
                (UVector3)target,
                maxDistanceDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 MoveTowards(
            in Vector3 current,
            in Vector3 target,
            float maxDistanceDelta)
            => UVector3.MoveTowards(
                (UVector3)current,
                (UVector3)target,
                maxDistanceDelta);

        /// <summary>
        /// deltaTime を明示的に受け取る版。
        /// Unity版の deltaTime 省略オーバーロードは Time.deltaTime に暗黙依存するため、
        /// この Facade では意図的に公開しない。
        /// </summary>
        public static Vector3 SmoothDamp(
            Vector3 current,
            Vector3 target,
            ref Vector3 currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
        {
            UVector3 velocity = currentVelocity;
            UVector3 result = UVector3.SmoothDamp(
                current,
                target,
                ref velocity,
                smoothTime,
                maxSpeed,
                deltaTime);

            currentVelocity = velocity;
            return result;
        }

        public static Vector3 SmoothDamp(
            in Vector3 current,
            in Vector3 target,
            ref Vector3 currentVelocity,
            float smoothTime,
            float maxSpeed,
            float deltaTime)
        {
            UVector3 velocity = currentVelocity;
            UVector3 result = UVector3.SmoothDamp(
                (UVector3)current,
                (UVector3)target,
                ref velocity,
                smoothTime,
                maxSpeed,
                deltaTime);

            currentVelocity = velocity;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Scale(Vector3 a, Vector3 b)
            => UVector3.Scale((UVector3)a, (UVector3)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Scale(in Vector3 a, in Vector3 b)
            => UVector3.Scale((UVector3)a, (UVector3)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector3 scale)
        {
            UVector3 value = this;
            value.Scale(scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(in Vector3 scale)
        {
            UVector3 value = this;
            value.Scale((UVector3)scale);
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
            => UVector3.Cross((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(in Vector3 lhs, in Vector3 rhs)
            => UVector3.Cross((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
            => UVector3.Reflect((UVector3)inDirection, (UVector3)inNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(in Vector3 inDirection, in Vector3 inNormal)
            => UVector3.Reflect((UVector3)inDirection, (UVector3)inNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 value)
            => UVector3.Normalize((UVector3)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(in Vector3 value)
            => UVector3.Normalize((UVector3)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            UVector3 value = this;
            value.Normalize();
            this = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector3 lhs, Vector3 rhs)
            => UVector3.Dot((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Vector3 lhs, in Vector3 rhs)
            => UVector3.Dot((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
            => UVector3.Project((UVector3)vector, (UVector3)onNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Project(in Vector3 vector, in Vector3 onNormal)
            => UVector3.Project((UVector3)vector, (UVector3)onNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
            => UVector3.ProjectOnPlane((UVector3)vector, (UVector3)planeNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ProjectOnPlane(
            in Vector3 vector,
            in Vector3 planeNormal)
            => UVector3.ProjectOnPlane((UVector3)vector, (UVector3)planeNormal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(Vector3 from, Vector3 to)
            => UVector3.Angle((UVector3)from, (UVector3)to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(in Vector3 from, in Vector3 to)
            => UVector3.Angle((UVector3)from, (UVector3)to);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
            => UVector3.SignedAngle(
                (UVector3)from,
                (UVector3)to,
                (UVector3)axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(
            in Vector3 from,
            in Vector3 to,
            in Vector3 axis)
            => UVector3.SignedAngle(
                (UVector3)from,
                (UVector3)to,
                (UVector3)axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3 a, Vector3 b)
            => UVector3.Distance((UVector3)a, (UVector3)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(in Vector3 a, in Vector3 b)
            => UVector3.Distance((UVector3)a, (UVector3)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
            => UVector3.ClampMagnitude((UVector3)vector, maxLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMagnitude(in Vector3 vector, float maxLength)
            => UVector3.ClampMagnitude((UVector3)vector, maxLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(Vector3 vector)
            => ((UVector3)vector).magnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(in Vector3 vector)
            => ((UVector3)vector).magnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(Vector3 vector)
            => ((UVector3)vector).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(in Vector3 vector)
            => ((UVector3)vector).sqrMagnitude;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
            => UVector3.Min((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(in Vector3 lhs, in Vector3 rhs)
            => UVector3.Min((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
            => UVector3.Max((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(in Vector3 lhs, in Vector3 rhs)
            => UVector3.Max((UVector3)lhs, (UVector3)rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UVector3)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UVector3)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format, IFormatProvider formatProvider)
            => ((UVector3)this).ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UVector3)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Vector3 other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Vector3 other)
            => ((UVector3)this).Equals((UVector3)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Vector3 other)
            => ((UVector3)this).Equals((UVector3)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 a, Vector3 b)
            => (UVector3)a + (UVector3)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 a, Vector3 b)
            => (UVector3)a - (UVector3)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 a)
            => -(UVector3)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 a, float d)
            => (UVector3)a * d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(float d, Vector3 a)
            => d * (UVector3)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 a, float d)
            => (UVector3)a / d;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
            => (UVector3)lhs == (UVector3)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
            => (UVector3)lhs != (UVector3)rhs;

        /// <summary>
        /// Foundation の Vector2 / Vector3 間の変換。
        /// UnityEngine.Vector2 / Vector3 と同じ変換方向を提供する。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(Vector3 value)
            => new(value.x, value.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Vector2 value)
            => new(value.x, value.y, 0f);

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Vector3 を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UVector3(Vector3 value)
            => new(value.x, value.y, value.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(UVector3 value)
            => new(value.x, value.y, value.z);
    }
}
