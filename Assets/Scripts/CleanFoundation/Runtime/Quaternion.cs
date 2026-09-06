using System;
using System.Globalization;

using System.Runtime.CompilerServices;
namespace CleanFoundation
{
    [Serializable]
    public struct Quaternion : IEquatable<Quaternion>, IFormattable
    {
        public float x,y,z,w;
        public const float kEpsilon=1E-06f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Quaternion(float x,float y,float z,float w){this.x=x;this.y=y;this.z=z;this.w=w;}
        public float this[int index]{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get{switch(index){case 0:return x;case 1:return y;case 2:return z;case 3:return w;default:throw new IndexOutOfRangeException("Invalid Quaternion index!");}}[MethodImpl(MethodImplOptions.AggressiveInlining)] set{switch(index){case 0:x=value;break;case 1:y=value;break;case 2:z=value;break;case 3:w=value;break;default:throw new IndexOutOfRangeException("Invalid Quaternion index!");}}}
        public static Quaternion identity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Quaternion(0,0,0,1); }
        public Vector3 eulerAngles{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>MakePositive(ToEulerRad(this)*Mathf.Rad2Deg);[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>this=Euler(value);}
        public readonly Quaternion normalized { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Normalize(this); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(float newX,float newY,float newZ,float newW){x=newX;y=newY;z=newZ;w=newW;}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion FromToRotation(Vector3 fromDirection,Vector3 toDirection)=>FromToRotation(in fromDirection,in toDirection);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion FromToRotation(in Vector3 fromDirection,in Vector3 toDirection)
        {
            float fm=fromDirection.magnitude,tm=toDirection.magnitude;if(fm<Vector3.kEpsilon||tm<Vector3.kEpsilon)return identity;
            Vector3 f=fromDirection/fm,t=toDirection/tm;float dot=Mathf.Clamp(Vector3.Dot(f,t),-1f,1f);
            if(dot>1f-1E-06f)return identity;
            if(dot<-1f+1E-06f){Vector3 axis=Vector3.Cross(Vector3.right,f);if(axis.sqrMagnitude<Vector3.kEpsilonNormalSqrt)axis=Vector3.Cross(Vector3.up,f);axis.Normalize();return AngleAxis(180f,axis);}
            Vector3 c=Vector3.Cross(f,t);Quaternion q=new Quaternion(c.x,c.y,c.z,1f+dot);return Normalize(q);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Inverse(Quaternion rotation)=>Inverse(in rotation);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Inverse(in Quaternion rotation){float norm=Dot(rotation,rotation);if(norm<=Mathf.Epsilon)return identity;float inv=1f/norm;return new Quaternion(-rotation.x*inv,-rotation.y*inv,-rotation.z*inv,rotation.w*inv);}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Slerp(Quaternion a,Quaternion b,float t)=>SlerpUnclamped(a,b,Mathf.Clamp01(t)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Slerp(in Quaternion a,in Quaternion b,float t)=>SlerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion SlerpUnclamped(Quaternion a,Quaternion b,float t)=>SlerpUnclamped(in a,in b,t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion SlerpUnclamped(in Quaternion a,in Quaternion b,float t)
        {
            Quaternion qa=Normalize(a),qb=Normalize(b);float dot=Dot(qa,qb);
            if(dot<0f){dot=-dot;qb=new Quaternion(-qb.x,-qb.y,-qb.z,-qb.w);}
            if(dot>0.9995f)return Normalize(new Quaternion(qa.x+(qb.x-qa.x)*t,qa.y+(qb.y-qa.y)*t,qa.z+(qb.z-qa.z)*t,qa.w+(qb.w-qa.w)*t));
            dot=Mathf.Clamp(dot,-1f,1f);float theta0=Mathf.Acos(dot);float theta=theta0*t;float sin0=Mathf.Sin(theta0);float s0=Mathf.Sin(theta0-theta)/sin0;float s1=Mathf.Sin(theta)/sin0;
            return Normalize(new Quaternion(qa.x*s0+qb.x*s1,qa.y*s0+qb.y*s1,qa.z*s0+qb.z*s1,qa.w*s0+qb.w*s1));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Lerp(Quaternion a,Quaternion b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Lerp(in Quaternion a,in Quaternion b,float t)=>LerpUnclamped(a,b,Mathf.Clamp01(t));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LerpUnclamped(Quaternion a,Quaternion b,float t)=>LerpUnclamped(in a,in b,t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LerpUnclamped(in Quaternion a,in Quaternion b,float t){float sign=Dot(a,b)<0f?-1f:1f;return Normalize(new Quaternion(a.x+(b.x*sign-a.x)*t,a.y+(b.y*sign-a.y)*t,a.z+(b.z*sign-a.z)*t,a.w+(b.w*sign-a.w)*t));}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion AngleAxis(float angle,Vector3 axis)=>AngleAxis(angle,in axis);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion AngleAxis(float angle,in Vector3 axis){float sq=axis.sqrMagnitude;if(sq<Vector3.kEpsilonNormalSqrt)return identity;float inv=1f/(float)Math.Sqrt(sq);float half=angle*Mathf.Deg2Rad*0.5f;float s=Mathf.Sin(half);return new Quaternion(axis.x*inv*s,axis.y*inv*s,axis.z*inv*s,Mathf.Cos(half));}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LookRotation(Vector3 forward,Vector3 upwards)=>LookRotation(in forward,in upwards);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LookRotation(in Vector3 forward,in Vector3 upwards)
        {
            if(forward.sqrMagnitude<Vector3.kEpsilonNormalSqrt)return identity;
            Vector3 z=forward.normalized;Vector3 xAxis=Vector3.Cross(upwards,z);
            if(xAxis.sqrMagnitude<Vector3.kEpsilonNormalSqrt){Vector3 fallback=Mathf.Abs(z.y)<0.999f?Vector3.up:Vector3.right;xAxis=Vector3.Cross(fallback,z);}
            xAxis.Normalize();Vector3 yAxis=Vector3.Cross(z,xAxis);
            float m00=xAxis.x,m01=yAxis.x,m02=z.x,m10=xAxis.y,m11=yAxis.y,m12=z.y,m20=xAxis.z,m21=yAxis.z,m22=z.z;
            float trace=m00+m11+m22;Quaternion q;
            if(trace>0f){float s=(float)Math.Sqrt(trace+1f)*2f;q=new Quaternion((m21-m12)/s,(m02-m20)/s,(m10-m01)/s,0.25f*s);}
            else if(m00>m11&&m00>m22){float s=(float)Math.Sqrt(1f+m00-m11-m22)*2f;q=new Quaternion(0.25f*s,(m01+m10)/s,(m02+m20)/s,(m21-m12)/s);}
            else if(m11>m22){float s=(float)Math.Sqrt(1f+m11-m00-m22)*2f;q=new Quaternion((m01+m10)/s,0.25f*s,(m12+m21)/s,(m02-m20)/s);}
            else{float s=(float)Math.Sqrt(1f+m22-m00-m11)*2f;q=new Quaternion((m02+m20)/s,(m12+m21)/s,0.25f*s,(m10-m01)/s);}
            return Normalize(q);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LookRotation(Vector3 forward)=>LookRotation(in forward,Vector3.up); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion LookRotation(in Vector3 forward)=>LookRotation(in forward,Vector3.up);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(Quaternion a,Quaternion b)=>a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w; [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Dot(in Quaternion a,in Quaternion b)=>a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetLookRotation(Vector3 view)=>this=LookRotation(view); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetLookRotation(in Vector3 view)=>this=LookRotation(in view); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetLookRotation(Vector3 view,Vector3 up)=>this=LookRotation(view,up); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetLookRotation(in Vector3 view,in Vector3 up)=>this=LookRotation(in view,in up);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static bool IsEqualUsingDot(float dot)=>dot>0.9999989867f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(Quaternion a,Quaternion b)=>Angle(in a,in b); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Angle(in Quaternion a,in Quaternion b){float d=Mathf.Min(Mathf.Abs(Dot(a,b)),1f);return IsEqualUsingDot(d)?0f:Mathf.Acos(d)*2f*Mathf.Rad2Deg;}

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Euler(float x,float y,float z)=>Euler(new Vector3(x,y,z)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Euler(Vector3 euler)=>Euler(in euler);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Euler(in Vector3 euler)
        {
            float hx=euler.x*Mathf.Deg2Rad*0.5f,hy=euler.y*Mathf.Deg2Rad*0.5f,hz=euler.z*Mathf.Deg2Rad*0.5f;
            float sx=Mathf.Sin(hx),cx=Mathf.Cos(hx),sy=Mathf.Sin(hy),cy=Mathf.Cos(hy),sz=Mathf.Sin(hz),cz=Mathf.Cos(hz);
            Quaternion qx=new Quaternion(sx,0,0,cx),qy=new Quaternion(0,sy,0,cy),qz=new Quaternion(0,0,sz,cz);
            return Normalize(qy*qx*qz);
        }
        private static Vector3 ToEulerRad(in Quaternion q0)
        {
            Quaternion q=Normalize(q0);float xx=q.x*q.x,yy=q.y*q.y,zz=q.z*q.z,xy=q.x*q.y,xz=q.x*q.z,yz=q.y*q.z,wx=q.w*q.x,wy=q.w*q.y,wz=q.w*q.z;
            float m00=1f-2f*(yy+zz),m02=2f*(xz+wy),m10=2f*(xy+wz),m11=1f-2f*(xx+zz),m12=2f*(yz-wx),m20=2f*(xz-wy),m22=1f-2f*(xx+yy);
            float rx=Mathf.Asin(Mathf.Clamp(-m12,-1f,1f));float cx=Mathf.Cos(rx);float ry,rz;
            if(Mathf.Abs(cx)>1E-06f){rz=Mathf.Atan2(m10,m11);ry=Mathf.Atan2(m02,m22);}else{rz=0f;ry=Mathf.Atan2(-m20,m00);}
            return new Vector3(rx,ry,rz);
        }
        private static Vector3 MakePositive(Vector3 e){float min=-9f/(500f*Mathf.PI),max=360f+min;if(e.x<min)e.x+=360f;else if(e.x>max)e.x-=360f;if(e.y<min)e.y+=360f;else if(e.y>max)e.y-=360f;if(e.z<min)e.z+=360f;else if(e.z>max)e.z-=360f;return e;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly void ToAngleAxis(out float angle,out Vector3 axis){Quaternion q=Normalize(this);float ww=Mathf.Clamp(q.w,-1f,1f);angle=2f*Mathf.Acos(ww)*Mathf.Rad2Deg;float s=(float)Math.Sqrt(Mathf.Max(0f,1f-ww*ww));axis=s<1E-06f?Vector3.right:new Vector3(q.x/s,q.y/s,q.z/s);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetFromToRotation(Vector3 fromDirection,Vector3 toDirection)=>this=FromToRotation(fromDirection,toDirection); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void SetFromToRotation(in Vector3 fromDirection,in Vector3 toDirection)=>this=FromToRotation(in fromDirection,in toDirection);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion RotateTowards(Quaternion from,Quaternion to,float maxDegreesDelta)=>RotateTowards(in from,in to,maxDegreesDelta); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion RotateTowards(in Quaternion from,in Quaternion to,float maxDegreesDelta){float angle=Angle(from,to);return angle==0f?to:SlerpUnclamped(from,to,Mathf.Min(1f,maxDegreesDelta/angle));}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Normalize(Quaternion q)=>Normalize(in q); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion Normalize(in Quaternion q){float mag=Mathf.Sqrt(Dot(q,q));if(mag<Mathf.Epsilon)return identity;return new Quaternion(q.x/mag,q.y/mag,q.z/mag,q.w/mag);} [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Normalize()=>this=Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Quaternion operator *(Quaternion lhs,Quaternion rhs)=>new Quaternion(lhs.w*rhs.x+lhs.x*rhs.w+lhs.y*rhs.z-lhs.z*rhs.y,lhs.w*rhs.y+lhs.y*rhs.w+lhs.z*rhs.x-lhs.x*rhs.z,lhs.w*rhs.z+lhs.z*rhs.w+lhs.x*rhs.y-lhs.y*rhs.x,lhs.w*rhs.w-lhs.x*rhs.x-lhs.y*rhs.y-lhs.z*rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector3 operator *(Quaternion r,Vector3 p){float x2=r.x*2f,y2=r.y*2f,z2=r.z*2f,xx=r.x*x2,yy=r.y*y2,zz=r.z*z2,xy=r.x*y2,xz=r.x*z2,yz=r.y*z2,wx=r.w*x2,wy=r.w*y2,wz=r.w*z2;return new Vector3((1f-(yy+zz))*p.x+(xy-wz)*p.y+(xz+wy)*p.z,(xy+wz)*p.x+(1f-(xx+zz))*p.y+(yz-wx)*p.z,(xz-wy)*p.x+(yz+wx)*p.y+(1f-(xx+yy))*p.z);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Quaternion a,Quaternion b)=>IsEqualUsingDot(Dot(a,b)); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Quaternion a,Quaternion b)=>!(a==b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>x.GetHashCode()^y.GetHashCode()<<2^z.GetHashCode()>>2^w.GetHashCode()>>1; [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Quaternion o&&Equals(o); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Quaternion o)=>x.Equals(o.x)&&y.Equals(o.y)&&z.Equals(o.z)&&w.Equals(o.w); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Quaternion o)=>x.Equals(o.x)&&y.Equals(o.y)&&z.Equals(o.z)&&w.Equals(o.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"({x.ToString(format,provider)}, {y.ToString(format,provider)}, {z.ToString(format,provider)}, {w.ToString(format,provider)})";}
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Quaternion(Quaternion v)=>new UnityEngine.Quaternion(v.x,v.y,v.z,v.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Quaternion(UnityEngine.Quaternion v)=>new Quaternion(v.x,v.y,v.z,v.w);
#endif
    }
}
