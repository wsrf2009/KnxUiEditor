using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure.Control;


namespace UIEditor.Entity.Control
{
    [Serializable]
    public class LabelNode : ControlBaseNode
    {
        private static int index = 0;

        // 字体大小
        public int FontSize { get; set; }

        public ContentAlignment TextAlign { get; set; }

        #region 构造函数
        public LabelNode()
        {
            index++;

            this.Text = "标签_" + index;

            this.FontSize = 10;
            this.TextAlign = ContentAlignment.MiddleLeft;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxLabelType;
        }

        public LabelNode(KNXLabel knx)
            : base(knx)
        {
            this.FontSize = knx.FontSize;
            if (string.IsNullOrEmpty(knx.TextAlign))
            {
                this.TextAlign = ContentAlignment.MiddleLeft;
            }
            else
            {
                this.TextAlign = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), knx.TextAlign);
            }


            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxLabelType;

        }

        protected LabelNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        #region knx
        public KNXLabel ToKnx()
        {
            var knx = new KNXLabel();

            base.ToKnx(knx);

            knx.FontSize = this.FontSize;
            knx.TextAlign = this.TextAlign.ToString();
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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("字体大小");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.FontSize);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);


            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("文字对齐方式");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.TextAlign, typeof(ContentAlignment));
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
                    this.FontSize = Convert.ToInt32(context.Value);
                    break;
                case 14:
                    this.TextAlign = (ContentAlignment)context.Value;
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxLabelType);
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
            return formatter.Deserialize(stream) as LabelNode;
        }
    }
}
