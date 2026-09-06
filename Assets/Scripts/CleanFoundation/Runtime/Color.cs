using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CleanFoundation
{
    /// <summary>
    ///   <para>Representation of RGBA colors.</para>
    /// </summary>
    [Serializable]
    public partial struct Color : IEquatable<Color>, IFormattable
    {
      /// <summary>
      ///   <para>Red component of the color.</para>
      /// </summary>
      public float r;
      /// <summary>
      ///   <para>Green component of the color.</para>
      /// </summary>
      public float g;
      /// <summary>
      ///   <para>Blue component of the color.</para>
      /// </summary>
      public float b;
      /// <summary>
      ///   <para>Alpha component of the color (0 is transparent, 1 is opaque).</para>
      /// </summary>
      public float a;
      private static Dictionary<Color, string> m_defaultColorNames;

      /// <summary>
      ///   <para>Constructs a new Color with given r,g,b,a components.</para>
      /// </summary>
      /// <param name="r">Red component.</param>
      /// <param name="g">Green component.</param>
      /// <param name="b">Blue component.</param>
      /// <param name="a">Alpha component.</param>
      public Color(float r, float g, float b, float a)
      {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
      }

      /// <summary>
      ///   <para>Constructs a new Color with given r,g,b components and sets a to 1.</para>
      /// </summary>
      /// <param name="r">Red component.</param>
      /// <param name="g">Green component.</param>
      /// <param name="b">Blue component.</param>
      public Color(float r, float g, float b)
      {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = 1f;
      }

      /// <summary>
      ///   <para>Returns a formatted string of this color.</para>
      /// </summary>
      /// <param name="format">A numeric format string.</param>
      /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
      
      [MethodImpl(MethodImplOptions.AggressiveInlining)] public override readonly string ToString()
      {
        return this.ToString((string) null, (IFormatProvider) null);
      }

      /// <summary>
      ///   <para>Returns a formatted string of this color.</para>
      /// </summary>
      /// <param name="format">A numeric format string.</param>
      /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
      
      [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format) => this.ToString(format, (IFormatProvider) null);

      /// <summary>
      ///   <para>Returns a formatted string of this color.</para>
      /// </summary>
      /// <param name="format">A numeric format string.</param>
      /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
      
      [MethodImpl(MethodImplOptions.AggressiveInlining)] public readonly string ToString(string format, IFormatProvider formatProvider)
      {
        if (string.IsNullOrEmpty(format))
          format = "F3";
        if (formatProvider == null)
          formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
        return $"RGBA({this.r.ToString(format, formatProvider)}, {this.g.ToString(format, formatProvider)}, {this.b.ToString(format, formatProvider)}, {this.a.ToString(format, formatProvider)})";
      }

      /// <summary>
      ///   <para>Returns the hash code for this color. This lets you use colors as keys in hash tables.</para>
      /// </summary>
      public override readonly int GetHashCode()
      {
        return this.r.GetHashCode() ^ this.g.GetHashCode() << 2 ^ this.b.GetHashCode() >> 2 ^ this.a.GetHashCode() >> 1;
      }

      /// <summary>
      ///   <para>Returns true if the given color is exactly equal to this color, i.e. if the red, green, blue, and alpha values are exactly the same.</para>
      /// </summary>
      /// <param name="other">The other Color that is used for the equality check.</param>
      /// <returns>
      ///   <para>True if the given color is exactly equal to this color.</para>
      /// </returns>
      public override readonly bool Equals(object other)
      {
        return other is Color other1 && this.Equals(in other1);
      }

      /// <summary>
      ///   <para>Returns true if the given color is exactly equal to this color, i.e. if the red, green, blue, and alpha values are exactly the same.</para>
      /// </summary>
      /// <param name="other">The other Color that is used for the equality check.</param>
      /// <returns>
      ///   <para>True if the given color is exactly equal to this color.</para>
      /// </returns>
      public readonly bool Equals(Color other)
      {
        return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
      }

      public readonly bool Equals(in Color other)
      {
        return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
      }

      public static Color operator +(Color a, Color b)
      {
        return new Color()
        {
          r = a.r + b.r,
          g = a.g + b.g,
          b = a.b + b.b,
          a = a.a + b.a
        };
      }

      public static Color operator -(Color a, Color b)
      {
        return new Color()
        {
          r = a.r - b.r,
          g = a.g - b.g,
          b = a.b - b.b,
          a = a.a - b.a
        };
      }

      public static Color operator *(Color a, Color b)
      {
        return new Color()
        {
          r = a.r * b.r,
          g = a.g * b.g,
          b = a.b * b.b,
          a = a.a * b.a
        };
      }

      public static Color operator *(Color a, Vector4 b)
      {
        return new Color()
        {
          r = a.r * b.x,
          g = a.g * b.y,
          b = a.b * b.z,
          a = a.a * b.w
        };
      }

      public static Color operator *(Color a, float b)
      {
        return new Color()
        {
          r = a.r * b,
          g = a.g * b,
          b = a.b * b,
          a = a.a * b
        };
      }

      public static Color operator *(float b, Color a)
      {
        return new Color()
        {
          r = a.r * b,
          g = a.g * b,
          b = a.b * b,
          a = a.a * b
        };
      }

      public static Color operator /(Color a, float b)
      {
        return new Color()
        {
          r = a.r / b,
          g = a.g / b,
          b = a.b / b,
          a = a.a / b
        };
      }

      public static bool operator ==(Color lhs, Color rhs)
      {
        float num1 = lhs.r - rhs.r;
        float num2 = lhs.g - rhs.g;
        float num3 = lhs.b - rhs.b;
        float num4 = lhs.a - rhs.a;
        return (double) num1 * (double) num1 + (double) num2 * (double) num2 + (double) num3 * (double) num3 + (double) num4 * (double) num4 < 9.999999439624929E-11;
      }

      public static bool operator !=(Color lhs, Color rhs) => !(lhs == rhs);

      /// <summary>
      ///   <para>Linearly interpolates between colors a and b using the interpolation ratio t.</para>
      /// </summary>
      /// <param name="a">The start color, returned when t = 0.</param>
      /// <param name="b">The end color, returned when t = 1.</param>
      /// <param name="t">The interpolation ratio. Will be clamped to the range [0; 1].</param>
      /// <returns>
      ///   <para>The color resulting from linear interpolation between a and b.</para>
      /// </returns>
      public static Color Lerp(Color a, Color b, float t)
      {
        t = Mathf.Clamp01(t);
        return new Color()
        {
          r = a.r + (b.r - a.r) * t,
          g = a.g + (b.g - a.g) * t,
          b = a.b + (b.b - a.b) * t,
          a = a.a + (b.a - a.a) * t
        };
      }

      public static Color Lerp(in Color a, in Color b, float t)
      {
        t = Mathf.Clamp01(t);
        return new Color()
        {
          r = a.r + (b.r - a.r) * t,
          g = a.g + (b.g - a.g) * t,
          b = a.b + (b.b - a.b) * t,
          a = a.a + (b.a - a.a) * t
        };
      }

      /// <summary>
      ///   <para>Linearly interpolates between colors a and b using the interpolation ratio t.</para>
      /// </summary>
      /// <param name="a">The start color, returned when t = 0.</param>
      /// <param name="b">The end color, returned when t = 1.</param>
      /// <param name="t">The interpolation ratio. The ratio will not be clamped, and can be outside of the [0; 1] range.</param>
      /// <returns>
      ///   <para>The color resulting from linear interpolation between a and b.</para>
      /// </returns>
      public static Color LerpUnclamped(Color a, Color b, float t)
      {
        return new Color()
        {
          r = a.r + (b.r - a.r) * t,
          g = a.g + (b.g - a.g) * t,
          b = a.b + (b.b - a.b) * t,
          a = a.a + (b.a - a.a) * t
        };
      }

      public static Color LerpUnclamped(in Color a, in Color b, float t)
      {
        return new Color()
        {
          r = a.r + (b.r - a.r) * t,
          g = a.g + (b.g - a.g) * t,
          b = a.b + (b.b - a.b) * t,
          a = a.a + (b.a - a.a) * t
        };
      }

      internal readonly Color RGBMultiplied(float multiplier)
      {
        return new Color()
        {
          r = this.r * multiplier,
          g = this.g * multiplier,
          b = this.b * multiplier,
          a = this.a
        };
      }

      internal readonly Color AlphaMultiplied(float multiplier)
      {
        return new Color()
        {
          r = this.r,
          g = this.g,
          b = this.b,
          a = this.a * multiplier
        };
      }

      internal readonly Color RGBMultiplied(Color multiplier)
      {
        return new Color()
        {
          r = this.r * multiplier.r,
          g = this.g * multiplier.g,
          b = this.b * multiplier.b,
          a = this.a
        };
      }

      internal readonly Color RGBMultiplied(in Color multiplier)
      {
        return new Color()
        {
          r = this.r * multiplier.r,
          g = this.g * multiplier.g,
          b = this.b * multiplier.b,
          a = this.a
        };
      }

      /// <summary>
      ///   <para>The grayscale value of the color. (Read Only)</para>
      /// </summary>
      public readonly float grayscale
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return (float) (0.29899999499320984 * (double) this.r + 0.5870000123977661 * (double) this.g + 57.0 / 500.0 * (double) this.b);
        }
      }

      /// <summary>
      ///   <para>A color value in linear space converted from an sRGB value.</para>
      /// </summary>
      public readonly Color linear
      {
        get
        {
          return new Color()
          {
            r = Mathf.GammaToLinearSpace(this.r),
            g = Mathf.GammaToLinearSpace(this.g),
            b = Mathf.GammaToLinearSpace(this.b),
            a = this.a
          };
        }
      }

      /// <summary>
      ///   <para>A version of the color that has had the gamma curve applied.</para>
      /// </summary>
      public readonly Color gamma
      {
        get
        {
          return new Color()
          {
            r = Mathf.LinearToGammaSpace(this.r),
            g = Mathf.LinearToGammaSpace(this.g),
            b = Mathf.LinearToGammaSpace(this.b),
            a = this.a
          };
        }
      }

      /// <summary>
      ///   <para>Returns the maximum color component value: Max(r,g,b).</para>
      /// </summary>
      public readonly float maxColorComponent => Mathf.Max(Mathf.Max(this.r, this.g), this.b);

      public static implicit operator Vector4(Color c)
      {
        return new Vector4()
        {
          x = c.r,
          y = c.g,
          z = c.b,
          w = c.a
        };
      }

      public static implicit operator Color(Vector4 v)
      {
        return new Color()
        {
          r = v.x,
          g = v.y,
          b = v.z,
          a = v.w
        };
      }

      public float this[int index]
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] readonly get
        {
          switch (index)
          {
            case 0:
              return this.r;
            case 1:
              return this.g;
            case 2:
              return this.b;
            case 3:
              return this.a;
            default:
              throw new IndexOutOfRangeException($"Invalid Color index({index.ToString()})!");
          }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] set
        {
          switch (index)
          {
            case 0:
              this.r = value;
              break;
            case 1:
              this.g = value;
              break;
            case 2:
              this.b = value;
              break;
            case 3:
              this.a = value;
              break;
            default:
              throw new IndexOutOfRangeException($"Invalid Color index({index.ToString()})!");
          }
        }
      }

      public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
      {
        if ((double) rgbColor.b > (double) rgbColor.g && (double) rgbColor.b > (double) rgbColor.r)
          Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
        else if ((double) rgbColor.g > (double) rgbColor.r)
          Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
        else
          Color.RGBToHSVHelper(0.0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
      }

      public static void RGBToHSV(in Color rgbColor, out float H, out float S, out float V)
      {
        if ((double) rgbColor.b > (double) rgbColor.g && (double) rgbColor.b > (double) rgbColor.r)
          Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
        else if ((double) rgbColor.g > (double) rgbColor.r)
          Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
        else
          Color.RGBToHSVHelper(0.0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
      }

      private static void RGBToHSVHelper(
        float offset,
        float dominantcolor,
        float colorone,
        float colortwo,
        out float H,
        out float S,
        out float V)
      {
        V = dominantcolor;
        if ((double) V != 0.0)
        {
          float num1 = (double) colorone <= (double) colortwo ? colorone : colortwo;
          float num2 = V - num1;
          if ((double) num2 != 0.0)
          {
            S = num2 / V;
            H = offset + (colorone - colortwo) / num2;
          }
          else
          {
            S = 0.0f;
            H = offset + (colorone - colortwo);
          }
          H /= 6f;
          if ((double) H >= 0.0)
            return;
          ++H;
        }
        else
        {
          S = 0.0f;
          H = 0.0f;
        }
      }

      /// <summary>
      ///   <para>Creates an RGB colour from HSV input.</para>
      /// </summary>
      /// <param name="H">Hue [0..1].</param>
      /// <param name="S">Saturation [0..1].</param>
      /// <param name="V">Brightness value [0..1].</param>
      /// <param name="hdr">Output HDR colours. If true, the returned colour will not be clamped to [0..1].</param>
      /// <returns>
      ///   <para>An opaque colour with HSV matching the input.</para>
      /// </returns>
      public static Color HSVToRGB(float H, float S, float V) => Color.HSVToRGB(H, S, V, true);

      /// <summary>
      ///   <para>Creates an RGB colour from HSV input.</para>
      /// </summary>
      /// <param name="H">Hue [0..1].</param>
      /// <param name="S">Saturation [0..1].</param>
      /// <param name="V">Brightness value [0..1].</param>
      /// <param name="hdr">Output HDR colours. If true, the returned colour will not be clamped to [0..1].</param>
      /// <returns>
      ///   <para>An opaque colour with HSV matching the input.</para>
      /// </returns>
      public static Color HSVToRGB(float H, float S, float V, bool hdr)
      {
        Color white = Color.white;
        if ((double) S == 0.0)
        {
          white.r = V;
          white.g = V;
          white.b = V;
        }
        else if ((double) V == 0.0)
        {
          white.r = 0.0f;
          white.g = 0.0f;
          white.b = 0.0f;
        }
        else
        {
          white.r = 0.0f;
          white.g = 0.0f;
          white.b = 0.0f;
          float num1 = S;
          float num2 = V;
          float f = H * 6f;
          int num3 = Mathf.FloorToInt(f);
          float num4 = f - (float) num3;
          float num5 = num2 * (1f - num1);
          float num6 = num2 * (float) (1.0 - (double) num1 * (double) num4);
          float num7 = num2 * (float) (1.0 - (double) num1 * (1.0 - (double) num4));
          switch (num3)
          {
            case -1:
              white.r = num2;
              white.g = num5;
              white.b = num6;
              break;
            case 0:
              white.r = num2;
              white.g = num7;
              white.b = num5;
              break;
            case 1:
              white.r = num6;
              white.g = num2;
              white.b = num5;
              break;
            case 2:
              white.r = num5;
              white.g = num2;
              white.b = num7;
              break;
            case 3:
              white.r = num5;
              white.g = num6;
              white.b = num2;
              break;
            case 4:
              white.r = num7;
              white.g = num5;
              white.b = num2;
              break;
            case 5:
              white.r = num2;
              white.g = num5;
              white.b = num6;
              break;
            case 6:
              white.r = num2;
              white.g = num7;
              white.b = num5;
              break;
          }
          if (!hdr)
          {
            white.r = Mathf.Clamp01(white.r);
            white.g = Mathf.Clamp01(white.g);
            white.b = Mathf.Clamp01(white.b);
          }
        }
        return white;
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9411765f, 0.9725491f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color aliceBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9411765f, 0.9725491f, 1f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9803922f, 0.9215687f, 0.8431373f, 1f)
      ///           </para>
      /// </summary>
      public static Color antiqueWhite
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9803922f, 0.9215687f, 0.8431373f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4980392f, 1f, 0.8313726f, 1f)
      ///           </para>
      /// </summary>
      public static Color aquamarine
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4980392f, 1f, 0.8313726f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9411765f, 1f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color azure
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.9411765f, 1f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9607844f, 0.9607844f, 0.8627452f, 1f)
      ///           </para>
      /// </summary>
      public static Color beige
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9607844f, 0.9607844f, 272f * (float) Math.E / 857f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.8941177f, 0.7686275f, 1f)
      ///           </para>
      /// </summary>
      public static Color bisque
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.8941177f, 0.7686275f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color black
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9215687f, 0.8039216f, 1f)
      ///           </para>
      /// </summary>
      public static Color blanchedAlmond
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9215687f, 0.8039216f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color blue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5411765f, 0.1686275f, 0.8862746f, 1f)
      ///           </para>
      /// </summary>
      public static Color blueViolet
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5411765f, 0.1686275f, 0.8862746f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6470588f, 0.1647059f, 0.1647059f, 1f)
      ///           </para>
      /// </summary>
      public static Color brown
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6470588f, 0.1647059f, 0.1647059f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8705883f, 0.7215686f, 0.5294118f, 1f)
      ///           </para>
      /// </summary>
      public static Color burlywood
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8705883f, 0.7215686f, 0.5294118f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.372549f, 0.6196079f, 0.627451f, 1f)
      ///           </para>
      /// </summary>
      public static Color cadetBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.372549f, 0.6196079f, 0.627451f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4980392f, 1f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color chartreuse
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.4980392f, 1f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8235295f, 0.4117647f, 0.1176471f, 1f)
      ///           </para>
      /// </summary>
      public static Color chocolate
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8235295f, 0.4117647f, 0.1176471f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 0f, 0f)
      ///           </para>
      /// </summary>
      public static Color clear
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 0.0f, 0.0f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.4980392f, 0.3137255f, 1f)
      ///           </para>
      /// </summary>
      public static Color coral
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.4980392f, 0.3137255f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.3921569f, 0.5843138f, 0.9294118f, 1f)
      ///           </para>
      /// </summary>
      public static Color cornflowerBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.3921569f, 0.5843138f, 0.9294118f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9725491f, 0.8627452f, 1f)
      ///           </para>
      /// </summary>
      public static Color cornsilk
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9725491f, 272f * (float) Math.E / 857f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8627452f, 0.07843138f, 0.2352941f, 1f)
      ///           </para>
      /// </summary>
      public static Color crimson
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(272f * (float) Math.E / 857f, 0.07843138f, 0.2352941f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 1f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color cyan
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 1f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 0.5450981f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 0.5450981f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.5450981f, 0.5450981f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkCyan
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.0f, 0.5450981f, 0.5450981f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7215686f, 0.5254902f, 0.04313726f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkGoldenRod
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7215686f, 0.5254902f, 0.04313726f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6627451f, 0.6627451f, 0.6627451f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6627451f, 0.6627451f, 0.6627451f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.3921569f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.3921569f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7411765f, 0.7176471f, 0.4196079f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkKhaki
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7411765f, 0.7176471f, 0.4196079f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5450981f, 0f, 0.5450981f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkMagenta
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5450981f, 0.0f, 0.5450981f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.3333333f, 0.4196079f, 0.1843137f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkOliveGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.3333333f, 0.4196079f, 0.1843137f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.5490196f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkOrange
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.5490196f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6f, 0.1960784f, 0.8000001f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkOrchid
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6f, 0.1960784f, 0.8000001f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5450981f, 0f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.5450981f, 0.0f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9137256f, 0.5882353f, 0.4784314f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkSalmon
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9137256f, 0.5882353f, 0.4784314f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5607843f, 0.7372549f, 0.5607843f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkSeaGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5607843f, 0.7372549f, 0.5607843f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.282353f, 0.2392157f, 0.5450981f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkSlateBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.282353f, 0.2392157f, 0.5450981f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1843137f, 0.3098039f, 0.3098039f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkSlateGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1843137f, 0.3098039f, 0.3098039f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.8078432f, 0.8196079f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkTurquoise
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.0f, 0.8078432f, 0.8196079f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5803922f, 0f, 0.8274511f, 1f)
      ///           </para>
      /// </summary>
      public static Color darkViolet
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5803922f, 0.0f, 0.8274511f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.07843138f, 0.5764706f, 1f)
      ///           </para>
      /// </summary>
      public static Color deepPink
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.07843138f, 0.5764706f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.7490196f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color deepSkyBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.7490196f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4117647f, 0.4117647f, 0.4117647f, 1f)
      ///           </para>
      /// </summary>
      public static Color dimGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4117647f, 0.4117647f, 0.4117647f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1176471f, 0.5647059f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color dodgerBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1176471f, 0.5647059f, 1f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6980392f, 0.1333333f, 0.1333333f, 1f)
      ///           </para>
      /// </summary>
      public static Color firebrick
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6980392f, 0.1333333f, 0.1333333f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9803922f, 0.9411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color floralWhite
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9803922f, 0.9411765f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1333333f, 0.5450981f, 0.1333333f, 1f)
      ///           </para>
      /// </summary>
      public static Color forestGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1333333f, 0.5450981f, 0.1333333f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8627452f, 0.8627452f, 0.8627452f, 1f)
      ///           </para>
      /// </summary>
      public static Color gainsboro
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(272f * (float) Math.E / 857f, 272f * (float) Math.E / 857f, 272f * (float) Math.E / 857f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9725491f, 0.9725491f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color ghostWhite
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9725491f, 0.9725491f, 1f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.8431373f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color gold
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.8431373f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.854902f, 0.6470588f, 0.1254902f, 1f)
      ///           </para>
      /// </summary>
      public static Color goldenRod
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.854902f, 0.6470588f, 0.1254902f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5f, 0.5f, 0.5f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray => Color.gray5;

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5f, 0.5f, 0.5f, 1f)
      ///           </para>
      /// </summary>
      public static Color grey => Color.gray5;

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1f, 0.1f, 0.1f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray1
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.1f, 0.1f, 0.1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.2f, 0.2f, 0.2f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray2
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.2f, 0.2f, 0.2f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.3f, 0.3f, 0.3f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray3
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.3f, 0.3f, 0.3f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4f, 0.4f, 0.4f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray4
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.4f, 0.4f, 0.4f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5f, 0.5f, 0.5f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray5
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.5f, 0.5f, 0.5f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6f, 0.6f, 0.6f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray6
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.6f, 0.6f, 0.6f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7f, 0.7f, 0.7f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray7
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.7f, 0.7f, 0.7f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8f, 0.8f, 0.8f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray8
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.8f, 0.8f, 0.8f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9f, 0.9f, 0.9f, 1f)
      ///           </para>
      /// </summary>
      public static Color gray9
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.9f, 0.9f, 0.9f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 1f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color green
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 1f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6784314f, 1f, 0.1843137f, 1f)
      ///           </para>
      /// </summary>
      public static Color greenYellow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6784314f, 1f, 0.1843137f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9411765f, 1f, 0.9411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color honeydew
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9411765f, 1f, 0.9411765f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.4117647f, 0.7058824f, 1f)
      ///           </para>
      /// </summary>
      public static Color hotPink
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.4117647f, 0.7058824f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8039216f, 0.3607843f, 0.3607843f, 1f)
      ///           </para>
      /// </summary>
      public static Color indianRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8039216f, 0.3607843f, 0.3607843f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.2941177f, 0f, 0.509804f, 1f)
      ///           </para>
      /// </summary>
      public static Color indigo
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.2941177f, 0.0f, 0.509804f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 1f, 0.9411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color ivory
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 1f, 0.9411765f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9411765f, 0.9019608f, 0.5490196f, 1f)
      ///           </para>
      /// </summary>
      public static Color khaki
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9411765f, 0.9019608f, 0.5490196f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9019608f, 0.9019608f, 0.9803922f, 1f)
      ///           </para>
      /// </summary>
      public static Color lavender
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9019608f, 0.9019608f, 0.9803922f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9411765f, 0.9607844f, 1f)
      ///           </para>
      /// </summary>
      public static Color lavenderBlush
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9411765f, 0.9607844f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4862745f, 0.9882354f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color lawnGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4862745f, 0.9882354f, 0.0f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9803922f, 0.8039216f, 1f)
      ///           </para>
      /// </summary>
      public static Color lemonChiffon
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9803922f, 0.8039216f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6784314f, 0.8470589f, 0.9019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6784314f, 0.8470589f, 0.9019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9411765f, 0.5019608f, 0.5019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightCoral
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9411765f, 0.5019608f, 0.5019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8784314f, 1f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightCyan
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.8784314f, 1f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9333334f, 0.8666667f, 0.509804f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightGoldenRod
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9333334f, 0.8666667f, 0.509804f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9803922f, 0.9803922f, 0.8235295f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightGoldenRodYellow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9803922f, 0.9803922f, 0.8235295f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8274511f, 0.8274511f, 0.8274511f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8274511f, 0.8274511f, 0.8274511f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5647059f, 0.9333334f, 0.5647059f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5647059f, 0.9333334f, 0.5647059f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.7137255f, 0.7568628f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightPink
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.7137255f, 0.7568628f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.627451f, 0.4784314f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSalmon
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.627451f, 0.4784314f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1254902f, 0.6980392f, 0.6666667f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSeaGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1254902f, 0.6980392f, 0.6666667f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5294118f, 0.8078432f, 0.9803922f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSkyBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5294118f, 0.8078432f, 0.9803922f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5176471f, 0.4392157f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSlateBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5176471f, 0.4392157f, 1f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4666667f, 0.5333334f, 0.6f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSlateGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4666667f, 0.5333334f, 0.6f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6901961f, 0.7686275f, 0.8705883f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightSteelBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6901961f, 0.7686275f, 0.8705883f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 1f, 0.8784314f, 1f)
      ///           </para>
      /// </summary>
      public static Color lightYellow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 1f, 0.8784314f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1960784f, 0.8039216f, 0.1960784f, 1f)
      ///           </para>
      /// </summary>
      public static Color limeGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1960784f, 0.8039216f, 0.1960784f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9803922f, 0.9411765f, 0.9019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color linen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9803922f, 0.9411765f, 0.9019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color magenta
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.0f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6901961f, 0.1882353f, 0.3764706f, 1f)
      ///           </para>
      /// </summary>
      public static Color maroon
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6901961f, 0.1882353f, 0.3764706f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4f, 0.8039216f, 0.6666667f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumAquamarine
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4f, 0.8039216f, 0.6666667f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 0.8039216f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 0.8039216f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7294118f, 0.3333333f, 0.8274511f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumOrchid
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7294118f, 0.3333333f, 0.8274511f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5764706f, 0.4392157f, 0.8588236f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumPurple
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5764706f, 0.4392157f, 0.8588236f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.2352941f, 0.7019608f, 0.4431373f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumSeaGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.2352941f, 0.7019608f, 0.4431373f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.482353f, 0.4078432f, 0.9333334f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumSlateBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.482353f, 0.4078432f, 0.9333334f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.9803922f, 0.6039216f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumSpringGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.0f, 0.9803922f, 0.6039216f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.282353f, 0.8196079f, 0.8000001f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumTurquoise
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.282353f, 0.8196079f, 0.8000001f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7803922f, 0.08235294f, 0.5215687f, 1f)
      ///           </para>
      /// </summary>
      public static Color mediumVioletRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7803922f, 0.08235294f, 0.5215687f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.09803922f, 0.09803922f, 0.4392157f, 1f)
      ///           </para>
      /// </summary>
      public static Color midnightBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.09803922f, 0.09803922f, 0.4392157f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9607844f, 1f, 0.9803922f, 1f)
      ///           </para>
      /// </summary>
      public static Color mintCream
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9607844f, 1f, 0.9803922f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.8941177f, 0.882353f, 1f)
      ///           </para>
      /// </summary>
      public static Color mistyRose
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.8941177f, 0.882353f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.8941177f, 0.7098039f, 1f)
      ///           </para>
      /// </summary>
      public static Color moccasin
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.8941177f, 0.7098039f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.8705883f, 0.6784314f, 1f)
      ///           </para>
      /// </summary>
      public static Color navajoWhite
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.8705883f, 0.6784314f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0f, 0.5019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color navyBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 0.0f, 0.5019608f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9921569f, 0.9607844f, 0.9019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color oldLace
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9921569f, 0.9607844f, 0.9019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5019608f, 0.5019608f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color olive
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5019608f, 0.5019608f, 0.0f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4196079f, 0.5568628f, 0.1372549f, 1f)
      ///           </para>
      /// </summary>
      public static Color oliveDrab
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4196079f, 0.5568628f, 0.1372549f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.6470588f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color orange
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.6470588f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.2705882f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color orangeRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.2705882f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.854902f, 0.4392157f, 0.8392158f, 1f)
      ///           </para>
      /// </summary>
      public static Color orchid
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.854902f, 0.4392157f, 0.8392158f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9333334f, 0.909804f, 0.6666667f, 1f)
      ///           </para>
      /// </summary>
      public static Color paleGoldenRod
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9333334f, 0.909804f, 0.6666667f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5960785f, 0.9843138f, 0.5960785f, 1f)
      ///           </para>
      /// </summary>
      public static Color paleGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5960785f, 0.9843138f, 0.5960785f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6862745f, 0.9333334f, 0.9333334f, 1f)
      ///           </para>
      /// </summary>
      public static Color paleTurquoise
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6862745f, 0.9333334f, 0.9333334f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8588236f, 0.4392157f, 0.5764706f, 1f)
      ///           </para>
      /// </summary>
      public static Color paleVioletRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8588236f, 0.4392157f, 0.5764706f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.937255f, 0.8352942f, 1f)
      ///           </para>
      /// </summary>
      public static Color papayaWhip
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.937255f, 0.8352942f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.854902f, 0.7254902f, 1f)
      ///           </para>
      /// </summary>
      public static Color peachPuff
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.854902f, 0.7254902f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8039216f, 0.5215687f, 0.2470588f, 1f)
      ///           </para>
      /// </summary>
      public static Color peru
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8039216f, 0.5215687f, 0.2470588f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.7529413f, 0.7960785f, 1f)
      ///           </para>
      /// </summary>
      public static Color pink
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.7529413f, 0.7960785f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8666667f, 0.627451f, 0.8666667f, 1f)
      ///           </para>
      /// </summary>
      public static Color plum
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8666667f, 0.627451f, 0.8666667f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6901961f, 0.8784314f, 0.9019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color powderBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6901961f, 0.8784314f, 0.9019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.627451f, 0.1254902f, 0.9411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color purple
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.627451f, 0.1254902f, 0.9411765f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4f, 0.2f, 0.6f, 1f)
      ///           </para>
      /// </summary>
      public static Color rebeccaPurple
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.4f, 0.2f, 0.6f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0f, 0f, 1f)
      ///           </para>
      /// </summary>
      public static Color red
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 0.0f, 0.0f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7372549f, 0.5607843f, 0.5607843f, 1f)
      ///           </para>
      /// </summary>
      public static Color rosyBrown
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7372549f, 0.5607843f, 0.5607843f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.254902f, 0.4117647f, 0.882353f, 1f)
      ///           </para>
      /// </summary>
      public static Color royalBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.254902f, 0.4117647f, 0.882353f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5450981f, 0.2705882f, 0.07450981f, 1f)
      ///           </para>
      /// </summary>
      public static Color saddleBrown
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5450981f, 0.2705882f, 0.07450981f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9803922f, 0.5019608f, 0.4470589f, 1f)
      ///           </para>
      /// </summary>
      public static Color salmon
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9803922f, 0.5019608f, 0.4470589f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9568628f, 0.6431373f, 0.3764706f, 1f)
      ///           </para>
      /// </summary>
      public static Color sandyBrown
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9568628f, 0.6431373f, 0.3764706f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1803922f, 0.5450981f, 0.3411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color seaGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1803922f, 0.5450981f, 0.3411765f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9607844f, 0.9333334f, 1f)
      ///           </para>
      /// </summary>
      public static Color seashell
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9607844f, 0.9333334f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.627451f, 0.3215686f, 0.1764706f, 1f)
      ///           </para>
      /// </summary>
      public static Color sienna
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.627451f, 0.3215686f, 0.1764706f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.7529413f, 0.7529413f, 0.7529413f, 1f)
      ///           </para>
      /// </summary>
      public static Color silver
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.7529413f, 0.7529413f, 0.7529413f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5294118f, 0.8078432f, 0.9215687f, 1f)
      ///           </para>
      /// </summary>
      public static Color skyBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5294118f, 0.8078432f, 0.9215687f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4156863f, 0.3529412f, 0.8039216f, 1f)
      ///           </para>
      /// </summary>
      public static Color slateBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4156863f, 0.3529412f, 0.8039216f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.4392157f, 0.5019608f, 0.5647059f, 1f)
      ///           </para>
      /// </summary>
      public static Color slateGray
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.4392157f, 0.5019608f, 0.5647059f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9803922f, 0.9803922f, 1f)
      ///           </para>
      /// </summary>
      public static Color snow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9803922f, 0.9803922f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8627452f, 0.1921569f, 0.1960784f, 1f)
      ///           </para>
      /// </summary>
      public static Color softRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(272f * (float) Math.E / 857f, 0.1921569f, 0.1960784f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.1882353f, 0.682353f, 0.7490196f, 1f)
      ///           </para>
      /// </summary>
      public static Color softBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.1882353f, 0.682353f, 0.7490196f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.5490196f, 0.7882354f, 0.1411765f, 1f)
      ///           </para>
      /// </summary>
      public static Color softGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.5490196f, 0.7882354f, 0.1411765f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.9333334f, 0.5490196f, 1f)
      ///           </para>
      /// </summary>
      public static Color softYellow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.9333334f, 0.5490196f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 1f, 0.4980392f, 1f)
      ///           </para>
      /// </summary>
      public static Color springGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(0.0f, 1f, 0.4980392f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.2745098f, 0.509804f, 0.7058824f, 1f)
      ///           </para>
      /// </summary>
      public static Color steelBlue
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.2745098f, 0.509804f, 0.7058824f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8235295f, 0.7058824f, 0.5490196f, 1f)
      ///           </para>
      /// </summary>
      public static Color tan
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8235295f, 0.7058824f, 0.5490196f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0f, 0.5019608f, 0.5019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color teal
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.0f, 0.5019608f, 0.5019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8470589f, 0.7490196f, 0.8470589f, 1f)
      ///           </para>
      /// </summary>
      public static Color thistle
      {
         get
        {
          return new Color(0.8470589f, 0.7490196f, 0.8470589f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.3882353f, 0.2784314f, 1f)
      ///           </para>
      /// </summary>
      public static Color tomato
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.3882353f, 0.2784314f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.2509804f, 0.8784314f, 0.8156863f, 1f)
      ///           </para>
      /// </summary>
      public static Color turquoise
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.2509804f, 0.8784314f, 0.8156863f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9333334f, 0.509804f, 0.9333334f, 1f)
      ///           </para>
      /// </summary>
      public static Color violet
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9333334f, 0.509804f, 0.9333334f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.8156863f, 0.1254902f, 0.5647059f, 1f)
      ///           </para>
      /// </summary>
      public static Color violetRed
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.8156863f, 0.1254902f, 0.5647059f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9607844f, 0.8705883f, 0.7019608f, 1f)
      ///           </para>
      /// </summary>
      public static Color wheat
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9607844f, 0.8705883f, 0.7019608f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 1f, 1f, 1f)
      ///           </para>
      /// </summary>
      public static Color white
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get => new Color(1f, 1f, 1f, 1f);
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.9607844f, 0.9607844f, 0.9607844f, 1f)
      ///           </para>
      /// </summary>
      public static Color whiteSmoke
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.9607844f, 0.9607844f, 0.9607844f, 1f);
        }
      }

      /// <summary>
      ///   <para>Color Preset of RGBA(1f, 0.92f, 0.016f, 1f).</para>
      /// </summary>
      public static Color yellow
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.92156863f, 0.015686275f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(0.6039216f, 0.8039216f, 0.1960784f, 1f)
      ///           </para>
      /// </summary>
      public static Color yellowGreen
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(0.6039216f, 0.8039216f, 0.1960784f, 1f);
        }
      }

      /// <summary>
      ///   <para>
      ///               Color Preset of RGBA(1f, 0.92f, 0.016f, 1f)
      ///           </para>
      /// </summary>
      public static Color yellowNice
      {
         [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
          return new Color(1f, 0.92156863f, 0.015686275f, 1f);
        }
      }

      internal static Dictionary<Color, string> defaultColorNames
      {
        get => Color.m_defaultColorNames ?? (Color.m_defaultColorNames = Color.InitializeColorNames());
      }

      private static Dictionary<Color, string> InitializeColorNames()
      {
        return new Dictionary<Color, string>()
        {
          {
            Color.red,
            "red"
          },
          {
            Color.green,
            "green"
          },
          {
            Color.blue,
            "blue"
          },
          {
            Color.yellow,
            "yellow"
          },
          {
            Color.cyan,
            "cyan"
          },
          {
            Color.magenta,
            "magenta"
          },
          {
            Color.gray1,
            "gray1"
          },
          {
            Color.gray2,
            "gray2"
          },
          {
            Color.gray3,
            "gray3"
          },
          {
            Color.gray4,
            "gray4"
          },
          {
            Color.gray5,
            "gray5"
          },
          {
            Color.gray6,
            "gray6"
          },
          {
            Color.gray7,
            "gray7"
          },
          {
            Color.gray8,
            "gray8"
          },
          {
            Color.gray9,
            "gray9"
          },
          {
            Color.white,
            "white"
          },
          {
            Color.whiteSmoke,
            "whiteSmoke"
          },
          {
            Color.gainsboro,
            "gainsboro"
          },
          {
            Color.lightGray,
            "lightGray"
          },
          {
            Color.silver,
            "silver"
          },
          {
            Color.darkGray,
            "darkGray"
          },
          {
            Color.dimGray,
            "dimGray"
          },
          {
            Color.black,
            "black"
          },
          {
            Color.darkRed,
            "darkRed"
          },
          {
            Color.brown,
            "brown"
          },
          {
            Color.firebrick,
            "firebrick"
          },
          {
            Color.crimson,
            "crimson"
          },
          {
            Color.softRed,
            "softRed"
          },
          {
            Color.indianRed,
            "indianRed"
          },
          {
            Color.violetRed,
            "violetRed"
          },
          {
            Color.mediumVioletRed,
            "mediumVioletRed"
          },
          {
            Color.deepPink,
            "deepPink"
          },
          {
            Color.hotPink,
            "hotPink"
          },
          {
            Color.lightPink,
            "lightPink"
          },
          {
            Color.pink,
            "pink"
          },
          {
            Color.paleVioletRed,
            "paleVioletRed"
          },
          {
            Color.maroon,
            "maroon"
          },
          {
            Color.rosyBrown,
            "rosyBrown"
          },
          {
            Color.lightCoral,
            "lightCoral"
          },
          {
            Color.salmon,
            "salmon"
          },
          {
            Color.tomato,
            "tomato"
          },
          {
            Color.darkSalmon,
            "darkSalmon"
          },
          {
            Color.coral,
            "coral"
          },
          {
            Color.orangeRed,
            "orangeRed"
          },
          {
            Color.lightSalmon,
            "lightSalmon"
          },
          {
            Color.sienna,
            "sienna"
          },
          {
            Color.saddleBrown,
            "saddleBrown"
          },
          {
            Color.chocolate,
            "chocolate"
          },
          {
            Color.sandyBrown,
            "sandyBrown"
          },
          {
            Color.peru,
            "peru"
          },
          {
            Color.darkOrange,
            "darkOrange"
          },
          {
            Color.burlywood,
            "burlywood"
          },
          {
            Color.tan,
            "tan"
          },
          {
            Color.moccasin,
            "moccasin"
          },
          {
            Color.peachPuff,
            "peachPuff"
          },
          {
            Color.bisque,
            "bisque"
          },
          {
            Color.navajoWhite,
            "navajoWhite"
          },
          {
            Color.wheat,
            "wheat"
          },
          {
            Color.orange,
            "orange"
          },
          {
            Color.darkGoldenRod,
            "darkGoldenRod"
          },
          {
            Color.goldenRod,
            "goldenRod"
          },
          {
            Color.lightGoldenRod,
            "lightGoldenRod"
          },
          {
            Color.gold,
            "gold"
          },
          {
            Color.softYellow,
            "softYellow"
          },
          {
            Color.lightGoldenRodYellow,
            "lightGoldenRodYellow"
          },
          {
            Color.beige,
            "beige"
          },
          {
            Color.lemonChiffon,
            "lemonChiffon"
          },
          {
            Color.lightYellow,
            "lightYellow"
          },
          {
            Color.khaki,
            "khaki"
          },
          {
            Color.paleGoldenRod,
            "paleGoldenRod"
          },
          {
            Color.darkKhaki,
            "darkKhaki"
          },
          {
            Color.olive,
            "olive"
          },
          {
            Color.oliveDrab,
            "oliveDrab"
          },
          {
            Color.yellowGreen,
            "yellowGreen"
          },
          {
            Color.darkOliveGreen,
            "darkOliveGreen"
          },
          {
            Color.softGreen,
            "softGreen"
          },
          {
            Color.greenYellow,
            "greenYellow"
          },
          {
            Color.chartreuse,
            "chartreuse"
          },
          {
            Color.lawnGreen,
            "lawnGreen"
          },
          {
            Color.darkGreen,
            "darkGreen"
          },
          {
            Color.forestGreen,
            "forestGreen"
          },
          {
            Color.limeGreen,
            "limeGreen"
          },
          {
            Color.darkSeaGreen,
            "darkSeaGreen"
          },
          {
            Color.lightGreen,
            "lightGreen"
          },
          {
            Color.paleGreen,
            "paleGreen"
          },
          {
            Color.seaGreen,
            "seaGreen"
          },
          {
            Color.mediumSeaGreen,
            "mediumSeaGreen"
          },
          {
            Color.springGreen,
            "springGreen"
          },
          {
            Color.mediumSpringGreen,
            "mediumSpringGreen"
          },
          {
            Color.aquamarine,
            "aquamarine"
          },
          {
            Color.mediumAquamarine,
            "mediumAquamarine"
          },
          {
            Color.turquoise,
            "turquoise"
          },
          {
            Color.mediumTurquoise,
            "mediumTurquoise"
          },
          {
            Color.lightSeaGreen,
            "lightSeaGreen"
          },
          {
            Color.lightSlateGray,
            "lightSlateGray"
          },
          {
            Color.slateGray,
            "slateGray"
          },
          {
            Color.darkSlateGray,
            "darkSlateGray"
          },
          {
            Color.teal,
            "teal"
          },
          {
            Color.darkCyan,
            "darkCyan"
          },
          {
            Color.lightCyan,
            "lightCyan"
          },
          {
            Color.mintCream,
            "mintCream"
          },
          {
            Color.honeydew,
            "honeydew"
          },
          {
            Color.azure,
            "azure"
          },
          {
            Color.paleTurquoise,
            "paleTurquoise"
          },
          {
            Color.darkTurquoise,
            "darkTurquoise"
          },
          {
            Color.cadetBlue,
            "cadetBlue"
          },
          {
            Color.powderBlue,
            "powderBlue"
          },
          {
            Color.softBlue,
            "softBlue"
          },
          {
            Color.lightBlue,
            "lightBlue"
          },
          {
            Color.deepSkyBlue,
            "deepSkyBlue"
          },
          {
            Color.skyBlue,
            "skyBlue"
          },
          {
            Color.lightSkyBlue,
            "lightSkyBlue"
          },
          {
            Color.steelBlue,
            "steelBlue"
          },
          {
            Color.dodgerBlue,
            "dodgerBlue"
          },
          {
            Color.lightSteelBlue,
            "lightSteelBlue"
          },
          {
            Color.ghostWhite,
            "ghostWhite"
          },
          {
            Color.aliceBlue,
            "aliceBlue"
          },
          {
            Color.lavender,
            "lavender"
          },
          {
            Color.cornflowerBlue,
            "cornflowerBlue"
          },
          {
            Color.royalBlue,
            "royalBlue"
          },
          {
            Color.navyBlue,
            "navyBlue"
          },
          {
            Color.midnightBlue,
            "midnightBlue"
          },
          {
            Color.darkBlue,
            "darkBlue"
          },
          {
            Color.mediumBlue,
            "mediumBlue"
          },
          {
            Color.slateBlue,
            "slateBlue"
          },
          {
            Color.lightSlateBlue,
            "lightSlateBlue"
          },
          {
            Color.mediumSlateBlue,
            "mediumSlateBlue"
          },
          {
            Color.darkSlateBlue,
            "darkSlateBlue"
          },
          {
            Color.mediumPurple,
            "mediumPurple"
          },
          {
            Color.rebeccaPurple,
            "rebeccaPurple"
          },
          {
            Color.blueViolet,
            "blueViolet"
          },
          {
            Color.indigo,
            "indigo"
          },
          {
            Color.purple,
            "purple"
          },
          {
            Color.darkOrchid,
            "darkOrchid"
          },
          {
            Color.darkViolet,
            "darkViolet"
          },
          {
            Color.mediumOrchid,
            "mediumOrchid"
          },
          {
            Color.darkMagenta,
            "darkMagenta"
          },
          {
            Color.violet,
            "violet"
          },
          {
            Color.plum,
            "plum"
          },
          {
            Color.thistle,
            "thistle"
          },
          {
            Color.orchid,
            "orchid"
          },
          {
            Color.lavenderBlush,
            "lavenderBlush"
          },
          {
            Color.seashell,
            "seashell"
          },
          {
            Color.blanchedAlmond,
            "blanchedAlmond"
          },
          {
            Color.papayaWhip,
            "papayaWhip"
          },
          {
            Color.cornsilk,
            "cornsilk"
          },
          {
            Color.ivory,
            "ivory"
          },
          {
            Color.linen,
            "linen"
          },
          {
            Color.floralWhite,
            "floralWhite"
          },
          {
            Color.antiqueWhite,
            "antiqueWhite"
          },
          {
            Color.oldLace,
            "oldLace"
          },
          {
            Color.mistyRose,
            "mistyRose"
          },
          {
            Color.snow,
            "snow"
          }
        };
      }
    #if UNITY_5_3_OR_NEWER
      
      [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator UnityEngine.Color(Color value) => new UnityEngine.Color(value.r, value.g, value.b, value.a);

      
      [MethodImpl(MethodImplOptions.AggressiveInlining)] public static implicit operator Color(UnityEngine.Color value) => new Color(value.r, value.g, value.b, value.a);
    #endif

    }
}
