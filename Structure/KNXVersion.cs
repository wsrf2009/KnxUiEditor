using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure
{
    public class KNXVersion
    {
        public KNXVersion()
        {
            Version = 1;
            EditorVersion = "1.0.0";
            LastModified = System.DateTime.Now.ToString();
            SerialNumber = "xxxxxx-xxxxxx-xxxxxx-xxxxxx";
        }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 编辑器版本
        /// </summary>
        public string EditorVersion { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string LastModified { get; set; }


        /// <summary>
        /// 产品序号
        /// </summary>
        public string SerialNumber { get; set; }

    }
}
