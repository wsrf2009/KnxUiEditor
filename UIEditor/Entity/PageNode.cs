using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using Structure;
using UIEditor.Component;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using Utils;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(PageNode.PropertyConverter))]
    [Serializable]
    public class PageNode : ContainerNode
    {
        #region 常量
        private const string NAME_BACKGROUNDIMAGE = "PageBackgroundImage.png";
        #endregion

        #region 变量
        private static int index = 0;

        public Image ImgBackgroundImage
        {
            get
            {
                if (null != this.BackgroundImage)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.BackgroundImage));
                }

                return null;
            }
        }
        #endregion

        #region 属性

        #endregion

        #region 构造函数
        public PageNode():base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;

            this.Text = UIResMang.GetString("TextPage");
            this.Title = UIResMang.GetString("TextPage") + index;
            SetText(this.Title);

            this.Size = MyCache.AppSize;
            this.Alpha = 1;
            this.Radius = 0;

            this.BackgroundImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "bj7.png"));
        }

        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <param name="knx"></param>
        public PageNode(KNXPage knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
            SetText(this.Title);

            if (ImportedHelper.IsLessThan2_0_3())
            {
                if (!string.IsNullOrEmpty(knx.BackgroundImage))
                {
                    this.BackgroundImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.BackgroundImage));
                }
            }
            else if (ImportedHelper.IsLessThan2_5_6())
            {
                this.BackgroundImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_BACKGROUNDIMAGE));
            }
            else
            {
                this.BackgroundImage = knx.BackgroundImage;
            }
        }

        public PageNode(KNXPage knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.BackgroundImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_BACKGROUNDIMAGE)); 
            }
            else
            {
                this.BackgroundImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.BackgroundImage));
            }
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            PageNode node = base.Clone() as PageNode;
            node.BackgroundImage = this.BackgroundImage;
            return node;
        }

        public override object Copy()
        {
            PageNode node = base.Copy() as PageNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextPage"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextPage"));
        }

        public override void DrawAt(Graphics g, float ratio, bool preview)
        {
            base.DrawAt(g, ratio, preview);

            if (null == g)
            {
                return;
            }

            if (null != this.ImgBackgroundImage)
            {
                g.DrawImage(ImageHelper.Resize(this.ImgBackgroundImage, this.RectInPage.Size, false), 0, 0);
            }
            else
            {
                Color backColor = Color.FromArgb((int)(this.Alpha * 255), this.BackgroundColor);
                SolidBrush brush = new SolidBrush(backColor);
                FillRoundRectangle(g, brush, this.RectInPage, this.Radius, 1.0f, ratio);
            }

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name)
                {
                    node.DrawAt(g, ratio, preview);
                }
            }

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name)
                {
                    node.DrawAt(g, ratio, preview);
                }
            }
        }
        #endregion

        #region 转为KNX
        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <returns></returns>
        public KNXPage ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXPage();

            base.ToKnx(knx, worker);

            knx.BackgroundImage = this.BackgroundImage;
            MyCache.ValidResImgNames.Add(knx.BackgroundImage);

            return knx;
        }

        public KNXPage ExportTo(BackgroundWorker worker, string dir)
        {
            KNXPage knx = this.ToKnx(worker);

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

                STControlPropertyDescriptor PropSize = new STControlPropertyDescriptor(collection["Size"]);
                PropSize.SetCategory(UIResMang.GetString("CategoryLayout"));
                PropSize.SetDisplayName(UIResMang.GetString("PropSize"));
                PropSize.SetDescription(UIResMang.GetString(""));
                PropSize.SetIsReadOnly(true);
                list.Add(PropSize);

                STControlPropertyDescriptor PropBackColor = new STControlPropertyDescriptor(collection["BackgroundColor"]);
                PropBackColor.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropBackColor.SetDisplayName(UIResMang.GetString("PropBackColor"));
                PropBackColor.SetDescription(UIResMang.GetString(""));
                list.Add(PropBackColor);

                STControlPropertyDescriptor PropBackgroundImage = new STControlPropertyDescriptor(collection["BackgroundImage"]);
                PropBackgroundImage.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropBackgroundImage.SetDisplayName(UIResMang.GetString("PropBackgroundImage"));
                PropBackgroundImage.SetDescription(UIResMang.GetString(""));
                list.Add(PropBackgroundImage);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion

        #region 公共方法
        public PageNode CreateTwinsPageNode()
        {
            PageNode node = this.Clone() as PageNode;
            this.Tag = node;
            node.Tag = this;

            return node;
        }

        public void CopyPageNode()
        {
            PageNode pageNodeClone = this.Tag as PageNode;
            if (null != pageNodeClone)
            {
                PageNode pageNodeCopy = pageNodeClone.Copy() as PageNode;
                this.Text = pageNodeCopy.Text;
                this.Tag = pageNodeCopy;
                pageNodeCopy.Tag = this;
            }
        }

        public PageNode GetTwinsPageNode()
        {
            return this.Tag as PageNode;
        }

        public void SetNewSize(Size size)
        {
            //this.Width = width;
            //this.Height = height;
            this.Size = size;

            PageNode node = this.Tag as PageNode;
            if (null != node)
            {
                //node.Width = width;
                //node.Height = height;
                node.Size = size;
            }
        }

        public void SetNewTitle(string title)
        {
            this.Title = title;
            SetText(title);

            PageNode node = this.Tag as PageNode;
            if (null != node)
            {
                node.Text = this.Text;
                node.Title = this.Title;
            }
        }
        #endregion
    }
}