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
using GroupAddress = UIEditor.GroupAddress.EdGroupAddress;
using UIEditor.Component;
using UIEditor.KNX.DatapointType;
using UIEditor.GroupAddress;
using System.Threading;
using UIEditor.KNX.DatapointAction;

namespace UIEditor
{
    public enum DataStatus { Add, Modify }

    public partial class FrmGroupAddress : Form
    {
        private EdGroupAddress _address = null;
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
                //this.txtName.Enabled = false;
                this.txtWriteAddress.Enabled = false;
                //this.btnDPTName.Enabled = false;
                //this.tvDPTName.Enabled = false;
            }
            //else if (DataStatus.Add == AddressStatus)
            //{
                foreach (var it in MyCache.NodeTypes)
                {
                    this.tvDPTName.Nodes.Add(it);
                }
                this.tvDPTName.Height = 300;
                this.tvDPTName.Visible = false;
                this.tvDPTName.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvDPTName_NodeMouseDoubleClick);
                this.Controls.Add(this.tvDPTName);
            //}
        }

        public EdGroupAddress Address
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
                this.txtName.Text = Address.Name;
                this.txtWriteAddress.Text = Address.KnxAddress;
                this.btnDPTName.Text = Address.DPTName;
                //TreeViewHelper.SelectNode2Level(this.tvDPTName, Address.DPTName);
                this.cmbPriority.SelectedItem = Address.Priority.ToString();
                this.txtDefaultValue.Text = Address.DefaultValue;
                this.txtReadTimespan.Text = Address.ReadTimeSpan.ToString();
                this.cbxCommunication.Checked = Address.IsCommunication;
                this.cbxRead.Checked = Address.IsRead;
                this.cbxWrite.Checked = Address.IsWrite;
                this.cbxTransmit.Checked = Address.IsTransmit;
                this.cbxUpgrade.Checked = Address.IsUpgrade;
                //this.textBoxTip.Text = Address.Tip;

                if (_address.Actions != null)
                {
                    foreach (var it in _address.Actions)
                    {
                        this.listBoxActios.Items.Add(it.Name);
                    }
                }

                this.buttonDeleteAction.Enabled = false;
                this.buttonAddAction.Enabled = false;

                //new Thread(tvDPTNameSelect).Start();
                //this.backgroundWorker1.RunWorkerAsync();
            }
        }

        private void FrmGroupAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tvDPTName.Nodes.Clear();
            this.treeViewDefaultActions.Nodes.Clear();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.btnDPTName.Text))
            {
                MessageBox.Show(ResourceMng.GetString("Message46"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            Address.Name = this.txtName.Text.Trim();
            Address.KnxAddress = this.txtWriteAddress.Text.Trim();

            // 数据类型
            DatapointType dpt = DatapointType.GetTypeNode(this.btnDPTName.Text); //this.tvDPTName.SelectedNode as DatapointType;
            if (null != dpt)
            {
                Address.KnxMainNumber = dpt.KNXMainNumber;
                Address.KnxSubNumber = dpt.KNXSubNumber;
                Address.DPTName = dpt.Text;
                Address.Type = dpt.Type;
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

            //Address.WireNumber = this.txtWireNumber.Text.Trim();
            Address.ReadTimeSpan = Convert.ToInt32(txtReadTimespan.Text);
            Address.IsCommunication = this.cbxCommunication.Checked;
            Address.IsRead = this.cbxRead.Checked;
            Address.IsWrite = this.cbxWrite.Checked;
            Address.IsTransmit = this.cbxTransmit.Checked;
            Address.IsUpgrade = this.cbxUpgrade.Checked;
            //Address.Tip = this.textBoxTip.Text;

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
            DatapointActionNode action = getActionAccrodingToActionName(actionName);
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
                MessageBox.Show(ResourceMng.GetString("Message22"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxActionName.Focus();
                return;
            }

            string param = this.comboBoxActionValue.Text.Trim();
            if (param.Trim().Length <= 0)
            {
                MessageBox.Show(ResourceMng.GetString("Message23"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                string errorMsg = ResourceMng.GetString("Message24");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.comboBoxActionValue.Focus();
                Console.Write(errorMsg + LogHelper.Format(ex));

                return;
            }

            addActionToList(new DatapointActionNode(name, value), true);
        }

        private void buttonDeleteAction_Click(object sender, EventArgs e)
        {
            string actionName = (string)this.listBoxActios.SelectedItem;
            DatapointActionNode action = getActionAccrodingToActionName(actionName);
            if (null != action)
            {
                Address.Actions.Remove(action);
                this.listBoxActios.Items.Remove(actionName);
                this.textBoxActionName.Text = "";
                this.comboBoxActionValue.Text = "";
                this.listBoxActios.Refresh();
            }
        }

        private DatapointActionNode getActionAccrodingToActionName(string name)
        {
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

            if (DatapointType.DPT_1 == Address.KnxMainNumber)
            {
                MessageBox.Show(ResourceMng.GetString("Message25"), ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (DatapointType.DPT_3 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_7 == Address.KnxSubNumber)
                {
                    MessageBox.Show(ResourceMng.GetString("Message26"), ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (DatapointType.DPT_5 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_1 == Address.KnxSubNumber)
                {
                    MessageBox.Show(ResourceMng.GetString("Message27"), ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (DatapointType.DPT_18 == Address.KnxMainNumber)
            {
                if (DatapointType.DPST_1 == Address.KnxSubNumber)
                {
                    MessageBox.Show(ResourceMng.GetString("Message28"), ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (KNXDataType.Bit1 == Address.Type)
            {
                MessageBox.Show(ResourceMng.GetString("Message29"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                MessageBox.Show(ResourceMng.GetString("Message30"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (KNXDataType.Bit8 == Address.Type)
            {
                MessageBox.Show(ResourceMng.GetString("Message31"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Address.Actions = new List<DatapointActionNode>();
            }

            DatapointActionNode action = getActionAccrodingToActionName(newAction.Name);
            if (null != action)
            {
                if (duplicateTip)
                {
                    MessageBox.Show(ResourceMng.GetString("Message32"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                return false;
            }

            if (KNXDataType.Bit1 == Address.Type)
            {
                if ((0 > newAction.Value) || (1 < newAction.Value))
                {
                    MessageBox.Show(ResourceMng.GetString("Message33"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit4 == Address.Type)
            {
                if ((0 > newAction.Value) || (15 < newAction.Value))
                {
                    MessageBox.Show(ResourceMng.GetString("Message34"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else if (KNXDataType.Bit8 == Address.Type)
            {
                if ((-127 > newAction.Value) || (255 < newAction.Value))
                {
                    MessageBox.Show(ResourceMng.GetString("Message35"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                if ((DatapointType.DPT_5 == Address.KnxMainNumber) && (DatapointType.DPST_1 == Address.KnxSubNumber))
                {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show(ResourceMng.GetString("Message36"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }
                }
                else if ((DatapointType.DPT_18 == Address.KnxMainNumber) && (DatapointType.DPST_1 == Address.KnxSubNumber))
                {
                    if ((0 > newAction.Value) || (255 < newAction.Value))
                    {
                        MessageBox.Show(ResourceMng.GetString("Message36"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        }

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Thread.Sleep(1);

        //    TreeViewHelper.SelectNode2Level(this.tvDPTName, Address.DPTName);

        //    if (DataStatus.Modify == AddressStatus)
        //    {
        //        this.tvDPTName.Enabled = false;
        //        //this.tlpTop.Enabled = false;
        //    }
        //}
    }
}
