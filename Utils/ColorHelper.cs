using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Utils
{
    public class ColorHelper
    {
        /// <summary>
        /// 颜色类Color的实例转换为字符串
        /// </summary>
        /// <param name="selectedColor"></param>
        /// <returns></returns>
        public static string ColorToHexStr(Color selectedColor)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("#");
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.R }));
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.G }));
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.B }));
            return sb.ToString();
        }

        /// <summary>
        /// 将颜色字符串转为Color类实例
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color HexStrToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }

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

            Double Y = 0.299 * R + 0.587 * G + 0.114 * B; ;

            Double Cb = 0.564 * (B - Y);
            Double Cr = 0.713 * (R - Y);

            Y += Y * percent / 100;

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
