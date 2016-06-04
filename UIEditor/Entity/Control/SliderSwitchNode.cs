using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure;
using Structure.Control;
using UIEditor.Component;
using UIEditor.Controls;

namespace UIEditor.Entity.Control
{
    [Serializable]
    public class SliderSwitchNode : ControlBaseNode
    {
        private static int index = 0;

        #region 属性
        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        public string LeftImage { get; set; }
        //public int LeftImageLeft { get; set; }
        //public int LeftImageTop { get; set; }
        //public int LeftImageWidth { get; set; }
        //public int LeftImageHeight { get; set; }


        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        public string RightImage { get; set; }
        //public int RightImageLeft { get; set; }
        //public int RightImageTop { get; set; }
        //public int RightImageWidth { get; set; }
        //public int RightImageHeight { get; set; }

        /// <summary>
        /// Slider滑动图片
        /// </summary>
        public string SliderImage { get; set; }

        ///// <summary>
        ///// 最小值
        ///// </summary>
        //public int MinValue { get; set; }

        ///// <summary>
        ///// 最大值
        ///// </summary>
        //public int MaxValue { get; set; }

        //要滑块两侧显示的符号
        //public SliderSymbol ControlSymbol { get; set; }

        ////滑动时延迟时间(单位毫秒)
        //public int SendInterval { get; set; }

        public bool IsRelativeControl { get; set; }
        #endregion

        #region 构造函数
        public SliderSwitchNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextSliderSwitch") + "_" + index;

            this.Width = 260;
            this.Height = 40;
            this.FlatStyle = EFlatStyle.Stereo;

            this.LeftImage = "sl_left.png";
            this.RightImage = "sl_right.png";
            this.SliderImage = "sl.png";
            this.IsRelativeControl = false;
            //this.MinValue = 1;
            //this.MaxValue = 100;
            //this.ControlSymbol = SliderSymbol.DardBright;
            //this.SendInterval = 10;

            //this.CalculateChildViewBound();

            string FileLeftImage = Path.Combine(MyCache.ProjImagePath, this.LeftImage);
            if (!File.Exists(FileLeftImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.LeftImage), Path.Combine(MyCache.ProjImagePath, this.LeftImage));
            }

            string FileRightImage = Path.Combine(MyCache.ProjImagePath, this.RightImage);
            if (!File.Exists(FileRightImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.RightImage), Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }

            string FileSliderImage = Path.Combine(MyCache.ProjImagePath, this.SliderImage);
            if (!File.Exists(FileSliderImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.SliderImage), Path.Combine(MyCache.ProjImagePath, this.SliderImage));
            }

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;
        }

        public SliderSwitchNode(KNXSliderSwitch knx)
            : base(knx)
        {
            this.LeftImage = knx.LeftImage;
            //this.LeftImageLeft = knx.LeftImageLeft;
            //this.LeftImageTop = knx.LeftImageTop;
            //this.LeftImageWidth = knx.LeftImageWidth;
            //this.LeftImageHeight = knx.LeftImageHeight;
            this.RightImage = knx.RightImage;
            //this.RightImageLeft = knx.RightImageLeft;
            //this.RightImageTop = knx.RightImageTop;
            //this.RightImageWidth = knx.RightImageWidth;
            //this.RightImageHeight = knx.RightImageHeight;
            this.SliderImage = knx.SliderImage;
            //this.MinValue = knx.MinValue;
            //this.MaxValue = knx.MaxValue;
            //this.ControlSymbol = knx.ControlSymbol;
            //this.SendInterval = knx.SendInterval;
            this.IsRelativeControl = knx.IsRelativeControl;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;
        }

        protected SliderSwitchNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXSliderSwitch ToKnx()
        {
            var knx = new KNXSliderSwitch();
            base.ToKnx(knx);

            knx.LeftImage = this.LeftImage;
            //knx.LeftImageLeft = this.LeftImageLeft;
            //knx.LeftImageTop = this.LeftImageTop;
            //knx.LeftImageWidth = this.LeftImageWidth;
            //knx.LeftImageHeight = this.LeftImageHeight;
            knx.RightImage = this.RightImage;
            //knx.RightImageLeft = this.RightImageLeft;
            //knx.RightImageTop = this.RightImageTop;
            //knx.RightImageWidth = this.RightImageWidth;
            //knx.RightImageHeight = this.RightImageHeight;
            knx.SliderImage = this.SliderImage;
            //knx.MinValue = this.MinValue;
            //knx.MaxValue = this.MaxValue;
            //knx.ControlSymbol = this.ControlSymbol;
            //knx.SendInterval = this.SendInterval;
            knx.IsRelativeControl = this.IsRelativeControl;

            return knx;
        }

        public override void DisplayProperties(Grid grid)
        {
            base.DisplayProperties(grid);

            var nameModel = new SourceGrid.Cells.Views.Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            var intEditor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(int));

            var valueChangedController = new ValueChangedEvent();

            //this.CalculateChildViewBound();

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
            grid[currentRow, MyConst.ValueColumn].Editor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(Double)); ;
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
            if (null == this.BackgroundColor)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroundColor);
                grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
                grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var colorButtonController = new SourceGrid.Cells.Controllers.Button();
            colorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(colorButtonController);

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

            #region ControlBaseNode
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
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.LeftImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController = new SourceGrid.Cells.Controllers.Button();
            imageButtonController.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController);

            /* 18 右侧图片 */
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
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController2 = new SourceGrid.Cells.Controllers.Button();
            imageButtonController2.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController2);

            /* 19 滑动块上的图片 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropSlideImage"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.SliderImage == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.SliderImage);
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.SliderImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController3 = new SourceGrid.Cells.Controllers.Button();
            imageButtonController3.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController3);

            /* 20 是否为相对调节 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropIsRelative"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.IsRelativeControl, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            ///* 21 最小值 */
            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropMin);
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MinValue);
            //grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            //grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            ///* 22 最大值 */
            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropMax);
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MaxValue);
            //grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            //grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropControlSymbol);
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ControlSymbol, typeof(SliderSymbol));
            //grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropSendInterval);
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.SendInterval);
            //grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            //grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);


            #endregion
        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;
            var grid = context.Grid as Grid;

            switch (row)
            {
                #region TreeNode属性
                case 0:    // 类型
                    break;
                case 1:    // 名称
                    this.Text = (string)context.Value;
                    break;
                #endregion

                #region ViewNode属性
                case 2:     // 左上角顶点位置X
                    this.Left = Convert.ToInt32(context.Value);
                    break;
                case 3:     // 左上角顶点位置Y
                    this.Top = Convert.ToInt32(context.Value);
                    break;
                case 4:     // 宽度
                    this.Width = Convert.ToInt32(context.Value);
                    //this.CalculateChildViewBound();
                    break;
                case 5:     // 高度
                    this.Height = Convert.ToInt32(context.Value);
                    //this.CalculateChildViewBound();
                    break;
                case 6:     // 不透明度
                    this.Alpha = Convert.ToDouble(context.Value);
                    break;
                case 7:     // 圆角半径
                    this.Radius = Convert.ToInt32(context.Value);
                    break;
                case 8:     // 背景色
                    this.BackgroundColor = (string)context.Value;
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
                case 12:    // ETS写地址表
                    break;
                case 13:    // ETS读地址
                    break;
                case 14:    // 有没有提示
                    this.HasTip = Convert.ToBoolean(context.Value);
                    break;
                case 15:    // 提示
                    this.Tip = (string)context.Value;
                    break;
                case 16:    // 是否可以点击
                    this.Clickable = Convert.ToBoolean(context.Value);
                    break;
                #endregion

                #region 自定义属性
                case 17:
                    this.LeftImage = (string)context.Value;
                    break;
                case 18:
                    this.RightImage = (string)context.Value;
                    break;
                case 19:
                    this.SliderImage = (string)context.Value;
                    break;
                case 20:
                    this.IsRelativeControl = Convert.ToBoolean(context.Value);
                    break;
                //case 21:
                //    this.MinValue = Convert.ToInt32(context.Value);
                //    break;
                //case 22:
                //    this.MaxValue = Convert.ToInt32(context.Value);
                //    break;
                //case 23:
                //    this.ControlSymbol = (SliderSymbol)context.Value;
                //    break;
                //case 24:
                //    this.SendInterval = Convert.ToInt32(context.Value);
                //    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxSliderSwitchType);
                    break;
                #endregion
            }

            this.PropertiesChangedNotify(this, EventArgs.Empty);

        }

        public override ViewNode Clon2()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as SliderSwitchNode;
        }

        //public void CalculateChildViewBound()
        //{
        //    int sliderEdgeWidth = 3;
        //    int x = 5;  // 偏移为5
        //    int y = sliderEdgeWidth + 5;  // 
        //    int height = this.Height - 2 * y;   // 计算出高度
        //    int width = height;     // 计算出宽度

        //    this.LeftImageLeft = x;
        //    this.LeftImageTop = y;
        //    this.LeftImageWidth = width;
        //    this.LeftImageHeight = height;

        //    x = this.Width - x - width;
        //    this.RightImageLeft = x;
        //    this.RightImageTop = y;
        //    this.RightImageWidth = width;
        //    this.RightImageHeight = height;
        //}
    }
}
