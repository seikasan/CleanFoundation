using System;
using System.Globalization;

using System.Runtime.CompilerServices;
namespace CleanFoundation.Geometry
{
    [Serializable]
    public struct Bounds : IEquatable<Bounds>, IFormattable
    {
        private Vector3 m_Center;
        private Vector3 m_Extents;
        public Vector3 center{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Center;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Center=value;}
        public Vector3 size{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Extents*2f;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Extents=value*0.5f;}
        public Vector3 extents{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Extents;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Extents=value;}
        public Vector3 min{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Center-m_Extents;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>SetMinMax(value,max);}
        public Vector3 max{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Center+m_Extents;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>SetMinMax(min,value);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Bounds(Vector3 center,Vector3 size){m_Center=center;m_Extents=size*0.5f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetMinMax(Vector3 min,Vector3 max){m_Extents=(max-min)*0.5f;m_Center=min+m_Extents;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Encapsulate(Vector3 point)=>SetMinMax(Vector3.Min(min,point),Vector3.Max(max,point));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Encapsulate(Bounds bounds){Encapsulate(bounds.min);Encapsulate(bounds.max);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Expand(float amount){amount*=0.5f;m_Extents+=new Vector3(amount,amount,amount);} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Expand(Vector3 amount)=>m_Extents+=amount*0.5f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(Vector3 point){Vector3 mn=min,mx=max;return point.x>=mn.x&&point.x<=mx.x&&point.y>=mn.y&&point.y<=mx.y&&point.z>=mn.z&&point.z<=mx.z;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Intersects(Bounds bounds){Vector3 mn=min,mx=max,omn=bounds.min,omx=bounds.max;return mn.x<=omx.x&&mx.x>=omn.x&&mn.y<=omx.y&&mx.y>=omn.y&&mn.z<=omx.z&&mx.z>=omn.z;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly float SqrDistance(Vector3 point){Vector3 mn=min,mx=max;float dx=point.x<mn.x?mn.x-point.x:point.x>mx.x?point.x-mx.x:0f;float dy=point.y<mn.y?mn.y-point.y:point.y>mx.y?point.y-mx.y:0f;float dz=point.z<mn.z?mn.z-point.z:point.z>mx.z?point.z-mx.z:0f;return dx*dx+dy*dy+dz*dz;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly Vector3 ClosestPoint(Vector3 point){Vector3 mn=min,mx=max;return new Vector3(Mathf.Clamp(point.x,mn.x,mx.x),Mathf.Clamp(point.y,mn.y,mx.y),Mathf.Clamp(point.z,mn.z,mx.z));}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool IntersectRay(Ray ray){float d;return IntersectRay(ray,out d);} [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool IntersectRay(Ray ray,out float distance)
        {
            Vector3 mn=min,mx=max,o=ray.origin,d=ray.direction;float tmin=0f,tmax=float.PositiveInfinity;
            if(!Slab(o.x,d.x,mn.x,mx.x,ref tmin,ref tmax)||!Slab(o.y,d.y,mn.y,mx.y,ref tmin,ref tmax)||!Slab(o.z,d.z,mn.z,mx.z,ref tmin,ref tmax)){distance=0f;return false;}distance=tmin;return true;
        }
        private static bool Slab(float o,float d,float mn,float mx,ref float tmin,ref float tmax){if(Mathf.Abs(d)<1E-08f)return o>=mn&&o<=mx;float inv=1f/d;float a=(mn-o)*inv,b=(mx-o)*inv;if(a>b){float t=a;a=b;b=t;}if(a>tmin)tmin=a;if(b<tmax)tmax=b;return tmin<=tmax&&tmax>=0f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"Center: {m_Center.ToString(format,provider)}, Extents: {m_Extents.ToString(format,provider)}";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>m_Center.GetHashCode()^(m_Extents.GetHashCode()<<2); [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Bounds b&&Equals(b); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Bounds b)=>m_Center.Equals(b.m_Center)&&m_Extents.Equals(b.m_Extents); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Bounds a,Bounds b)=>a.m_Center==b.m_Center&&a.m_Extents==b.m_Extents; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Bounds a,Bounds b)=>!(a==b);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Bounds(Bounds v)=>new UnityEngine.Bounds(v.center,v.size);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Bounds(UnityEngine.Bounds v)=>new Bounds(v.center,v.size);
#endif
    }
}
