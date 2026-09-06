using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CleanFoundation
{
    [Serializable]
    public partial struct Vector4 : IEquatable<Vector4>, IFormattable
    {
        public float x, y, z, w;
        public const float kEpsilon = 1E-05f;
        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get { switch (index) { case 0:return x; case 1:return y; case 2:return z; case 3:return w; default: throw new IndexOutOfRangeException("Invalid Vector4 index!"); } }
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set { switch (index) { case 0:x=value;break; case 1:y=value;break; case 2:z=value;break; case 3:w=value;break; default: throw new IndexOutOfRangeException("Invalid Vector4 index!"); } }
        }
        public readonly float magnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)Math.Sqrt((double)x*x+(double)y*y+(double)z*z+(double)w*w); }
        public readonly float sqrMagnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => x*x+y*y+z*z+w*w; }
        public readonly Vector4 normalized { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Normalize(this); }
        public static Vector4 zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector4(0,0,0,0); }
        public static Vector4 one { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector4(1,1,1,1); }
        public static Vector4 positiveInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector4(float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity); }
        public static Vector4 negativeInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector4(float.NegativeInfinity,float.NegativeInfinity,float.NegativeInfinity,float.NegativeInfinity); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector4(float x,float y,float z,float w){this.x=x;this.y=y;this.z=z;this.w=w;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector4(float x,float y,float z){this.x=x;this.y=y;this.z=z;w=0;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector4(float x,float y){this.x=x;this.y=y;z=0;w=0;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(float newX,float newY,float newZ,float newW){x=newX;y=newY;z=newZ;w=newW;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Lerp(Vector4 a,Vector4 b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Lerp(in Vector4 a,in Vector4 b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 LerpUnclamped(Vector4 a,Vector4 b,float t)=>new Vector4(a.x+(b.x-a.x)*t,a.y+(b.y-a.y)*t,a.z+(b.z-a.z)*t,a.w+(b.w-a.w)*t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 LerpUnclamped(in Vector4 a,in Vector4 b,float t)=>new Vector4(a.x+(b.x-a.x)*t,a.y+(b.y-a.y)*t,a.z+(b.z-a.z)*t,a.w+(b.w-a.w)*t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 MoveTowards(Vector4 current,Vector4 target,float maxDistanceDelta)=>MoveTowards(in current,in target,maxDistanceDelta);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 MoveTowards(in Vector4 current,in Vector4 target,float maxDistanceDelta){Vector4 d=target-current;float sq=d.sqrMagnitude;if(sq==0f||(maxDistanceDelta>=0f&&sq<=maxDistanceDelta*maxDistanceDelta))return target;float m=(float)Math.Sqrt(sq);return current+d/m*maxDistanceDelta;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Scale(Vector4 a,Vector4 b)=>new Vector4(a.x*b.x,a.y*b.y,a.z*b.z,a.w*b.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Scale(in Vector4 a,in Vector4 b)=>new Vector4(a.x*b.x,a.y*b.y,a.z*b.z,a.w*b.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(Vector4 s){x*=s.x;y*=s.y;z*=s.z;w*=s.w;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(in Vector4 s){x*=s.x;y*=s.y;z*=s.z;w*=s.w;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Normalize(Vector4 a)=>Normalize(in a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Normalize(in Vector4 a){float m=a.magnitude;return m>kEpsilon?a/m:zero;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Normalize(){float m=magnitude;if(m>kEpsilon){x/=m;y/=m;z/=m;w/=m;}else this=zero;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(Vector4 a,Vector4 b)=>a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(in Vector4 a,in Vector4 b)=>a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Project(Vector4 a,Vector4 b)=>Project(in a,in b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Project(in Vector4 a,in Vector4 b){float d=Dot(b,b);return d<Mathf.Epsilon?zero:b*(Dot(a,b)/d);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(Vector4 a,Vector4 b)=>(a-b).magnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(in Vector4 a,in Vector4 b){return (a-b).magnitude;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Magnitude(Vector4 a)=>a.magnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Magnitude(in Vector4 a)=>a.magnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(Vector4 a)=>a.sqrMagnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(in Vector4 a)=>a.sqrMagnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly float SqrMagnitude()=>sqrMagnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Min(Vector4 lhs,Vector4 rhs)=>new Vector4(Mathf.Min(lhs.x,rhs.x),Mathf.Min(lhs.y,rhs.y),Mathf.Min(lhs.z,rhs.z),Mathf.Min(lhs.w,rhs.w));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Min(in Vector4 lhs,in Vector4 rhs)=>Min(lhs,rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Max(Vector4 lhs,Vector4 rhs)=>new Vector4(Mathf.Max(lhs.x,rhs.x),Mathf.Max(lhs.y,rhs.y),Mathf.Max(lhs.z,rhs.z),Mathf.Max(lhs.w,rhs.w));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 Max(in Vector4 lhs,in Vector4 rhs)=>Max(lhs,rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>x.GetHashCode()^y.GetHashCode()<<2^z.GetHashCode()>>2^w.GetHashCode()>>1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Vector4 other&&Equals(other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Vector4 other)=>x.Equals(other.x)&&y.Equals(other.y)&&z.Equals(other.z)&&w.Equals(other.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Vector4 other)=>x.Equals(other.x)&&y.Equals(other.y)&&z.Equals(other.z)&&w.Equals(other.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"({x.ToString(format,provider)}, {y.ToString(format,provider)}, {z.ToString(format,provider)}, {w.ToString(format,provider)})";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator +(Vector4 a,Vector4 b)=>new Vector4(a.x+b.x,a.y+b.y,a.z+b.z,a.w+b.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator -(Vector4 a,Vector4 b)=>new Vector4(a.x-b.x,a.y-b.y,a.z-b.z,a.w-b.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator -(Vector4 a)=>new Vector4(-a.x,-a.y,-a.z,-a.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator *(Vector4 a,float d)=>new Vector4(a.x*d,a.y*d,a.z*d,a.w*d);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator *(float d,Vector4 a)=>a*d;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector4 operator /(Vector4 a,float d)=>new Vector4(a.x/d,a.y/d,a.z/d,a.w/d);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Vector4 lhs,Vector4 rhs){float a=lhs.x-rhs.x,b=lhs.y-rhs.y,c=lhs.z-rhs.z,d=lhs.w-rhs.w;return a*a+b*b+c*c+d*d<9.99999944E-11f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Vector4 lhs,Vector4 rhs)=>!(lhs==rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector4(Vector3 v)=>new Vector4(v.x,v.y,v.z,0f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector3(Vector4 v)=>new Vector3(v.x,v.y,v.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector4(Vector2 v)=>new Vector4(v.x,v.y,0f,0f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector2(Vector4 v)=>new Vector2(v.x,v.y);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Vector4(Vector4 v)=>new UnityEngine.Vector4(v.x,v.y,v.z,v.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector4(UnityEngine.Vector4 v)=>new Vector4(v.x,v.y,v.z,v.w);
#endif
    }
}
