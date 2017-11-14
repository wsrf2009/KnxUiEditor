using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Threading;
using GroupAddress;
using KNX.DatapointAction;
using UIEditor.Component;
using KNX;
using KNX.DatapointType;
using Utils;
using UIEditor.GroupAddress;


namespace UIEditor
{
    public enum DataStatus { Add, Modify }

    public partial class FrmGroupAddress : Form
    {
        public EdGroupAddress Address { get; set; }
        private TreeView tvDPTName = new TreeView();

        public FrmGroupAddress(DataStatus status)
        {
            InitializeComponent();

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cmbPriority.Items.Add(it);
            }

            foreach (var it in MyCache.NodeActions)
            {
                this.treeViewDefaultActions.Nodes.Add(it);
            }

            AddressStatus = status;
            if (DataStatus.Modify == AddressStatus)
            {
                this.txtID.Enabled = false;
                this.txtWriteAddress.Enabled = false;
                this.Text = UIResMang.GetString("TextModifyAddress");
            }
            else if (DataStatus.Add == AddressStatus)
            {
                this.Text = UIResMang.GetString("TextAddNewAddress");
            }

            foreach (var it in MyCache.NodeTypes)
            {
                this.tvDPTName.Nodes.Add(it);
            }
            this.tvDPTName.Height = 300;
            this.tvDPTName.Visible = false;
            this.tvDPTName.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvDPTName_NodeMouseDoubleClick);
            this.Controls.Add(this.tvDPTName);
        }

        public DataStatus AddressStatus { get; set; }

        private void FrmGroupAddress_Load(object sender, EventArgs e)
        {
            if (Address != null)
            {
                this.txtID.Text = Address.Id;
                this.txtName.Text = Address.Name;
                this.txtWriteAddress.Text = Address.KnxAddress;
                this.btnDPTName.Text = Address.DPTName;
                this.cmbPriority.SelectedItem = Address.Priority.ToString();
                this.cbxCommunication.Checked = Address.IsCommunication;
                this.cbxRead.Checked = Address.IsRead;
                this.cbxWrite.Checked = Address.IsWrite;
                this.cbxTransmit.Checked = Address.IsTransmit;
                this.cbxUpgrade.Checked = Address.IsUpgrade;

                if (this.Address.Actions != null)
                {
                    this.listBoxActios.Items.AddRange(this.Address.Actions.GetActionNames());
                }

                this.buttonDeleteAction.Enabled = false;
                this.buttonAddAction.Enabled = false;
            }
        }

        private void FrmGroupAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tvDPTName.Nodes.Clear();
            this.treeViewDefaultActions.Nodes.Clear();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Address.Name = this.txtName.Text.Trim();

            try
            {
                KNXAddressHelper.StringToAddress(this.txtWriteAddress.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtWriteAddress.Focus();
                return;
            }
            Address.KnxAddress = this.txtWriteAddress.Text.Trim();

            // 数据类型
            if (string.IsNullOrWhiteSpace(this.btnDPTName.Text))
            {
                MessageBox.Show(UIResMang.GetString("Message46"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.btnDPTName.Focus();
                return;
            }
            DatapointType dpt = DPTHelper.GetTypeNode(this.btnDPTName.Text); //this.tvDPTName.SelectedNode as DatapointType;
            if (null != dpt)
            {
                Address.KnxMainNumber = dpt.KNXMainNumber;
                Address.KnxSubNumber = dpt.KNXSubNumber;
                Address.DPTName = dpt.Text;
                Address.Type = dpt.Type;
            }

            // 优先级
            var selectedTextPriority = this.cmbPriority.SelectedItem;
            if (selectedTextPriority != null)
            {
                KNXPriority priority = KNXPriority.Low;
                Enum.TryParse(selectedTextPriority.ToString(), out priority);
                Address.Priority = priority;
            }

            Address.IsCommunication = this.cbxCommunication.Checked;
            Address.IsRead = this.cbxRead.Checked;
            Address.IsWrite = this.cbxWrite.Checked;
            Address.IsTransmit = this.cbxTransmit.Checked;
            Address.IsUpgrade = this.cbxUpgrade.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void listBoxActios_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonAddAction.Enabled = false;
            this.buttonDeleteAction.Enabled = true;

            string actionName = (string)this.listBoxActios.SelectedItem;
            DatapointActionNode action = Address.Actions.GetActionAccrodingToActionName(actionName);
            if (null != action)
            {
                this.textBoxActionName.Text = action.ActionName;
                this.comboBoxActionValue.Text = action.Value.ToString();
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
                MessageBox.Show(UIResMang.GetString("Message22"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxActionName.Focus();
                return;
            }

            string param = this.comboBoxActionValue.Text.Trim();
            if (param.Trim().Length <= 0)
            {
                MessageBox.Show(UIResMang.GetString("Message23"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                string errorMsg = UIResMang.GetString("Message24");
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.comboBoxActionValue.Focus();
                Console.Write(errorMsg + LogHelper.Format(ex));

                return;
            }

            addActionToList(new DatapointActionNode(name, value), true);
        }

        private void buttonDeleteAction_Click(object sender, EventArgs e)
        {
            string actionName = (string)this.listBoxActios.SelectedItem;
            DatapointActionNode action = Address.Actions.GetActionAccrodingToActionName(actionName);
            if (null != action)
            {
                //Address.Actions.Actions.Remove(action);
                Address.Actions.RemoveActionNode(action);
                this.listBoxActios.Items.Remove(actionName);
                this.textBoxActionName.Text = "";
                this.comboBoxActionValue.Text = "";
                this.listBoxActios.Refresh();
            }
        }

        //private DatapointActionNode getActionAccrodingToActionName(string name)
        //{
        //    if (null != name)
        //    {
        //        foreach (var action in Address.Actions.Actions)
        //        {
        //            if (name.Equals(action.ActionName, StringComparison.CurrentCulture))
        //            {
        //                return action;
        //            }
        //        }
        //    }

        //    return null;
        //}

        private void buttonHelper_Click(object sender, EventArgs e)
        {

            if (DatapointType.DPT_1 == Address.KnxMainNumber)
            {
                MessageBox.Show(UIResMang.GetString("Message25"), UIResMang.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (DatapointType.DPT_3 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_7 == Address.KnxSubNumber)
                {
                    MessageBox.Show(UIResMang.GetString("Message26"), UIResMang.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (DatapointType.DPT_5 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_1 == Address.KnxSubNumber)
                {
                    MessageBox.Show(UIResMang.GetString("Message27"), UIResMang.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (DatapointType.DPT_18 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_1 == Address.KnxSubNumber)
                {
                    MessageBox.Show(UIResMang.GetString("Message28"), UIResMang.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (KNXDataType.Bit1 == Address.Type)
            {
                MessageBox.Show(UIResMang.GetString("Message29"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                MessageBox.Show(UIResMang.GetString("Message30"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (KNXDataType.Bit8 == Address.Type)
            {
                MessageBox.Show(UIResMang.GetString("Message31"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void treeViewDefaultActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;

            DatapointType actionNode = currentNode as DatapointType;

            if (null != actionNode)
            {
                if ((Address.KnxMainNumber == actionNode.KNXMainNumber) && (Address.KnxSubNumber == actionNode.KNXSubNumber))
                {
                    this.buttonAddAction.Enabled = true;
                }
                else if (Address.Type == actionNode.Type)
                {
                    this.buttonAddAction.Enabled = true;
                }
                else
                {
                    this.buttonAddAction.Enabled = false;
                }
            }
            else
            {
                this.buttonAddAction.Enabled = false;
            }

            this.buttonDeleteAction.Enabled = false;
        }

        private void buttonAddAction_Click(object sender, EventArgs e)
        {
            foreach (TreeNode it in this.treeViewDefaultActions.SelectedNode.Nodes)
            {
                DatapointActionNode actionNode = it as DatapointActionNode;
                if (null != actionNode)
                {
                    bool r = addActionToList(actionNode, true);
                    if (!r)
                    {
                        return;
                    }
                }
                else
                {
                    foreach (var node in it.Nodes)
                    {
                        DatapointActionNode actNode = node as DatapointActionNode;
                        if (null != actNode)
                        {
                            bool r = addActionToList(actNode, true);
                            if (!r)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        private bool addActionToList(DatapointActionNode newAction, bool duplicateTip)
        {
            if (null == Address.Actions)
            {
                //Address.Actions = new List<DatapointActionNode>();
                Address.Actions = new GroupAddressActions();
            }

            DatapointActionNode action = Address.Actions.GetActionAccrodingToActionName(newAction.ActionName);
            if (null != action)
            {
                if (duplicateTip)
                {
                    MessageBox.Show(UIResMang.GetString("Message32"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                return false;
            }

            if (KNXDataType.Bit1 == Address.Type)
            {
                if ((0 > newAction.Value) || (1 < newAction.Value))
                {
                    MessageBox.Show(UIResMang.GetString("Message33"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                if ((0 > newAction.Value) || (15 < newAction.Value))
                {
                    MessageBox.Show(UIResMang.GetString("Message34"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit8 == Address.Type)
            {
                if ((-127 > newAction.Value) || (255 < newAction.Value))
                {
                    MessageBox.Show(UIResMang.GetString("Message35"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                if ((DatapointType.DPT_5 == Address.KnxMainNumber) && (DatapointType.DPST_1 == Address.KnxSubNumber))
                {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show(UIResMang.GetString("Message36"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
                else if ((DatapointType.DPT_18 == Address.KnxMainNumber) && (DatapointType.DPST_1 == Address.KnxSubNumber))
                {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show(UIResMang.GetString("Message36"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
            }

            //Address.Actions.Add(newAction);
            Address.Actions.AddActionNode(newAction);
            this.listBoxActios.Items.Add(newAction.ActionName);
            this.textBoxActionName.Text = "";
            this.comboBoxActionValue.Text = "";
            this.listBoxActios.Refresh();

            return true;
        }

        private void btnDPTName_Click(object sender, EventArgs e)
        {
            if (this.tvDPTName.Visible)
            {
                this.tvDPTName.Visible = false;
            }
            else
            {
                this.tvDPTName.Top = this.btnDPTName.Bottom;
                this.tvDPTName.Left = this.btnDPTName.Left;
                this.tvDPTName.Width = this.btnDPTName.Width;

                this.tvDPTName.Visible = true;
                this.tvDPTName.BringToFront();

                TreeViewHelper.SelectNode2Level(this.tvDPTName, this.btnDPTName.Text);
            }
        }

        private void tvDPTName_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DatapointType selectedNode = e.Node as DatapointType;
            this.btnDPTName.Text = selectedNode.Text;
            this.tvDPTName.Visible = false;

            Address.KnxMainNumber = selectedNode.KNXMainNumber;
            Address.KnxSubNumber = selectedNode.KNXSubNumber;
            Address.Type = selectedNode.Type;
        }
    }
}
