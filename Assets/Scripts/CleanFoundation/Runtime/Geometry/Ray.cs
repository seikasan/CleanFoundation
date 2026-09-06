using System;
using System.Globalization;

using System.Runtime.CompilerServices;
namespace CleanFoundation.Geometry
{
    [Serializable]
    public struct Ray : IFormattable
    {
        private Vector3 m_Origin,m_Direction;
        public Vector3 origin{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Origin;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Origin=value;} public Vector3 direction{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Direction;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Direction=value.normalized;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Ray(Vector3 origin,Vector3 direction){m_Origin=origin;m_Direction=direction.normalized;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly Vector3 GetPoint(float distance)=>m_Origin+m_Direction*distance;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"Origin: {m_Origin.ToString(format,provider)}, Dir: {m_Direction.ToString(format,provider)}";}
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Ray(Ray v)=>new UnityEngine.Ray(v.m_Origin,v.m_Direction);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Ray(UnityEngine.Ray v)=>new Ray(v.origin,v.direction);
#endif
    }
}
