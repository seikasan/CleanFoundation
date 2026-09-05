using System;
using System.Runtime.CompilerServices;
using UPose = UnityEngine.Pose;

namespace CleanFoundation.Geometry
{
    /// <summary>
    /// UnityEngine.Pose の薄い Facade。
    /// 位置と回転からなる純粋な姿勢情報を表す。
    /// </summary>
    [Serializable]
    public struct Pose : IEquatable<Pose>, IFormattable
    {
        public Vector3 position;
        public Quaternion rotation;

        public static Pose identity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UPose.identity;
        }

        public readonly Vector3 forward
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => rotation * Vector3.forward;
        }

        public readonly Vector3 right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => rotation * Vector3.right;
        }

        public readonly Vector3 up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => rotation * Vector3.up;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pose(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        /// <summary>
        /// lhs を基準として、この Pose を変換する。
        /// UnityEngine.Pose.GetTransformedBy(Pose) と同じ挙動。
        ///
        /// Transform を受け取る Unity 版オーバーロードは、
        /// UnityEngine.Transform への依存を持ち込むため公開しない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Pose GetTransformedBy(Pose lhs)
            => ((UPose)this).GetTransformedBy((UPose)lhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Pose other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Pose other)
            => position.Equals(other.position)
               && rotation.Equals(other.rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Pose other)
            => position.Equals(in other.position)
               && rotation.Equals(in other.rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UPose)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Pose lhs, Pose rhs)
            => (UPose)lhs == (UPose)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Pose lhs, Pose rhs)
            => (UPose)lhs != (UPose)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UPose)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UPose)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(
            string format,
            IFormatProvider formatProvider)
            => string.Format(
                formatProvider,
                "({0}, {1})",
                position.ToString(format, formatProvider),
                rotation.ToString(format, formatProvider));

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Pose を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UPose(Pose value)
            => new(value.position, value.rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Pose(UPose value)
            => new(value.position, value.rotation);
    }
}
