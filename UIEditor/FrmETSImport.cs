using GroupAddress;
using KNX;
using KNX.DatapointType;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.GroupAddress;
using Utils;

namespace UIEditor
{
    public partial class FrmETSImport : Form
    {
        #region 常量
        private const string EtsFilter = "ETS project files (*.knxproj)|*.knxproj|All files (*.*)|*.*";
        private const string XmlFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        private const string OpcFilter = "OPC files (*.esf)|*.esf|All files (*.*)|*.*";
        private const int Height_TreeView_DPT = 300;
        #endregion

        #region 枚举变量
        private enum FilterType
        {
            Name,
            GroupAddress
        }
        #endregion

        #region 变量
        private FrmProgress importInd;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private ComboBox cbbPriority = new ComboBox();
        private TreeView tvDPTName = new TreeView();
        private List<ImGroupAddr> listAddress;
        private bool IsSelectAll = false;
        #endregion

        #region 窗体构造函数
        public FrmETSImport()
        {
            InitializeComponent();

            this.cbbFilterType.Items.Insert((int)FilterType.Name, UIResMang.GetString("Name"));
            this.cbbFilterType.Items.Insert((int)FilterType.GroupAddress, UIResMang.GetString("GroupAddress"));
            this.cbbFilterType.SelectedIndex = (int)FilterType.Name;

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cbbPriority.Items.Add(it);
            }
            this.cbbPriority.Visible = false;
            this.cbbPriority.SelectedIndexChanged += new System.EventHandler(this.cbbPriority_SelectedIndexChanged);
            this.dataGridView.Controls.Add(this.cbbPriority);

            foreach (var it in MyCache.NodeTypes)
            {
                this.tvDPTName.Nodes.Add(it);
            }
            this.tvDPTName.Visible = false;
            this.tvDPTName.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvDPTName_NodeMouseDoubleClick);
            this.dataGridView.Controls.Add(this.tvDPTName);
        }
        #endregion

        #region 窗体事件
        private void FrmETSImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tvDPTName.Nodes.Clear();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 清空临时目录
        /// </summary>
        private static void ClearProjTempFolder()
        {
            if (Directory.Exists(MyCache.ProjTempFolder))
            {
                FileHelper.DeleteFolder(MyCache.ProjTempFolder);
            }
            Directory.CreateDirectory(MyCache.ProjTempFolder);
        }

        private void LoadAllAddress()
        {
            if (null == listAddress)
            {
                return;
            }

            var data = listAddress.ToList();


            RefreshDataTable(data);
        }

        private void SearchAddress()
        {
            if (null == listAddress)
            {
                return;
            }

            string searchText = this.tbFilterText.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filterAddress = new List<ImGroupAddr>();
                if ((int)FilterType.Name == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in listAddress where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }
                else if ((int)FilterType.GroupAddress == this.cbbFilterType.SelectedIndex)
                {
                    filterAddress = (from i in listAddress where i.KnxAddress.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i).ToList();
                }

                RefreshDataTable(filterAddress);
            }
            else
            {
                LoadAllAddress();
            }
        }

        private void RefreshDataTable(List<ImGroupAddr> data)
        {
            var sortData = (from i in data orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();
            this.dataGridView.DataSource = new BindingList<ImGroupAddr>(sortData);

            FormatGrid(this.dataGridView);
        }

        private void FormatGrid(DataGridView grid)
        {
            grid.ColumnHeadersVisible = true;
            if (grid.Columns.Count > 1)
            {
                int i = 0;

                var col = grid.Columns["Id"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["IsSelected"];
                col.HeaderText = UIResMang.GetString("Selected");
                col.Width = 50;
                col.DisplayIndex = i++;
                col.ReadOnly = false;

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

                col = grid.Columns["DPTName"];
                col.Width = 290;
                col.HeaderText = UIResMang.GetString("DatapointType");
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["DPTNameIsDetermined"];
                col.Visible = false;
                col.DisplayIndex = i++;

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
                col.ReadOnly = true;
            }
        }

        /// <summary>
        /// 导入ETS项目文件中的 groupaddress
        /// </summary>
        private void ImportEtsProject()
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = EtsFilter;
                    ofd.FilterIndex = 1;
                    ofd.DefaultExt = "knxproj";
                    ofd.RestoreDirectory = true;

                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        ClearProjTempFolder();

                        if (Directory.Exists(MyCache.ProjTempFolder))
                        {
                            this.backWorkerImportEtsProject.RunWorkerAsync(ofd); // 运行 backgroundWorker 组件
                            importInd = new FrmProgress(this.backWorkerImportEtsProject);
                            importInd.Text = string.Format(UIResMang.GetString("TextIsImporting"), ofd.FileName);
                            importInd.ShowDialog(this);
                            importInd.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = UIResMang.GetString("Message17");
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }

        /// <summary>
        /// 导入 ETS 项目导出的地址文件 xml 格式
        /// </summary>
        private void ImportEtsAddressXml()
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofd.Filter = XmlFilter;
                    ofd.FilterIndex = 1;
                    ofd.DefaultExt = "xml";
                    ofd.RestoreDirectory = true;

                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        ClearProjTempFolder();

                        // 存放ETS文件，解压并解析xml
                        if (ofd.SafeFileName != null)
                        {
                            string etsXmlFile = Path.Combine(MyCache.ProjTempFolder, ofd.SafeFileName);
                            File.Copy(ofd.FileName, etsXmlFile);
                            // 导入的地址表
                            var importAddress = ETSImport.ParseGroupAddressXml(etsXmlFile);

                            //Merge(importAddress);
                        }

                        //this.dgvGroupAddress.DataSource = new BindingList<GroupAddress>(MyCache.GroupAddressTable);
                        LoadAllAddress();

                        //_saved = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = UIResMang.GetString("Message17");
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }

        private void ImportOPC()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = OpcFilter;
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "esf";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    this.backgroundWorkerImportOPC.RunWorkerAsync(ofd);
                    importInd = new FrmProgress(this.backgroundWorkerImportOPC);
                    importInd.Text = string.Format(UIResMang.GetString("TextIsImporting"), ofd.FileName);
                    importInd.ShowDialog(this);
                    importInd.Close();
                }
            }
        }

        private ImGroupAddr GetGroupAddress(string id)
        {
            ImGroupAddr address = null;

            foreach (ImGroupAddr addr in listAddress)
            {
                if (id == addr.Id)
                {
                    address = addr;
                    break;
                }
            }

            return address;
        }
        #endregion

        #region Event 事件
        #region 按钮
        private void buttonImportETSProject_Click(object sender, EventArgs e)
        {
            this.dataGridView.Visible = true;

            ImportEtsProject();

            this.buttonImportETSProject.Visible = false;
            this.buttonImportOPC.Visible = false;

            this.buttonBack.Visible = true;
            this.buttonBack.Enabled = true;

            this.buttonFinish.Visible = true;
            this.buttonFinish.Enabled = true;

            this.buttonCancel.Visible = true;
            this.buttonCancel.Enabled = true;
        }

        private void buttonImportETSAddressXML_Click(object sender, EventArgs e)
        {
            this.dataGridView.Visible = true;

            ImportEtsAddressXml();

            this.buttonImportETSProject.Visible = false;
            this.buttonImportOPC.Visible = false;

            this.buttonBack.Visible = true;
            this.buttonBack.Enabled = true;

            //this.buttonNext.Visible = true;
            //this.buttonNext.Enabled = true;

            this.buttonFinish.Visible = true;
            this.buttonFinish.Enabled = true;

            this.buttonCancel.Visible = true;
            this.buttonCancel.Enabled = true;
        }

        private void buttonImportOPC_Click(object sender, EventArgs e)
        {
            try
            {
                ImportOPC();

                this.dataGridView.Visible = true;

                this.buttonImportETSProject.Visible = false;
                this.buttonImportOPC.Visible = false;

                this.buttonBack.Visible = true;
                this.buttonBack.Enabled = true;

                this.buttonFinish.Visible = true;
                this.buttonFinish.Enabled = true;

                this.buttonCancel.Visible = true;
                this.buttonCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.pictureBox.Image = null;
            this.label.Text = null;

            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Rows.Clear();
            this.dataGridView.Visible = false;

            this.buttonImportETSProject.Visible = true;
            this.buttonImportOPC.Visible = true;

            this.buttonBack.Visible = false;

            //this.buttonNext.Visible = false;

            this.buttonFinish.Visible = false;

            this.buttonCancel.Visible = false;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            this.dataGridView.EndEdit();

            this.backWorkerSave.RunWorkerAsync();
            importInd = new FrmProgress(this.backWorkerSave);
            importInd.Text = UIResMang.GetString("Importing");
            importInd.ShowDialog(this);
            importInd.Close();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion

        #region BackgroundWorker
        private void backWorkerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            OpenFileDialog ofd = e.Argument as OpenFileDialog;

            worker.ReportProgress(0, UIResMang.GetString("TextIsCopying"));

            // 存放ETS文件，解压并解析xml
            string etsProjectFile = Path.Combine(MyCache.ProjTempFolder, ofd.SafeFileName);
            File.Copy(ofd.FileName, etsProjectFile);

            // 导入的地址表
            this.listAddress = ETSImport.ParseEtsProjectFile(etsProjectFile, worker);
        }

        private void backWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadAllAddress();

            this.importInd.Close();

            this.pictureBox.Image = UIResMang.GetImage("Help_32x32");
            this.label.Text = UIResMang.GetString("Message43");
        }

        private void backgroundWorkerImportOPC_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            OpenFileDialog ofd = e.Argument as OpenFileDialog;
            string opcFile = ofd.FileName;

            this.listAddress = ETSImport.ParseOpcFile(opcFile, worker);
        }

        private void backgroundWorkerImportOPC_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadAllAddress();

            this.importInd.Close();

            this.pictureBox.Image = UIResMang.GetImage("Help_32x32");
            this.label.Text = UIResMang.GetString("Message43");
        }

        private void backWorkerSave_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //int len = this.dataGridView.RowCount;

            //for (int i = 0; i < len; i++)
            int len = listAddress.Count;
            for (int i = 0; i < len; i++)
            {
                float f = (float)i / (len - 1);
                worker.ReportProgress((int)(f * 100));

                //DataGridViewRow row = this.dataGridView.Rows[i];
                //bool isSelected = (bool)row.Cells["IsSelected"].Value;
                //if (isSelected)
                //{
                //    EdGroupAddress addr = new EdGroupAddress();
                //    addr.Id = row.Cells["Id"].Value as string;
                //    addr.Name = row.Cells["Name"].Value as string;
                //    addr.KnxAddress = row.Cells["KnxAddress"].Value as string;
                //    addr.DPTName = row.Cells["DPTName"].Value as string;
                //    DatapointType dpt = DPTHelper.GetTypeNode(addr.DPTName);
                //    if (null != dpt)
                //    {
                //        addr.KnxMainNumber = dpt.KNXMainNumber;
                //        addr.KnxSubNumber = dpt.KNXSubNumber;
                //        addr.Type = dpt.Type;
                //    }
                //    addr.IsCommunication = (bool)row.Cells["IsCommunication"].Value;
                //    addr.IsRead = (bool)row.Cells["IsRead"].Value;
                //    addr.IsWrite = (bool)row.Cells["IsWrite"].Value;
                //    addr.IsTransmit = (bool)row.Cells["IsTransmit"].Value;
                //    addr.IsUpgrade = (bool)row.Cells["IsUpgrade"].Value;
                //    addr.Priority = (KNXPriority)row.Cells["Priority"].Value;
                //    addr.Actions = DPTHelper.GetActionNodes(addr.DPTName);

                //    MyCache.GroupAddressTable.Add(addr);
                //}

                ImGroupAddr imAddr = listAddress[i];
                if (imAddr.IsSelected)
                {
                    MgGroupAddress addr = new MgGroupAddress();
                    addr.Id = imAddr.Id;
                    addr.Name = imAddr.Name;
                    addr.KnxAddress = imAddr.KnxAddress;
                    addr.DPTName = imAddr.DPTName;
                    DatapointType dpt = DPTHelper.GetTypeNode(imAddr.DPTName);
                    if (null != dpt)
                    {
                        addr.KnxMainNumber = dpt.KNXMainNumber;
                        addr.KnxSubNumber = dpt.KNXSubNumber;
                        addr.Type = dpt.Type;
                    }
                    addr.IsCommunication = imAddr.IsCommunication;
                    addr.IsRead = imAddr.IsRead;
                    addr.IsWrite = imAddr.IsWrite;
                    addr.IsTransmit = imAddr.IsTransmit;
                    addr.IsUpgrade = imAddr.IsUpgrade;
                    addr.Priority = imAddr.Priority;
                    //addr.Actions = DPTHelper.GetActionNodes(addr.DPTName);
                    addr.Actions = new GroupAddressActions(DPTHelper.GetActionNodes(addr.DPTName));

                    //MyCache.GroupAddressTable.Add(addr);
                    var formName = typeof(FrmGroupAddressMgt).Name;
                    if (Application.OpenForms[formName] != null)
                    {
                        var frm = Application.OpenForms[formName] as FrmGroupAddressMgt;
                        if (frm != null)
                        {
                            frm.AddMgAddress(addr);
                        }
                    }
                }
            }
        }

        private void backWorkerSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.importInd.Close();
            //this.Close();
        }
        #endregion

        #region DataGridView
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                this.dataGridView.RowHeadersWidth - 4,
                                                e.RowBounds.Height);
            Color color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;
            if (((DataGridView)sender).Rows[e.RowIndex].Selected)
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.SelectionForeColor;
            else
                color = ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor;

            TextRenderer.DrawText(e.Graphics,
                                    (e.RowIndex + 1).ToString(),
                                    this.dataGridView.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    color,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            if (rowIndex < 0)
            {
                return;
            }

            if (1 == e.ColumnIndex)
            {
                string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value;
                ImGroupAddr address = GetGroupAddress(id);
                if (null != address)
                {
                    address.IsSelected = !isSelected;
                }
            }
            else
                if ((null != dataGridView.CurrentCell) && (4 == e.ColumnIndex))
                {
                    this.dataGridView.Rows[rowIndex].Cells["DPTNameIsDetermined"].Value = true;
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex, false);
                    string text = dataGridView.CurrentCell.Value.ToString();

                    if (this.tvDPTName.Visible)
                    {
                        this.tvDPTName.Visible = false;
                    }
                    else
                    {
                        this.tvDPTName.Height = Height_TreeView_DPT;
                        if ((rect.Bottom + this.tvDPTName.Height) > this.dataGridView.Bottom)
                        {
                            if (rect.Top <= this.tvDPTName.Height)
                            {
                                this.tvDPTName.Top = rect.Bottom;
                                this.tvDPTName.Height = this.dataGridView.Bottom - rect.Bottom;
                            }
                            else
                            {
                                this.tvDPTName.Top = rect.Top - this.tvDPTName.Height;
                            }
                        }
                        else
                        {
                            this.tvDPTName.Top = rect.Bottom;
                        }

                        this.tvDPTName.Left = rect.Left;
                        this.tvDPTName.Width = rect.Width;

                        TreeViewHelper.SelectNode2Level(this.tvDPTName, text);
                    }

                    this.cbbPriority.Visible = false;
                }
                else if (6 == e.ColumnIndex)
                {
                    string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsCommunication"].Value;
                    ImGroupAddr address = GetGroupAddress(id);
                    if (null != address)
                    {
                        address.IsCommunication = !isSelected;
                    }
                }
                else if (7 == e.ColumnIndex)
                {
                    string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsRead"].Value;
                    ImGroupAddr address = GetGroupAddress(id);
                    if (null != address)
                    {
                        address.IsRead = !isSelected;
                    }
                }
                else if (8 == e.ColumnIndex)
                {
                    string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsWrite"].Value;
                    ImGroupAddr address = GetGroupAddress(id);
                    if (null != address)
                    {
                        address.IsWrite = !isSelected;
                    }
                }
                else if (9 == e.ColumnIndex)
                {
                    string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsTransmit"].Value;
                    ImGroupAddr address = GetGroupAddress(id);
                    if (null != address)
                    {
                        address.IsTransmit = !isSelected;
                    }
                }
                else if (10 == e.ColumnIndex)
                {
                    string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsUpgrade"].Value;
                    ImGroupAddr address = GetGroupAddress(id);
                    if (null != address)
                    {
                        address.IsUpgrade = !isSelected;
                    }
                }
                else if ((null != dataGridView.CurrentCell) && (11 == e.ColumnIndex))
                {
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex, false);
                    this.cbbPriority.Text = dataGridView.CurrentCell.Value.ToString();
                    this.cbbPriority.Left = rect.Left;
                    this.cbbPriority.Top = rect.Top;
                    this.cbbPriority.Width = rect.Width;
                    this.cbbPriority.Height = rect.Height;

                    this.tvDPTName.Visible = false;
                    this.cbbPriority.Visible = true;
                }
                else
                {
                    this.cbbPriority.Visible = false;
                    this.tvDPTName.Visible = false;
                }
        }

        private void dataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            this.cbbPriority.Visible = false;
            this.tvDPTName.Visible = false;
        }

        private void dataGridView_MouseWheel(object sender, MouseEventArgs e)
        {
            this.cbbPriority.Visible = false;
            this.tvDPTName.Visible = false;
        }

        private void dataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cbbPriority.Visible = false;
            this.tvDPTName.Visible = false;
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && 4 == e.ColumnIndex)
            {
                bool isSelected = (bool)this.dataGridView.Rows[e.RowIndex].Cells["IsSelected"].Value;
                bool isDetermined = (bool)this.dataGridView.Rows[e.RowIndex].Cells["DPTNameIsDetermined"].Value;
                string strFileName = this.dataGridView.Rows[e.RowIndex].Cells["DPTName"].Value.ToString();
                int textStartPos = 2;
                Image image = null;
                Rectangle imgRect = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 2, e.CellBounds.Height - 6,
                        e.CellBounds.Height - 6);
                if (isSelected && !isDetermined)
                {
                    image = UIResMang.GetImage("Help_16x16");

                    textStartPos += e.CellBounds.Height - 6;
                }

                //画单元格的边界线
                Point p1 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top);
                Point p2 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top + e.CellBounds.Height);
                Point p3 = new Point(e.CellBounds.Left, e.CellBounds.Top + e.CellBounds.Height);
                Point[] ps = new Point[] { p1, p2, p3 };
                using (Brush gridBrush = new SolidBrush(this.dataGridView.GridColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush, 2))
                    {
                        Font newFont = new Font("宋体", 9, FontStyle.Regular);//自定义字体
                        //判断当前行是否为选中行，如果为选中行，则要修改图片的背景色和文字的字体颜色
                        if ((null != dataGridView.CurrentRow) && (dataGridView.CurrentRow.Index == e.RowIndex))
                        {
                            using (Brush backColorBrush = new SolidBrush(Color.FromArgb(051, 153, 255)))
                            {
                                //以背景色填充单元格
                                e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                                e.Graphics.DrawLines(gridLinePen, ps);

                                if (isSelected && !isDetermined)
                                {
                                    e.Graphics.DrawImage(image, imgRect);
                                }

                                SizeF sizeText = e.Graphics.MeasureString(strFileName, newFont);
                                e.Graphics.DrawString(strFileName, newFont, Brushes.White, new RectangleF(e.CellBounds.Left + textStartPos,
                                    e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width - textStartPos,
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

                                if (isSelected && !isDetermined)
                                {
                                    e.Graphics.DrawImage(image, imgRect);
                                }

                                SizeF sizeText = e.Graphics.MeasureString(strFileName, e.CellStyle.Font);
                                e.Graphics.DrawString(strFileName, e.CellStyle.Font, Brushes.Black, new RectangleF(e.CellBounds.Left + textStartPos,
                                    e.CellBounds.Top + (e.CellBounds.Height - sizeText.Height) / 2, e.CellBounds.Width - textStartPos,
                                    sizeText.Height), StringFormat.GenericDefault);

                                e.Handled = true;
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                if (1 == colIndex)
                {
                    bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells["IsSelected"].EditedFormattedValue;
                    string name = this.dataGridView.Rows[rowIndex].Cells["DPTName"].Value.ToString();
                    if (isSelected)
                    {
                        if (name.Length <= 0)
                        {
                            MessageBox.Show(UIResMang.GetString("Message42"), UIResMang.GetString("Message4"), MessageBoxButtons.OK);
                            this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value = false;
                        }
                        else
                        {
                            string address = this.dataGridView.Rows[rowIndex].Cells["KnxAddress"].Value as string;
                            //bool isExsit = ProjResManager.AddressIsExsit(address);
                            var formName = typeof(FrmGroupAddressMgt).Name;
                            if (Application.OpenForms[formName] != null)
                            {
                                var frm = Application.OpenForms[formName] as FrmGroupAddressMgt;
                                if (frm != null)
                                {
                                    bool isExsit = frm.AddressIsExsit(address);
                                    if (isExsit)
                                    {
                                        MessageBox.Show(string.Format(UIResMang.GetString("Message44"), address), UIResMang.GetString("Message4"), MessageBoxButtons.OK);
                                        this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value = false;
                                    }
                                }
                            }
                            //if (isExsit)
                            //{
                            //    MessageBox.Show(string.Format(UIResMang.GetString("Message44"), address), UIResMang.GetString("Message4"), MessageBoxButtons.OK);
                            //    this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value = false;
                            //}
                        }
                    }
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                if ((1 == colIndex) && (null != this.dataGridView.CurrentCell))
                {
                    this.dataGridView.CurrentCell = this.dataGridView.Rows[rowIndex].Cells[2];
                }
            }
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (this.IsSelectAll)
                {
                    this.IsSelectAll = false;

                    this.dataGridView.CurrentCell = null;

                    for (int i = 0; i < this.dataGridView.RowCount; i++)
                    {
                        this.dataGridView.Rows[i].Cells["IsSelected"].Value = false;
                    }
                }
                else
                {
                    this.IsSelectAll = true;

                    this.dataGridView.CurrentCell = null;

                    for (int i = 0; i < this.dataGridView.RowCount; i++)
                    {
                        string address = this.dataGridView.Rows[i].Cells["KnxAddress"].Value as string;
                        //bool isExsit = ProjResManager.AddressIsExsit(address);
                        //if (!isExsit)
                        //{
                        //    this.dataGridView.Rows[i].Cells["IsSelected"].Value = true;
                        //}
                        var formName = typeof(FrmGroupAddressMgt).Name;
                        if (Application.OpenForms[formName] != null)
                        {
                            var frm = Application.OpenForms[formName] as FrmGroupAddressMgt;
                            if (frm != null)
                            {
                                bool isExsit = frm.AddressIsExsit(address);
                                if (!isExsit)
                                {
                                    this.dataGridView.Rows[i].Cells["IsSelected"].Value = true;
                                }
                            }
                        }
                    }
                }

                //this.dataGridView.CurrentCell = null;
                //this.dataGridView.EndEdit();
            }
        }
        #endregion

        #region ComboBox
        private void cbbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (null != this.dataGridView.CurrentCell)
            //{
            this.dataGridView.CurrentCell.Value = ((ComboBox)sender).Text;

            int rowIndex = this.dataGridView.CurrentCell.RowIndex;
            string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
            ImGroupAddr address = GetGroupAddress(id);
            if (null != address)
            {
                address.Priority = (KNXPriority)this.dataGridView.CurrentCell.Value;
            }
            //}
        }

        private void cbbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion

        #region TreeView
        private void tvDPTName_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DatapointType selectedNode = e.Node as DatapointType;
            this.dataGridView.CurrentCell.Value = selectedNode.Text;
            this.tvDPTName.Visible = false;

            int rowIndex = this.dataGridView.CurrentCell.RowIndex;
            string id = (string)this.dataGridView.Rows[rowIndex].Cells["Id"].Value;
            ImGroupAddr address = GetGroupAddress(id);
            if (null != address)
            {
                address.DPTName = selectedNode.Text;
            }
        }
        #endregion

        #region TextBox
        private void tbFilterText_TextChanged(object sender, EventArgs e)
        {
            SearchAddress();
        }
        #endregion
        #endregion
    }
}
