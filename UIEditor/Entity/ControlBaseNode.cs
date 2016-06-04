
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
using System.Drawing;

namespace UIEditor.Entity
{
    [Serializable]
    public abstract class ControlBaseNode : ViewNode
    {
        #region 属性

        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        public bool HasTip { get; set; }

        public string Tip { get; set; }

        /// <summary>
        /// 控件是否可点击
        /// </summary>
        public bool Clickable { get; set; }

        /// <summary>
        /// 用户自定义的控件图标图标
        /// </summary>
        public string Icon { get; set; }

        #endregion

        #region 构造函数

        public ControlBaseNode()
        {
            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
            this.HasTip = false;
            this.Tip = "";
            this.Clickable = false;
            this.Icon = null;
        }

        /// <summary>
        /// KNXControlBase 转 ControlBaseNode
        /// </summary>
        /// <param name="knx"></param>
        public ControlBaseNode(KNXControlBase knx)
            : base(knx)
        {
            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();
            this.HasTip = knx.HasTip;
            this.Tip = knx.Tip;
            this.Clickable = knx.Clickable;
            this.Icon = knx.Icon;
        }

        protected ControlBaseNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { 
        }

        #endregion

        #region KNX

        /// <summary>
        /// ControlBaseNode 转 KNXControlBase
        /// </summary>
        /// <param name="knx"></param>
        public void ToKnx(KNXControlBase knx)
        {
            base.ToKnx(knx);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;
            knx.HasTip = this.HasTip;
            knx.Tip = this.Tip;
            knx.Clickable = this.Clickable;
            knx.Icon = this.Icon;
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

        #endregion

        /// <summary>
        /// 显示ControlBaseNode的属性于GridView中
        /// </summary>
        /// <param name="grid"></param>
        public override void DisplayProperties(Grid grid)
        {
            base.DisplayProperties(grid);
        }

        /// <summary>
        /// GridView中的属性发生改变
        /// </summary>
        /// <param name="context"></param>
        public override void ChangePropValues(CellContext context)
        {
        }

        /// <summary>
        /// 选取多个组地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PickMultiWriteAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            frm.MultiSelect = true;
            frm.PickType = FrmGroupAddressPick.AddressType.Write;
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

        /// <summary>
        /// 选取一个组地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PickSingleReadAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            frm.MultiSelect = false;
            frm.PickType = FrmGroupAddressPick.AddressType.Read;
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

        protected void PickTimerMultiWriteAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            frm.MultiSelect = true;
            frm.PickType = FrmGroupAddressPick.AddressType.Write;
            frm.NeedActions = true;
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

        protected void PickTimerMultiReadAddress(object sender, EventArgs e)
        {
            var context = (CellContext)sender;
            var btnCell = (Button)context.Cell;

            var frm = new FrmGroupAddressPick();
            frm.MultiSelect = true;
            frm.PickType = FrmGroupAddressPick.AddressType.Read;
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

    /// <summary>
    /// GridView中的值发生改变
    /// </summary>
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
