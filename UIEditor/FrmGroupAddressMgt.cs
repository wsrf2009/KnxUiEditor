using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NLog;
using UIEditor.Component;
using UIEditor.Controls;
using UIEditor.GroupAddress;
using Structure;
using Structure.ETS;
using System.Drawing;
using System.Threading;
using UIEditor.KNX.DatapointAction;

namespace UIEditor
{
    public partial class FrmGroupAddressMgt : Form
    {
        #region 变量
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private List<MgGroupAddress> addressList = new List<MgGroupAddress>();
        private bool IsSelectAll = false;
        private ComboBox cbbPriority = new ComboBox();
        private bool Changed { get; set; }
        #endregion

        #region 构造函数
        public FrmGroupAddressMgt()
        {
            InitializeComponent();

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cbbPriority.Items.Add(it);
            }
            this.cbbPriority.Visible = false;
            this.cbbPriority.SelectedIndexChanged += new System.EventHandler(this.cbbDataType_SelectedIndexChanged);
            this.dgvGroupAddress.Controls.Add(this.cbbPriority);
        }
        #endregion

        #region 用户方法
        private void SearchAddress()
        {
            //_search = true;

            string searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                addressList.Clear();

                var filterAddress = from i in MyCache.GroupAddressTable where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i;

                foreach (var it in filterAddress)
                {
                    var temp = new MgGroupAddress(it);

                    addressList.Add(temp);
                }

                refreshDataTable();
            }
        }

        private void LoadAllAddress()
        {
            addressList.Clear();

            foreach (var it in MyCache.GroupAddressTable)
            {
                var temp = new MgGroupAddress(it);

                addressList.Add(temp);
            }

            refreshDataTable();
        }

        /// <summary>
        /// 刷新组地址表 dgvGroupAddress
        /// </summary>
        private void refreshDataTable()
        {
            // 排序
            var data = addressList.ToList();

            // 排序
            var sortData = (from i in data orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();

            this.dgvGroupAddress.DataSource = new BindingList<MgGroupAddress>(sortData);

            FormatGrid(dgvGroupAddress);
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
                EdGroupAddress address = MyCache.GetGroupAddress(item.Id);
                if (null != address)
                {
                    FrmGroupAddress frm = new FrmGroupAddress(DataStatus.Modify);
                    frm.Address = address;
                    var dlgResult = frm.ShowDialog(this);

                    if (dlgResult == DialogResult.OK)
                    {
                        /* 刷新修改的行 */
                        MgGroupAddress disAddress = new MgGroupAddress(frm.Address);
                        this.dgvGroupAddress.Rows[rowIndex].Cells[1].Value = disAddress.Id;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[2].Value = disAddress.Name;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[3].Value = disAddress.KnxAddress;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[4].Value = disAddress.DPTName;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[5].Value = disAddress.IsCommunication;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[6].Value = disAddress.IsRead;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[7].Value = disAddress.IsWrite;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[8].Value = disAddress.IsTransmit;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[9].Value = disAddress.IsUpgrade;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[10].Value = disAddress.Priority;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[11].Value = disAddress.DefaultValue;
                        this.dgvGroupAddress.Rows[rowIndex].Cells[12].Value = disAddress.ReadTimeSpan;

                        this.dgvGroupAddress.EndEdit();

                        //Changed = true;
                    }
                }
            }
        }

        private void WriteToDatasource()
        {
            this.dgvGroupAddress.EndEdit();

            foreach (var item in addressList)
            {
                foreach (EdGroupAddress address in MyCache.GroupAddressTable)
                {
                    if (address.Id == item.Id)
                    {
                        address.Name = item.Name;
                        address.IsCommunication = item.IsCommunication;
                        address.IsRead = item.IsRead;
                        address.IsWrite = item.IsWrite;
                        address.IsTransmit = item.IsTransmit;
                        address.IsUpgrade = item.IsUpgrade;
                        address.Priority = item.Priority;
                        address.DefaultValue = item.DefaultValue;
                        address.ReadTimeSpan = item.ReadTimeSpan;

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 保存当前组地址数据，
        /// </summary>
        private void SaveAddressList()
        {
            Cursor = Cursors.WaitCursor;

            //WriteToDatasource();

            try
            {
                //// 保存缓存中的数据到 JSON 文件
                //GroupAddressStorage.Save();

                WriteToDatasource();

                var formName = typeof(FrmMain).Name;
                if (Application.OpenForms[formName] != null)
                {
                    var frm = Application.OpenForms[formName] as FrmMain;
                    if (frm != null)
                    {
                        frm.Saved = false;
                    }
                }

                Changed = false;
            }
            catch (Exception ex)
            {
                string errorMsg = ResourceMng.GetString("Message18");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void NewAddress()
        {
            var frm = new FrmGroupAddress(DataStatus.Add) { Address = new EdGroupAddress() };
            var dlgResult = frm.ShowDialog(this);

            if (dlgResult == DialogResult.OK)
            {
                // 当前地址了列表是否为空
                if (MyCache.GroupAddressTable != null)
                {
                    // 判断地址是否冲突
                    if (CheckUnique(MyCache.GroupAddressTable, frm.Address) == true)
                    {
                        MyCache.GroupAddressTable.Add(frm.Address);
                        //dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
                        LoadAllAddress();
                        //Changed = true;
                    }
                    else
                    {
                        MessageBox.Show(ResourceMng.GetString("Message19"), ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 添加组地址，检测唯一性
        /// </summary>
        /// <param name="addressList"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool CheckUnique(BindingList<EdGroupAddress> addressList, EdGroupAddress address)
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
            for (int i = this.dgvGroupAddress.Rows.Count - 1; i > -1; i--)
            {
                MgGroupAddress disAddress = this.dgvGroupAddress.Rows[i].DataBoundItem as MgGroupAddress;
                if (disAddress.IsSelected)
                {
                    addressList.Remove(disAddress);
                    this.dgvGroupAddress.Rows.RemoveAt(i);
                    foreach (EdGroupAddress address in MyCache.GroupAddressTable)
                    {
                        if (address.Id == disAddress.Id)
                        {
                            MyCache.GroupAddressTable.Remove(address);

                            this.Changed = true;

                            break;
                        }
                    }
                }
            }
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;

                var col = grid.Columns["IsSelected"];
                col.HeaderText = ResourceMng.GetString("Selected");
                col.Width = 60;
                col.DisplayIndex = i++;

                col = grid.Columns["Id"];
                col.Width = 5;
                col.Visible = false;
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Name"];
                col.Width = 160;
                col.HeaderText = ResourceMng.GetString("Name");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxAddress"];
                col.Width = 100;
                col.HeaderText = ResourceMng.GetString("GroupAddress");
                col.DefaultCellStyle.Format = "y";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["DPTName"];
                col.Width = 160;
                col.HeaderText = ResourceMng.GetString("DatapointType");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsCommunication"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Communication");
                col.DisplayIndex = i++;

                col = grid.Columns["IsRead"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Read");
                col.DisplayIndex = i++;

                col = grid.Columns["IsWrite"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Write");
                col.DisplayIndex = i++;

                col = grid.Columns["IsTransmit"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Transmit");
                col.DisplayIndex = i++;

                col = grid.Columns["IsUpgrade"];
                col.Width = 50;
                col.HeaderText = ResourceMng.GetString("Upgrade");
                col.DisplayIndex = i++;

                col = grid.Columns["Priority"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("Priority");
                col.DisplayIndex = i++;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("DefaultValue");
                col.DisplayIndex = i++;

                col = grid.Columns["ReadTimespan"];
                col.Width = 80;
                col.HeaderText = ResourceMng.GetString("IntervalTimeOfRead");
                col.DisplayIndex = i++;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = ResourceMng.GetString("GroupAddressActions");
                col.DisplayIndex = i++;
                col.ReadOnly = true;
            }
        }

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗口关闭时，提示用户保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMgrGroupAddresses_FormClosing(object sender, FormClosingEventArgs e)
        {
            //WriteToDatasource();

            // 当前数据有没有保存
            if (this.Changed)
            {
                var result = MessageBox.Show(ResourceMng.GetString("Message20"), ResourceMng.GetString("Message4"), MessageBoxButtons.YesNoCancel);

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

        #region 控件事件

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
            //WriteToDatasource();
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
                LoadAllAddress();
                this.Changed = true;
            }
        }

        private void FrmGroupAddressMgt_Load(object sender, EventArgs e)
        {
            LoadAllAddress();

            FormatGrid(dgvGroupAddress);

            //this.Changed = false;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtSearch.Text = "";
            //this.dgvGroupAddress.DataSource = new BindingList<GroupAddress>(MyCache.GroupAddressTable);
            LoadAllAddress();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAddress();
            }
        }
        #endregion

        #region DataGridView 事件

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
            Console.Write("\nrow index:" + e.RowIndex + "\tcolumn index:" + e.ColumnIndex);

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
                    if (1 == e.ColumnIndex)
                    {

                    }
                    else if (2 == e.ColumnIndex) // 名称
                    {

                    }
                    else if (3 == e.ColumnIndex) // 地址
                    {

                    }
                    else if (4 == e.ColumnIndex)
                    {

                    }
                    else if (5 == e.ColumnIndex) // 是否通讯
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
                    else if (6 == e.ColumnIndex) // 是否读
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
                    else if (7 == e.ColumnIndex) // 是否写
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
                    else if (8 == e.ColumnIndex) // 是否传输
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
                    else if (9 == e.ColumnIndex) // 是否更新
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
                    else if (10 == e.ColumnIndex) // 优先级
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
                    else if (11 == e.ColumnIndex) // 默认值
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
                    else if (12 == e.ColumnIndex) // 读取时间间隔
                    {
                        var frm = new FrmSetReadTimeSpan();
                        var result = frm.ShowDialog();
                        if (DialogResult.OK == result)
                        {
                            int value = frm.time;
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
                }
            }
        }

        private void dgvGroupAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex >= 0)
            {
                if (10 == colIndex)
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

                    if (11 == colIndex)
                    {
                        this.Changed = true;
                    }
                    else if (12 == colIndex)
                    {
                        this.Changed = true;
                    }
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
                    //弹出操作菜单
                    //cmsDgvGroupAddress.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void dgvGroupAddress_Scroll(object sender, ScrollEventArgs e)
        {
            //this.cbbDataType.Visible = false;
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_MouseWheel(object sender, MouseEventArgs e)
        {
            //this.cbbDataType.Visible = false;
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_MouseEnter(object sender, EventArgs e)
        {
            this.dgvGroupAddress.Focus();
        }

        private void dgvGroupAddress_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //this.cbbDataType.Visible = false;
            this.cbbPriority.Visible = false;
        }

        private void dgvGroupAddress_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex >= 0)
            {
                if (13 == colIndex)
                {
                    string Id = this.dgvGroupAddress.Rows[rowIndex].Cells["Id"].Value as string;
                    EdGroupAddress addr = MyCache.GetGroupAddress(Id);
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
                                string seperator = ";";
                                SizeF sizeSeperator = e.Graphics.MeasureString(seperator, font);
                                float startPos = .0f;

                                //判断当前行是否为选中行，如果为选中行，则要修改图片的背景色和文字的字体颜色
                                if ((null != this.dgvGroupAddress.CurrentRow) && (this.dgvGroupAddress.CurrentRow.Index == e.RowIndex))
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

        private void dgvGroupAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                switch (colIndex)
                {
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        this.Changed = true;
                        this.dgvGroupAddress.CurrentCell = this.dgvGroupAddress.Rows[rowIndex].Cells[0];
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        protected bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }

        private void cbbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgvGroupAddress.CurrentCell.Value = ((ComboBox)sender).Text;

            this.Changed = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
    }
}
