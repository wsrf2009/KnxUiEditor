using SourceGrid;
using Structure;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UIEditor.Component;

namespace UIEditor.Entity.Control
{
    [Serializable]
    class DigitalAdjustmentNode : ControlBaseNode
    {
        private static int index = 0;

        #region 属性
        public string LeftImage { get; set; }
        public string RightImage { get; set; }
        public Structure.Control.KNXDigitalAdjustment.EDigitalNumber DigitalNumber { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public EMeasurementUnit Unit { get; set; }
        #endregion

        #region 构造函数
        public DigitalAdjustmentNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextDigitalAdjustment") + index;

            this.Width = 150;
            this.Height = 45;
            this.FlatStyle = EFlatStyle.Stereo;
            this.FontColor = FrmMainHelp.ColorToHexStr(Color.Yellow);
            this.FontSize = 28;

            this.LeftImage = "sl_left.png";
            this.RightImage = "sl_right.png";
            this.DigitalNumber = KNXDigitalAdjustment.EDigitalNumber.TwoDigit;
            this.MaxValue = 30;
            this.MinValue = 16;
            this.Unit = EMeasurementUnit.None;

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

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustment;
        }

        public DigitalAdjustmentNode(KNXDigitalAdjustment knx)
            : base(knx)
        {
            this.LeftImage = knx.LeftImage;
            this.RightImage = knx.RightImage;
            this.DigitalNumber = (KNXDigitalAdjustment.EDigitalNumber)Enum.ToObject(typeof(KNXDigitalAdjustment.EDigitalNumber), knx.DigitalNumber);
            this.MaxValue = knx.MaxValue;
            this.MinValue = knx.MinValue;
            this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit);

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustment;
        }

        protected DigitalAdjustmentNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXDigitalAdjustment ToKnx()
        {
            var knx = new KNXDigitalAdjustment();
            base.ToKnx(knx);

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;
            knx.DigitalNumber = (int)this.DigitalNumber;
            knx.MaxValue = this.MaxValue;
            knx.MinValue = this.MinValue;
            knx.Unit = (int)this.Unit;

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

            /* 19 数字位数 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropDigitalNumber"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.DigitalNumber, typeof(KNXDigitalAdjustment.EDigitalNumber));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 20 最大值 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropMax"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MaxValue);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 21 最小值 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropMin"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MinValue);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 22 计量单位 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropMeasurementUnit"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Unit, typeof(EMeasurementUnit)/*, stringEditor*/);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
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
                case 17: // 控件左侧图片
                    this.LeftImage = (string)context.Value;
                    break;
                case 18: // 控件右侧图片
                    this.RightImage = (string)context.Value;
                    break;
                case 19: // 数字位数
                    this.DigitalNumber = (KNXDigitalAdjustment.EDigitalNumber)Convert.ToInt32(context.Value);
                    if (KNXDigitalAdjustment.EDigitalNumber.OneDigit == this.DigitalNumber)
                    {
                        this.MaxValue = 9;
                        this.MinValue = 0;
                    }
                    else if (KNXDigitalAdjustment.EDigitalNumber.TwoDigit == this.DigitalNumber)
                    {
                        this.MaxValue = 99;
                        this.MinValue = 0;
                    }
                    else if (KNXDigitalAdjustment.EDigitalNumber.ThreeDigit == this.DigitalNumber)
                    {
                        this.MaxValue = 999;
                        this.MinValue = 0;
                    }

                    break;
                case 20: // 最大值
                    this.MaxValue = Convert.ToInt32(context.Value);
                    break;
                case 21: // 最小值
                    this.MinValue = Convert.ToInt32(context.Value);
                    break;
                case 22:    // 计量单位
                    this.Unit = (EMeasurementUnit)context.Value;
                    break;
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
    }
}
