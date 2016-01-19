using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceGrid;
using Structure;
using UIEditor.Component;


namespace UIEditor.Entity
{
    [Serializable]
    public class AppNode : ViewNode
    {
        private static int index = 0;

        #region 属性
        /// <summary>
        /// 产品说明信息
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// 企业Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 应用程序图标
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 默认语言
        /// </summary>
        public Language DefaultLanguage { get; set; }

        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int ScreenWidth { get; set; }

        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int ScreenHeight { get; set; }

        #endregion

        #region 构造函数
        public AppNode()
        {
            index++;

            Text = "应用程序_" + index;


            this.About = "KNX App UIEditor";
            this.Logo = MyConst.DefaultIcon;
            this.Symbol = MyConst.DefaultIcon;
            this.DefaultLanguage = Language.Chinese;
            this.ScreenWidth = 800;
            this.ScreenHeight = 600;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;
        }

        public AppNode(KNXApp knx)
            : base(knx)
        {
            Text = knx.Text;

            this.About = knx.About;
            this.Logo = knx.Logo;
            this.Symbol = knx.Symbol;
            this.DefaultLanguage = knx.DefaultLanguage;
            this.ScreenWidth = knx.ScreenWidth;
            this.ScreenHeight = knx.ScreenHeight;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxAppType;
        }

        protected AppNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        #region toKNX
        public KNXApp ToKnx()
        {
            KNXApp knx = new KNXApp();

            base.ToKnx(knx);

            knx.About = this.About;
            knx.Logo = this.Logo;
            knx.Symbol = this.Symbol;
            knx.DefaultLanguage = this.DefaultLanguage;
            knx.ScreenWidth = this.ScreenWidth;
            knx.ScreenHeight = this.ScreenHeight;

            knx.Areas = new System.Collections.Generic.List<KNXArea>();

            return knx;
        }
        #endregion

        #region 显示属性
        public override void DisplayProperties(Grid grid)
        {
            #region 显示属性

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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("程序说明");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.About);
            var multilineEditor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            multilineEditor.Control.Multiline = true;
            grid[currentRow, MyConst.ValueColumn].Editor = multilineEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("企业Logo");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.Logo == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Logo);
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.Logo));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController = new SourceGrid.Cells.Controllers.Button();
            imageButtonController.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("图标");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            if (this.Symbol == null)
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell("");
            }
            else
            {
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Symbol);
                grid[currentRow, MyConst.ValueColumn].Image =
                    ImageHelper.LoadImage(Path.Combine(MyCache.ProjImagePath, this.Symbol));
            }
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            grid[currentRow, MyConst.ButtonColumn] = new SourceGrid.Cells.Button("...");
            var imageButtonController2 = new SourceGrid.Cells.Controllers.Button();
            imageButtonController2.Executed += PickImage;
            grid[currentRow, MyConst.ButtonColumn].Controller.AddController(imageButtonController2);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("屏幕宽度(像素)");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ScreenWidth);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("屏幕高度(像素)");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ScreenHeight);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            #endregion
        }
        #endregion

        #region 修改属性值
        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            #region app
            switch (row)
            {
                case 2:
                    this.Text = context.Value.ToString();
                    break;
                case 3:
                    this.About = context.Value.ToString();
                    break;
                case 4:
                    this.Logo = context.Value.ToString();
                    break;
                case 5:
                    this.Symbol = context.Value.ToString();
                    break;
                case 6:
                    this.ScreenWidth = Convert.ToInt32(context.Value);
                    break;
                case 7:
                    this.ScreenHeight = Convert.ToInt32(context.Value);
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.View.KnxAppType);
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
            return formatter.Deserialize(stream) as AppNode;
        }

        #endregion
    }
}
