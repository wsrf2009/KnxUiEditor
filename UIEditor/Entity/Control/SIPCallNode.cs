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
    public class SIPCallNode : ControlBaseNode
    {
        private static int index = 0;

        #region 属性
        // 标签
        public string Lable { get; set; }
        //
        public string Note { get; set; }
        //用户名称
        public string UserName { get; set; }
        //SIP域名
        public string SIPDomain { get; set; }
        //认证用户
        public string AuthenticationUserName { get; set; }
        //认证密码
        public string AuthenticationUserPassword { get; set; }
        //端口
        public int Port { get; set; }
        //协议类型
        public Prococal Prococal { get; set; }
        //代理地址
        public string ProxyAddressUrl { get; set; }
        #endregion

        #region 构造函数
        public SIPCallNode()
        {
            index++;

            this.Lable = "test";
            this.Note = "test";
            this.UserName = "test user";
            this.SIPDomain = "";
            this.AuthenticationUserName = "test";
            this.AuthenticationUserPassword = "123456";
            this.Port = 9090;
            this.Prococal = Prococal.TCP;
            this.ProxyAddressUrl = "";

            this.Text = "可视门铃_" + index;
            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSipCallType;
        }
        public SIPCallNode(KNXSIPCall knx)
            : base(knx)
        {
            this.Lable = knx.Lable;
            this.Note = knx.Note;
            this.UserName = knx.UserName;
            this.SIPDomain = knx.SIPDomain;
            this.AuthenticationUserName = knx.AuthenticationUserName;
            this.AuthenticationUserPassword = knx.AuthenticationUserPassword;
            this.Port = knx.Port;
            this.Prococal = knx.Prococal;
            this.ProxyAddressUrl = knx.ProxyAddressUrl;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSipCallType;

        }

        protected SIPCallNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        #region knx
        public KNXSIPCall ToKnx()
        {
            var knx = new KNXSIPCall();

            base.ToKnx(knx);

            knx.Lable = this.Lable;
            knx.Note = this.Note;
            knx.UserName = this.UserName;
            knx.SIPDomain = this.SIPDomain;
            knx.AuthenticationUserName = this.AuthenticationUserName;
            knx.AuthenticationUserPassword = this.AuthenticationUserPassword;
            knx.Port = this.Port;
            knx.Prococal = this.Prococal;
            knx.ProxyAddressUrl = this.ProxyAddressUrl;


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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("标签");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Lable);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("说明");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Note);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("用户名称");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.UserName);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("SIP域名");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.SIPDomain);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("认证用户");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.AuthenticationUserName);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("认证密码");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.AuthenticationUserPassword);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("端口");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Port);
            grid[currentRow, MyConst.ValueColumn].Editor = intEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("协议类型");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.Prococal, typeof(Prococal));
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);

            currentRow++;
            grid.Rows.Insert(currentRow);
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("代理地址");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.ProxyAddressUrl);
            grid[currentRow, MyConst.ValueColumn].Editor = stringEditor;
            grid[currentRow, MyConst.ValueColumn].AddController(valueChangedController);
            #endregion

        }

        public override void ChangePropValues(CellContext context)
        {
            int row = context.Position.Row;

            switch (row)
            {
                #region 标准属性
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

                #region 自定义属性
                case 13:
                    this.Lable = context.Value.ToString();
                    break;
                case 14:
                    this.Note = context.Value.ToString();
                    break;
                case 15:
                    this.UserName = context.Value.ToString();
                    break;
                case 16:
                    this.SIPDomain = context.Value.ToString();
                    break;
                case 17:
                    this.AuthenticationUserName = context.Value.ToString();
                    break;
                case 18:
                    this.AuthenticationUserPassword = context.Value.ToString();
                    break;
                case 19:
                    this.Port = Convert.ToInt32(context.Value);
                    break;
                case 20:
                    this.Prococal = (Prococal)context.Value;
                    break;
                case 21:
                    this.ProxyAddressUrl = context.Value.ToString();
                    break;
                default:
                    ShowSaveEntityMsg(MyConst.Controls.KnxSipCallType);
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
            return formatter.Deserialize(stream) as GridNode;
        }
    }
}
