using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace KNX
{
    public class KNXResMang
    {
        public static string GetString(string strId)
        {
            ResourceManager rm = new ResourceManager("KNX.Properties.Resources", Assembly.GetExecutingAssembly());
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;

            return rm.GetString(strId, ci);
        }

        public static Image GetImage(string imgId) {
            ResourceManager rm = new ResourceManager("UIEditor.Properties.Resources", Assembly.GetExecutingAssembly());
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;

            return (Image)rm.GetObject(imgId, ci);
        }
    }
}
