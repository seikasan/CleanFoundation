using System;

using System.Runtime.CompilerServices;
namespace CleanFoundation.Geometry
{
    [Serializable]
    public struct Pose : IEquatable<Pose>, IFormattable
    {
        public Vector3 position; public Quaternion rotation;
        public static Pose identity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Pose(Vector3.zero,Quaternion.identity); } public readonly Vector3 forward { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => rotation*Vector3.forward; } public readonly Vector3 right { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => rotation*Vector3.right; } public readonly Vector3 up { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => rotation*Vector3.up; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Pose(Vector3 position,Quaternion rotation){this.position=position;this.rotation=rotation;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly Pose GetTransformedBy(Pose lhs)=>new Pose(lhs.position+lhs.rotation*position,lhs.rotation*rotation);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Pose p&&Equals(p); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Pose p)=>position.Equals(p.position)&&rotation.Equals(p.rotation); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Pose p)=>position.Equals(p.position)&&rotation.Equals(p.rotation); [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>position.GetHashCode()^(rotation.GetHashCode()<<1); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Pose a,Pose b)=>a.position==b.position&&a.rotation==b.rotation; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Pose a,Pose b)=>!(a==b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider)=>string.Format(provider,"({0}, {1})",position.ToString(format,provider),rotation.ToString(format,provider));
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Pose(Pose v)=>new UnityEngine.Pose(v.position,v.rotation);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Pose(UnityEngine.Pose v)=>new Pose(v.position,v.rotation);
#endif
    }
}
