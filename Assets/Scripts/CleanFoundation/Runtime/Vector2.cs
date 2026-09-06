using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CleanFoundation
{
    [Serializable]
    public partial struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public float x;
        public float y;
        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;

        public float this[int index]
        {
             [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get
            {
                switch (index) { case 0: return x; case 1: return y; default: throw new IndexOutOfRangeException("Invalid Vector2 index!"); }
            }
             [MethodImpl(MethodImplOptions.AggressiveInlining)] set
            {
                switch (index) { case 0: x = value; break; case 1: y = value; break; default: throw new IndexOutOfRangeException("Invalid Vector2 index!"); }
            }
        }

        public readonly float magnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)Math.Sqrt((double)x * x + (double)y * y); }
        public readonly float sqrMagnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => x * x + y * y; }
        public readonly Vector2 normalized { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Normalize(this); }
        public static Vector2 zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(0f, 0f); }
        public static Vector2 one { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(1f, 1f); }
        public static Vector2 up { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(0f, 1f); }
        public static Vector2 down { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(0f, -1f); }
        public static Vector2 left { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(-1f, 0f); }
        public static Vector2 right { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(1f, 0f); }
        public static Vector2 positiveInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(float.PositiveInfinity, float.PositiveInfinity); }
        public static Vector2 negativeInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2(float.NegativeInfinity, float.NegativeInfinity); }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector2(float x, float y) { this.x = x; this.y = y; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(float newX, float newY) { x = newX; y = newY; }

         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => LerpUnclamped(a, b, Mathf.Clamp01(t));
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Lerp(in Vector2 a, in Vector2 b, float t) => LerpUnclamped(a, b, Mathf.Clamp01(t));
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t) => new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 LerpUnclamped(in Vector2 a, in Vector2 b, float t) => new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta) => MoveTowards(in current, in target, maxDistanceDelta);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 MoveTowards(in Vector2 current, in Vector2 target, float maxDistanceDelta)
        {
            float dx = target.x - current.x, dy = target.y - current.y;
            float sq = dx * dx + dy * dy;
            if (sq == 0f || (maxDistanceDelta >= 0f && sq <= maxDistanceDelta * maxDistanceDelta)) return target;
            float dist = (float)Math.Sqrt(sq);
            return new Vector2(current.x + dx / dist * maxDistanceDelta, current.y + dy / dist * maxDistanceDelta);
        }

         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Scale(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Scale(in Vector2 a, in Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(Vector2 scale) { x *= scale.x; y *= scale.y; }
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(in Vector2 scale) { x *= scale.x; y *= scale.y; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Normalize(Vector2 value) => Normalize(in value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Normalize(in Vector2 value)
        {
            float mag = value.magnitude;
            return mag > kEpsilon ? value / mag : zero;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Normalize() { float mag = magnitude; if (mag > kEpsilon) { x /= mag; y /= mag; } else { x = 0f; y = 0f; } }

         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal) => Reflect(in inDirection, in inNormal);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Reflect(in Vector2 inDirection, in Vector2 inNormal) { float f = -2f * Dot(inNormal, inDirection); return new Vector2(f * inNormal.x + inDirection.x, f * inNormal.y + inDirection.y); }
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Perpendicular(Vector2 inDirection) => new Vector2(-inDirection.y, inDirection.x);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Perpendicular(in Vector2 inDirection) => new Vector2(-inDirection.y, inDirection.x);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(Vector2 lhs, Vector2 rhs) => lhs.x * rhs.x + lhs.y * rhs.y;
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(in Vector2 lhs, in Vector2 rhs) => lhs.x * rhs.x + lhs.y * rhs.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(Vector2 from, Vector2 to) => Angle(in from, in to);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(in Vector2 from, in Vector2 to)
        {
            float denominator = (float)Math.Sqrt((double)from.sqrMagnitude * to.sqrMagnitude);
            if (denominator < kEpsilonNormalSqrt) return 0f;
            float dot = Mathf.Clamp(Dot(from, to) / denominator, -1f, 1f);
            return (float)Math.Acos(dot) * Mathf.Rad2Deg;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SignedAngle(Vector2 from, Vector2 to) => SignedAngle(in from, in to);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SignedAngle(in Vector2 from, in Vector2 to)
        {
            float unsigned = Angle(in from, in to);
            float sign = Mathf.Sign(from.x * to.y - from.y * to.x);
            return unsigned * sign;
        }

         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(Vector2 a, Vector2 b) => (a - b).magnitude;
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(in Vector2 a, in Vector2 b) { float x = a.x - b.x, y = a.y - b.y; return (float)Math.Sqrt((double)x * x + (double)y * y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 ClampMagnitude(Vector2 vector, float maxLength) => ClampMagnitude(in vector, maxLength);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 ClampMagnitude(in Vector2 vector, float maxLength)
        {
            float sqr = vector.sqrMagnitude;
            if (sqr > maxLength * maxLength)
            {
                float mag = (float)Math.Sqrt(sqr);
                return new Vector2(vector.x / mag * maxLength, vector.y / mag * maxLength);
            }
            return vector;
        }
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(Vector2 a) => a.sqrMagnitude;
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(in Vector2 a) => a.sqrMagnitude;
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly float SqrMagnitude() => sqrMagnitude;
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Min(Vector2 lhs, Vector2 rhs) => new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Min(in Vector2 lhs, in Vector2 rhs) => new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Max(Vector2 lhs, Vector2 rhs) => new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 Max(in Vector2 lhs, in Vector2 rhs) => new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) => SmoothDamp(in current, in target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 SmoothDamp(in Vector2 current, in Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float omega = 2f / smoothTime;
            float x = omega * deltaTime;
            float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
            float changeX = current.x - target.x, changeY = current.y - target.y;
            float maxChange = maxSpeed * smoothTime;
            float sqr = changeX * changeX + changeY * changeY;
            if (sqr > maxChange * maxChange)
            {
                float mag = (float)Math.Sqrt(sqr);
                changeX = changeX / mag * maxChange; changeY = changeY / mag * maxChange;
            }
            float targetX = current.x - changeX, targetY = current.y - changeY;
            float tempX = (currentVelocity.x + omega * changeX) * deltaTime;
            float tempY = (currentVelocity.y + omega * changeY) * deltaTime;
            currentVelocity.x = (currentVelocity.x - omega * tempX) * exp;
            currentVelocity.y = (currentVelocity.y - omega * tempY) * exp;
            float outputX = targetX + (changeX + tempX) * exp;
            float outputY = targetY + (changeY + tempY) * exp;
            float origX = target.x - current.x, origY = target.y - current.y;
            float outX = outputX - target.x, outY = outputY - target.y;
            if (origX * outX + origY * outY > 0f)
            {
                outputX = target.x; outputY = target.y;
                currentVelocity.x = (outputX - target.x) / deltaTime;
                currentVelocity.y = (outputY - target.y) / deltaTime;
            }
            return new Vector2(outputX, outputY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString() => ToString(null, null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format) => ToString(format, null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "F2";
            if (formatProvider == null) formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj) => obj is Vector2 other && Equals(other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Vector2 other) => x.Equals(other.x) && y.Equals(other.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Vector2 other) => x.Equals(other.x) && y.Equals(other.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator /(Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator -(Vector2 a) => new Vector2(-a.x, -a.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator *(Vector2 a, float d) => new Vector2(a.x * d, a.y * d);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator *(float d, Vector2 a) => new Vector2(a.x * d, a.y * d);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 operator /(Vector2 a, float d) => new Vector2(a.x / d, a.y / d);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Vector2 lhs, Vector2 rhs) { float dx = lhs.x - rhs.x, dy = lhs.y - rhs.y; return dx * dx + dy * dy < 9.99999944E-11f; }
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Vector2 lhs, Vector2 rhs) => !(lhs == rhs);

#if UNITY_5_3_OR_NEWER
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Vector2(Vector2 value) => new UnityEngine.Vector2(value.x, value.y);
         [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector2(UnityEngine.Vector2 value) => new Vector2(value.x, value.y);
#endif
    }
}
