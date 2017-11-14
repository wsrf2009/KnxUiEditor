using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Structure;
using UIEditor.Component;
using System.Drawing.Design;
using UIEditor.PropertyGridEditor;
using UIEditor.UserClass;


namespace UIEditor.Entity
{
    [TypeConverter(typeof(AppNode.PropertyConverter))]
    [Serializable]
    public class AppNode : ViewNode
    {
        #region 常量
        private const string NAME_SYMBOL = "Symbol.png";
        private const string NAME_BACKGROUNDIMAGE = "AppBackgroundImage.png";
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        /// <summary>
        /// 产品说明信息
        /// </summary>
        [EditorAttribute(typeof(PropertyGridMultiLineTextEditor), typeof(UITypeEditor))]
        public string About { get; set; }

        /// <summary>
        /// 应用程序图标
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [ReadOnlyAttribute(true)]
        public int Version { get; set; }

        /// <summary>
        /// 编辑器版本
        /// </summary>
        [ReadOnlyAttribute(true)]
        public string EditorVersion { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ReadOnlyAttribute(true)]
        public string LastModified { get; set; }
        #endregion

        #region 构造函数
        public AppNode()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;

            this.Text = UIResMang.GetString("TextProject");
            this.Title = UIResMang.GetString("TextProject") + index;
            SetText(this.Title);

            //this.Width = 1280;
            //this.Height = 800;
            this.Size = new Size(1280, 800);

            //MyCache.Width = this.Width;
            //MyCache.Height = this.Height;
            MyCache.AppSize = this.Size;

            this.About = "KNX App UIEditor";
            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "symbol2.png"));
            this.BackgroundImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "bj1.jpg"));
        }

        public AppNode(KNXApp knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;
            SetText(this.Title);

            //MyCache.Width = this.Width;
            //MyCache.Height = this.Height;
            MyCache.AppSize = this.Size;

            this.About = knx.About;

            if (ImportedHelper.IsLessThan2_0_3())
            {
                //this.Width = 1280;
                //this.Height = 800;
                this.Size = new Size(1280, 800);

                if (!string.IsNullOrEmpty(knx.Symbol))
                {
                    this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.Symbol));
                }
                else if (!string.IsNullOrEmpty(knx.Logo))
                {
                    this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.Logo));
                }
                if (!string.IsNullOrEmpty(knx.BackgroundImage))
                {
                    this.BackgroundImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.BackgroundImage));
                }
            }
            else if (ImportedHelper.IsLessThan2_5_6())
            {
                this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_SYMBOL));
                this.BackgroundImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_BACKGROUNDIMAGE));
            }
            else
            {
                //this.Width = knx.Width;
                //this.Height = knx.Height;
                this.Size = new Size(knx.Width, knx.Height);
                this.Symbol = knx.Symbol;
                this.BackgroundImage = knx.BackgroundImage;
            }

            GetProjectVersion();
        }

        public AppNode(KNXApp knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id
            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_SYMBOL));
                this.BackgroundImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_BACKGROUNDIMAGE));
            }
            else
            {
                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.Symbol));
                this.BackgroundImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.BackgroundImage));
            }
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            AppNode node = base.Clone() as AppNode;
            node.About = this.About;
            node.Symbol = this.Symbol;
            node.BackgroundImage = this.BackgroundImage;
            return node;
        }

        public override object Copy()
        {
            AppNode node = base.Copy() as AppNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextProject"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextProject"));
        }
        #endregion

        #region 导出
        public KNXApp ToKnx(BackgroundWorker worker)
        {
            KNXApp knx = new KNXApp();

            base.ToKnx(knx, worker);

            GetProjectVersion();

            //knx.Width = this.Width;
            //knx.Height = this.Height;
            knx.Width = this.Size.Width;
            knx.Height = this.Size.Height;

            knx.About = this.About;
            knx.Symbol = this.Symbol;
            knx.BackgroundImage = this.BackgroundImage;

            knx.Areas = new System.Collections.Generic.List<KNXArea>();

            MyCache.ValidResImgNames.Add(knx.Symbol);
            MyCache.ValidResImgNames.Add(knx.BackgroundImage);
            

            return knx;
        }

        public KNXApp ExportTo(BackgroundWorker worker, string dir)
        {
            KNXApp knx = this.ToKnx(worker);

            knx.Symbol = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.Symbol), dir);
            knx.BackgroundImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.BackgroundImage), dir);

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

                STControlPropertyDescriptor propTitle = new STControlPropertyDescriptor(collection["Title"]);
                propTitle.SetCategory(UIResMang.GetString("CategoryTitle"));
                propTitle.SetDisplayName(UIResMang.GetString("PropTitle"));
                list.Add(propTitle);

                //STControlPropertyDescriptor PropWidth = new STControlPropertyDescriptor(collection["Width"]);
                //PropWidth.SetCategory(UIResMang.GetString("CategoryLayout"));
                //PropWidth.SetDisplayName(UIResMang.GetString("PropWidth"));
                //list.Add(PropWidth);

                //STControlPropertyDescriptor PropHeight = new STControlPropertyDescriptor(collection["Height"]);
                //PropHeight.SetCategory(UIResMang.GetString("CategoryLayout"));
                //PropHeight.SetDisplayName(UIResMang.GetString("PropHeight"));
                //list.Add(PropHeight);

                STControlPropertyDescriptor PropSize = new STControlPropertyDescriptor(collection["Size"]);
                PropSize.SetCategory(UIResMang.GetString("CategoryLayout"));
                PropSize.SetDisplayName(UIResMang.GetString("PropSize"));
                PropSize.SetDescription(UIResMang.GetString(""));
                list.Add(PropSize);

                STControlPropertyDescriptor PropAppBackgroundImage = new STControlPropertyDescriptor(collection["BackgroundImage"]);
                PropAppBackgroundImage.SetCategory(UIResMang.GetString(""));
                PropAppBackgroundImage.SetDisplayName(UIResMang.GetString("PropAppBackgroundImage"));
                PropAppBackgroundImage.SetDescription(UIResMang.GetString("DescriptionForPropAppBackgroundImage"));
                list.Add(PropAppBackgroundImage);

                STControlPropertyDescriptor PropAbout = new STControlPropertyDescriptor(collection["About"]);
                PropAbout.SetCategory(UIResMang.GetString(""));
                PropAbout.SetDisplayName(UIResMang.GetString("PropAbout"));
                PropAbout.SetDescription(UIResMang.GetString("DescriptionForPropAbout"));
                list.Add(PropAbout);

                STControlPropertyDescriptor PropSymbol = new STControlPropertyDescriptor(collection["Symbol"]);
                PropSymbol.SetCategory(UIResMang.GetString(""));
                PropSymbol.SetDisplayName(UIResMang.GetString("PropSymbol"));
                PropSymbol.SetDescription(UIResMang.GetString(""));
                list.Add(PropSymbol);

                STControlPropertyDescriptor PropVersion = new STControlPropertyDescriptor(collection["Version"]);
                PropVersion.SetCategory(UIResMang.GetString("CategoryVersion"));
                PropVersion.SetDisplayName(UIResMang.GetString("PropVersion"));
                PropVersion.SetDescription(UIResMang.GetString(""));
                PropVersion.SetIsReadOnly(true);
                list.Add(PropVersion);

                STControlPropertyDescriptor PropEditorVersion = new STControlPropertyDescriptor(collection["EditorVersion"]);
                PropEditorVersion.SetCategory(UIResMang.GetString("CategoryVersion"));
                PropEditorVersion.SetDisplayName(UIResMang.GetString("PropEditorVersion"));
                PropEditorVersion.SetDescription(UIResMang.GetString("DescriptionForPropEditorVersion"));
                PropEditorVersion.SetIsReadOnly(true);
                list.Add(PropEditorVersion);

                STControlPropertyDescriptor PropLastModified = new STControlPropertyDescriptor(collection["LastModified"]);
                PropLastModified.SetCategory(UIResMang.GetString("CategoryVersion"));
                PropLastModified.SetDisplayName(UIResMang.GetString("LastModifiedTime"));
                PropLastModified.SetDescription(UIResMang.GetString(""));
                PropLastModified.SetIsReadOnly(true);
                list.Add(PropLastModified);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion

        #region 私有方法
        private void GetProjectVersion()
        {
            this.Version = MyCache.VersionOfImportedFile.Version;
            this.EditorVersion = MyCache.VersionOfImportedFile.EditorVersion;
            this.LastModified = MyCache.VersionOfImportedFile.LastModified;
        }
        #endregion
    }
}
