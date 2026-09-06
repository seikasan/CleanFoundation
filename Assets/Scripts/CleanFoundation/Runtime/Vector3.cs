using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CleanFoundation
{
    [Serializable]
    public partial struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public float x,y,z;
        public const float kEpsilon=1E-05f;
        public const float kEpsilonNormalSqrt=1E-15f;
        public float this[int index]{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get{switch(index){case 0:return x;case 1:return y;case 2:return z;default:throw new IndexOutOfRangeException("Invalid Vector3 index!");}}[MethodImpl(MethodImplOptions.AggressiveInlining)] set{switch(index){case 0:x=value;break;case 1:y=value;break;case 2:z=value;break;default:throw new IndexOutOfRangeException("Invalid Vector3 index!");}}}
        public readonly float magnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (float)Math.Sqrt((double)x*x+(double)y*y+(double)z*z); }
        public readonly float sqrMagnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => x*x+y*y+z*z; }
        public readonly Vector3 normalized { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Normalize(this); }
        public static Vector3 zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(0,0,0); } public static Vector3 one { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(1,1,1); } public static Vector3 forward { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(0,0,1); } public static Vector3 back { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(0,0,-1); } public static Vector3 up { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(0,1,0); } public static Vector3 down { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(0,-1,0); } public static Vector3 left { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(-1,0,0); } public static Vector3 right { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(1,0,0); } public static Vector3 positiveInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity); } public static Vector3 negativeInfinity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector3(float.NegativeInfinity,float.NegativeInfinity,float.NegativeInfinity); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector3(float x,float y,float z){this.x=x;this.y=y;this.z=z;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector3(float x,float y){this.x=x;this.y=y;z=0f;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(float newX,float newY,float newZ){x=newX;y=newY;z=newZ;}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Slerp(Vector3 a,Vector3 b,float t)=>SlerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Slerp(in Vector3 a,in Vector3 b,float t)=>SlerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 SlerpUnclamped(Vector3 a,Vector3 b,float t)=>SlerpUnclamped(in a,in b,t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 SlerpUnclamped(in Vector3 a,in Vector3 b,float t)
        {
            float magA=a.magnitude,magB=b.magnitude;
            if(magA<kEpsilon||magB<kEpsilon)return LerpUnclamped(a,b,t);
            Vector3 na=a/magA,nb=b/magB;
            float dot=Mathf.Clamp(Dot(na,nb),-1f,1f);
            Vector3 dir;
            if(dot>0.9995f)dir=Normalize(LerpUnclamped(na,nb,t));
            else if(dot<-0.9995f)
            {
                Vector3 axis=Cross(na,Mathf.Abs(na.x)<Mathf.Abs(na.z)?right:forward);
                if(axis.sqrMagnitude<kEpsilonNormalSqrt)axis=Cross(na,up);
                axis.Normalize();
                dir=RotateAroundAxis(na,axis,PI*t);
            }
            else
            {
                float theta=(float)Math.Acos(dot); float sinTheta=(float)Math.Sin(theta);
                float wa=(float)Math.Sin((1f-t)*theta)/sinTheta; float wb=(float)Math.Sin(t*theta)/sinTheta;
                dir=na*wa+nb*wb;
            }
            return dir*Mathf.LerpUnclamped(magA,magB,t);
        }
        private const float PI=3.1415927f;
        private static Vector3 RotateAroundAxis(in Vector3 v,in Vector3 axis,float angle){float c=Mathf.Cos(angle),s=Mathf.Sin(angle);return v*c+Cross(axis,v)*s+axis*(Dot(axis,v)*(1f-c));}

        public static void OrthoNormalize(ref Vector3 normal,ref Vector3 tangent){normal=NormalizeOrFallback(normal,right);tangent-=Project(tangent,normal);tangent=NormalizeOrFallback(tangent,Perpendicular(normal));}
        public static void OrthoNormalize(ref Vector3 normal,ref Vector3 tangent,ref Vector3 binormal){OrthoNormalize(ref normal,ref tangent);binormal-=Project(binormal,normal);binormal-=Project(binormal,tangent);binormal=NormalizeOrFallback(binormal,Cross(normal,tangent));}
        private static Vector3 NormalizeOrFallback(Vector3 v,Vector3 fallback){float m=v.magnitude;return m>kEpsilon?v/m:fallback;}
        private static Vector3 Perpendicular(in Vector3 v){Vector3 a=Mathf.Abs(v.x)>Mathf.Abs(v.z)?new Vector3(-v.y,v.x,0):new Vector3(0,-v.z,v.y);return NormalizeOrFallback(a,right);}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 RotateTowards(Vector3 current,Vector3 target,float maxRadiansDelta,float maxMagnitudeDelta)=>RotateTowards(in current,in target,maxRadiansDelta,maxMagnitudeDelta);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 RotateTowards(in Vector3 current,in Vector3 target,float maxRadiansDelta,float maxMagnitudeDelta)
        {
            float curMag=current.magnitude,tarMag=target.magnitude;
            float newMag=Mathf.MoveTowards(curMag,tarMag,maxMagnitudeDelta);
            if(curMag<kEpsilon)return tarMag<kEpsilon?zero:(target/tarMag)*newMag;
            if(tarMag<kEpsilon)return (current/curMag)*newMag;
            Vector3 c=current/curMag,t=target/tarMag;
            float angle=Mathf.Acos(Mathf.Clamp(Dot(c,t),-1f,1f));
            if(angle==0f)return t*newMag;
            float step=maxRadiansDelta>=0f?Mathf.Min(maxRadiansDelta,angle):-Mathf.Min(-maxRadiansDelta,Mathf.PI-angle);
            Vector3 dir;
            Vector3 axis=Cross(c,t);
            if(axis.sqrMagnitude<kEpsilonNormalSqrt)axis=Perpendicular(c);else axis.Normalize();
            dir=RotateAroundAxis(c,axis,step);
            return dir*newMag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Lerp(Vector3 a,Vector3 b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Lerp(in Vector3 a,in Vector3 b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 LerpUnclamped(Vector3 a,Vector3 b,float t)=>new Vector3(a.x+(b.x-a.x)*t,a.y+(b.y-a.y)*t,a.z+(b.z-a.z)*t); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 LerpUnclamped(in Vector3 a,in Vector3 b,float t)=>new Vector3(a.x+(b.x-a.x)*t,a.y+(b.y-a.y)*t,a.z+(b.z-a.z)*t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 MoveTowards(Vector3 current,Vector3 target,float maxDistanceDelta)=>MoveTowards(in current,in target,maxDistanceDelta);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 MoveTowards(in Vector3 current,in Vector3 target,float maxDistanceDelta){Vector3 d=target-current;float sq=d.sqrMagnitude;if(sq==0f||(maxDistanceDelta>=0f&&sq<=maxDistanceDelta*maxDistanceDelta))return target;float m=(float)Math.Sqrt(sq);return current+d/m*maxDistanceDelta;}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 SmoothDamp(Vector3 current,Vector3 target,ref Vector3 currentVelocity,float smoothTime,float maxSpeed,float deltaTime)=>SmoothDamp(in current,in target,ref currentVelocity,smoothTime,maxSpeed,deltaTime);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 SmoothDamp(in Vector3 current,in Vector3 target,ref Vector3 currentVelocity,float smoothTime,float maxSpeed,float deltaTime)
        {
            smoothTime=Mathf.Max(0.0001f,smoothTime);float omega=2f/smoothTime;float xx=omega*deltaTime;float exp=1f/(1f+xx+0.48f*xx*xx+0.235f*xx*xx*xx);
            Vector3 change=current-target;float maxChange=maxSpeed*smoothTime;float sq=change.sqrMagnitude;if(sq>maxChange*maxChange)change=change/(float)Math.Sqrt(sq)*maxChange;
            Vector3 adjustedTarget=current-change;Vector3 temp=(currentVelocity+change*omega)*deltaTime;currentVelocity=(currentVelocity-temp*omega)*exp;Vector3 output=adjustedTarget+(change+temp)*exp;
            if(Dot(target-current,output-target)>0f){output=target;currentVelocity=(output-target)/deltaTime;}return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Scale(Vector3 a,Vector3 b)=>new Vector3(a.x*b.x,a.y*b.y,a.z*b.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Scale(in Vector3 a,in Vector3 b)=>new Vector3(a.x*b.x,a.y*b.y,a.z*b.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(Vector3 s){x*=s.x;y*=s.y;z*=s.z;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(in Vector3 s){x*=s.x;y*=s.y;z*=s.z;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Cross(Vector3 lhs,Vector3 rhs)=>new Vector3(lhs.y*rhs.z-lhs.z*rhs.y,lhs.z*rhs.x-lhs.x*rhs.z,lhs.x*rhs.y-lhs.y*rhs.x); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Cross(in Vector3 lhs,in Vector3 rhs)=>new Vector3(lhs.y*rhs.z-lhs.z*rhs.y,lhs.z*rhs.x-lhs.x*rhs.z,lhs.x*rhs.y-lhs.y*rhs.x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Reflect(Vector3 d,Vector3 n)=>Reflect(in d,in n); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Reflect(in Vector3 d,in Vector3 n){float f=-2f*Dot(n,d);return d+n*f;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Normalize(Vector3 value)=>Normalize(in value); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Normalize(in Vector3 value){float m=value.magnitude;return m>kEpsilon?value/m:zero;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Normalize(){float m=magnitude;if(m>kEpsilon){x/=m;y/=m;z/=m;}else this=zero;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(Vector3 lhs,Vector3 rhs)=>lhs.x*rhs.x+lhs.y*rhs.y+lhs.z*rhs.z; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(in Vector3 lhs,in Vector3 rhs)=>lhs.x*rhs.x+lhs.y*rhs.y+lhs.z*rhs.z;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Project(Vector3 vector,Vector3 onNormal)=>Project(in vector,in onNormal); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Project(in Vector3 vector,in Vector3 onNormal){float d=Dot(onNormal,onNormal);return d<Mathf.Epsilon?zero:onNormal*(Dot(vector,onNormal)/d);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 ProjectOnPlane(Vector3 vector,Vector3 planeNormal)=>ProjectOnPlane(in vector,in planeNormal); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 ProjectOnPlane(in Vector3 vector,in Vector3 planeNormal){float d=Dot(planeNormal,planeNormal);return d<Mathf.Epsilon?vector:vector-planeNormal*(Dot(vector,planeNormal)/d);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(Vector3 from,Vector3 to)=>Angle(in from,in to); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(in Vector3 from,in Vector3 to){float den=(float)Math.Sqrt((double)from.sqrMagnitude*to.sqrMagnitude);if(den<kEpsilonNormalSqrt)return 0f;return Mathf.Acos(Mathf.Clamp(Dot(from,to)/den,-1f,1f))*Mathf.Rad2Deg;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SignedAngle(Vector3 from,Vector3 to,Vector3 axis)=>SignedAngle(in from,in to,in axis); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SignedAngle(in Vector3 from,in Vector3 to,in Vector3 axis){float unsigned=Angle(from,to);Vector3 cross=Cross(from,to);return unsigned*Mathf.Sign(Dot(axis,cross));}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(Vector3 a,Vector3 b)=>(a-b).magnitude; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(in Vector3 a,in Vector3 b){float dx=a.x-b.x,dy=a.y-b.y,dz=a.z-b.z;return (float)Math.Sqrt((double)dx*dx+(double)dy*dy+(double)dz*dz);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 ClampMagnitude(Vector3 v,float max)=>ClampMagnitude(in v,max); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 ClampMagnitude(in Vector3 v,float max){float sq=v.sqrMagnitude;if(sq>max*max){float m=(float)Math.Sqrt(sq);return v/m*max;}return v;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Magnitude(Vector3 v)=>v.magnitude; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Magnitude(in Vector3 v)=>v.magnitude; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(Vector3 v)=>v.sqrMagnitude; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float SqrMagnitude(in Vector3 v)=>v.sqrMagnitude;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Min(Vector3 a,Vector3 b)=>new Vector3(Mathf.Min(a.x,b.x),Mathf.Min(a.y,b.y),Mathf.Min(a.z,b.z)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Min(in Vector3 a,in Vector3 b)=>new Vector3(Mathf.Min(a.x,b.x),Mathf.Min(a.y,b.y),Mathf.Min(a.z,b.z)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Max(Vector3 a,Vector3 b)=>new Vector3(Mathf.Max(a.x,b.x),Mathf.Max(a.y,b.y),Mathf.Max(a.z,b.z)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 Max(in Vector3 a,in Vector3 b)=>new Vector3(Mathf.Max(a.x,b.x),Mathf.Max(a.y,b.y),Mathf.Max(a.z,b.z));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"({x.ToString(format,provider)}, {y.ToString(format,provider)}, {z.ToString(format,provider)})";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>x.GetHashCode()^(y.GetHashCode()<<2)^(z.GetHashCode()>>2); [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Vector3 o&&Equals(o); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Vector3 o)=>x.Equals(o.x)&&y.Equals(o.y)&&z.Equals(o.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Vector3 o)=>x.Equals(o.x)&&y.Equals(o.y)&&z.Equals(o.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator +(Vector3 a,Vector3 b)=>new Vector3(a.x+b.x,a.y+b.y,a.z+b.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator -(Vector3 a,Vector3 b)=>new Vector3(a.x-b.x,a.y-b.y,a.z-b.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator -(Vector3 a)=>new Vector3(-a.x,-a.y,-a.z); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator *(Vector3 a,float d)=>new Vector3(a.x*d,a.y*d,a.z*d); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator *(float d,Vector3 a)=>a*d; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator /(Vector3 a,float d)=>new Vector3(a.x/d,a.y/d,a.z/d); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Vector3 a,Vector3 b){float dx=a.x-b.x,dy=a.y-b.y,dz=a.z-b.z;return dx*dx+dy*dy+dz*dz<9.99999944E-11f;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Vector3 a,Vector3 b)=>!(a==b);
        public static implicit operator Vector2(Vector3 v)=>new Vector2(v.x,v.y); public static implicit operator Vector3(Vector2 v)=>new Vector3(v.x,v.y,0f);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Vector3(Vector3 v)=>new UnityEngine.Vector3(v.x,v.y,v.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector3(UnityEngine.Vector3 v)=>new Vector3(v.x,v.y,v.z);
#endif
    }
}
