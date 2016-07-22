using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using SourceGrid;
using Structure;
using UIEditor.Component;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;


namespace UIEditor.Entity
{
    [TypeConverter(typeof(AppNode.PropertyConverter))]
    [Serializable]
    public class AppNode : ViewNode
    {
        private static int index = 0;

        #region 属性
        /// <summary>
        /// 产品说明信息
        /// </summary>
        [EditorAttribute(typeof(PropertyGridRichTextEditor), typeof(UITypeEditor))]
        public string About { get; set; }

        /// <summary>
        /// 企业Logo
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public String Logo { get; set; }

        /// <summary>
        /// 应用程序图标
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }

        #endregion

        #region 构造函数
        public AppNode()
        {
            index++;

            string FileDefaultIcon = Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon);
            if (!File.Exists(FileDefaultIcon))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, MyConst.DefaultIcon), Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon));
            }

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;
            this.Text = ResourceMng.GetString("TextApplication") + "_" + index;

            this.About = "KNX App UIEditor";
            this.Logo = MyConst.DefaultIcon;
            this.Symbol = MyConst.DefaultIcon;
            //this.DefaultLanguage = Language.Chinese;
            this.Width = 800;
            this.Height = 600;
            //this.Size = new Size(800, 600);
        }

        public override object Clone()
        {
            AppNode node = base.Clone() as AppNode;
            node.About = this.About;
            node.Logo = this.Logo;
            node.Symbol = this.Symbol;
            return node;
        }

        public AppNode(KNXApp knx)
            : base(knx)
        {
            string FileDefaultIcon = Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon);
            if (!File.Exists(FileDefaultIcon))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, MyConst.DefaultIcon), Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon));
            }

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;
            this.Text = knx.Text;

            this.About = knx.About;
            this.Logo = knx.Logo;
            this.Symbol = knx.Symbol;
            //this.DefaultLanguage = knx.DefaultLanguage;
        }

        protected AppNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        public KNXApp ToKnx()
        {
            KNXApp knx = new KNXApp();

            base.ToKnx(knx);

            knx.About = this.About;
            knx.Logo = this.Logo;
            knx.Symbol = this.Symbol;

            knx.Areas = new System.Collections.Generic.List<KNXArea>();

            return knx;
        }

        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);

                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                STControlPropertyDescriptor propText = new STControlPropertyDescriptor(collection["Text"]);
                propText.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                propText.SetDisplayName(ResourceMng.GetString("PropText"));
                propText.SetDescription(ResourceMng.GetString("DescriptionForPropText"));
                list.Add(propText);

                STControlPropertyDescriptor PropAbout = new STControlPropertyDescriptor(collection["About"]);
                PropAbout.SetCategory(ResourceMng.GetString(""));
                PropAbout.SetDisplayName(ResourceMng.GetString("PropAbout"));
                PropAbout.SetDescription(ResourceMng.GetString("DescriptionForPropAbout"));
                list.Add(PropAbout);

                STControlPropertyDescriptor PropComLogo = new STControlPropertyDescriptor(collection["Logo"]);
                PropComLogo.SetCategory(ResourceMng.GetString(""));
                PropComLogo.SetDisplayName(ResourceMng.GetString("PropComLogo"));
                PropComLogo.SetDescription(ResourceMng.GetString(""));
                list.Add(PropComLogo);

                STControlPropertyDescriptor PropIcon = new STControlPropertyDescriptor(collection["Symbol"]);
                PropIcon.SetCategory(ResourceMng.GetString(""));
                PropIcon.SetDisplayName(ResourceMng.GetString("PropIcon"));
                PropIcon.SetDescription(ResourceMng.GetString(""));
                list.Add(PropIcon);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }

    }
}
