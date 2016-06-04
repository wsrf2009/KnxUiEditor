
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using SourceGrid;
using Structure;
using UIEditor.Component;
using Button = SourceGrid.Cells.Button;
using System.Windows.Forms;

namespace UIEditor.Entity
{
    /// <summary>
    /// 所有树上面添加元素的基础类，主要分配ID
    /// </summary>
    [Serializable]
    public abstract class ViewNode : TreeNode, ISerializable
    {
        /// <summary>
        /// 控件的平面样式
        /// </summary>
        public enum EFlatStyle
        {
            /// <summary>
            /// 扁平化
            /// </summary>
            Flat,

            /// <summary>
            /// 立体化
            /// </summary>
            Stereo,
        }

        private static int InitId = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds);
        public delegate void PropertiesChangedDelegate(object sender, EventArgs e);
        public event PropertiesChangedDelegate PropertiesChangedEvent;

        #region 属性

        /// <summary>
        /// 控件的唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 控件左上角顶点的水平位置X
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 控件左上角顶点的垂直位置Y
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 控件的宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 控件的高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 边框宽度
        /// </summary>
        public bool DisplayBorder { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// 控件的不透明度
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// 控件的圆角半径
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// 控件的外观
        /// </summary>
        public EFlatStyle FlatStyle { get; set; }

        /// <summary>
        /// 控件的背景色
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 控件的背景图片
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// 控件的字体颜色
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }



        #endregion

        public static int GenId()
        {
            return InitId++;
        }

        #region 构造函数

        public ViewNode()
        {
            this.Id = GenId();
            this.Left = 0;
            this.Top = 0;
            this.Width = 0;
            this.Height = 0;
            this.DisplayBorder = true;
            this.BorderColor = FrmMainHelp.ColorToHexStr(Color.Black);
            this.Alpha = 0.7;
            this.Radius = 5;
            this.FlatStyle = EFlatStyle.Flat;
            this.BackgroundColor = FrmMainHelp.ColorToHexStr(Color.BlanchedAlmond);
            this.BackgroundImage = null;
            this.FontColor = "#000000";
            this.FontSize = 24;
            this.Text = "ViewNode";
        }

        /// <summary>
        /// KNXView 转 ViewNode
        /// </summary>
        /// <param name="knx"></param>
        public ViewNode(KNXView knx)
        {
            this.Id = knx.Id;
            this.Text = knx.Text;
            this.Left = knx.Left;
            this.Top = knx.Top;
            this.Width = knx.Width;
            this.Height = knx.Height;
            this.DisplayBorder = knx.DisplayBorder;
            this.BorderColor = knx.BorderColor;
            this.Alpha = knx.Alpha;
            this.Radius = knx.Radius;
            this.FlatStyle = (EFlatStyle)Enum.ToObject(typeof(EFlatStyle), knx.FlatStyle);
            this.BackgroundColor = knx.BackgroundColor ?? "#FFFFFF";
            this.BackgroundImage = knx.BackgroundImage;
            this.FontColor = knx.FontColor ?? "#000000";
            this.FontSize = knx.FontSize;
            //this.Font = (Font)new FontConverter().ConvertFromString(knx.Font);
        }

        protected ViewNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        /// <summary>
        /// ViewNode 转 KNXView
        /// </summary>
        /// <param name="knx"></param>
        protected void ToKnx(KNXView knx)
        {
            knx.Id = this.Id;
            knx.Text = this.Text;
            knx.Top = this.Top;
            knx.Left = this.Left;
            knx.Height = this.Height;
            knx.Width = this.Width;
            knx.DisplayBorder = this.DisplayBorder;
            knx.BorderColor = this.BorderColor;
            knx.Alpha = this.Alpha;
            knx.Radius = this.Radius;
            knx.FlatStyle = (int)this.FlatStyle;
            knx.BackgroundColor = this.BackgroundColor;
            knx.BackgroundImage = this.BackgroundImage;
            knx.FontColor = this.FontColor;
            knx.FontSize = this.FontSize;
            //knx.Font = new FontConverter().ConvertToInvariantString(this.Font);
        }

        #region 属性显示抽象函数

        /// <summary>
        /// 显示ViewNode的属性于GridView中
        /// </summary>
        /// <param name="grid"></param>
        public virtual void DisplayProperties(Grid grid)
        {
            grid.Tag = this;
        }

        /// <summary>
        /// GridView中的属性发生改变
        /// </summary>
        /// <param name="context"></param>
        public virtual void ChangePropValues(CellContext context)
        {

        }

        #endregion

        #region 事件处理函数

        /// <summary>
        /// 从本地电脑中选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PickImage(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.FileName = "";
                dlg.Multiselect = false;
                dlg.Filter = MyConst.PicFilter;
                dlg.FilterIndex = 3;
                dlg.RestoreDirectory = true;
                dlg.ValidateNames = true;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.InitialDirectory = MyCache.ProjectResImgDir;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.SafeFileName != null)
                    {
                        string selectedFile = dlg.SafeFileName;
                        string imageFile = Path.Combine(MyCache.ProjImagePath, selectedFile);

                        if (File.Exists(imageFile))
                        {
                            // 使用现有资源图片
                            var myimage = Image.FromFile(imageFile);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.Resize(myimage, new Size(16, 16), true);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = Path.GetFileName(imageFile);
                        }
                        else
                        {
                            // 复制图片文件到资源目录
                            File.Copy(dlg.FileName, Path.Combine(Application.StartupPath, imageFile));
                            var myimage = Image.FromFile(imageFile);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.Resize(myimage, new Size(16, 16), true);
                            btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = Path.GetFileName(imageFile);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选择颜色对话窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PickColor(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var selectColor = ColorTranslator.FromHtml("#FF000000");

            var tempValue = btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value;
            if (tempValue != null)
            {
                selectColor = FrmMainHelp.HexStrToColor((string)tempValue);
            }
            var myDialog = new ColorDialog { AllowFullOpen = true, ShowHelp = true, Color = selectColor };

            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.CreateImage(myDialog.Color);
                btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = FrmMainHelp.ColorToHexStr(myDialog.Color);
            }
        }

        ///// <summary>
        ///// 选择字体对话窗
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void PickFont(object sender, EventArgs e)
        //{
        //    var context = (CellContext)sender;
        //    var btnCell = (Button)context.Cell;
        //    var grid = context.Grid;
        //    ViewNode vNode = grid.Tag as ViewNode;

        //    FontDialog fontDialog = new FontDialog();
        //    if (null != vNode.Font)
        //    {
        //        fontDialog.Font = vNode.Font;
        //    }
        //    if (fontDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Image = ImageHelper.CreateImage(fontDialog.Font);
        //        btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = fontDialog.Font;
        //    }
        //}

        protected void ShowSaveEntityMsg(string knxtype)
        {
            MessageBox.Show(string.Format(ResourceMng.GetString("Message37"), knxtype), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        public abstract ViewNode Clon2();

        protected void PropertiesChangedNotify(object sender, EventArgs e)
        {
            if (null == PropertiesChangedEvent)
            {
                PropertiesChangedEvent += new PropertiesChangedDelegate(ViewNode_PropertiesChangedEvent);
            }

            PropertiesChangedEvent(sender, e);
        }

        private void ViewNode_PropertiesChangedEvent(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }


}