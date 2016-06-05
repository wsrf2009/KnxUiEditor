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
using System.Drawing;
using Structure.Control;

namespace UIEditor.Entity
{
    [Serializable]
    public class GroupBoxNode : ContainerNode
    {
        #region 变量
        private static int index = 0;
        #endregion

        #region 构造函数
        public GroupBoxNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextGroupBox") + "_" + index;

            this.Width = 300;
            this.Height = 400;


            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;
        }

        /// <summary>
        /// KNXGrid 转 GridNode
        /// </summary>
        /// <param name="knx"></param>
        public GroupBoxNode(KNXGroupBox knx)
            : base(knx)
        {
            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;
        }

        protected GroupBoxNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion

        #region KNX
        /// <summary>
        /// GridNode转KNXGrid
        /// </summary>
        /// <returns></returns>
        public KNXGroupBox ToKnx()
        {
            var knx = new KNXGroupBox();

            base.ToKnx(knx);

            return knx;
        }
        #endregion

        /// <summary>
        /// 显示GridNode的属性于GridView中
        /// </summary>
        /// <param name="grid"></param>
        public override void DisplayProperties(Grid grid)
        {
            base.DisplayProperties(grid);

            var nameModel = new Cell();
            nameModel.BackColor = grid.BackColor;

            var stringEditor = new TextBox(typeof(string));
            var intEditor = new TextBoxNumeric(typeof(int));

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
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var backcolorButtonController = new SourceGrid.Cells.Controllers.Button();
            backcolorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(backcolorButtonController);
            #endregion

            #region ControlBaseNode
            /* 9 ETS写地址表 */
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

            /* 10 ETS读地址 */
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
            #endregion
        }

        /// <summary>
        /// GridView中的属性发生改变
        /// </summary>
        /// <param name="context"></param>
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
                    this.Text = context.Value.ToString();
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
                case 7:     //  圆角半径
                    this.Radius = Convert.ToInt32(context.Value);
                    break;
                case 8:     //  背景色
                    this.BackgroundColor = (string)context.Value;
                    grid[row, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
                    break;
                #endregion

                #region ControlBaseNode属性
                case 9:    // ETS写地址表
                    break;
                case 10:    // ETS读地址
                    break;
                #endregion

                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxGroupBoxType);
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
            return formatter.Deserialize(stream) as GroupBoxNode;
        }
    }
}
