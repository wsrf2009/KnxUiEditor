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
using Button = SourceGrid.Cells.Button;

namespace UIEditor.Entity
{
    [Serializable]
    public class PageNode : ContainerNode
    {
        private static int index = 0;

        /// <summary>
        /// 背景图片，如果有图片，则优先显示
        /// </summary>
        public string BackgroudImage { get; set; }

        #region

        public PageNode()
        {
            index++;

            Text = "页面_" + index;
            RowCount = 1;
            ColumnCount = 1;
            BackgroudImage = MyConst.DefaultIcon;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }


        public PageNode(KNXPage knx)
            : base(knx)
        {
            this.BackgroudImage = knx.BackgroudImage;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }

        protected PageNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        public KNXPage ToKnx()
        {
            var knx = new KNXPage();

            base.ToKnx(knx);

            knx.BackgroudImage = this.BackgroudImage;

            knx.Controls = new List<KNXControlBase>();
            knx.Grids = new List<KNXGrid>();

            return knx;
        }

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

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("背景图片");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.BackgroudImage == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroudImage);
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.BackgroudImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new Button("...");
            var imageButtonController = new SourceGrid.Cells.Controllers.Button();
            imageButtonController.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController);

            #endregion
        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            #region page

            switch (row)
            {
                case 2:
                    this.Text = context.Value.ToString();
                    break;
                case 3:
                    this.RowCount = Convert.ToInt32(context.Value);
                    break;
                case 4:
                    this.ColumnCount = Convert.ToInt32(context.Value);
                    break;
                case 5:
                    this.BackgroudImage = context.Value.ToString();
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.View.KnxPageType);
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
            return formatter.Deserialize(stream) as PageNode;
        }
    }
}