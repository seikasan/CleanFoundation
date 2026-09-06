using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CleanFoundation
{
    [Serializable]
    public partial struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        private int m_X, m_Y;
        public int x { [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_X; [MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_X=value; }
        public int y { [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get=>m_Y; [MethodImpl(MethodImplOptions.AggressiveInlining)] set=>m_Y=value; }
        public int this[int index]{[MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get{switch(index){case 0:return m_X;case 1:return m_Y;default:throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!");}}[MethodImpl(MethodImplOptions.AggressiveInlining)] set{switch(index){case 0:m_X=value;break;case 1:m_Y=value;break;default:throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!");}}}
        public readonly float magnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Mathf.Sqrt((float)(m_X*m_X+m_Y*m_Y)); }
        public readonly int sqrMagnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => m_X*m_X+m_Y*m_Y; }
        public static Vector2Int zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(0,0); } public static Vector2Int one { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(1,1); } public static Vector2Int up { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(0,1); } public static Vector2Int down { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(0,-1); } public static Vector2Int left { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(-1,0); } public static Vector2Int right { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Vector2Int(1,0); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Vector2Int(int x,int y){m_X=x;m_Y=y;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Set(int x,int y){m_X=x;m_Y=y;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(Vector2Int a,Vector2Int b)=>Distance(in a,in b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Distance(in Vector2Int a,in Vector2Int b){float x=a.m_X-b.m_X,y=a.m_Y-b.m_Y;return (float)Math.Sqrt((double)x*x+(double)y*y);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Min(Vector2Int lhs,Vector2Int rhs)=>new Vector2Int(Mathf.Min(lhs.m_X,rhs.m_X),Mathf.Min(lhs.m_Y,rhs.m_Y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Min(in Vector2Int lhs,in Vector2Int rhs)=>Min(lhs,rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Max(Vector2Int lhs,Vector2Int rhs)=>new Vector2Int(Mathf.Max(lhs.m_X,rhs.m_X),Mathf.Max(lhs.m_Y,rhs.m_Y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Max(in Vector2Int lhs,in Vector2Int rhs)=>Max(lhs,rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Scale(Vector2Int a,Vector2Int b)=>new Vector2Int(a.m_X*b.m_X,a.m_Y*b.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int Scale(in Vector2Int a,in Vector2Int b)=>Scale(a,b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(Vector2Int scale){m_X*=scale.m_X;m_Y*=scale.m_Y;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Scale(in Vector2Int scale){m_X*=scale.m_X;m_Y*=scale.m_Y;}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Clamp(Vector2Int min,Vector2Int max){m_X=Mathf.Clamp(m_X,min.m_X,max.m_X);m_Y=Mathf.Clamp(m_Y,min.m_Y,max.m_Y);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Clamp(in Vector2Int min,in Vector2Int max){m_X=Mathf.Clamp(m_X,min.m_X,max.m_X);m_Y=Mathf.Clamp(m_Y,min.m_Y,max.m_Y);}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int FloorToInt(Vector2 v)=>new Vector2Int(Mathf.FloorToInt(v.x),Mathf.FloorToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int FloorToInt(in Vector2 v)=>new Vector2Int(Mathf.FloorToInt(v.x),Mathf.FloorToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int CeilToInt(Vector2 v)=>new Vector2Int(Mathf.CeilToInt(v.x),Mathf.CeilToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int CeilToInt(in Vector2 v)=>new Vector2Int(Mathf.CeilToInt(v.x),Mathf.CeilToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int RoundToInt(Vector2 v)=>new Vector2Int(Mathf.RoundToInt(v.x),Mathf.RoundToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int RoundToInt(in Vector2 v)=>new Vector2Int(Mathf.RoundToInt(v.x),Mathf.RoundToInt(v.y));
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator -(Vector2Int v)=>new Vector2Int(-v.m_X,-v.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator +(Vector2Int a,Vector2Int b)=>new Vector2Int(a.m_X+b.m_X,a.m_Y+b.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator -(Vector2Int a,Vector2Int b)=>new Vector2Int(a.m_X-b.m_X,a.m_Y-b.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator *(Vector2Int a,Vector2Int b)=>new Vector2Int(a.m_X*b.m_X,a.m_Y*b.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator *(int a,Vector2Int b)=>new Vector2Int(a*b.m_X,a*b.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator *(Vector2Int a,int b)=>new Vector2Int(a.m_X*b,a.m_Y*b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Vector2Int operator /(Vector2Int a,int b)=>new Vector2Int(a.m_X/b,a.m_Y/b);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator ==(Vector2Int lhs,Vector2Int rhs)=>lhs.m_X==rhs.m_X&&lhs.m_Y==rhs.m_Y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool operator !=(Vector2Int lhs,Vector2Int rhs)=>!(lhs==rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly bool Equals(object obj)=>obj is Vector2Int other&&Equals(other);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(Vector2Int other)=>m_X==other.m_X&&m_Y==other.m_Y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly bool Equals(in Vector2Int other)=>m_X==other.m_X&&m_Y==other.m_Y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly int GetHashCode()=>m_X*73856093^m_Y*83492791;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()=>ToString(null,null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format)=>ToString(format,null);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format,IFormatProvider provider){if(provider==null)provider=CultureInfo.InvariantCulture.NumberFormat;return $"({m_X.ToString(format,provider)}, {m_Y.ToString(format,provider)})";}
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector2(Vector2Int v)=>new Vector2(v.m_X,v.m_Y);
#if UNITY_5_3_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Vector2Int(Vector2Int v)=>new UnityEngine.Vector2Int(v.m_X,v.m_Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Vector2Int(UnityEngine.Vector2Int v)=>new Vector2Int(v.x,v.y);
#endif
    }
}
