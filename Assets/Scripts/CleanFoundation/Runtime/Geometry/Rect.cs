using System;
using System.Globalization;

using System.Runtime.CompilerServices;
namespace CleanFoundation.Geometry
{
    [Serializable]
    public struct Rect : IEquatable<Rect>, IFormattable
    {
        private float m_XMin,m_YMin,m_Width,m_Height;
        public static Rect zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Rect(0,0,0,0); }
        public float x{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_XMin;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_XMin=value;} public float y{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_YMin;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_YMin=value;} public float width{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Width;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Width=value;} public float height{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Height;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Height=value;}
        public Vector2 position{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>new Vector2(m_XMin,m_YMin);[MethodImpl(MethodImplOptions.AggressiveInlining)] set{m_XMin=value.x;m_YMin=value.y;}}
        public Vector2 center{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>new Vector2(m_XMin+m_Width*0.5f,m_YMin+m_Height*0.5f);[MethodImpl(MethodImplOptions.AggressiveInlining)] set{m_XMin=value.x-m_Width*0.5f;m_YMin=value.y-m_Height*0.5f;}}
        public Vector2 size{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>new Vector2(m_Width,m_Height);[MethodImpl(MethodImplOptions.AggressiveInlining)] set{m_Width=value.x;m_Height=value.y;}}
        public Vector2 min{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>new Vector2(xMin,yMin);[MethodImpl(MethodImplOptions.AggressiveInlining)] set{xMin=value.x;yMin=value.y;}} public Vector2 max{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>new Vector2(xMax,yMax);[MethodImpl(MethodImplOptions.AggressiveInlining)] set{xMax=value.x;yMax=value.y;}}
        public float xMin{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_XMin;[MethodImpl(MethodImplOptions.AggressiveInlining)] set{float old=xMax;m_XMin=value;m_Width=old-m_XMin;}} public float yMin{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_YMin;[MethodImpl(MethodImplOptions.AggressiveInlining)] set{float old=yMax;m_YMin=value;m_Height=old-m_YMin;}} public float xMax{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_XMin+m_Width;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Width=value-m_XMin;} public float yMax{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_YMin+m_Height;[MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Height=value-m_YMin;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Rect(float x,float y,float width,float height){m_XMin=x;m_YMin=y;m_Width=width;m_Height=height;} [MethodImpl(MethodImplOptions.AggressiveInlining)] public Rect(Vector2 position,Vector2 size){m_XMin=position.x;m_YMin=position.y;m_Width=size.x;m_Height=size.y;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Rect MinMaxRect(float xmin,float ymin,float xmax,float ymax)=>new Rect(xmin,ymin,xmax-xmin,ymax-ymin); [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(float x,float y,float width,float height){m_XMin=x;m_YMin=y;m_Width=width;m_Height=height;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(Vector2 point)=>point.x>=xMin&&point.x<xMax&&point.y>=yMin&&point.y<yMax;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(Vector3 point)=>Contains(new Vector2(point.x,point.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Contains(Vector3 point,bool allowInverse)
        {
            if(!allowInverse)return Contains(point);
            bool xin=m_Width<0f?point.x<=xMin&&point.x>xMax:point.x>=xMin&&point.x<xMax;
            bool yin=m_Height<0f?point.y<=yMin&&point.y>yMax:point.y>=yMin&&point.y<yMax;
            return xin&&yin;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Overlaps(Rect other)=>other.xMax>xMin&&other.xMin<xMax&&other.yMax>yMin&&other.yMin<yMax;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Overlaps(Rect other,bool allowInverse){if(!allowInverse)return Overlaps(other);Rect a=OrderMinMax(this),b=OrderMinMax(other);return a.Overlaps(b);}
        private static Rect OrderMinMax(Rect r){if(r.xMin>r.xMax){float mn=r.xMax,mx=r.xMin;r.x=mn;r.width=mx-mn;}if(r.yMin>r.yMax){float mn=r.yMax,mx=r.yMin;r.y=mn;r.height=mx-mn;}return r;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 NormalizedToPoint(Rect rectangle,Vector2 normalizedRectCoordinates)=>new Vector2(Mathf.Lerp(rectangle.x,rectangle.xMax,normalizedRectCoordinates.x),Mathf.Lerp(rectangle.y,rectangle.yMax,normalizedRectCoordinates.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2 PointToNormalized(Rect rectangle,Vector2 point)=>new Vector2(Mathf.InverseLerp(rectangle.x,rectangle.xMax,point.x),Mathf.InverseLerp(rectangle.y,rectangle.yMax,point.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(string.IsNullOrEmpty(format))format="F2";if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"(x:{m_XMin.ToString(format,provider)}, y:{m_YMin.ToString(format,provider)}, width:{m_Width.ToString(format,provider)}, height:{m_Height.ToString(format,provider)})";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>m_XMin.GetHashCode()^(m_Width.GetHashCode()<<2)^(m_YMin.GetHashCode()>>2)^(m_Height.GetHashCode()>>1); [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Rect r&&Equals(r); [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Rect r)=>m_XMin.Equals(r.m_XMin)&&m_YMin.Equals(r.m_YMin)&&m_Width.Equals(r.m_Width)&&m_Height.Equals(r.m_Height); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Rect a,Rect b)=>a.Equals(b); [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Rect a,Rect b)=>!a.Equals(b);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Rect(Rect v)=>new UnityEngine.Rect(v.m_XMin,v.m_YMin,v.m_Width,v.m_Height);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Rect(UnityEngine.Rect v)=>new Rect(v.x,v.y,v.width,v.height);
#endif
    }
}
