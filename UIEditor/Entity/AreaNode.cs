using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using Structure;
using UIEditor.Component;
using System.Drawing;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(AreaNode.PropertyConverter))]
    [Serializable]
    public class AreaNode : ViewNode
    {
        #region 常量
        private const string NAME_SYMBOL = "Symbol.png";
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        //[EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor)),
        //TypeConverterAttribute(typeof(STImageConverter))]
        //public Image Symbol { get; set; }
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }

        public string PinCode { get; set; }
        #endregion

        #region 构造函数
        public AreaNode()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;

            this.Text = UIResMang.GetString("TextArea");
            this.Title = UIResMang.GetString("TextArea") + index;
            SetText(this.Title);

            //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjectResImgDir, "default_icon.png"));
            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "symbol2.png"));

            this.PinCode = "";
        }

        public AreaNode(KNXArea knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;
            SetText(this.Title);

            //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(this.ImagePath, NAME_SYMBOL));
            this.PinCode = knx.PinCode;

            if (ImportedHelper.IsLessThan2_5_6())
            {
                this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_SYMBOL));
            }
            else
            {
                this.Symbol = knx.Symbol;
            }
        }

        public AreaNode(KNXArea knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            //string knxImage = GetImageName(knx.Id); // KNX图片资源名称
            //string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径
            //FileHelper.CopyFolderContent(knxImagePath, this.ImagePath, true); // 拷贝KNX图片资源

            //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(this.ImagePath, NAME_SYMBOL));
            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_SYMBOL));
            }
            else
            {
                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.Symbol));
            }
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            AreaNode node = base.Clone() as AreaNode;
            node.Symbol = this.Symbol;
            node.PinCode = this.PinCode;

            return node;
        }

        public override object Copy()
        {
            AreaNode node = base.Copy() as AreaNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextArea"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextArea"));
        }
        #endregion

        #region 导出
        public KNXArea ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXArea();

            base.ToKnx(knx, worker);

            knx.Symbol = this.Symbol;
            //ImageHelper.SaveImageAsPNG(this.Symbol, Path.Combine(this.ImagePath, NAME_SYMBOL));
            knx.PinCode = this.PinCode;

            knx.Rooms = new List<KNXRoom>();

            //foreach (string file in Directory.GetFiles(this.ImagePath))
            //{
            //    string fileName = file.Substring(file.LastIndexOf("\\") + 1);
            //    if (fileName == NAME_SYMBOL)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        File.Delete(file);
            //    }
            //}

            MyCache.ValidResImgNames.Add(knx.Symbol);

            return knx;
        }

        public KNXArea ExportTo(BackgroundWorker worker, string dir)
        {
            KNXArea knx = this.ToKnx(worker);

            knx.Symbol = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.Symbol), dir);
            //FileHelper.CopyFolder(this.ImagePath, dir, true);

            return knx;
        }
        #endregion

        #region 属性框显示
        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);

                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                //STControlPropertyDescriptor propId = new STControlPropertyDescriptor(collection["Id"]);
                //propId.SetCategory(UIResMang.GetString("CategoryId"));
                //propId.SetDisplayName(UIResMang.GetString("PropId"));
                //propId.SetIsReadOnly(true);
                //list.Add(propId);

                STControlPropertyDescriptor propTitle = new STControlPropertyDescriptor(collection["Title"]);
                propTitle.SetCategory(UIResMang.GetString("CategoryTitle"));
                propTitle.SetDisplayName(UIResMang.GetString("PropTitle"));
                //propTitle.SetDescription(UIResMang.GetString("DescriptionForPropTitle"));
                list.Add(propTitle);

                STControlPropertyDescriptor PropTitleFont = new STControlPropertyDescriptor(collection["TitleFont"]);
                PropTitleFont.SetCategory(UIResMang.GetString("CategoryTitle"));
                PropTitleFont.SetDisplayName(UIResMang.GetString("PropFont"));
                //PropTitleFont.SetDescription(UIResMang.GetString("DescriptionForPropTitleFont"));
                list.Add(PropTitleFont);

                STControlPropertyDescriptor PropIcon = new STControlPropertyDescriptor(collection["Symbol"]);
                PropIcon.SetCategory(UIResMang.GetString(""));
                PropIcon.SetDisplayName(UIResMang.GetString("PropIcon"));
                PropIcon.SetDescription(UIResMang.GetString(""));
                list.Add(PropIcon);

                STControlPropertyDescriptor PropPassword = new STControlPropertyDescriptor(collection["PinCode"]);
                PropPassword.SetCategory(UIResMang.GetString(""));
                PropPassword.SetDisplayName(UIResMang.GetString("PropPassword"));
                PropPassword.SetDescription(UIResMang.GetString(""));
                list.Add(PropPassword);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
