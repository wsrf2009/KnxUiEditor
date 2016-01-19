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
    public class SliderNode : ControlBaseNode
    {
        private static int index = 0;

        #region  属性
        //Slider左边背景图片(SliderSymbol与此属性不能共存)
        public string LeftImage { get; set; }
        //Slider左边背景图片(SliderSymbol与此属性不能共存)
        public string RightImage { get; set; }
        //Slider滑动图片
        public string SliderImage { get; set; }
        //最小值
        public int MinValue { get; set; }
        //最大值
        public int MaxValue { get; set; }
        //要滑块两侧显示的符号。
        public SliderSymbol ControlSymbol { get; set; }

        //滑动时延迟时间(单位毫秒)
        public int SendInterval { get; set; }
        #endregion

        #region 构造函数
        public SliderNode()
        {
            index++;

            this.LeftImage = MyConst.DefaultIcon;
            this.RightImage = MyConst.DefaultIcon;
            this.SliderImage = MyConst.DefaultIcon;
            this.MinValue = 1;
            this.MaxValue = 100;
            this.ControlSymbol = SliderSymbol.DardBright;
            this.SendInterval = 10;

            this.Text = "滑动条_" + index;
            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderType;
        }

        public SliderNode(KNXSlider knx)
            : base(knx)
        {

            this.LeftImage = knx.LeftImage;
            this.RightImage = knx.RightImage;
            this.SliderImage = knx.SliderImage;
            this.MinValue = knx.MinValue;
            this.MaxValue = knx.MaxValue;
            this.ControlSymbol = knx.ControlSymbol;
            this.SendInterval = knx.SendInterval;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderType;
        }

        protected SliderNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        #region knx
        public KNXSlider ToKnx()
        {
            var knx = new KNXSlider();
            base.ToKnx(knx);

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;
            knx.SliderImage = this.SliderImage;
            knx.MinValue = this.MinValue;
            knx.MaxValue = this.MaxValue;
            knx.ControlSymbol = this.ControlSymbol;
            knx.SendInterval = this.SendInterval;

            return knx;
        }
        #endregion

        public override void DisplayProperties(Grid grid)
        {
            #region 基本属性

            grid.Tag = this;

            var nameModel = new SourceGrid.Cells.Views.Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            var intEditor = new SourceGrid.Cells.Editors.TextBoxNumeric(typeof(int));

            var valueChangedController = new ValueChangedEvent();

            var currentRow = 1;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropType);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Name);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropTitle);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Text);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropBackColor);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroudColor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var backColorButtonController = new SourceGrid.Cells.Controllers.Button();
            backColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(backColorButtonController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropForeColor);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.FontColor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var foreColorButtonController = new SourceGrid.Cells.Controllers.Button();
            foreColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(foreColorButtonController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropRow);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Row);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropColumn);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Column);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);


            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropRowSpan);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RowSpan);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropColumnSpan);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ColumnSpan);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);


            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropEtsWriteAddressIds);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(EtsAddressDictToString(this.WriteAddressIds));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var writeAddButton = new SourceGrid.Cells.Controllers.Button();
            writeAddButton.Executed += PickMultiAddress;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(writeAddButton);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropEtsReadAddressId);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(EtsAddressDictToString(this.ReadAddressId));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var readAddButton = new SourceGrid.Cells.Controllers.Button();
            readAddButton.Executed += PickAddress;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(readAddButton);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropHasTip);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.HasTip, typeof(bool));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropTip);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Tip, stringEditor);
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

            #region 自定义属性
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropLeftImage);
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

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropRightImage);
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


            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropSlideImage);
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

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropMin);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MinValue);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropMax);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.MaxValue);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropControlSymbol);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ControlSymbol, typeof(SliderSymbol));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropSendInterval);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.SendInterval);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);


            #endregion
        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            switch (row)
            {
                #region 基础属性
                case 2:
                    this.Text = context.Value.ToString();
                    break;
                case 3:
                    this.BackgroudColor = context.Value.ToString();
                    break;
                case 4:
                    this.FontColor = context.Value.ToString();
                    break;
                case 5:
                    this.Row = Convert.ToInt32(context.Value);
                    break;
                case 6:
                    this.Column = Convert.ToInt32(context.Value);
                    break;
                case 7:
                    this.RowSpan = Convert.ToInt32(context.Value);
                    break;
                case 8:
                    this.ColumnSpan = Convert.ToInt32(context.Value);
                    break;
                case 9:
                    //this.WriteAddressIds = null;
                    break;
                case 10:
                    // this.ReadAddressId = context.Value.ToString();
                    break;
                case 11:
                    this.HasTip = Convert.ToBoolean(context.Value);
                    break;
                case 12:
                    this.Tip = context.Value.ToString();
                    break;
                #endregion

                #region 扩展属性
                case 13:
                    this.LeftImage = context.Value.ToString();
                    break;
                case 14:
                    this.RightImage = context.Value.ToString();
                    break;
                case 15:
                    this.SliderImage = context.Value.ToString();
                    break;
                case 16:
                    this.MinValue = Convert.ToInt32(context.Value);
                    break;
                case 17:
                    this.MaxValue = Convert.ToInt32(context.Value);
                    break;
                case 18:
                    this.ControlSymbol = (SliderSymbol)context.Value;
                    break;
                case 19:
                    this.SendInterval = Convert.ToInt32(context.Value);
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxSliderType);
                    break;
                #endregion
            }



        }

        public override ViewNode Clon2()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as SliderNode;
        }
    }
}
