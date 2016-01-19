using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceGrid;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Views;
using Structure;

namespace UIEditor.Entity
{
    public enum GridBorderStyle
    {
        None, FixedSingle, Fixed3D
    }

    [Serializable]
    public class GridNode : ContainerNode
    {
        private static int index = 0;

        #region 属性
        public int Row { get; set; }

        public int Column { get; set; }

        public int RowSpan { get; set; }

        public int ColumnSpan { get; set; }

        public GridBorderStyle BorderStyle { get; set; }

        #endregion

        #region 构造函数
        public GridNode()
        {
            index++;

            Text = "表格_" + index;
            RowCount = 1;
            ColumnCount = 1;

            Row = 1;
            Column = 1;
            RowSpan = 1;
            ColumnSpan = 1;
            BorderStyle = GridBorderStyle.FixedSingle;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxGridType;
        }


        public GridNode(KNXGrid knx)
            : base(knx)
        {

            this.Row = knx.Row + 1;
            this.Column = knx.Column + 1;
            this.RowSpan = knx.RowSpan;
            this.ColumnSpan = knx.ColumnSpan;
            this.BorderStyle = (GridBorderStyle)Enum.Parse(typeof(GridBorderStyle), knx.BorderStyle);

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxGridType;
        }

        protected GridNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        #region knx
        public KNXGrid ToKnx()
        {
            var knx = new KNXGrid();

            base.ToKnx(knx);

            knx.Row = this.Row - 1;
            knx.Column = this.Column - 1;
            knx.RowSpan = this.RowSpan;
            knx.ColumnSpan = this.ColumnSpan;
            knx.BorderStyle = Enum.GetName(typeof(GridBorderStyle), this.BorderStyle);

            knx.Controls = new List<KNXControlBase>();

            return knx;
        }
        #endregion

        public override void DisplayProperties(Grid grid)
        {
            #region 显示属性

            grid.Tag = this;

            var nameModel = new Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new TextBox(typeof(string));
            var intEditor = new TextBoxNumeric(typeof(int));

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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("边框格式");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BorderStyle,
                typeof(GridBorderStyle));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PorpRowCount);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.RowCount);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(MyConst.PropColumnCount);
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ColumnCount);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            #endregion
        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            #region grid

            switch (row)
            {
                case 2:
                    this.Text = context.Value.ToString();
                    break;
                case 3:
                    this.Row = Convert.ToInt32(context.Value);
                    break;
                case 4:
                    this.Column = Convert.ToInt32(context.Value);
                    break;
                case 5:
                    this.RowSpan = Convert.ToInt32(context.Value);
                    break;
                case 6:
                    this.ColumnSpan = Convert.ToInt32(context.Value);
                    break;
                case 7:
                    this.BorderStyle = (GridBorderStyle)context.Value;
                    break;
                case 8:
                    this.RowCount = Convert.ToInt32(context.Value);
                    break;
                case 9:
                    this.ColumnCount = Convert.ToInt32(context.Value);
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.View.KnxGridType);
                    break;
            }


            #endregion
        }

        public override ViewNode Clon2()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as GridNode;
        }
    }
}
