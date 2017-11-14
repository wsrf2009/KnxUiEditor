using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.Entity.Control;

namespace UIEditor
{
    public partial class FrmDimmer : Form
    {
        #region 变量
        private DimmerNode node { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaSwitch { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaDimRelatively { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaDimAbsoultely { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStateOnOff { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStateDimValue { get; set; }
        #endregion

        public FrmDimmer(DimmerNode node)
        {
            InitializeComponent();

            this.Text = this.Text + "-" + node.Title;

            this.ckbSwitch.Checked = this.tlpSwitch.Enabled = node.Switch.Enable;
            this.ckbDimRelatively.Checked = this.tlpDimRelatively.Enabled = node.DimRelatively.Enable;
            this.ckbDimAbsolutely.Checked = this.tlpDimAbsolutely.Enabled = node.DimAbsolutely.Enable;
            this.ckbStateOnOff.Checked = this.tlpStateOnOff.Enabled = node.StateOnOff.Enable;
            this.ckbStateDimValue.Checked = this.tlpStateDimValue.Enabled = node.StateDimValue.Enable;

            this.gaSwitch = new Dictionary<string, KNXSelectedAddress>(node.Switch.MapSelectedAddress);
            this.gaDimRelatively = new Dictionary<string, KNXSelectedAddress>(node.DimRelatively.MapSelectedAddress);
            this.gaDimAbsoultely = new Dictionary<string, KNXSelectedAddress>(node.DimAbsolutely.MapSelectedAddress);
            this.gaStateOnOff = new Dictionary<string, KNXSelectedAddress>(node.StateOnOff.MapSelectedAddress);
            this.gaStateDimValue = new Dictionary<string, KNXSelectedAddress>(node.StateDimValue.MapSelectedAddress);

            this.txbxSwitch.Text = KNXSelectedAddress.GetGroupAddressName(this.gaSwitch);
            this.txbxDimRelatively.Text = KNXSelectedAddress.GetGroupAddressName(this.gaDimRelatively);
            this.txbxDimAbsolutely.Text = KNXSelectedAddress.GetGroupAddressName(this.gaDimAbsoultely);
            this.txbxStateOnOff.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateOnOff);
            this.txbxStateDimValue.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateDimValue);

            this.node = node;
        }

        #region 事件
        #region 按钮
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.node.Switch.Enable = this.ckbSwitch.Checked;
                this.node.Switch.MapSelectedAddress = this.gaSwitch;

                this.node.DimRelatively.Enable = this.ckbDimRelatively.Checked;
                this.node.DimRelatively.MapSelectedAddress = this.gaDimRelatively;

                this.node.DimAbsolutely.Enable = this.ckbDimAbsolutely.Checked;
                this.node.DimAbsolutely.MapSelectedAddress = this.gaDimAbsoultely;

                this.node.StateOnOff.Enable = this.ckbStateOnOff.Checked;
                this.node.StateOnOff.MapSelectedAddress = this.gaStateOnOff;

                this.node.StateDimValue.Enable = this.ckbStateDimValue.Checked;
                this.node.StateDimValue.MapSelectedAddress = this.gaStateDimValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbSwitch.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaSwitch;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaSwitch = frm.SelectedAddress;

                    this.txbxSwitch.Text = KNXSelectedAddress.GetGroupAddressName(this.gaSwitch);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDimRelatively_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbDimRelatively.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaDimRelatively;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaDimRelatively = frm.SelectedAddress;

                    this.txbxDimRelatively.Text = KNXSelectedAddress.GetGroupAddressName(this.gaDimRelatively);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDimAbsolutely_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbDimAbsolutely.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaDimAbsoultely;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaDimAbsoultely = frm.SelectedAddress;

                    this.txbxDimAbsolutely.Text = KNXSelectedAddress.GetGroupAddressName(this.gaDimAbsoultely);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStateOnOff_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStateOnOff.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStateOnOff;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStateOnOff = frm.SelectedAddress;

                    this.txbxStateOnOff.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateOnOff);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStateDimValue_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStateDimValue.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStateDimValue;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStateDimValue = frm.SelectedAddress;

                    this.txbxStateDimValue.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateDimValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Checkbox
        private void ckbSwitch_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpSwitch.Enabled = this.ckbSwitch.Checked;
        }

        private void ckbDimRelatively_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpDimRelatively.Enabled = this.ckbDimRelatively.Checked;
        }

        private void ckbDimAbsolutely_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpDimAbsolutely.Enabled = this.ckbDimAbsolutely.Checked;
        }

        private void ckbStateOnOff_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStateOnOff.Enabled = this.ckbStateOnOff.Checked;
        }

        private void ckbStateDimValue_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStateDimValue.Enabled = this.ckbStateDimValue.Checked;
        }
        #endregion
        #endregion
    }
}
