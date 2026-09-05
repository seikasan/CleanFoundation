using System;
using System.Runtime.CompilerServices;
using UColor = UnityEngine.Color;
using UVector4 = UnityEngine.Vector4;

namespace CleanFoundation
{
    /// <summary>
    /// UnityEngine.Color の薄い Facade。
    /// 公開面は Unity の Color に近づけ、色計算は UnityEngine.Color に委譲する。
    /// </summary>
    [Serializable]
    public partial struct Color : IEquatable<Color>, IFormattable
    {
        public float r;
        public float g;
        public float b;
        public float a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1f;
        }

        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => ((UColor)this)[index];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (index)
                {
                    case 0:
                        r = value;
                        break;
                    case 1:
                        g = value;
                        break;
                    case 2:
                        b = value;
                        break;
                    case 3:
                        a = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Color index({index})!");
                }
            }
        }

        public readonly float grayscale
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UColor)this).grayscale;
        }

        public readonly Color linear
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UColor)this).linear;
        }

        public readonly Color gamma
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UColor)this).gamma;
        }

        public readonly float maxColorComponent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ((UColor)this).maxColorComponent;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(Color a, Color b, float t)
            => UColor.Lerp((UColor)a, (UColor)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(in Color a, in Color b, float t)
            => UColor.Lerp((UColor)a, (UColor)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color LerpUnclamped(Color a, Color b, float t)
            => UColor.LerpUnclamped((UColor)a, (UColor)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color LerpUnclamped(in Color a, in Color b, float t)
            => UColor.LerpUnclamped((UColor)a, (UColor)b, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RGBToHSV(
            Color rgbColor,
            out float H,
            out float S,
            out float V)
            => UColor.RGBToHSV((UColor)rgbColor, out H, out S, out V);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RGBToHSV(
            in Color rgbColor,
            out float H,
            out float S,
            out float V)
            => UColor.RGBToHSV((UColor)rgbColor, out H, out S, out V);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HSVToRGB(float H, float S, float V)
            => UColor.HSVToRGB(H, S, V);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HSVToRGB(float H, float S, float V, bool hdr)
            => UColor.HSVToRGB(H, S, V, hdr);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly string ToString()
            => ((UColor)this).ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(string format)
            => ((UColor)this).ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly string ToString(
            string format,
            IFormatProvider formatProvider)
            => ((UColor)this).ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode()
            => ((UColor)this).GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
            => obj is Color other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Color other)
            => ((UColor)this).Equals((UColor)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(in Color other)
            => ((UColor)this).Equals((UColor)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator +(Color a, Color b)
            => (UColor)a + (UColor)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator -(Color a, Color b)
            => (UColor)a - (UColor)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator *(Color a, Color b)
            => (UColor)a * (UColor)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator *(Color a, Vector4 b)
            => (UColor)a * (UVector4)b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator *(Color a, float b)
            => (UColor)a * b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator *(float b, Color a)
            => b * (UColor)a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color operator /(Color a, float b)
            => (UColor)a / b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color lhs, Color rhs)
            => (UColor)lhs == (UColor)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color lhs, Color rhs)
            => (UColor)lhs != (UColor)rhs;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(Color c)
            => new(c.r, c.g, c.b, c.a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Color(Vector4 v)
            => new(v.x, v.y, v.z, v.w);

        /// <summary>
        /// Unity 境界との相互変換。
        /// Domain / Application 側では UnityEngine.Color を直接記述する必要がない。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UColor(Color value)
            => new(value.r, value.g, value.b, value.a);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Color(UColor value)
            => new(value.r, value.g, value.b, value.a);

        public static Color aliceBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.aliceBlue;
        }

        public static Color antiqueWhite
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.antiqueWhite;
        }

        public static Color aquamarine
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.aquamarine;
        }

        public static Color azure
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.azure;
        }

        public static Color beige
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.beige;
        }

        public static Color bisque
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.bisque;
        }

        public static Color black
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.black;
        }

        public static Color blanchedAlmond
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.blanchedAlmond;
        }

        public static Color blue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.blue;
        }

        public static Color blueViolet
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.blueViolet;
        }

        public static Color brown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.brown;
        }

        public static Color burlywood
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.burlywood;
        }

        public static Color cadetBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.cadetBlue;
        }

        public static Color chartreuse
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.chartreuse;
        }

        public static Color chocolate
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.chocolate;
        }

        public static Color clear
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.clear;
        }

        public static Color coral
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.coral;
        }

        public static Color cornflowerBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.cornflowerBlue;
        }

        public static Color cornsilk
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.cornsilk;
        }

        public static Color crimson
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.crimson;
        }

        public static Color cyan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.cyan;
        }

        public static Color darkBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkBlue;
        }

        public static Color darkCyan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkCyan;
        }

        public static Color darkGoldenRod
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkGoldenRod;
        }

        public static Color darkGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkGray;
        }

        public static Color darkGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkGreen;
        }

        public static Color darkKhaki
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkKhaki;
        }

        public static Color darkMagenta
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkMagenta;
        }

        public static Color darkOliveGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkOliveGreen;
        }

        public static Color darkOrange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkOrange;
        }

        public static Color darkOrchid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkOrchid;
        }

        public static Color darkRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkRed;
        }

        public static Color darkSalmon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkSalmon;
        }

        public static Color darkSeaGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkSeaGreen;
        }

        public static Color darkSlateBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkSlateBlue;
        }

        public static Color darkSlateGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkSlateGray;
        }

        public static Color darkTurquoise
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkTurquoise;
        }

        public static Color darkViolet
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.darkViolet;
        }

        public static Color deepPink
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.deepPink;
        }

        public static Color deepSkyBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.deepSkyBlue;
        }

        public static Color dimGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.dimGray;
        }

        public static Color dodgerBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.dodgerBlue;
        }

        public static Color firebrick
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.firebrick;
        }

        public static Color floralWhite
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.floralWhite;
        }

        public static Color forestGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.forestGreen;
        }

        public static Color gainsboro
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gainsboro;
        }

        public static Color ghostWhite
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.ghostWhite;
        }

        public static Color gold
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gold;
        }

        public static Color goldenRod
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.goldenRod;
        }

        public static Color gray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray;
        }

        public static Color grey
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.grey;
        }

        public static Color gray1
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray1;
        }

        public static Color gray2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray2;
        }

        public static Color gray3
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray3;
        }

        public static Color gray4
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray4;
        }

        public static Color gray5
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray5;
        }

        public static Color gray6
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray6;
        }

        public static Color gray7
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray7;
        }

        public static Color gray8
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray8;
        }

        public static Color gray9
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.gray9;
        }

        public static Color green
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.green;
        }

        public static Color greenYellow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.greenYellow;
        }

        public static Color honeydew
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.honeydew;
        }

        public static Color hotPink
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.hotPink;
        }

        public static Color indianRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.indianRed;
        }

        public static Color indigo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.indigo;
        }

        public static Color ivory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.ivory;
        }

        public static Color khaki
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.khaki;
        }

        public static Color lavender
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lavender;
        }

        public static Color lavenderBlush
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lavenderBlush;
        }

        public static Color lawnGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lawnGreen;
        }

        public static Color lemonChiffon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lemonChiffon;
        }

        public static Color lightBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightBlue;
        }

        public static Color lightCoral
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightCoral;
        }

        public static Color lightCyan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightCyan;
        }

        public static Color lightGoldenRod
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightGoldenRod;
        }

        public static Color lightGoldenRodYellow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightGoldenRodYellow;
        }

        public static Color lightGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightGray;
        }

        public static Color lightGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightGreen;
        }

        public static Color lightPink
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightPink;
        }

        public static Color lightSalmon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSalmon;
        }

        public static Color lightSeaGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSeaGreen;
        }

        public static Color lightSkyBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSkyBlue;
        }

        public static Color lightSlateBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSlateBlue;
        }

        public static Color lightSlateGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSlateGray;
        }

        public static Color lightSteelBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightSteelBlue;
        }

        public static Color lightYellow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.lightYellow;
        }

        public static Color limeGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.limeGreen;
        }

        public static Color linen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.linen;
        }

        public static Color magenta
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.magenta;
        }

        public static Color maroon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.maroon;
        }

        public static Color mediumAquamarine
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumAquamarine;
        }

        public static Color mediumBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumBlue;
        }

        public static Color mediumOrchid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumOrchid;
        }

        public static Color mediumPurple
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumPurple;
        }

        public static Color mediumSeaGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumSeaGreen;
        }

        public static Color mediumSlateBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumSlateBlue;
        }

        public static Color mediumSpringGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumSpringGreen;
        }

        public static Color mediumTurquoise
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumTurquoise;
        }

        public static Color mediumVioletRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mediumVioletRed;
        }

        public static Color midnightBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.midnightBlue;
        }

        public static Color mintCream
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mintCream;
        }

        public static Color mistyRose
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.mistyRose;
        }

        public static Color moccasin
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.moccasin;
        }

        public static Color navajoWhite
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.navajoWhite;
        }

        public static Color navyBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.navyBlue;
        }

        public static Color oldLace
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.oldLace;
        }

        public static Color olive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.olive;
        }

        public static Color oliveDrab
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.oliveDrab;
        }

        public static Color orange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.orange;
        }

        public static Color orangeRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.orangeRed;
        }

        public static Color orchid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.orchid;
        }

        public static Color paleGoldenRod
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.paleGoldenRod;
        }

        public static Color paleGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.paleGreen;
        }

        public static Color paleTurquoise
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.paleTurquoise;
        }

        public static Color paleVioletRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.paleVioletRed;
        }

        public static Color papayaWhip
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.papayaWhip;
        }

        public static Color peachPuff
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.peachPuff;
        }

        public static Color peru
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.peru;
        }

        public static Color pink
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.pink;
        }

        public static Color plum
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.plum;
        }

        public static Color powderBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.powderBlue;
        }

        public static Color purple
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.purple;
        }

        public static Color rebeccaPurple
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.rebeccaPurple;
        }

        public static Color red
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.red;
        }

        public static Color rosyBrown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.rosyBrown;
        }

        public static Color royalBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.royalBlue;
        }

        public static Color saddleBrown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.saddleBrown;
        }

        public static Color salmon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.salmon;
        }

        public static Color sandyBrown
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.sandyBrown;
        }

        public static Color seaGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.seaGreen;
        }

        public static Color seashell
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.seashell;
        }

        public static Color sienna
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.sienna;
        }

        public static Color silver
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.silver;
        }

        public static Color skyBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.skyBlue;
        }

        public static Color slateBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.slateBlue;
        }

        public static Color slateGray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.slateGray;
        }

        public static Color snow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.snow;
        }

        public static Color softRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.softRed;
        }

        public static Color softBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.softBlue;
        }

        public static Color softGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.softGreen;
        }

        public static Color softYellow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.softYellow;
        }

        public static Color springGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.springGreen;
        }

        public static Color steelBlue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.steelBlue;
        }

        public static Color tan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.tan;
        }

        public static Color teal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.teal;
        }

        public static Color thistle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.thistle;
        }

        public static Color tomato
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.tomato;
        }

        public static Color turquoise
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.turquoise;
        }

        public static Color violet
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.violet;
        }

        public static Color violetRed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.violetRed;
        }

        public static Color wheat
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.wheat;
        }

        public static Color white
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.white;
        }

        public static Color whiteSmoke
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.whiteSmoke;
        }

        public static Color yellow
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.yellow;
        }

        public static Color yellowGreen
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.yellowGreen;
        }

        public static Color yellowNice
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => UColor.yellowNice;
        }
    }
}
