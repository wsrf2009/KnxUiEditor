using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UIEditor.Component
{
    public class ColorHelper
    {
        /// <summary>
        /// 相对调节颜色的亮度
        /// </summary>
        /// <param name="oldColor">调节亮度的基础色，类型为Color</param>
        /// <param name="percent">调节的百分比，整型量：-100~100</param>
        /// <returns>调节亮度后的颜色，类型为Color</returns>
        public static Color changeBrightnessOfColor(Color oldColor, int percent)
        {
            int R = oldColor.R;
            int G = oldColor.G;
            int B = oldColor.B;
            //int value = (int)(2.55 * percent);
            //int r = oldColor.R + /*value*/ oldColor.R*percent/100;
            //int g = oldColor.G +/*value*/ oldColor.G*percent/100;
            //int b = oldColor.B +/*value*/ oldColor.B*percent/100;
            //if (r > 255)
            //{
            //    r = 255;
            //}
            //else if (r < 0)
            //{
            //    r = 0;
            //}

            //if (g > 255)
            //{
            //    g = 255;
            //}
            //else if (g < 0)
            //{
            //    g = 0;
            //}

            //if (b > 255)
            //{
            //    b = 255;
            //}
            //else if (b < 0)
            //{
            //    b = 0;
            //}

            //return Color.FromArgb(r, g, b);

            Double Y = 0.299 * R + 0.587 * G + 0.114 * B; ;
            //Double U = -0.1687*R-0.3313*G+0.5*B+128;
            //Double V = 0.5*R-0.4187*G-0.0813*B+128;
            Double Cb = 0.564*(B - Y);
            Double Cr = 0.713 * (R - Y);

            Y += Y * percent / 100;

            //R = (int)(Y + 1.402 * (V - 128));
            //G = (int)(Y - 0.34414 * (U - 128) - 0.71414 * (V - 128));
            //B = (int)(Y + 1.722 * (U - 128));
            R = (int)(Y + 1.042 * Cr);
            G = (int)(Y - 0.344 * Cb - 0.714 * Cr);
            B = (int)(Y + 1.772 * Cb);

            if (R > 255)
            {
                R = 255;
            }
            else if (R < 0)
            {
                R = 0;
            }

            if (G > 255)
            {
                G = 255;
            }
            else if (G < 0)
            {
                G = 0;
            }

            if (B > 255)
            {
                B = 255;
            }
            else if (B < 0)
            {
                B = 0;
            }

            return Color.FromArgb(R, G, B);
        }
    }
}
