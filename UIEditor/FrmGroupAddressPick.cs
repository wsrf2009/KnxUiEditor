using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using GroupAddress;
using Structure;
using UIEditor.Component;
using KNX;
using KNX.DatapointAction;

namespace UIEditor
{
    /// <summary>
    /// Respresents a standard dialog that can be used as a base for custom dialog forms.
    /// </summary>
    public partial class FrmGroupAddressPick : Form
    {
        #region 枚举量
        public enum AddressType { Write, Read };
        private enum FilterType
        {
            Name,
            GroupAddress
        }
        #endregion

        #region 变量
        public Dictionary<string, KNXSelectedAddress> SelectedAddress { get; set; }
        private List<MgGroupAddress> MgAddressList = new List<MgGroupAddress>();
        public AddressType PickType { get; set; }

        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }

        private bool _search = false;
        private bool _multiSelect = false;
        public bool NeedActions;
        #endregion

        #region 构造函数
        public FrmGroupAddressPick()
        {
            InitializeComponent();

            this.cbbFilterType.Items.Insert((int)FilterType.Name, UIResMang.GetString("Name"));
            this.cbbFilterType.Items.Insert((int)FilterType.GroupAddress, UIResMang.GetString("GroupAddress"));
            this.cbbFilterType.SelectedIndex = (int)FilterType.Name;

            SelectedAddress = new Dictionary<string, KNXSelectedAddress>();
        }
        #endregion

        #region 控件事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            SetSelectIDs();

            if (SelectedAddress.Count > 1 && MultiSelect == false)
            {
                MessageBox.Show(UIResMang.GetString("Message21"));
                this.SelectedAddress.Clear();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void SetSelectIDs()
        {
            //if (_search == false)
            //{
            //    // 清空之前选择的
                SelectedAddress.Clear();
            //}

            int count = dgvData.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                var groupAddress = dgvData.Rows[i].DataBoundItem as MgGroupAddress;
                if (groupAddress != null)
                {
                    if (groupAddress.IsSelected == true)
                    {
                        SelectedAddress[groupAddress.Id] = new KNXSelectedAddress()
                        {
                            Id = groupAddress.Id,
                            Name = groupAddress.Name,
                            //Type = (int)Enum.Parse(typeof(KNXDataType), groupAddress.Type),
                            Type = (int)groupAddress.Type,
                            DefaultValue = groupAddress.DefaultValue
                        };
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 窗体事件
        private void FrmGroupAddressPick_Load(object sender, EventArgs e)
        {
            foreach (EdGroupAddress address in MyCache.GroupAddressTable)
            {
                var temp = new MgGroupAddress(address);
                this.MgAddressList.Add(temp);

                if ((null != SelectedAddress) && (SelectedAddress.ContainsKey(address.Id)))
                {
                    temp.IsSelected = true;
                    temp.DefaultValue = SelectedAddress[address.Id].DefaultValue;
                }
            }

            LoadAllAddress();
        }
        #endregion

        #region 私有方法
        private void LoadAllAddress()
        {
            _search = false;

            var data = new List<MgGroupAddress>();
            var sl = new List<MgGroupAddress>();

            foreach (var it in this.MgAddressList)
            {
                if (this.NeedActions && ((null == it.Actions) || (it.Actions.Actions.Count <= 0)))
                {
                    continue;
                }

                if (it.IsSelected)
                {
                    sl.Add(it);
                }
                else
                {
                    data.Add(it);
                }
            }

            RefreashTable(data, sl);
        }

        private void SearchAddress()
        {
            this.dgvData.EndEdit();

            if (_search)
            {
                int count = dgvData.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    var groupAddress = dgvData.Rows[i].DataBoundItem as MgGroupAddress;
                    if (groupAddress != null)
                    {
                        foreach (var item in this.MgAddressList)
                        {
                            if (item.Id == groupAddress.Id)
                            {
                                item.IsSelected = groupAddress.IsSelected;
                                break;
                            }
                        }
                    }
                }
            }

            _search = true;

            string searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var data = new List<MgGroupAddress>();
                var sl = new List<MgGroupAddress>();
                var filterAddress = new List<MgGroupAddress>();
                if ((int)FilterType.Name == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in this.MgAddressList where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }
                else if ((int)FilterType.GroupAddress == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in this.MgAddressList where i.KnxAddress.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }

                foreach (var it in filterAddress)
                {
                    if (it.IsSelected)
                    {
                        sl.Add(it);
                    }
                    else
                    {
                        data.Add(it);
                    }
                }

                RefreashTable(data, sl);
            }
            else
            {
                LoadAllAddress();
            }
        }

        private void RefreashTable(List<MgGroupAddress> data, List<MgGroupAddress> Selected)
        {
            data = (from i in data orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();
            Selected = (from i in Selected orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();
            data.InsertRange(0, Selected);

            this.dgvData.DataSource = new BindingList<MgGroupAddress>(data);

            FormatGrid(dgvData);
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;

                var col = grid.Columns["IsSelected"];
                col.HeaderText = UIResMang.GetString("Selected");
                col.Width = 50;
                col.DisplayIndex = i++;

                col = grid.Columns["Id"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Name"];
                col.Width = 260;
                col.HeaderText = UIResMang.GetString("Name");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxAddress"];
                col.Width = 80;
                col.HeaderText = UIResMang.GetString("GroupAddress");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxMainNumber"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["KnxSubNumber"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Type"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["DPTName"];
                col.Width = 290;
                col.HeaderText = UIResMang.GetString("DatapointType");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsCommunication"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["IsRead"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["IsWrite"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["IsTransmit"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["IsUpgrade"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Priority"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = UIResMang.GetString("DefaultValue");
                col.DisplayIndex = i++;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = UIResMang.GetString("GroupAddressActions");
                col.DisplayIndex = i++;
                col.ReadOnly = true;
            }
        }
        #endregion

        #region Event 事件
        #region Button
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadAllAddress();
        }
        #endregion

        #region DataGridView
        private void dgvGroupAddress_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                this.dgvData.RowHeadersWidth - 4,
                                                e.RowBounds.Height);
            Color color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;
            if (((DataGridView)sender).Rows[e.RowIndex].Selected)
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.SelectionForeColor;
            else
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;

            TextRenderer.DrawText(e.Graphics,
                                    (e.RowIndex + 1).ToString(),
                                    this.dgvData.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    color,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.MultiSelect == false)
            {
                if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
                //
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)dgvData.Rows[i].Cells[0];
                    checkcell.Value = false;
                }
                //
                DataGridViewCheckBoxCell selectCell = (DataGridViewCheckBoxCell)dgvData.Rows[e.RowIndex].Cells[0];
                selectCell.Value = true;
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex >= 0)
            {
                if (15 == colIndex)
                {
                    string Id = this.dgvData.Rows[rowIndex].Cells["Id"].Value as string;
                    EdGroupAddress addr = MgGroupAddress.GetGroupAddress(this.MgAddressList, Id);
                    if ((null != addr) && (null != addr.Actions))
                    {
                        //画单元格的边界线
                        Point p1 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top);
                        Point p2 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top + e.CellBounds.Height);
                        Point p3 = new Point(e.CellBounds.Left, e.CellBounds.Top + e.CellBounds.Height);
                        Point[] ps = new Point[] { p1, p2, p3 };
                        using (Brush gridBrush = new SolidBrush(this.dgvData.GridColor))
                        {
                            using (Pen gridLinePen = new Pen(gridBrush, 2))
                            {
                                Font font = new Font("宋体", 9, FontStyle.Regular);//自定义字体
                                string actions = addr.Actions.ToString();

                                //判断当前行是否为选中行，如果为选中行，则要修改图片的背景色和文字的字体颜色
                                if (this.dgvData.CurrentRow.Index == e.RowIndex)
                                {
                                    using (Brush backColorBrush = new SolidBrush(Color.FromArgb(051, 153, 255)))
                                    {
                                        //以背景色填充单元格
                                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                                        e.Graphics.DrawLines(gridLinePen, ps);

                                        SizeF sizeText = e.Graphics.MeasureString(actions, font);
                                        e.Graphics.DrawString(actions, font, Brushes.White, new RectangleF(e.CellBounds.Left,
                                            e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width,
                                            sizeText.Height), StringFormat.GenericDefault);

                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                                    {
                                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                                        e.Graphics.DrawLines(gridLinePen, ps);

                                        SizeF sizeText = e.Graphics.MeasureString(actions, e.CellStyle.Font);
                                        e.Graphics.DrawString(actions, e.CellStyle.Font, Brushes.Blue, new RectangleF(e.CellBounds.Left,
                                            e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width,
                                            sizeText.Height), StringFormat.GenericDefault);

                                        e.Handled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ComboBox
        private void cbbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion
        #endregion
    }
}
