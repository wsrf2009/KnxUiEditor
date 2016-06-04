
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceGrid;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Views;
using Structure;
using UIEditor.Component;

namespace UIEditor.Entity
{
    [Serializable]
    public class AreaNode : ViewNode
    {
        private static int index = 0;

        #region 构造函数

        public AreaNode()
        {
            index++;

            Text = ResourceMng.GetString("TextArea") + "_" + index;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;
        }

        public AreaNode(KNXArea knx)
            : base(knx)
        {
            Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;
        }

        protected AreaNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        public KNXArea ToKnx()
        {
            var knx = new KNXArea();

            base.ToKnx(knx);

            knx.Rooms = new List<KNXRoom>();

            return knx;
        }

        #region 属性显示

        public override void DisplayProperties(Grid grid)
        {
            #region 显示属性
            grid.Tag = this;

            var nameModel = new Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new TextBox(typeof(string));

            var valueChangedController = new ValueChangedEvent();

            var currentRow = 0;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropType"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Name);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropTitle"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Text);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            #endregion
        }

        #endregion

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            #region area

            switch (row)
            {
                case 1:
                    this.Text = context.Value.ToString();
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.View.KnxAreaType);
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
            return formatter.Deserialize(stream) as AreaNode;
        }
    }
}
