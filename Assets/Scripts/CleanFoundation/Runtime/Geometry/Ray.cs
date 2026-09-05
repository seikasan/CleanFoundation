using System;
using System.Runtime.CompilerServices;
using URay = UnityEngine.Ray;
using UVector3 = UnityEngine.Vector3;

namespace CleanFoundation.Geometry
{
    /// <summary>
    /// UnityEngine.Ray の薄い Facade。
    /// 公開面は Unity の Ray に近づけ、計算処理は UnityEngine.Ray に委譲する。
    /// </summary>
    [Serializable]
    public struct Ray : IFormattable
    {
        private URay _value;

        public Vector3 origin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.origin;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.origin = value;
        }

        public Vector3 direction
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value.direction;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _value.direction = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Ray(Vector3 origin, Vector3 direction)
        {
            _value = new URay(
                (UVector3)origin,
                (UVector3)direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Ray(URay value)
        {
            _value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Vector3 GetPoint(float distance)
            => _value.GetPoint(distance);

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
        public static implicit operator URay(Ray value)
            => value._value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ray(URay value)
            => new(value);
    }
}
