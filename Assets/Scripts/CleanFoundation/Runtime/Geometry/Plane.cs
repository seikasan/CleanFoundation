using System;
using System.Runtime.CompilerServices;
using UPlane = UnityEngine.Plane;

namespace CleanFoundation.Geometry
{
    /// <summary>
    /// UnityEngine.Plane の薄い Facade。
    /// </summary>
    [Serializable]
    public struct Plane : IFormattable
    {
        private UPlane _value;

        public Vector3 normal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.normal;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.normal = value;
        }

        public float distance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.distance;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.distance = value;
        }

        public readonly Plane flipped
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _value.flipped;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 inNormal, Vector3 inPoint)
        {
            _value = new UPlane(inNormal, inPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 inNormal, float d)
        {
            _value = new UPlane(inNormal, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            _value = new UPlane(a, b, c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Plane(UPlane value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetNormalAndPosition(
            Vector3 inNormal,
            Vector3 inPoint)
            => _value.SetNormalAndPosition(inNormal, inPoint);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set3Points(
            Vector3 a,
            Vector3 b,
            Vector3 c)
            => _value.Set3Points(a, b, c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flip()
            => _value.Flip();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Translate(Vector3 translation)
            => _value.Translate(translation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 ClosestPointOnPlane(Vector3 point)
            => _value.ClosestPointOnPlane(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float GetDistanceToPoint(Vector3 point)
            => _value.GetDistanceToPoint(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool GetSide(Vector3 point)
            => _value.GetSide(point);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool SameSide(
            Vector3 inPt0,
            Vector3 inPt1)
            => _value.SameSide(inPt0, inPt1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Raycast(
            Ray ray,
            out float enter)
            => _value.Raycast(ray, out enter);

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
        public static implicit operator UPlane(Plane value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Plane(UPlane value)
            => new(value);
    }
}
