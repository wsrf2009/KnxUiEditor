using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Structure;
using System.Drawing;
using System.Threading;
using GroupAddress;
using NLog;
using KNX;
using UIEditor;
using KNX.DatapointAction;
using UIEditor.Component;
using Utils;

namespace UIEditor
{
    public partial class FrmGroupAddressMgt : Form
    {
        #region 枚举变量
        private enum FilterType
        {
            Name,
            GroupAddress
        }
        #endregion

        #region 变量
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private List<MgGroupAddress> MgAddressList = new List<MgGroupAddress>();
        private bool IsSelectAll = false;
        private ComboBox cbbPriority = new ComboBox();
        private bool Changed { get; set; }
        #endregion

        #region 构造函数
        public FrmGroupAddressMgt()
        {
            InitializeComponent();

            foreach (EdGroupAddress address in MyCache.GroupAddressTable)
            {
                var temp = new MgGroupAddress(address);
                this.MgAddressList.Add(temp);
            }

            this.cbbFilterType.Items.Insert((int)FilterType.Name, UIResMang.GetString("Name"));
            this.cbbFilterType.Items.Insert((int)FilterType.GroupAddress, UIResMang.GetString("GroupAddress"));
            this.cbbFilterType.SelectedIndex = (int)FilterType.Name;

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cbbPriority.Items.Add(it);
            }
            this.cbbPriority.Visible = false;
            this.cbbPriority.SelectedIndexChanged += new System.EventHandler(this.cbbDataType_SelectedIndexChanged);
            this.dgvGroupAddress.Controls.Add(this.cbbPriority);
        }
        #endregion

        #region 窗体事件
        private void FrmGroupAddressMgt_Load(object sender, EventArgs e)
        {
            DisplayAllAdress();

            FormatGrid(dgvGroupAddress);
        }

        /// <summary>
        /// 窗口关闭时，提示用户保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMgrGroupAddresses_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 当前数据有没有保存
            if (this.Changed)
            {
                var result = MessageBox.Show(UIResMang.GetString("Message20"), UIResMang.GetString("Message4"), MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    SaveAddressList();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region 私有方法
        private void SearchAddress()
        {
            string searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filterAddress = new List<MgGroupAddress>();
                if ((int)FilterType.Name == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in this.MgAddressList where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }
                else if ((int)FilterType.GroupAddress == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in this.MgAddressList where i.KnxAddress.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }

                refreshDataTable(filterAddress);
            }
            else
            {
                DisplayAllAdress();
            }
        }

        private void DisplayAllAdress()
        {
            refreshDataTable(MgAddressList);
        }

        /// <summary>
        /// 刷新组地址表 dgvGroupAddress
        /// </summary>
        private void refreshDataTable(List<MgGroupAddress> data)
        {
            // 排序
            var sortData = (from i in data orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();

            this.dgvGroupAddress.DataSource = new BindingList<MgGroupAddress>(sortData);

            FormatGrid(dgvGroupAddress);
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;

                var col = grid.Columns["IsSelected"];
                col.HeaderText = UIResMang.GetString("Selected");
                col.Width = 60;
                col.DisplayIndex = i++;

                col = grid.Columns["Id"];
                col.Width = 5;
                col.Visible = false;
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Name"];
                col.Width = 160;
                col.HeaderText = UIResMang.GetString("Name");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxAddress"];
                col.Width = 100;
                col.HeaderText = UIResMang.GetString("GroupAddress");
                col.DefaultCellStyle.Format = "y";
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
                col.Width = 320;
                col.HeaderText = UIResMang.GetString("DatapointType");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsCommunication"];
                col.Width = 50;
                col.HeaderText = UIResMang.GetString("Communication");
                col.DisplayIndex = i++;

                col = grid.Columns["IsRead"];
                col.Width = 50;
                col.HeaderText = UIResMang.GetString("Read");
                col.DisplayIndex = i++;

                col = grid.Columns["IsWrite"];
                col.Width = 50;
                col.HeaderText = UIResMang.GetString("Write");
                col.DisplayIndex = i++;

                col = grid.Columns["IsTransmit"];
                col.Width = 50;
                col.HeaderText = UIResMang.GetString("Transmit");
                col.DisplayIndex = i++;

                col = grid.Columns["IsUpgrade"];
                col.Width = 50;
                col.HeaderText = UIResMang.GetString("Upgrade");
                col.DisplayIndex = i++;

                col = grid.Columns["Priority"];
                col.Width = 80;
                col.HeaderText = UIResMang.GetString("Priority");
                col.DisplayIndex = i++;

                col = grid.Columns["DefaultValue"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = UIResMang.GetString("GroupAddressActions");
                col.DisplayIndex = i++;
                col.ReadOnly = true;
            }
        }

        private void NewAddress()
        {
            var frm = new FrmGroupAddress(DataStatus.Add) { Address = new EdGroupAddress() };
            var dlgResult = frm.ShowDialog(this);

            if (dlgResult == DialogResult.OK)
            {
                // 判断地址是否冲突
                if (CheckUnique(this.MgAddressList, frm.Address) == true)
                {
                    this.MgAddressList.Add(new MgGroupAddress(frm.Address));
                    DisplayAllAdress();

                    this.Changed = true;
                }
                else
                {
                    MessageBox.Show(UIResMang.GetString("Message19"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 修改组地址信息
        /// </summary>
        private void ModifyAddress()
        {
            if (this.dgvGroupAddress.SelectedRows.Count > 0)
            {
                int rowIndex = this.dgvGroupAddress.SelectedRows[0].Cells[0].RowIndex;
                var item = this.dgvGroupAddress.SelectedRows[0].DataBoundItem as MgGroupAddress;
                if (null != item)
                {
                    FrmGroupAddress frm = new FrmGroupAddress(DataStatus.Modify);
                    frm.Address = item;
                    var dlgResult = frm.ShowDialog(this);

                    if (dlgResult == DialogResult.OK)
                    {
                        /* 刷新修改的行 */
                        DisplayAllAdress();

                        this.Changed = true;
                    }
                }
            }
        }

        private void WriteToDatasource()
        {
            this.dgvGroupAddress.EndEdit();
            MyCache.GroupAddressTable.Clear();

            foreach (var item in this.MgAddressList)
            {
                MyCache.GroupAddressTable.Add(item);
            }
        }

        /// <summary>
        /// 保存当前组地址数据，
        /// </summary>
        private void SaveAddressList()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                //// 保存缓存中的数据到 JSON 文件
                WriteToDatasource();

                var formName = typeof(FrmMain).Name;
                if (Application.OpenForms[formName] != null)
                {
                    var frm = Application.OpenForms[formName] as FrmMain;
                    if (frm != null)
                    {
                        frm.ProjectChanged();
                    }
                }

                this.Changed = false;
            }
            catch (Exception ex)
            {
                string errorMsg = UIResMang.GetString("Message18");
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 添加组地址，检测唯一性
        /// </summary>
        /// <param name="addressList"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool CheckUnique(List<MgGroupAddress> addressList, EdGroupAddress address)
        {
            if (addressList != null && addressList.Count > 0)
            {
                var item = from i in addressList
                           where i.KnxAddress == address.KnxAddress
                           select i;

                if (item.Any())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 删除选中行
        /// </summary>
        private void DeleteAddress()
        {
            var rList = new List<MgGroupAddress>();
            foreach (var item in this.MgAddressList)
            {
                if (item.IsSelected)
                {
                    rList.Add(item);
                }
            }

            foreach (var item in rList)
            {
                this.MgAddressList.Remove(item);

                this.Changed = true;
            }

            for (int i = this.dgvGroupAddress.Rows.Count - 1; i > -1; i--)
            {
                MgGroupAddress disAddress = this.dgvGroupAddress.Rows[i].DataBoundItem as MgGroupAddress;
                if (disAddress.IsSelected)
                {
                    this.dgvGroupAddress.Rows.RemoveAt(i);

                    this.Changed = true;
                }
            }
        }
        #endregion

        #region Event事件
        #region Button
        /// <summary>
        /// 添加组地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewAddress();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyAddress();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteAddress();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAddressList();
        }

        /// <summary>
        /// 导入ETS项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportETSProject_Click(object sender, EventArgs e)
        {
            DialogResult result = (new FrmETSImport()).ShowDialog(this);
            if (DialogResult.OK == result)
            {
                DisplayAllAdress();
                this.Changed = true;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void btnExportAddr_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "Text File(*.txt)|*.txt|All Files(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    if (null != sw)
                    {
                        sw.Write("名称" + "\t" + "地址" + "\t" + "类型(大小)" + "\r\n");
                        foreach (EdGroupAddress address in this.MgAddressList)
                        {
                            string addr = address.Name + "\t" + address.KnxAddress + "\t" + address.DPTName + "(" + KNXAddressHelper.GetSize(address.Type) + ")" + "\r\n";
                            sw.Write(addr);
                        }
                    }

                    sw.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Cursor.Current = Cursors.Default;
            }

            btn.Enabled = true;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAddress();
            }
        }
        #endregion

        #region DataGridView
        private void dgvGroupAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvGroupAddress.Focused == true && e.KeyCode == Keys.F2)
            {
                ModifyAddress();
            }
        }

        private void dgvGroupAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (0 <= e.RowIndex)
            {
                ModifyAddress();
            }
        }

        private void dgvGroupAddress_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (this.IsSelectAll)
                {
                    this.IsSelectAll = false;

                    this.dgvGroupAddress.CurrentCell = null;

                    for (int i = 0; i < this.dgvGroupAddress.RowCount; i++)
                    {
                        this.dgvGroupAddress.Rows[i].Cells["IsSelected"].Value = false;
                    }
                }
                else
                {
                    this.IsSelectAll = true;

                    this.dgvGroupAddress.CurrentCell = null;

                    for (int i = 0; i < this.dgvGroupAddress.RowCount; i++)
                    {
                        this.dgvGroupAddress.Rows[i].Cells["IsSelected"].Value = true;
                    }
                }
            }
            else
            {
                this.dgvGroupAddress.EndEdit();

                bool anySelected = false;
                bool communicationIsChecked = false;
                bool readIsChecked = false;
                bool writeIsChecked = false;
                bool transmitIsChecked = false;
                bool upgradeIsChecked = false;
                foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                {
                    anySelected = (bool)row.Cells["IsSelected"].Value;
                    communicationIsChecked = (bool)row.Cells["IsCommunication"].Value;
                    readIsChecked = (bool)row.Cells["IsRead"].Value;
                    writeIsChecked = (bool)row.Cells["IsWrite"].Value;
                    transmitIsChecked = (bool)row.Cells["IsTransmit"].Value;
                    upgradeIsChecked = (bool)row.Cells["IsUpgrade"].Value;
                    if (anySelected)
                    {
                        break;
                    }
                }

                if (anySelected)
                {
                    if (1 == e.ColumnIndex) // Id
                    {

                    }
                    else if (2 == e.ColumnIndex) // Name
                    {

                    }
                    else if (3 == e.ColumnIndex) // KnxAddress
                    {

                    }
                    else if (4 == e.ColumnIndex) // KnxMainNumber
                    {

                    }
                    else if (5 == e.ColumnIndex) // KnxSubNumber
                    {

                    }
                    else if (6 == e.ColumnIndex) // Type
                    {

                    }
                    else if (7 == e.ColumnIndex) // DPTName
                    {

                    }
                    else if (8 == e.ColumnIndex) // 是否通讯
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (communicationIsChecked)
                                {
                                    row.Cells[e.ColumnIndex].Value = false;
                                }
                                else
                                {
                                    row.Cells[e.ColumnIndex].Value = true;
                                }
                            }
                        }

                        Changed = true;
                    }
                    else if (9 == e.ColumnIndex) // 是否读
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (readIsChecked)
                                {
                                    row.Cells[e.ColumnIndex].Value = false;
                                }
                                else
                                {
                                    row.Cells[e.ColumnIndex].Value = true;
                                }
                            }
                        }

                        Changed = true;
                    }
                    else if (10 == e.ColumnIndex) // 是否写
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (writeIsChecked)
                                {
                                    row.Cells[e.ColumnIndex].Value = false;
                                }
                                else
                                {
                                    row.Cells[e.ColumnIndex].Value = true;
                                }
                            }
                        }

                        Changed = true;
                    }
                    else if (11 == e.ColumnIndex) // 是否传输
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (transmitIsChecked)
                                {
                                    row.Cells[e.ColumnIndex].Value = false;
                                }
                                else
                                {
                                    row.Cells[e.ColumnIndex].Value = true;
                                }
                            }
                        }

                        Changed = true;
                    }
                    else if (12 == e.ColumnIndex) // 是否更新
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (upgradeIsChecked)
                                {
                                    row.Cells[e.ColumnIndex].Value = false;
                                }
                                else
                                {
                                    row.Cells[e.ColumnIndex].Value = true;
                                }
                            }
                        }

                        Changed = true;
                    }
                    else if (13 == e.ColumnIndex) // 优先级
                    {
                        var frm = new FrmSetPriority();
                        var result = frm.ShowDialog();
                        if (DialogResult.OK == result)
                        {
                            KNXPriority priority = frm.priority;
                            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                            {
                                bool isSelected = (bool)row.Cells[0].Value;
                                if (isSelected)
                                {
                                    row.Cells[e.ColumnIndex].Value = priority;
                                }
                            }

                            Changed = true;
                        }
                    }
                    else if (14 == e.ColumnIndex) // 默认值
                    {
                        var frm = new FrmSetDefaultValue();
                        var result = frm.ShowDialog();
                        if (DialogResult.OK == result)
                        {
                            int value = frm.value;
                            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                            {
                                bool isSelected = (bool)row.Cells[0].Value;
                                if (isSelected)
                                {
                                    row.Cells[e.ColumnIndex].Value = value;
                                }
                            }

                            Changed = true;
                        }
                    }
                    else if (15 == e.ColumnIndex) // Actions
                    {

                    }
                }
            }
        }

        private void dgvGroupAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex < 0)
            {
                return;
            }

            if (13 == colIndex)
            {
                Rectangle rect = dgvGroupAddress.GetCellDisplayRectangle(dgvGroupAddress.CurrentCell.ColumnIndex, dgvGroupAddress.CurrentCell.RowIndex, false);
                this.cbbPriority.Text = dgvGroupAddress.CurrentCell.Value.ToString();
                this.cbbPriority.Left = rect.Left;
                this.cbbPriority.Top = rect.Top;
                this.cbbPriority.Width = rect.Width;
                this.cbbPriority.Height = rect.Height;

                this.cbbPriority.Visible = true;
            }
            else
            {
                this.cbbPriority.Visible = false;

                if (14 == colIndex)
                {
                    this.Changed = true;
                }
                else if (15 == colIndex)
                {
                    this.Changed = true;
                }
            }
        }

        private void dgvGroupAddress_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            /* 显示行号 */
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                this.dgvGroupAddress.RowHeadersWidth - 4,
                                                e.RowBounds.Height);
            Color color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;
            if (((DataGridView)sender).Rows[e.RowIndex].Selected)
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.SelectionForeColor;
            else
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;

            TextRenderer.DrawText(e.Graphics,
                                    (e.RowIndex + 1).ToString(),
                                    this.dgvGroupAddress.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    color,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvGroupAddress_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.dgvGroupAddress.Rows.Count != 0)
            {
                for (int i = 0; i < this.dgvGroupAddress.Rows.Count; i++)
                {
                    if (0 == (i % 2))
                    {
                        this.dgvGroupAddress.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
                    }
                    else
                    {
                        this.dgvGroupAddress.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Lavender;
                    }
                }
            }
        }

        private void dgvGroupAddress_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvGroupAddress.Rows[e.RowIndex].Selected == false)
                    {
                        dgvGroupAddress.ClearSelection();
                        dgvGroupAddress.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvGroupAddress.SelectedRows.Count == 1)
                    {
                        dgvGroupAddress.CurrentCell = dgvGroupAddress.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                }
            }
        }

        private void dgvGroupAddress_Scroll(object sender, ScrollEventArgs e)
        {
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_MouseWheel(object sender, MouseEventArgs e)
        {
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_MouseEnter(object sender, EventArgs e)
        {
            this.dgvGroupAddress.Focus();
        }

        private void dgvGroupAddress_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex >= 0)
            {
                if (15 == colIndex)
                {
                    string Id = this.dgvGroupAddress.Rows[rowIndex].Cells["Id"].Value as string;
                    EdGroupAddress addr = MgGroupAddress.GetGroupAddress(this.MgAddressList, Id);
                    if ((null != addr) && (null != addr.Actions))
                    {
                        //画单元格的边界线
                        Point p1 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top);
                        Point p2 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top + e.CellBounds.Height);
                        Point p3 = new Point(e.CellBounds.Left, e.CellBounds.Top + e.CellBounds.Height);
                        Point[] ps = new Point[] { p1, p2, p3 };
                        using (Brush gridBrush = new SolidBrush(this.dgvGroupAddress.GridColor))
                        {
                            using (Pen gridLinePen = new Pen(gridBrush, 2))
                            {
                                Font font = new Font("宋体", 9, FontStyle.Regular);//自定义字体
                                string actions = addr.Actions.ToString();

                                //判断当前行是否为选中行，如果为选中行，则要修改图片的背景色和文字的字体颜色
                                if ((null != this.dgvGroupAddress.CurrentRow) && (this.dgvGroupAddress.CurrentRow.Index == e.RowIndex))
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

        private void dgvGroupAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                switch (colIndex)
                {
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        this.Changed = true;
                        this.dgvGroupAddress.CurrentCell = this.dgvGroupAddress.Rows[rowIndex].Cells[0];
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        #region ComboBox
        private void cbbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvGroupAddress.CurrentCell.Value = ((ComboBox)sender).Text;

            this.Changed = true;
        }

        private void cbbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion

        #region TextView
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion
        #endregion

        #region 公共方法
        public bool AddressIsExsit(string addr)
        {
            bool isExsit = false;
            foreach (var address in this.MgAddressList)
            {
                if (address.KnxAddress == addr)
                {
                    isExsit = true;
                    break;
                }
            }

            return isExsit;
        }

        public void AddMgAddress(MgGroupAddress addr)
        {
            this.MgAddressList.Add(addr);
        }
        #endregion
    }
}
