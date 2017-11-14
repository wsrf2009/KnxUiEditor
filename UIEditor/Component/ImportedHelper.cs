using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIEditor.Component
{
    public class ImportedHelper
    {
        private static bool IsLessThan(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                return false;
            }

            if (MyCache.VersionOfImportedFile.EditorVersion.CompareTo(version) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsLessThan2_0_3()
        {
            return IsLessThan("2.0.3");
        }

        public static bool IsLessThan2_1_8()
        {
            return IsLessThan("2.1.8");
        }

        public static bool IsLessThan2_5_2()
        {
            return IsLessThan("2.5.2");
        }

        public static bool IsLessThan2_5_3()
        {
            return IsLessThan("2.5.3");
        }

        public static bool IsLessThan2_5_4()
        {
            return IsLessThan("2.5.4");
        }

        public static bool IsLessThan2_5_6()
        {
            return IsLessThan("2.5.6");
        }

        public static bool IsLessThan2_5_7()
        {
            return IsLessThan("2.5.7");
        }

        public static bool IsLessThan2_7_1()
        {
            return IsLessThan("2.7.1");
        }
    }
}
