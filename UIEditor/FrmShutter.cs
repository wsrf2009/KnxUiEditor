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
using UIEditor.Component;
using UIEditor.Entity.Control;

namespace UIEditor
{
    public partial class FrmShutter : Form
    {
        #region 变量
        private ShutterNode node { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaShutterUpDown { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaShutterStop { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaPositionShutter { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaPositionBlinds { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStateUpperPosition { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStateLowerPosition { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStatusPositionShutter { get; set; }
        private Dictionary<string, KNXSelectedAddress> gaStatusPositionBlinds { get; set; }
        #endregion

        public FrmShutter(ShutterNode node)
        {
            InitializeComponent();

            this.Text = node.Title +"-" + this.Text;

            this.ckbShutterUpDown.Checked = this.tlpShutterUpDown.Enabled = node.ShutterUpDown.Enable;
            this.ckbShutterStop.Checked = this.tlpShutterStop.Enabled = node.ShutterStop.Enable;
            this.ckbPositionShutter.Checked = this.tlpPositionShutter.Enabled = node.AbsolutePositionOfShutter.Enable;
            this.ckbPositionBlinds.Checked = this.tlpPositionBlinds.Enabled = node.AbsolutePositionOfBlinds.Enable;
            this.ckbStateUpperPosition.Checked = this.tlpStateUpperPosition.Enabled = node.StateUpperPosition.Enable;
            this.ckbStateLowerPosition.Checked = this.tlpStateLowerPosition.Enabled = node.StateLowerPosition.Enable;
            this.ckbStatusPositionShutter.Checked = this.tlpStatusPositionShutter.Enabled = node.StatusActualPositionOfShutter.Enable;
            this.ckbStatusPositionBlinds.Checked = this.tlpStatusPositionBlinds.Enabled = node.StatusActualPositionOfBlinds.Enable;

            this.gaShutterUpDown = new Dictionary<string,KNXSelectedAddress>(node.ShutterUpDown.MapSelectedAddress);
            this.gaShutterStop = new Dictionary<string,KNXSelectedAddress>(node.ShutterStop.MapSelectedAddress);
            this.gaPositionShutter = new Dictionary<string,KNXSelectedAddress>(node.AbsolutePositionOfShutter.MapSelectedAddress);
            this.gaPositionBlinds = new Dictionary<string,KNXSelectedAddress>(node.AbsolutePositionOfBlinds.MapSelectedAddress);
            this.gaStateUpperPosition = new Dictionary<string,KNXSelectedAddress>(node.StateUpperPosition.MapSelectedAddress);
            this.gaStateLowerPosition = new Dictionary<string,KNXSelectedAddress>(node.StateLowerPosition.MapSelectedAddress);
            this.gaStatusPositionShutter = new Dictionary<string,KNXSelectedAddress>(node.StatusActualPositionOfShutter.MapSelectedAddress);
            this.gaStatusPositionBlinds = new Dictionary<string,KNXSelectedAddress>(node.StatusActualPositionOfBlinds.MapSelectedAddress);

            this.txbxShutterUpDown.Text = KNXSelectedAddress.GetGroupAddressName(this.gaShutterUpDown);
            this.txbxShutterStop.Text = KNXSelectedAddress.GetGroupAddressName(this.gaShutterStop);
            this.txbxPositionShutter.Text = KNXSelectedAddress.GetGroupAddressName(this.gaPositionShutter);
            this.txbxPositionBlinds.Text = KNXSelectedAddress.GetGroupAddressName(this.gaPositionBlinds);
            this.txbxStateUpperPosition.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateUpperPosition);
            this.txbxStateLowerPosition.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateLowerPosition);
            this.txbxStatusPositionShutter.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStatusPositionShutter);
            this.txbxStatusPositionBlinds.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStatusPositionBlinds);

            this.node = node;
        }

        #region 事件
        #region Button点击事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.node.ShutterUpDown.Enable = this.ckbShutterUpDown.Checked;
                this.node.ShutterUpDown.MapSelectedAddress = this.gaShutterUpDown;

                this.node.ShutterStop.Enable = this.ckbShutterStop.Checked;
                this.node.ShutterStop.MapSelectedAddress = this.gaShutterStop;

                this.node.AbsolutePositionOfShutter.Enable = this.ckbPositionShutter.Checked;
                this.node.AbsolutePositionOfShutter.MapSelectedAddress = this.gaPositionShutter;

                this.node.AbsolutePositionOfBlinds.Enable = this.ckbPositionBlinds.Checked;
                this.node.AbsolutePositionOfBlinds.MapSelectedAddress = this.gaPositionBlinds;

                this.node.StateUpperPosition.Enable = this.ckbStateUpperPosition.Checked;
                this.node.StateUpperPosition.MapSelectedAddress = this.gaStateUpperPosition;

                this.node.StateLowerPosition.Enable = this.ckbStateLowerPosition.Checked;
                this.node.StateLowerPosition.MapSelectedAddress = this.gaStateLowerPosition;

                this.node.StatusActualPositionOfShutter.Enable = this.ckbStatusPositionShutter.Checked;
                this.node.StatusActualPositionOfShutter.MapSelectedAddress = this.gaStatusPositionShutter;

                this.node.StatusActualPositionOfBlinds.Enable = this.ckbStatusPositionBlinds.Checked;
                this.node.StatusActualPositionOfBlinds.MapSelectedAddress = this.gaStatusPositionBlinds;
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

        private void btnShutterUpDown_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbShutterUpDown.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaShutterUpDown;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaShutterUpDown = frm.SelectedAddress;

                    this.txbxShutterUpDown.Text = KNXSelectedAddress.GetGroupAddressName(this.gaShutterUpDown);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnShutterStop_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbShutterStop.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaShutterStop;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaShutterStop = frm.SelectedAddress;

                    this.txbxShutterStop.Text = KNXSelectedAddress.GetGroupAddressName(this.gaShutterStop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnPositionShutter_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbPositionShutter.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaPositionShutter;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaPositionShutter = frm.SelectedAddress;

                    this.txbxPositionShutter.Text = KNXSelectedAddress.GetGroupAddressName(this.gaPositionShutter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnPositionBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbPositionBlinds.Text;
                frm.MultiSelect = true;
                frm.PickType = FrmGroupAddressPick.AddressType.Write;
                frm.SelectedAddress = this.gaPositionBlinds;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaPositionBlinds = frm.SelectedAddress;

                    this.txbxPositionBlinds.Text = KNXSelectedAddress.GetGroupAddressName(this.gaPositionBlinds);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStateUpperPosition_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStateUpperPosition.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStateUpperPosition;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStateUpperPosition = frm.SelectedAddress;

                    this.txbxStateUpperPosition.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateUpperPosition);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStateLowerPosition_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStateLowerPosition.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStateLowerPosition;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStateLowerPosition = frm.SelectedAddress;

                    this.txbxStateLowerPosition.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStateLowerPosition);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStatusPositionShutter_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStatusPositionShutter.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStatusPositionShutter;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStatusPositionShutter = frm.SelectedAddress;

                    this.txbxStatusPositionShutter.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStatusPositionShutter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnStatusPositionBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmGroupAddressPick();
                frm.Text = node.Title + " - " + this.ckbStatusPositionBlinds.Text;
                frm.MultiSelect = false;
                frm.PickType = FrmGroupAddressPick.AddressType.Read;
                frm.SelectedAddress = this.gaStatusPositionBlinds;
                var result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    this.gaStatusPositionBlinds = frm.SelectedAddress;

                    this.txbxStatusPositionBlinds.Text = KNXSelectedAddress.GetGroupAddressName(this.gaStatusPositionBlinds);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region CheckBox点击事件
        private void ckbShutterUpDown_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpShutterUpDown.Enabled = this.ckbShutterUpDown.Checked;
        }

        private void ckbShutterStop_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpShutterStop.Enabled = this.ckbShutterStop.Checked;
        }

        private void ckbPositionShutter_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpPositionShutter.Enabled = this.ckbPositionShutter.Checked;
        }

        private void ckbPositionBlinds_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpPositionBlinds.Enabled = this.ckbPositionBlinds.Checked;
        }

        private void cxbxStateUpperPosition_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStateUpperPosition.Enabled = this.ckbStateUpperPosition.Checked;
        }

        private void ckbStateLowerPosition_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStateLowerPosition.Enabled = this.ckbStateLowerPosition.Checked;
        }

        private void ckbStatusPositionShutter_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStatusPositionShutter.Enabled = this.ckbStatusPositionShutter.Checked;
        }

        private void ckbStatusPositionBlinds_CheckedChanged(object sender, EventArgs e)
        {
            this.tlpStatusPositionBlinds.Enabled = this.ckbStatusPositionBlinds.Checked;
        }
        #endregion
        #endregion

        #region 私有方法
        
        #endregion
    }
}
