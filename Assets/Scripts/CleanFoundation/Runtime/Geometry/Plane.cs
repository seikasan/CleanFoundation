using System;
using System.Globalization;

using System.Runtime.CompilerServices;
namespace CleanFoundation.Geometry
{
    [Serializable]
    public struct Plane : IEquatable<Plane>, IFormattable
    {
        private Vector3 m_Normal; private float m_Distance;
        public Vector3 normal{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Normal;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Normal=value;} public float distance{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Distance;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Distance=value;} public readonly Plane flipped { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Plane(-m_Normal,-m_Distance); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Plane(Vector3 inNormal,Vector3 inPoint){m_Normal=inNormal;m_Normal.Normalize();m_Distance=-Vector3.Dot(m_Normal,inPoint);} [MethodImpl(MethodImplOptions.AggressiveInlining)] public Plane(Vector3 inNormal,float d){m_Normal=inNormal;m_Normal.Normalize();m_Distance=d;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Plane(Vector3 a,Vector3 b,Vector3 c){m_Normal=Vector3.Cross(b-a,c-a);m_Normal.Normalize();m_Distance=-Vector3.Dot(m_Normal,a);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetNormalAndPosition(Vector3 inNormal,Vector3 inPoint){m_Normal=inNormal;m_Normal.Normalize();m_Distance=-Vector3.Dot(m_Normal,inPoint);} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set3Points(Vector3 a,Vector3 b,Vector3 c){m_Normal=Vector3.Cross(b-a,c-a);m_Normal.Normalize();m_Distance=-Vector3.Dot(m_Normal,a);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Flip(){m_Normal=-m_Normal;m_Distance=-m_Distance;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Translate(Vector3 translation){m_Distance+=Vector3.Dot(m_Normal,translation);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly Vector3 ClosestPointOnPlane(Vector3 point){float d=Vector3.Dot(m_Normal,point)+m_Distance;return point-m_Normal*d;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly float GetDistanceToPoint(Vector3 point)=>Vector3.Dot(m_Normal,point)+m_Distance; [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool GetSide(Vector3 point)=>GetDistanceToPoint(point)>0f; [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool SameSide(Vector3 a,Vector3 b){float da=GetDistanceToPoint(a),db=GetDistanceToPoint(b);return da>0f&&db>0f||da<=0f&&db<=0f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Raycast(Ray ray,out float enter){float a=Vector3.Dot(ray.direction,m_Normal);float num=-Vector3.Dot(ray.origin,m_Normal)-m_Distance;if(Mathf.Approximately(a,0f)){enter=0f;return false;}enter=num/a;return enter>0f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"(normal:{m_Normal.ToString(format,provider)}, distance:{m_Distance.ToString(format,provider)})";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Plane a,Plane b)=>a.m_Normal==b.m_Normal&&a.m_Distance==b.m_Distance; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Plane a,Plane b)=>!(a==b); [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Plane p&&Equals(p); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Plane p)=>this==p; [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>m_Distance.GetHashCode()^(m_Normal.GetHashCode()<<2);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Plane(Plane v){UnityEngine.Plane result=default;result.normal=v.m_Normal;result.distance=v.m_Distance;return result;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Plane(UnityEngine.Plane v){Plane result=default;result.m_Normal=v.normal;result.m_Distance=v.distance;return result;}
#endif
    }
}
