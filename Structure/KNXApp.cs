using System;
using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 应用名，这个应用程序的基本信息
    /// </summary>
    public class KNXApp : KNXView
    {
        /// <summary>
        /// 产品说明信息
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// 企业 Logo
        /// 已被弃用。2.1.1
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 应用程序图标
        /// </summary>
        public string Symbol { get; set; }

        ///// <summary>
        ///// 控件的背景图片
        ///// </summary>
        //public string BackgroundImage { get; set; }

        /// <summary>
        /// 空间划分
        /// </summary>
        public List<KNXArea> Areas { get; set; }

        ///// <summary>
        ///// 默认语言
        ///// </summary>
        //public Language DefaultLanguage { get; set; }

        ///// <summary>
        ///// 屏幕宽度
        ///// </summary>
        //public int ScreenWidth { get; set; }

        ///// <summary>
        ///// 屏幕高度
        ///// </summary>
        //public int ScreenHeight { get; set; }
    }
}
