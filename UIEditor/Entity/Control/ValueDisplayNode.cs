using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure.Control;

namespace UIEditor.Entity.Control
{
    [Serializable]
    public class ValueDisplayNode : ControlBaseNode
    {
        private static int _index = 0;

        #region 属性
        // 标签
        public string Lable { get; set; }


        // 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        public string UnitString { get; set; }


        // 显示的值
        public string Value { get; set; }


        // 可选的姓名或名称的显示值。这是类似的标签，但直接显示的实际值。
        public string ValueDescription { get; set; }


        // 定义小数位数
        public int DecimalPlaces { get; set; }
        #endregion

        #region 构造函数
        public ValueDisplayNode()
        {
            _index++;
            this.Text = "数据显示_" + _index;
            //
            this.Lable = "";
            this.UnitString = "";
            this.Value = ""; ;
            this.ValueDescription = "";
            this.DecimalPlaces = 3;
            //
            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;

        }

        public ValueDisplayNode(KNXValueDisplay knx)
            : base(knx)
        {
            this.Lable = knx.Lable;
            this.UnitString = knx.Unit;
            this.Value = knx.Value;
            this.ValueDescription = knx.Description;
            this.DecimalPlaces = knx.DecimalPlaces;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;

        }

        protected ValueDisplayNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        #region ToKnx
        public KNXValueDisplay ToKnx()
        {
            var knx = new KNXValueDisplay();

            base.ToKnx(knx);

            knx.Lable = this.Lable;
            knx.Unit = this.UnitString;
            knx.Value = this.Value;
            knx.Description = this.ValueDescription;
            knx.DecimalPlaces = this.DecimalPlaces;

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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("标签文字");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Lable);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("计量单位");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.UnitString);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("显示值");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Value);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("显示值说明");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ValueDescription);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("显示精度（小数位数）");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.DecimalPlaces);
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
                    this.Lable = context.Value.ToString();
                    break;
                case 14:
                    this.UnitString = context.Value.ToString();
                    break;
                case 15:
                    this.Value = context.Value.ToString();
                    break;
                case 16:
                    this.ValueDescription = context.Value.ToString();
                    break;
                case 17:
                    this.DecimalPlaces = Convert.ToInt32(context.Value);
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxValueDisplayType);
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
            return formatter.Deserialize(stream) as ValueDisplayNode;
        }
    }
}
