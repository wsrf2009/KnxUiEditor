
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure;
using Structure.Control;
using UIEditor.Component;
using System.Drawing;


namespace UIEditor.Entity.Control
{
    /// <summary>
    ///  窗帘开关
    /// </summary>
    [Serializable]
    public class BlindsNode : ControlBaseNode
    {
        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        public string LeftImage { get; set; }
        public string LeftText { get; set; }
        public int LeftTextFontSize { get; set; }
        public string LeftTextFontColor { get; set; }
        public string RightImage { get; set; }
        public string RightText { get; set; }
        public int RightTextFontSize { get; set; }
        public string RightTextFontColor { get; set; }
        ///// <summary>
        ///// true：只有一个标签，标签将显示在百叶窗控制中心
        ///// false：在左右各显示的标签，
        ///// </summary>
        //public bool HasSingleLabel { get; set; }

        ///// <summary>
        ///// 左标签
        ///// </summary>
        //public string LeftText { get; set; }

        ///// <summary>
        ///// 右标签
        ///// </summary>
        //public string RightText { get; set; }

        ///// <summary>
        ///// 是否有长按事件
        ///// </summary>
        //public bool HasLongClickCommand { get; set; }

        ///// <summary>
        ///// 滑块两侧要显示的图标
        ///// </summary>
        //public SliderSymbol ControlSymbol { get; set; }
        #endregion

        #region 构造函数
        public BlindsNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextBlinds") +"_"+ index;

            this.Width = 180;
            this.Height = 40;
            this.FlatStyle = EFlatStyle.Stereo;

            this.LeftImage = "arrow_down.png";
            this.LeftText = "";
            this.LeftTextFontSize = 16;
            this.LeftTextFontColor = FrmMainHelp.ColorToHexStr(Color.Black);
            this.RightImage = "arrow_up.png";
            this.RightText = "";
            this.RightTextFontSize = 16;
            this.RightTextFontColor = FrmMainHelp.ColorToHexStr(Color.Black); ;

            string FileImageLeft = Path.Combine(MyCache.ProjImagePath, this.LeftImage);
            if (!File.Exists(FileImageLeft))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.LeftImage), Path.Combine(MyCache.ProjImagePath, this.LeftImage));
            }

            string FileImageRight = Path.Combine(MyCache.ProjImagePath, this.RightImage);
            if (!File.Exists(FileImageRight))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.RightImage), Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxBlindsType;

            

            //this.HasSingleLabel = false;
            //this.LeftText = "";
            //this.RightText = "";
            //this.HasLongClickCommand = false;
            //this.ControlSymbol = SliderSymbol.DownUp;
        }

        /// <summary>
        /// KNXBlinds 转 BlindsNode
        /// </summary>
        /// <param name="knx"></param>
        public BlindsNode(KNXBlinds knx)
            : base(knx)
        {
            this.LeftImage = knx.LeftImage;
            this.LeftText = knx.LeftText;
            this.LeftTextFontSize = knx.LeftTextFontSize;
            this.LeftTextFontColor = knx.LeftTextFontColor;
            this.RightImage = knx.RightImage;
            this.RightText = knx.RightText;
            this.RightTextFontSize =  knx.RightTextFontSize;
            this.RightTextFontColor =  knx.RightTextFontColor;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxBlindsType;
        }

        protected BlindsNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        #endregion

        /// <summary>
        /// BlindsNode 转 KNXBlinds
        /// </summary>
        /// <returns></returns>
        public KNXBlinds ToKnx()
        {
            var knx = new KNXBlinds();

            base.ToKnx(knx);

            knx.LeftImage = this.LeftImage;
            knx.LeftText = this.LeftText;
            knx.LeftTextFontSize = this.LeftTextFontSize;
            knx.LeftTextFontColor = this.LeftTextFontColor;
            knx.RightImage = this.RightImage;
            knx.RightText = this.RightText;
            knx.RightTextFontSize = this.RightTextFontSize;
            knx.RightTextFontColor = this.RightTextFontColor;

            return knx;
        }

        /// <summary>
        /// 显示BlindsNode属性于GridView中
        /// </summary>
        /// <param name="grid"></param>
        public override void DisplayProperties(Grid grid)
        {
            base.DisplayProperties(grid);

            var nameModel = new SourceGrid.Cells.Views.Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            var intEditor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(int));

            var valueChangedController = new ValueChangedEvent();

            #region TreeNode属性
            /* 0 类型 */
            var currentRow = 0;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropType"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Name);

            /* 1 名称 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropTitle"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Text);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

            #region ViewNode属性
            /* 2 左上角顶点位置X */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropX"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Left);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 3 左上角顶点位置Y */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropY"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Top);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 4 宽度 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropWidth"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Width);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 5 高度 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropHeight"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Height);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 6 不透明度 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropAlpha"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Alpha);
            grid[currentRow, MyConst.ValueColumn].Editor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(Double));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 7 圆角半径 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropRadius"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Radius);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 8 背景色 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropBackColor"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var backcolorButtonController = new SourceGrid.Cells.Controllers.Button();
            backcolorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(backcolorButtonController);

            /* 9 平面样式 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropFlatStyle"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.FlatStyle, typeof(EFlatStyle));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 10 字体颜色 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropFontColor"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.FontColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.FontColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var foreColorButtonController = new SourceGrid.Cells.Controllers.Button();
            foreColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(foreColorButtonController);

            /* 11 字体大小 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropFontSize"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.FontSize);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

            #region ControlBaseNode属性
            /* 12 ETS写地址表 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropEtsWriteAddressIds"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(EtsAddressDictToString(this.WriteAddressIds));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var writeAddButton = new SourceGrid.Cells.Controllers.Button();
            writeAddButton.Executed += PickMultiWriteAddress;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(writeAddButton);

            /* 13 ETS读地址 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropEtsReadAddressId"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(EtsAddressDictToString(this.ReadAddressId));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var readAddButton = new SourceGrid.Cells.Controllers.Button();
            readAddButton.Executed += PickSingleReadAddress;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(readAddButton);

            /* 14 有没有提示 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropHasTip"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.HasTip, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 15 提示 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropTip"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Tip, stringEditor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 16 是否可以点击 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropClickable"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Clickable, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

            #region 自定义属性
            /* 17 控件左侧图片 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropLeftImage"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.LeftImage == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.LeftImage);
                grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.LeftImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController = new SourceGrid.Cells.Controllers.Button();
            imageButtonController.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController);

            /* 18 左标签 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropLeftText"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.LeftText, stringEditor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 19 左标签字体大小 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropLeftTextFontSize"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.LeftTextFontSize);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 20 左标签字体颜色 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropLeftTextFontColor"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.LeftTextFontColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.LeftTextFontColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            foreColorButtonController = new SourceGrid.Cells.Controllers.Button();
            foreColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(foreColorButtonController);

            /* 21 右侧图片 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropRightImage"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.RightImage == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RightImage);
                grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController2 = new SourceGrid.Cells.Controllers.Button();
            imageButtonController2.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController2);

            /* 22 右标签 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropRightText"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RightText, stringEditor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 23 左标签字体大小 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropRightTextFontSize"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RightTextFontSize);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 24 左标签字体颜色 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropRightTextFontColor"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RightTextFontColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.RightTextFontColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            foreColorButtonController = new SourceGrid.Cells.Controllers.Button();
            foreColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(foreColorButtonController);
            #endregion
        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;
            var grid = context.Grid as Grid;

            switch (row)
            {
                #region TreeNode属性
                case 0:    //  类型
                    break;
                case 1:    //  名称
                    this.Text = (string)context.Value;
                    break;
                #endregion

                #region ViewNode属性
                case 2:     //  左上角顶点位置X
                    this.Left = Convert.ToInt32(context.Value);
                    break;
                case 3:     //  左上角顶点位置Y
                    this.Top = Convert.ToInt32(context.Value);
                    break;
                case 4:     //  宽度
                    this.Width = Convert.ToInt32(context.Value);
                    break;
                case 5:     //  高度
                    this.Height = Convert.ToInt32(context.Value);
                    break;
                case 6:     //  不透明度
                    this.Alpha = Convert.ToDouble(context.Value);
                    break;
                case 7:     // 8 圆角半径
                    this.Radius = Convert.ToInt32(context.Value);
                    break;
                case 8:     //  背景色
                    this.BackgroundColor = (string)context.Value;
                    grid[row, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
                    break;
                case 9:     // 平面样式
                    this.FlatStyle = (EFlatStyle)Convert.ToInt32(context.Value);
                    break;
                case 10:     // 字体颜色
                    this.FontColor = (string)context.Value;
                    grid[row, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.FontColor);
                    break;
                case 11:    // 字体大小
                    this.FontSize = Convert.ToInt32(context.Value);
                    break;
                #endregion

                #region ControlBaseNode属性
                case 12:    //  ETS写地址表
                    break;
                case 13:    //  ETS读地址
                    break;
                case 14:    //  有没有提示
                    this.HasTip = Convert.ToBoolean(context.Value);
                    break;
                case 15:    //  提示
                    this.Tip = (string)context.Value;
                    break;
                case 16:    // 是否可以点击
                    this.Clickable = Convert.ToBoolean(context.Value);
                    break;
                #endregion

                #region 自定义属性
                case 17: // 控件左侧图片
                    this.LeftImage = (string)context.Value;
                    break;
                case 18:
                    this.LeftText = (string)context.Value;
                    break;
                case 19:
                    this.LeftTextFontSize = Convert.ToInt32(context.Value);
                    break;
                case 20:
                    this.LeftTextFontColor = (string)context.Value;
                    break;
                case 21: // 控件右侧图片
                    this.RightImage = (string)context.Value;
                    break;
                case 22:
                    this.RightText = (string)context.Value;
                    break;
                case 23:
                    this.RightTextFontSize = Convert.ToInt32(context.Value);
                    break;
                case 24:
                    this.RightTextFontColor = (string)context.Value;
                    break;
                #endregion

                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxBlindsType);
                    break;
                
            }

            this.PropertiesChangedNotify(this, EventArgs.Empty);
        }

        public override ViewNode Clon2()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as BlindsNode;
        }
    }
}
