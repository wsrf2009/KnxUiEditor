using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.Entity;
using System.Drawing;
using Structure;

namespace UIEditor.Component
{
    public class Template
    {
        /// <summary>
        /// 模板中的子控件
        /// </summary>
        public List<KNXView> Views { get; set; }
        /// <summary>
        /// 模板的预览图片
        /// </summary>
        public Image Preview { get; set; }
        /// <summary>
        /// 模板的名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模板图片资源存放路径
        /// </summary>
        public string ResImgDir { get; set; }
        ///// <summary>
        ///// 最后修改时间
        ///// </summary>
        //public string LastModifiedTime { get; set; }

        ///// <summary>
        ///// 编辑器版本
        ///// </summary>
        //public string EditorVersion { get; set; }

        public KNXVersion Version { get; set; }
    }
}
