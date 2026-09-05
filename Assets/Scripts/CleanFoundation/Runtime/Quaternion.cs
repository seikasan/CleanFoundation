using System;
using System.Runtime.CompilerServices;
using UQuaternion = UnityEngine.Quaternion;
using UVector3 = UnityEngine.Vector3;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Quaternion の薄い Facade。
    /// 公開面は Unity の Quaternion に近づけ、回転計算は UnityEngine.Quaternion に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Quaternion : IEquatable<Quaternion>, IFormattable
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public const float kEpsilon = UQuaternion.kEpsilon;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UQuaternion)this)[index];

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
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        public static Quaternion identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UQuaternion.identity;
        }

        public Vector3 eulerAngles
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UQuaternion)this).eulerAngles;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                UQuaternion q = this;
                q.eulerAngles = (UVector3)value;
                this = q;
            }
        }

        public readonly Quaternion normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UQuaternion.Normalize((UQuaternion)this);
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
        public static Quaternion FromToRotation(
            Vector3 fromDirection,
            Vector3 toDirection)
            => UQuaternion.FromToRotation(
                (UVector3)fromDirection,
                (UVector3)toDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion FromToRotation(
            in Vector3 fromDirection,
            in Vector3 toDirection)
            => UQuaternion.FromToRotation(
                (UVector3)fromDirection,
                (UVector3)toDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Inverse(Quaternion rotation)
            => UQuaternion.Inverse((UQuaternion)rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Inverse(in Quaternion rotation)
            => UQuaternion.Inverse((UQuaternion)rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Slerp(
            Quaternion a,
            Quaternion b,
            float t)
            => UQuaternion.Slerp(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Slerp(
            in Quaternion a,
            in Quaternion b,
            float t)
            => UQuaternion.Slerp(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion SlerpUnclamped(
            Quaternion a,
            Quaternion b,
            float t)
            => UQuaternion.SlerpUnclamped(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion SlerpUnclamped(
            in Quaternion a,
            in Quaternion b,
            float t)
            => UQuaternion.SlerpUnclamped(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Lerp(
            Quaternion a,
            Quaternion b,
            float t)
            => UQuaternion.Lerp(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Lerp(
            in Quaternion a,
            in Quaternion b,
            float t)
            => UQuaternion.Lerp(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LerpUnclamped(
            Quaternion a,
            Quaternion b,
            float t)
            => UQuaternion.LerpUnclamped(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LerpUnclamped(
            in Quaternion a,
            in Quaternion b,
            float t)
            => UQuaternion.LerpUnclamped(
                (UQuaternion)a,
                (UQuaternion)b,
                t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion AngleAxis(float angle, Vector3 axis)
            => UQuaternion.AngleAxis(angle, (UVector3)axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion AngleAxis(float angle, in Vector3 axis)
            => UQuaternion.AngleAxis(angle, (UVector3)axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LookRotation(
            Vector3 forward,
            Vector3 upwards)
            => UQuaternion.LookRotation(
                (UVector3)forward,
                (UVector3)upwards);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LookRotation(
            in Vector3 forward,
            in Vector3 upwards)
            => UQuaternion.LookRotation(
                (UVector3)forward,
                (UVector3)upwards);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LookRotation(Vector3 forward)
            => UQuaternion.LookRotation((UVector3)forward);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion LookRotation(in Vector3 forward)
            => UQuaternion.LookRotation((UVector3)forward);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Quaternion a, Quaternion b)
            => UQuaternion.Dot(
                (UQuaternion)a,
                (UQuaternion)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(in Quaternion a, in Quaternion b)
            => UQuaternion.Dot(
                (UQuaternion)a,
                (UQuaternion)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(Vector3 view)
            => this = LookRotation(view);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(in Vector3 view)
            => this = LookRotation(in view);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(Vector3 view, Vector3 up)
            => this = LookRotation(view, up);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetLookRotation(in Vector3 view, in Vector3 up)
            => this = LookRotation(in view, in up);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(Quaternion a, Quaternion b)
            => UQuaternion.Angle(
                (UQuaternion)a,
                (UQuaternion)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(in Quaternion a, in Quaternion b)
            => UQuaternion.Angle(
                (UQuaternion)a,
                (UQuaternion)b);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Euler(float x, float y, float z)
            => UQuaternion.Euler(x, y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Euler(Vector3 euler)
            => UQuaternion.Euler((UVector3)euler);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Euler(in Vector3 euler)
            => UQuaternion.Euler((UVector3)euler);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void ToAngleAxis(
            out float angle,
            out Vector3 axis)
        {
            ((UQuaternion)this).ToAngleAxis(
                out angle,
                out UVector3 unityAxis);

            axis = unityAxis;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFromToRotation(
            Vector3 fromDirection,
            Vector3 toDirection)
            => this = FromToRotation(fromDirection, toDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFromToRotation(
            in Vector3 fromDirection,
            in Vector3 toDirection)
            => this = FromToRotation(in fromDirection, in toDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion RotateTowards(
            Quaternion from,
            Quaternion to,
            float maxDegreesDelta)
            => UQuaternion.RotateTowards(
                (UQuaternion)from,
                (UQuaternion)to,
                maxDegreesDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion RotateTowards(
            in Quaternion from,
            in Quaternion to,
            float maxDegreesDelta)
            => UQuaternion.RotateTowards(
                (UQuaternion)from,
                (UQuaternion)to,
                maxDegreesDelta);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Normalize(Quaternion q)
            => UQuaternion.Normalize((UQuaternion)q);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Normalize(in Quaternion q)
            => UQuaternion.Normalize((UQuaternion)q);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
            => this = UQuaternion.Normalize((UQuaternion)this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion operator *(
            Quaternion lhs,
            Quaternion rhs)
            => (UQuaternion)lhs * (UQuaternion)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(
            Quaternion rotation,
            Vector3 point)
            => (UQuaternion)rotation * (UVector3)point;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(
            Quaternion lhs,
            Quaternion rhs)
            => (UQuaternion)lhs == (UQuaternion)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(
            Quaternion lhs,
            Quaternion rhs)
            => (UQuaternion)lhs != (UQuaternion)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UQuaternion)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Quaternion other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Quaternion other)
            => ((UQuaternion)this).Equals((UQuaternion)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Quaternion other)
            => ((UQuaternion)this).Equals((UQuaternion)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UQuaternion)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UQuaternion)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(
            string format,
            IFormatProvider formatProvider)
            => ((UQuaternion)this).ToString(format, formatProvider);

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Quaternion を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UQuaternion(Quaternion value)
            => new(value.x, value.y, value.z, value.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion(UQuaternion value)
            => new(value.x, value.y, value.z, value.w);
    }
}
