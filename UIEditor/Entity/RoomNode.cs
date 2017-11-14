using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using UIEditor.Component;
using System.Drawing.Design;
using Structure;
using UIEditor.PropertyGridEditor;
using System.Drawing;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(RoomNode.PropertyConverter)), Serializable]
    public class RoomNode : ViewNode
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

        /// <summary>
        /// 是否将该房间作为默认显示页面
        /// </summary>
        [BrowsableAttribute(false)]
        public EBool IsDefaultRoom { get; set; }
        #endregion

        #region 构造函数
        public RoomNode()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxRoomType;

            this.Text = UIResMang.GetString("TextRoom");
            this.Title = UIResMang.GetString("TextRoom") + index;
            SetText(this.Title);

            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "room.png"));

            //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjectResImgDir, "Room.png"));
            this.PinCode = "";
            this.IsDefaultRoom = EBool.No;
        }

        public RoomNode(KNXRoom knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            Name = ImageKey = SelectedImageKey = MyConst.View.KnxRoomType;
            SetText(this.Title);

            if (ImportedHelper.IsLessThan2_0_3())
            {
                if (!string.IsNullOrEmpty(knx.Symbol))
                {
                    this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.Symbol));
                    //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(this.ImagePath, knx.Symbol));
                }
            }
            else if (ImportedHelper.IsLessThan2_5_6())
            {
                this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_SYMBOL));
                //this.Symbol = ImageHelper.GetDiskImage(Path.Combine(this.ImagePath, NAME_SYMBOL));
            }
            else
            {
                this.Symbol = knx.Symbol;
            }

            PinCode = knx.PinCode;
            this.IsDefaultRoom = (EBool)Enum.ToObject(typeof(EBool), knx.DefaultRoom);
        }

        public RoomNode(KNXRoom knx, BackgroundWorker worker, string DirSrcImg)
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
            RoomNode node = base.Clone() as RoomNode;
            node.Symbol = this.Symbol;
            node.PinCode = this.PinCode;
            node.IsDefaultRoom = this.IsDefaultRoom;

            return node;
        }

        public override object Copy()
        {
            RoomNode node = base.Copy() as RoomNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextRoom"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextRoom"));
        }
        #endregion

        #region 导出
        public KNXRoom ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXRoom();

            base.ToKnx(knx, worker);

            knx.Symbol = this.Symbol;
            //ImageHelper.SaveImageAsPNG(this.Symbol, Path.Combine(this.ImagePath, NAME_SYMBOL));
            knx.PinCode = this.PinCode;
            knx.DefaultRoom = (int)this.IsDefaultRoom;

            knx.Pages = new List<KNXPage>();

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

        public KNXRoom ExportTo(BackgroundWorker worker, string dir)
        {
            KNXRoom knx = this.ToKnx(worker);

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
