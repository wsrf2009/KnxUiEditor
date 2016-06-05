using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Structure;
using UIEditor.Component;
using UIEditor.GroupAddress;
using System.Drawing;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.Controls
{
    /// <summary>
    /// Respresents a standard dialog that can be used as a base for custom dialog forms.
    /// </summary>
    public partial class FrmGroupAddressPick : Form
    {
        public enum AddressType { Write, Read };

        #region 变量
        public Dictionary<string, KNXSelectedAddress> SelectedAddress { get; set; }

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

            SelectedAddress = new Dictionary<string, KNXSelectedAddress>();
        }

        #endregion

        #region 控件事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            SetSelectIDs();

            if (SelectedAddress.Count > 1 && MultiSelect == false)
            {
                MessageBox.Show(ResourceMng.GetString("Message21"));
                this.SelectedAddress.Clear();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void SetSelectIDs()
        {
            if (_search == false)
            {
                // 清空之前选择的
                SelectedAddress.Clear();
            }

            int count = dgvData.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                var groupAddress = dgvData.Rows[i].DataBoundItem as PcGroupAddress;
                if (groupAddress != null)
                {
                    if (groupAddress.Selected == true)
                    {
                        SelectedAddress[groupAddress.Id] = new KNXSelectedAddress()
                        {
                            Id = groupAddress.Id,
                            Name = groupAddress.Name,
                            Type = (int)Enum.Parse(typeof(KNXDataType), groupAddress.Type),
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
            LoadAllAddress();
        }

        private void LoadAllAddress()
        {
            _search = false;

            var data = new List<PcGroupAddress>();

            foreach (var it in MyCache.GroupAddressTable)
            {
                if (((AddressType.Read == this.PickType) && (it.IsCommunication && it.IsRead)) ||
                    ((AddressType.Write == this.PickType) && (it.IsCommunication && it.IsWrite)))
                {
                    if (this.NeedActions && ((null == it.Actions) || (it.Actions.Count <= 0)))
                    {
                        continue;
                    }

                    var temp = new PcGroupAddress(it);

                    if (SelectedAddress != null && SelectedAddress.ContainsKey(temp.Id))
                    {
                        temp.Selected = true;
                        temp.DefaultValue = SelectedAddress[temp.Id].DefaultValue;
                    }

                    data.Add(temp);
                }
            }

            RefreashTable(data);
        }

        private void SearchAddress()
        {
            _search = true;

            string searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var data = new List<PcGroupAddress>();

                var filterAddress = from i in MyCache.GroupAddressTable where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i;

                foreach (var it in filterAddress)
                {
                    var temp = new PcGroupAddress(it);

                    if (SelectedAddress != null && SelectedAddress.ContainsKey(temp.Id))
                    {
                        temp.Selected = true;
                    }

                    data.Add(temp);
                }

                RefreashTable(data);
            }
        }

        private void RefreashTable(List<PcGroupAddress> data)
        {
            var sort1 = (from i in data orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();
            // 排序
            var sort2 = (from i in sort1 orderby i.Selected descending, i.KnxAddress select i).ToList();

            this.dgvData.DataSource = new BindingList<PcGroupAddress>(sort2);

            FormatGrid(dgvData);
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;
                var col = grid.Columns["Selected"];
                col.HeaderText = ResourceMng.GetString("Selected");
                col.Width = 50;
                col.DisplayIndex = i++;

                col = grid.Columns["Id"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Name"];
                col.Width = 260;
                col.HeaderText = ResourceMng.GetString("Name");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxAddress"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("GroupAddress");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Type"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["DPTName"];
                col.Width = 260;
                col.HeaderText = ResourceMng.GetString("DatapointType");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsCommunication"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Communication");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsRead"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Read");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsWrite"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Write");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("DefaultValue");
                col.DisplayIndex = i++;

                col = grid.Columns["ReadTimespan"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("IntervalTimeOfRead");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = ResourceMng.GetString("GroupAddressActions");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

            }
        }

        #endregion

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
                if (11 == colIndex)
                {
                    string Id = this.dgvData.Rows[rowIndex].Cells["Id"].Value as string;
                    EdGroupAddress addr = MyCache.GetGroupAddress(Id);
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
                                string seperator = ";";
                                SizeF sizeSeperator = e.Graphics.MeasureString(seperator, font);
                                float startPos = .0f;

                                //判断当前行是否为选中行，如果为选中行，则要修改图片的背景色和文字的字体颜色
                                if (this.dgvData.CurrentRow.Index == e.RowIndex)
                                {
                                    using (Brush backColorBrush = new SolidBrush(Color.FromArgb(051, 153, 255)))
                                    {
                                        //以背景色填充单元格
                                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                                        e.Graphics.DrawLines(gridLinePen, ps);

                                        foreach (DatapointActionNode action in addr.Actions)
                                        {
                                            if (startPos >= e.CellBounds.Width)
                                            {
                                                break;
                                            }

                                            if (startPos > 0)
                                            {
                                                e.Graphics.DrawString(seperator, font, Brushes.White, new RectangleF(e.CellBounds.Left + startPos,
                                                    e.CellBounds.Top + (e.CellBounds.Height - sizeSeperator.Height) / 2, e.CellBounds.Width - startPos,
                                                    sizeSeperator.Height), StringFormat.GenericDefault);
                                                startPos += sizeSeperator.Width;
                                            }

                                            SizeF sizeText = e.Graphics.MeasureString(action.Name, font);
                                            e.Graphics.DrawString(action.Name, font, Brushes.White, new RectangleF(e.CellBounds.Left + startPos,
                                                e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width - startPos,
                                                sizeText.Height), StringFormat.GenericDefault);
                                            startPos += sizeText.Width;
                                        }

                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                                    {
                                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                                        e.Graphics.DrawLines(gridLinePen, ps);

                                        foreach (DatapointActionNode action in addr.Actions)
                                        {
                                            if (startPos >= e.CellBounds.Width)
                                            {
                                                break;
                                            }

                                            if (startPos > 0)
                                            {
                                                e.Graphics.DrawString(seperator, font, Brushes.Black, new RectangleF(e.CellBounds.Left + startPos,
                                                    e.CellBounds.Top + (e.CellBounds.Height - sizeSeperator.Height) / 2, e.CellBounds.Width - startPos,
                                                    sizeSeperator.Height), StringFormat.GenericDefault);
                                                startPos += sizeSeperator.Width;
                                            }

                                            if (startPos >= e.CellBounds.Width)
                                            {
                                                break;
                                            }

                                            SizeF sizeText = e.Graphics.MeasureString(action.Name, e.CellStyle.Font);
                                            e.Graphics.DrawString(action.Name, e.CellStyle.Font, Brushes.Blue, new RectangleF(e.CellBounds.Left + startPos,
                                                e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width - startPos,
                                                sizeText.Height), StringFormat.GenericDefault);
                                            startPos += sizeText.Width;
                                        }

                                        e.Handled = true;
                                    }
                                }
                            }
                        }


                    }
                }
            }
        }
    }
}
