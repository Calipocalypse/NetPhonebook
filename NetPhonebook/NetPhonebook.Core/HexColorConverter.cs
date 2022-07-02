using System;
using System.Windows.Media;

namespace NetPhonebook.Core
{
    public static class HexColorConverter
    {
        public static string ToHex(Color color)
        {
            byte hexA = color.A;
            byte hexR = color.R;
            byte hexG = color.G;
            byte hexB = color.B;
            byte[] hexes = { hexA, hexR, hexG, hexB};
            return ("#" + Convert.ToHexString(hexes));
        }

        public static Color ToColor(string colorInHex)
        {
            if (colorInHex == null) return Color.FromRgb(5,5,5);
            colorInHex = colorInHex.Substring(1);
            byte colorA = Convert.ToByte(colorInHex.Substring(0, 2), 16);
            byte colorR = Convert.ToByte(colorInHex.Substring(2, 2), 16);
            byte colorG = Convert.ToByte(colorInHex.Substring(4, 2), 16);
            byte colorB = Convert.ToByte(colorInHex.Substring(6, 2), 16);
            return Color.FromArgb(colorA,colorR,colorG,colorB);
        }

        public static SolidColorBrush ToSolidColor(string colorInHex)
        {
            return new SolidColorBrush(ToColor(colorInHex));
        }

        public static string ToHex(SolidColorBrush colorButSolid)
        {
            return ToHex(colorButSolid.Color);
        }
    }
}