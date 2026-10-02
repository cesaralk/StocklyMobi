using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocklyMobi
{
    public static class ThemeColor
    {
        public static List<string> ColorList = new List<string>() {"#0F172A",
                                                                    "#1E293B",
                                                                    "#2563EB",
                                                                    "#0EA5E9",
                                                                    "#10B981",
                                                                    "#22C55E",
                                                                    "#F59E0B",
                                                                    "#DC2626",
                                                                    "#64748B" };

        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            //If correction factor is less than 0 , darken color.

            if(correctionFactor <0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }

            // if correction factor si greater than zero, lighten color.
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
