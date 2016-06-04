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

namespace UIEditor.Entity
{
    [Serializable]
    public class PageNode : ContainerNode
    {
        #region 变量
        private static int index = 0;
        #endregion

        #region 构造函数

        public PageNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextPage") + "_" + index;
            this.Width = 1280;
            this.Height = 800;
            this.Alpha = 1;
            this.Radius = 0;
            this.BackgroundColor = "#F0FFFF";
            this.BackgroundImage = "bj_kt.png";

            string FileImageOn = Path.Combine(MyCache.ProjImagePath, this.BackgroundImage);
            if (!File.Exists(FileImageOn))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.BackgroundImage), Path.Combine(MyCache.ProjImagePath, this.BackgroundImage));
            }

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }

        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <param name="knx"></param>
        public PageNode(KNXPage knx)
            : base(knx)
        {
            Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }

        protected PageNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <returns></returns>
        public KNXPage ToKnx()
        {
            var knx = new KNXPage();

            base.ToKnx(knx);

            //knx.GroupBoxs = new List<KNXGroupBox>();

            return knx;
        }

        /// <summary>
        /// 显示PageNode的属性于GridView中
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

            #region TreeNode 属性
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
            /* 2 宽度 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropWidth"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Width);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 3 高度 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropHeight"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Height);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            /* 4 背景色 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropBackColor"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var backColorButtonController = new SourceGrid.Cells.Controllers.Button();
            backColorButtonController.Executed += PickColor;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(backColorButtonController);

            /* 5 背景图片 */
            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell(ResourceMng.GetString("PropBackgroundImage"));
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.BackgroundImage == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.BackgroundImage);
                grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.BackgroundImage));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController = new SourceGrid.Cells.Controllers.Button();
            imageButtonController.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController);
            #endregion
        }

        /// <summary>
        /// GridView中的属性已改变
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
                    this.Text = (string)context.Value;
                    break;
                #endregion

                #region ViewNode属性
                case 2:     //  宽度
                    this.Width = Convert.ToInt32(context.Value);
                    break;
                case 3:     //  高度
                    this.Height = Convert.ToInt32(context.Value);
                    break;
                case 4:     //  背景色
                    this.BackgroundColor = (string)context.Value;
                    if (null != this.BackgroundColor)
                    {
                        grid[row, MyConst.ValueColumn].Image = ImageHelper.CreateImage(this.BackgroundColor);
                    }
                    else
                    {
                        grid[row, MyConst.ValueColumn].Image = ImageHelper.CreateImage(Color.Black);
                    }
                    break;
                case 5:     //  背景图片
                    this.BackgroundImage = (string)context.Value;
                    if (null != this.BackgroundImage)
                    {
                        grid[row, MyConst.ValueColumn].Image = ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.BackgroundImage));
                    }
                    else
                    {
                        grid[row, MyConst.ValueColumn].Image = null;
                    }

                    break;
                #endregion

                default:
                    ShowSaveEntityMsg(MyConst.View.KnxPageType);
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
            return formatter.Deserialize(stream) as PageNode;
        }
    }
}