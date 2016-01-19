using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Structure;
using Structure.ETS;
using UIEditor.Entity;
using GroupAddress = UIEditor.ETS.GroupAddress;
using UIEditor.Component;
using UIEditor.Actions;
using UIEditor.Actions.TypesB1.DPTSwitch;

namespace UIEditor
{
    public enum DataStatus { Add, Modify }

    public partial class FrmGroupAddress : Form
    {
        private GroupAddress _address = new GroupAddress();

        public FrmGroupAddress(DataStatus status)
        {
            InitializeComponent();

            foreach (var it in Enum.GetNames(typeof(KNXDataType)))
            {
                this.cmbType.Items.Add(it);
            }

            AddressStatus = status;

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cmbPriority.Items.Add(it);
            }

            foreach (var it in (new DefaultActions()).getDefalutActions()) {
                this.treeViewDefaultActions.Nodes.Add(it);
            }
        }

        public GroupAddress Address
        {
            get 
            {
                return _address; 
            }
            set 
            {
                _address = value;
                
            }
        }

        public DataStatus AddressStatus { get; set; }

        private void FrmGroupAddress_Load(object sender, EventArgs e)
        {
            if (Address != null)
            {
                this.txtID.Text = Address.Id;
                this.txtID.ReadOnly = true;
                this.txtName.Text = Address.Name;
                this.txtWriteAddress.Text = Address.KnxAddress;
                this.cmbType.SelectedItem = Address.Type.ToString();
                //if (Address.KnxDPTName.Length > 0)
                //{
                //    if (Address.KnxSize.Length > 0)
                //    {
                //        this.comboBoxDatapointType.Items.Add(Address.Type + "(" + Address.KnxSize + ")");
                //        this.comboBoxDatapointType.SelectedItem = Address.Type + "(" + Address.KnxSize + ")";
                //    }
                //    else
                //    {
                //        this.comboBoxDatapointType.Items.Add(Address.Type);
                //        this.comboBoxDatapointType.SelectedItem = Address.Type;
                //    }
                //}
                //else { 
                
                //}
                
                this.txtDefaultValue.Text = Address.DefaultValue;
                this.cmbPriority.SelectedItem = Address.Priority.ToString();
                this.txtWireNumber.Text = Address.WireNumber;
                this.txtReadTimespan.Text = Address.ReadTimeSpan.ToString();
                this.cbxCommunication.Checked = Address.IsCommunication;
                this.cbxRead.Checked = Address.IsRead;
                this.cbxWrite.Checked = Address.IsWrite;
                this.cbxTransmit.Checked = Address.IsTransmit;
                this.cbxUpgrade.Checked = Address.IsUpgrade;
                this.textBoxTip.Text = Address.Tip;

                if (_address.Actions != null) { 
                    foreach (var it in _address.Actions)
                    {
                        this.listBoxActios.Items.Add(it.Name);
                    }
                }

                this.buttonDeleteAction.Enabled = false;
                this.buttonAddAction.Enabled = false;

                if (5 == Address.KnxMainNumber)
                {
                    if (1 == Address.KnxSubNumber)
                    { 
                    
                    }
                }
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            Address.Name = this.txtName.Text.Trim();
            Address.KnxAddress = this.txtWriteAddress.Text.Trim();
            // 数据类型
            var selectedText = this.cmbType.SelectedItem;
            if (selectedText != null)
            {
                KNXDataType dataType = KNXDataType.Bit1;
                Enum.TryParse(selectedText.ToString(), out dataType);
                Address.Type = dataType;
                //Address.KnxType = selectedText.ToString();
            }
            // 默认值
            Address.DefaultValue = this.txtDefaultValue.Text.Trim();

            // 优先级
            var selectedTextPriority = this.cmbPriority.SelectedItem;
            if (selectedTextPriority != null)
            {
                KNXPriority priority = KNXPriority.Low;
                Enum.TryParse(selectedTextPriority.ToString(), out priority);
                Address.Priority = priority;
            }

            Address.WireNumber = this.txtWireNumber.Text.Trim();
            Address.ReadTimeSpan = Convert.ToInt32(txtReadTimespan.Text);
            Address.IsCommunication = this.cbxCommunication.Checked;
            Address.IsRead = this.cbxRead.Checked;
            Address.IsWrite = this.cbxWrite.Checked;
            Address.IsTransmit = this.cbxTransmit.Checked;
            Address.IsUpgrade = this.cbxUpgrade.Checked;
            Address.Tip = this.textBoxTip.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void listBoxActios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.Write("\nlistBoxActios_SelectedIndexChanged() selected: "+this.listBoxActios.SelectedIndex);

            this.buttonAddAction.Enabled = false;
            this.buttonDeleteAction.Enabled = true;

            string actionName = (string)this.listBoxActios.SelectedItem;
            KNXDatapointAction action = getActionAccrodingToActionName(actionName);
            if (null != action)
            {
                this.textBoxActionName.Text = action.Name;
                this.comboBoxActionValue.Text = action.Value.ToString();

                //    if (action.CanBeDelete)
                //    {
                //        this.buttonDeleteAction.Enabled = true;
                //    }
                //    else
                //    {
                //        this.buttonDeleteAction.Enabled = false;
                //    }
            }
            else 
            {
                this.textBoxActionName.Text = "";
                this.comboBoxActionValue.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = this.textBoxActionName.Text.Trim();
            if (name.Length <= 0)
            {
                MessageBox.Show("行为名称为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxActionName.Focus();
                return;
            }

            string param = this.comboBoxActionValue.Text.Trim();
            if (param.Trim().Length <= 0)
            {
                MessageBox.Show("行为值为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.comboBoxActionValue.Focus();
                return;
            }

            int value = 0;
            try
            {
                value = int.Parse(param);
            }
            catch (Exception ex)
            {
                string errorMsg = "输入的值错误，必须为数字";
                MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.comboBoxActionValue.Focus();
                Console.Write(errorMsg + LogHelper.Format(ex));

                return;
            }

            addActionToList(new KNXDatapointAction(name, value), true);
        }

        private void buttonDeleteAction_Click(object sender, EventArgs e)
        {
            string actionName = (string)this.listBoxActios.SelectedItem;
            KNXDatapointAction action = getActionAccrodingToActionName(actionName);
            if (null != action)
            {
                Address.Actions.Remove(action);
                this.listBoxActios.Items.Remove(actionName);
                this.textBoxActionName.Text = "";
                this.comboBoxActionValue.Text = "";
                this.listBoxActios.Refresh();
            }
        }

        private KNXDatapointAction getActionAccrodingToActionName(string name) {
            if (null != name)
            {
                foreach (var action in Address.Actions)
                {
                    if (name.Equals(action.Name, StringComparison.CurrentCulture))
                    {
                        return action;
                    }
                }
            }

            return null;
        }

        private void buttonHelper_Click(object sender, EventArgs e)
        {
            if (1 == Address.KnxMainNumber)
            {
                MessageBox.Show("位操作，输入范围：0或1", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (3 == Address.KnxMainNumber)
            {
                if (7 == Address.KnxSubNumber) {
                    MessageBox.Show("相对调光，输入范围：0~15\n0：无操作；1~7：调暗，数值越大调节幅度越小；8：无操作；9~15调亮：数值越大调节幅度越小", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (5 == Address.KnxMainNumber)
            {
                if (1 == Address.KnxSubNumber)
                {
                    MessageBox.Show("绝对调节，输入范围：0~255\n计算公式 v = 255 * t（调节度，也即百分值）。例如要调节到25%，v=255*0.25=63.75，去掉小数得v=63", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (18 == Address.KnxMainNumber)
            {
                if (1 == Address.KnxSubNumber)
                {
                    MessageBox.Show("场面选择，输入范围：0~63", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (KNXDataType.Bit1 == Address.Type)
            {
                MessageBox.Show("只能输入0或1", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                MessageBox.Show("输入范围：0~15", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (KNXDataType.Bit8 == Address.Type)
            {
                MessageBox.Show("输入范围：-127~255", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
        }

        private void treeViewDefaultActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;

            ActionNode actionNode = currentNode as ActionNode;

            if ((Address.KnxMainNumber == actionNode.knxMainNumber) && (Address.KnxSubNumber == actionNode.knxSubNumber))
            {
                this.buttonAddAction.Enabled = true;
            }
            else if (Address.Type == actionNode.type)
            {
                this.buttonAddAction.Enabled = true;
            }
            else
            {
                this.buttonAddAction.Enabled = false;
            }

            this.buttonDeleteAction.Enabled = false;
        }

        private void buttonAddAction_Click(object sender, EventArgs e)
        {
            ActionNode actionNode = this.treeViewDefaultActions.SelectedNode as ActionNode;
            if(null != actionNode)
            {
                if (null != actionNode.action)
                {
                    addActionToList(actionNode.action, true);
                }
                else if (actionNode.Nodes.Count > 0)
                {

                    foreach (var node in actionNode.Nodes) {
                        ActionNode cNode = node as ActionNode;
                        if (null != cNode) {
                            if (null != cNode.action) {
                                addActionToList(cNode.action, false);
                            }
                        }
                    }
                }
            }
        }

        private bool addActionToList(KNXDatapointAction newAction, bool duplicateTip)
        {
            if (null == Address.Actions) {
                Address.Actions = new List<KNXDatapointAction>();
            }

            KNXDatapointAction action = getActionAccrodingToActionName(newAction.Name);
            if (null != action)
            {
                if (duplicateTip)
                {
                    MessageBox.Show("添加失败，已经存在相同的行为名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

                return false;
            }

            if (KNXDataType.Bit1 == Address.Type) {
                if ((0 > newAction.Value) || (1 < newAction.Value))
                {
                    MessageBox.Show("只能输入0或1", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                if ((0 > newAction.Value) || (15 < newAction.Value))
                {
                    MessageBox.Show("输入范围：0~15", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit8 == Address.Type) 
            {
                if ((-127 > newAction.Value) || (255 < newAction.Value)) {
                    MessageBox.Show("输入范围：-127~255", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                if ((5 == Address.KnxMainNumber) && (1 == Address.KnxSubNumber)) {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show("输入范围：0~255", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
                else if ((18 == Address.KnxMainNumber) && (1 == Address.KnxSubNumber))
                {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show("输入范围：0~255", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            } 

            Address.Actions.Add(newAction);
            this.listBoxActios.Items.Add(newAction.Name);
            this.textBoxActionName.Text = "";
            this.comboBoxActionValue.Text = "";
            this.listBoxActios.Refresh();

            return true;
        }
    }
}
