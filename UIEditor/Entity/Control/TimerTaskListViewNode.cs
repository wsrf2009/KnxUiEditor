using SourceGrid;
using SourceGrid.Cells.Views;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UIEditor.Entity.Control
{
    [Serializable]
    public class TimerTaskListViewNode : ControlBaseNode
    {
        private static int index;
        private KNXTimer SelectedTimer;
        private List<KNXTimer> Timers;

        public TimerTaskListViewNode() {
            index++;

            this.Text = "定时任务列表_" + index;
            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxTimerTaskListViewType;
        }

        public TimerTaskListViewNode(KNXTimerTaskListView knx)
            : base(knx)
        {
            this.SelectedTimer = knx.SelectedTimer;

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxTimerTaskListViewType;
        }

        #region knx
        public KNXTimerTaskListView ToKnx()
        {
            var knx = new KNXTimerTaskListView();
            base.ToKnx(knx);

            knx.SelectedTimer = SelectedTimer;

            return knx;
        }
        #endregion

        protected TimerTaskListViewNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
            grid[currentRow, MyConst.NameColumn] = new SourceGrid.Cells.Cell("定时任务");
            grid[currentRow, MyConst.NameColumn].View = nameModel;
            this.Timers = new List<KNXTimer>();
            List<string> timerTaskList = new List<string>();
            bool isContain = false;
            foreach(var node in this.Parent.Nodes) {
                Console.Write("\nnode: " + node);
                ControlBaseNode controlBaseNode = node as ControlBaseNode;
                if (null != controlBaseNode)
                {
                    Console.Write(" type:"+node.GetType().ToString());
                    TimerButtonNode timerTaskButtonNode = controlBaseNode as TimerButtonNode;
                    if (null != timerTaskButtonNode)
                    {
                        string timerTaskName = timerTaskButtonNode.Text;
                        int timerTaskId = timerTaskButtonNode.Id;

                        Console.Write(" name:" + timerTaskButtonNode);

                        this.Timers.Add(new KNXTimer(timerTaskName, timerTaskId));
                        timerTaskList.Add(timerTaskName);
                        if (null != this.SelectedTimer)
                        {
                            if (this.SelectedTimer.Id == timerTaskId)
                            {
                                isContain = true;
                                this.SelectedTimer.Name = timerTaskName;
                            }
                        }
                    }
                }
            }

            SourceGrid.Cells.Editors.ComboBox cbInline = new SourceGrid.Cells.Editors.ComboBox(typeof(string), timerTaskList, false);
            cbInline.EditableMode = SourceGrid.EditableMode.Default | SourceGrid.EditableMode.Focus;
            if (timerTaskList.Count > 0) {
                if (isContain)
                {
                    grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(this.SelectedTimer.Name, cbInline);
                }
                else
                {
                    this.SelectedTimer = this.Timers[0];
                    grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(timerTaskList[0], cbInline);
                }
            } else {
                string noTimerTaskButton = "没有定时任务按钮，请先创建";
                timerTaskList.Add(noTimerTaskButton);
                grid[currentRow, MyConst.ValueColumn] = new SourceGrid.Cells.Cell(noTimerTaskButton, cbInline);
            }
            grid[currentRow, MyConst.ValueColumn].View = SourceGrid.Cells.Views.ComboBox.Default;
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
                case 13:
                    Console.Write("row 13:"+context.Value.ToString());
                    foreach (KNXTimer timer in this.Timers)
                    {
                        if (timer.Name.Equals(context.Value.ToString())) {
                            this.SelectedTimer = timer;
                        }
                    }
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
            return formatter.Deserialize(stream) as TimerButtonNode;
        }
    }
}
