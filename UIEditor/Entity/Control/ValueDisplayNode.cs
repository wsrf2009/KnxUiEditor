using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure.Control;
using UIEditor.Component;
using System.Reflection;
using Structure;

namespace UIEditor.Entity.Control
{
    [Serializable]
    public class ValueDisplayNode : ControlBaseNode
    {
        private static int _index = 0;

        #region 属性
        //// 标签
        //public string Lable { get; set; }


        /// <summary>
        /// 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        /// </summary>
        public /*Structure.Control.KNXValueDisplay.CMeasurementUnit*/ EMeasurementUnit Unit { get; set; }


        ///// <summary>
        ///// 显示的值
        ///// </summary>
        //public string Value { get; set; }


        //// 可选的姓名或名称的显示值。这是类似的标签，但直接显示的实际值。
        //public string ValueDescription { get; set; }


        /// <summary>
        /// 定义小数位数
        /// </summary>
        //public Structure.Control.KNXValueDisplay.EDisplayAccurancy DecimalPlaces { get; set; }
        #endregion

        #region 构造函数
        public ValueDisplayNode()
        {
            _index++;

            this.Text = ResourceMng.GetString("TextValueDisplay") + "_" + _index;
            this.Width = 200;

            this.Unit = EMeasurementUnit.None;
            //this.Value = "";
            //this.DecimalPlaces = Structure.Control.KNXValueDisplay.EDisplayAccurancy.None;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;
        }

        public ValueDisplayNode(KNXValueDisplay knx)
            : base(knx)
        {
            //this.Lable = knx.Lable;
            this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit);
            //this.Value = knx.Value;
            //this.ValueDescription = knx.Description;
            //this.DecimalPlaces = (Structure.Control.KNXValueDisplay.EDisplayAccurancy)Enum.ToObject(typeof(Structure.Control.KNXValueDisplay.EDisplayAccurancy), knx.DecimalPlaces);

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;

        }

        protected ValueDisplayNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXValueDisplay ToKnx()
        {
            var knx = new KNXValueDisplay();

            base.ToKnx(knx);

            //knx.Lable = this.Lable;
            knx.Unit = (int)this.Unit;
            //knx.Value = this.Value;
            //knx.Description = this.ValueDescription;
            //knx.DecimalPlaces = (int)this.DecimalPlaces;

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
            /* 12 ETS读地址 */
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

            /* 13 有没有提示 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropHasTip"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.HasTip, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 14 提示 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropTip"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Tip, stringEditor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 15 是否可以点击 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropClickable"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Clickable, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

            #region 自定义属性
            /* 16 计量单位 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropMeasurementUnit"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Unit, typeof(EMeasurementUnit)/*, stringEditor*/);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            ///* 18 显示值 */
            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropDisplayValue);
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Value);
            //grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            //grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            ///* 17 显示精度（小数位数） */
            //currentRow++;
            //grid.Rows.Insert(currentRow);
            //grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropDecimalDigits"));
            //grid[currentRow, MyConst.NameColumn].View = nameModel;
            //grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.DecimalPlaces, typeof(Structure.Control.KNXValueDisplay.EDisplayAccurancy));
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
                case 12:    // ETS读地址
                    break;
                case 13:    // 有没有提示
                    this.HasTip = Convert.ToBoolean(context.Value);
                    break;
                case 14:    // 提示
                    this.Tip = (string)context.Value;
                    break;
                case 15:    // 是否可以点击
                    this.Clickable = Convert.ToBoolean(context.Value);
                    break;
                #endregion

                #region 自定义属性
                case 16:    // 计量单位
                    this.Unit = /*(Structure.Control.KNXValueDisplay.CMeasurementUnit)Convert.ToInt32(context.Value)*/(EMeasurementUnit)context.Value;
                    break;
                //case 18:    // 显示值
                //    this.Value = context.Value.ToString();
                //    break;
                //case 17:    // 显示精度(小数位数)
                //    this.DecimalPlaces = (Structure.Control.KNXValueDisplay.EDisplayAccurancy)Convert.ToInt32(context.Value);
                //    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxValueDisplayType);
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
            return formatter.Deserialize(stream) as ValueDisplayNode;
        }
    }
}
