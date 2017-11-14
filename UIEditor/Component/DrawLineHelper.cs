using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UIEditor.Component
{
    public class DrawLineHelper
    {
        public static double MillimetersToPixelsWidth(IntPtr hDc, double length)
        {
            int width = NativeMethods.GetDeviceCaps(hDc, NativeMethods.CapIndex.HORZSIZE);
            int pixels = NativeMethods.GetDeviceCaps(hDc, NativeMethods.CapIndex.HORZRES);
            return (((double)pixels / (double)width) * (double)length);
        }

        public static double PixelsToMillimetersWidth(IntPtr hDc, double length)
        {
            int width = NativeMethods.GetDeviceCaps(hDc, NativeMethods.CapIndex.HORZSIZE);
            int pixels = NativeMethods.GetDeviceCaps(hDc, NativeMethods.CapIndex.HORZRES);
            return (double)width / (double)pixels * (double)length;
        }

        public class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetDC(IntPtr hWnd);

            [DllImport("gdi32.dll")]
            public static extern int GetDeviceCaps(IntPtr hdc, int Index);

            public class CapIndex
            {
                public static readonly int DRIVERVERSION = 0;
                public static readonly int TECHNOLOGY = 2;
                public static readonly int HORZSIZE = 4;
                public static readonly int VERTSIZE = 6;
                public static readonly int HORZRES = 8;
                public static readonly int VERTRES = 10;
                public static readonly int BITSPIXEL = 12;
                public static readonly int PLANES = 14;
                public static readonly int NUMBRUSHES = 16;
                public static readonly int NUMPENS = 18;
                public static readonly int NUMMARKERS = 20;
                public static readonly int NUMFONTS = 22;
                public static readonly int NUMCOLORS = 24;
                public static readonly int PDEVICESIZE = 26;
                public static readonly int CURVECAPS = 28;
                public static readonly int LINECAPS = 30;
                public static readonly int POLYGONALCAPS = 32;
                public static readonly int TEXTCAPS = 34;
                public static readonly int CLIPCAPS = 36;
                public static readonly int RASTERCAPS = 38;
                public static readonly int ASPECTX = 40;
                public static readonly int ASPECTY = 42;
                public static readonly int ASPECTXY = 44;
                public static readonly int SHADEBLENDCAPS = 45;
                public static readonly int LOGPIXELSX = 88;
                public static readonly int LOGPIXELSY = 90;
                public static readonly int SIZEPALETTE = 104;
                public static readonly int NUMRESERVED = 106;
                public static readonly int COLORRES = 108;
                public static readonly int PHYSICALWIDTH = 110;
                public static readonly int PHYSICALHEIGHT = 111;
                public static readonly int PHYSICALOFFSETX = 112;
                public static readonly int PHYSICALOFFSETY = 113;
                public static readonly int SCALINGFACTORX = 114;
                public static readonly int SCALINGFACTORY = 115;
                public static readonly int VREFRESH = 116;
                public static readonly int DESKTOPVERTRES = 117;
                public static readonly int DESKTOPHORZRES = 118;
                public static readonly int BLTALIGNMENT = 119;
            }
        }
    }
}
