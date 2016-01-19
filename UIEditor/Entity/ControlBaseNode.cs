
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using DevAge.Text.FixedLength;
using SourceGrid;
using SourceGrid.Cells.Controllers;
using Structure;
using UIEditor.Controls;
using Button = SourceGrid.Cells.Button;

namespace UIEditor.Entity
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class ControlBaseNode : ViewNode
    {
        #region 属性
        public int Row { get; set; }

        public int Column { get; set; }

        public int RowSpan { get; set; }

        public int ColumnSpan { get; set; }

        public string BackgroudColor { get; set; }

        public string FontColor { get; set; }

        public bool HasTip { get; set; }

        public string Tip { get; set; }

        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        #endregion

        #region 构造函数

        public ControlBaseNode()
        {
            Column = 1;
            Row = 1;
            ColumnSpan = 1;
            RowSpan = 1;
            BackgroudColor = "#FFFFFF";
            FontColor = "#000000";
            HasTip = false;
            Tip = "";
            ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
        }

        public ControlBaseNode(KNXControlBase knx)
            : base(knx)
        {
            // ViewNode 基类中的字段
            this.Text = knx.Text;
            // 字段
            this.Column = knx.Column + 1;
            this.Row = knx.Row + 1;
            this.ColumnSpan = knx.ColumnSpan;
            this.RowSpan = knx.RowSpan;
            this.BackgroudColor = knx.BackColor ?? "#FFFFFF";
            this.FontColor = knx.ForeColor ?? "#000000";
            this.HasTip = knx.HasTip;
            this.Tip = knx.Tip;
            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();
        }

        protected ControlBaseNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        #region ToKnx

        public void ToKnx(KNXControlBase knx)
        {
            // ViewNode 基类中的字段
            knx.Id = this.Id;
            knx.Text = this.Text;
            // 字段
            knx.Row = this.Row - 1;
            knx.Column = this.Column - 1;
            knx.RowSpan = this.RowSpan;
            knx.ColumnSpan = this.ColumnSpan;
            knx.BackColor = this.BackgroudColor;
            knx.ForeColor = this.FontColor;
            knx.HasTip = this.HasTip;
            knx.Tip = this.Tip;
            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;
        }

        /// <summary>
        /// 转换ETS Address ID字典为逗号分隔的字符串。
        /// </summary>
        /// <param name="etsAddIdDict"></param>
        /// <returns></returns>
        public static string EtsAddressDictToString(Dictionary<string, KNXSelectedAddress> etsAddIdDict)
        {
            var builder = new StringBuilder();
            foreach (var it in etsAddIdDict)
            {
                // 检查地址对象是否存在
                var address = MyCache.GroupAddressTable.FirstOrDefault(x => x.Id == it.Key);
                if (address != null)
                {
                    // 如果系统的地址列表中有则显示其名称
                    builder.Append(it.Value.Name).Append(MyConst.SplitSymbol);
                }
            }
            return builder.ToString().TrimEnd(MyConst.SplitSymbol);
        }

        //public static Dictionary<string, KNXSelectedAddress> EtsAddressStringToDict(string etsAddIds)
        //{
        //    var etsidDict = new Dictionary<string, KNXSelectedAddress>();

        //    if (!string.IsNullOrEmpty(etsAddIds))
        //    {
        //        var ids = etsAddIds.Split(MyConst.SplitSymbol);

        //        if (ids.Length > 0)
        //        {
        //            for (int i = 0, count = ids.Length; i < count; i++)
        //            {
        //                string key = ids[i];
        //                var address = MyCache.GroupAddressTable.FirstOrDefault(x => x.Id == key);
        //                etsidDict[key] = new KNXSelectedAddress { Id = address.Id, Name = address.Name, Type = (int)address.Type, DefaultValue = address.DefaultValue };
        //            }
        //        }
        //    }

        //    return etsidDict;
        //}

        #endregion

        public override void DisplayProperties(Grid grid)
        {

        }

        public override void ChangePropValues(CellContext context)
        {
        }

        protected void PickMultiAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            frm.MultiSelect = true;
            var tempValue = btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value;
            if (tempValue != null)
            {
                frm.SelectedAddress = this.WriteAddressIds;
            }

            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.WriteAddressIds = frm.SelectedAddress;
                btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = EtsAddressDictToString(this.WriteAddressIds);
            }
        }

        protected void PickAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            var tempValue = btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value;
            if (tempValue != null)
            {
                frm.SelectedAddress = this.ReadAddressId;
            }

            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.ReadAddressId = frm.SelectedAddress;
                btnCell.Grid[btnCell.Row.Index, MyConst.ValueColumn].Value = EtsAddressDictToString(this.ReadAddressId);
            }
        }
    }



    public class ValueChangedEvent : ControllerBase
    {
        public override void OnValueChanged(CellContext sender, EventArgs e)
        {
            base.OnValueChanged(sender, e);

            var selectNode = sender.Grid.Tag as ViewNode;

            string val = "Value of cell {0} is '{1}'";
            Debug.WriteLine(sender.Grid, string.Format(val, sender.Position, sender.Value));

            if (selectNode != null)
            {
                #region 属性值修改，Saved 设置为 False
                TreeView view = selectNode.TreeView;
                var frmMain = view.TopLevelControl as FrmMain;
                if (frmMain != null)
                {
                    frmMain.Saved = false;
                }
                #endregion

                //保存值到 ViewNode
                selectNode.ChangePropValues(sender);
            }
        }
    }
}
