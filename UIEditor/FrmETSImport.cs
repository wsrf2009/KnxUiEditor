using NLog;
using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.GroupAddress;
using UIEditor.KNX.DatapointType;

namespace UIEditor
{
    public partial class FrmETSImport : Form
    {
        private const string EtsFilter = "ETS project files (*.knxproj)|*.knxproj|All files (*.*)|*.*";
        private const string XmlFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        private const int Height_TreeView_DPT = 300;

        private FrmProgress importInd;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        //private List<ImGroupAddr> addressList = new List<ImGroupAddr>();
        private ComboBox cbbPriority = new ComboBox();
        private TreeView tvDPTName = new TreeView();
        private List<ImGroupAddr> listAddress;

        #region 窗体构造函数
        public FrmETSImport()
        {
            InitializeComponent();

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cbbPriority.Items.Add(it);
            }
            this.cbbPriority.Visible = false;
            this.cbbPriority.SelectedIndexChanged += new System.EventHandler(this.cbbDataType_SelectedIndexChanged);
            this.dataGridView.Controls.Add(this.cbbPriority);

            foreach (var it in MyCache.NodeTypes)
            {
                this.tvDPTName.Nodes.Add(it);
            }
            //this.tvDPTName.Height = 300;
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
            var data = listAddress.ToList();

            // 排序
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
                col.HeaderText = ResourceMng.GetString("Selected");
                col.Width = 50;
                col.DisplayIndex = i++;
                col.ReadOnly = false;

                col = grid.Columns["Name"];
                col.Width = 160;
                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

                col = grid.Columns["DPTNameIsDetermined"];
                col.Visible = false;
                col.DisplayIndex = i++;

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
                            this.backWorkerImport.RunWorkerAsync(ofd); // 运行 backgroundWorker 组件
                            importInd = new FrmProgress(this.backWorkerImport);
                            importInd.Text = string.Format(ResourceMng.GetString("TextIsImporting"), ofd.FileName);
                            importInd.ShowDialog(this);
                            importInd.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ResourceMng.GetString("Message17");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string errorMsg = ResourceMng.GetString("Message17");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }

        #region 按钮事件
        private void buttonImportETSProject_Click(object sender, EventArgs e)
        {
            this.dataGridView.Visible = true;

            ImportEtsProject();

            this.buttonImportETSProject.Visible = false;
            this.buttonImportETSAddressXML.Visible = false;

            this.buttonBack.Visible = true;
            this.buttonBack.Enabled = true;

            //this.buttonNext.Visible = true;
            //this.buttonNext.Enabled = true;

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
            this.buttonImportETSAddressXML.Visible = false;

            this.buttonBack.Visible = true;
            this.buttonBack.Enabled = true;

            //this.buttonNext.Visible = true;
            //this.buttonNext.Enabled = true;

            this.buttonFinish.Visible = true;
            this.buttonFinish.Enabled = true;

            this.buttonCancel.Visible = true;
            this.buttonCancel.Enabled = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.pictureBox.Image = null;
            this.label.Text = null;

            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Rows.Clear();
            this.dataGridView.Visible = false;

            this.buttonImportETSProject.Visible = true;
            this.buttonImportETSAddressXML.Visible = true;

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
            importInd.Text = ResourceMng.GetString("Importing");
            importInd.ShowDialog(this);
            importInd.Close();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void backWorkerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            OpenFileDialog ofd = e.Argument as OpenFileDialog;

            worker.ReportProgress(0, ResourceMng.GetString("TextIsCopying"));

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

            this.pictureBox.Image = ResourceMng.GetImage("Help_32x32");
            this.label.Text = ResourceMng.GetString("Message43");
        }

        private void backWorkerSave_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int len = this.dataGridView.RowCount;
            //float i = 0;
            for (int i = 0; i < len; i++)
            {
                float f = (float)i / (len-1);
                worker.ReportProgress((int)(f * 100));

                DataGridViewRow row = this.dataGridView.Rows[i];
                bool isSelected = (bool)row.Cells["IsSelected"].Value;
                if (isSelected)
                {
                    EdGroupAddress addr = new EdGroupAddress();
                    addr.Id = row.Cells["Id"].Value as string;
                    addr.Name = row.Cells["Name"].Value as string;
                    addr.KnxAddress = row.Cells["KnxAddress"].Value as string;
                    addr.DPTName = row.Cells["DPTName"].Value as string;
                    DatapointType dpt = DatapointType.GetTypeNode(addr.DPTName);
                    if (null != dpt)
                    {
                        addr.KnxMainNumber = dpt.KNXMainNumber;
                        addr.KnxSubNumber = dpt.KNXSubNumber;
                        addr.Type = dpt.Type;
                    }
                    addr.IsCommunication = (bool)row.Cells["IsCommunication"].Value;
                    addr.IsRead = (bool)row.Cells["IsRead"].Value;
                    addr.IsWrite = (bool)row.Cells["IsWrite"].Value;
                    addr.IsTransmit = (bool)row.Cells["IsTransmit"].Value;
                    addr.IsUpgrade = (bool)row.Cells["IsUpgrade"].Value;
                    addr.Priority = (KNXPriority)row.Cells["Priority"].Value;
                    addr.Actions = DatapointType.GetActionNodes(addr.DPTName);

                    MyCache.GroupAddressTable.Add(addr);
                }
            }
        }

        private void backWorkerSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.importInd.Close();
            //this.Close();
        }
        

        #region DataGridView 事件
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
            Rectangle rect = dataGridView.GetCellDisplayRectangle(dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex, false);

            if (4 == e.ColumnIndex)
            {
                this.dataGridView.Rows[e.RowIndex].Cells["DPTNameIsDetermined"].Value = true;

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
            else if (11 == e.ColumnIndex)
            {
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
                    image = ResourceMng.GetImage("Help_16x16");

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
                        if (dataGridView.CurrentRow.Index == e.RowIndex)
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

                                //写入字符串
                                //e.Graphics.DrawString(strFileName.ToString(), newFont, Brushes.White,
                                //    e.CellBounds.Left + textStartPos, e.CellBounds.Top + 5, StringFormat.GenericDefault);
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

                                //e.Graphics.DrawString(strFileName.ToString(), e.CellStyle.Font, Brushes.Black,
                                //    e.CellBounds.Left + textStartPos, e.CellBounds.Top + 5, StringFormat.GenericDefault);
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
                            MessageBox.Show(ResourceMng.GetString("Message42"), ResourceMng.GetString("Message4"), MessageBoxButtons.OK);
                            this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value = false;
                        }
                        else
                        {
                            string address = this.dataGridView.Rows[rowIndex].Cells["KnxAddress"].Value as string;
                            bool isExsit = MyCache.AddressIsExsit(address);
                            if (isExsit)
                            {
                                MessageBox.Show(string.Format(ResourceMng.GetString("Message44"), address), ResourceMng.GetString("Message4"), MessageBoxButtons.OK);
                                this.dataGridView.Rows[rowIndex].Cells["IsSelected"].Value = false;
                            }
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
                if (1 == colIndex)
                {
                    //this.dataGridView.Update();
                    //this.dataGridView.EndEdit();
                    //bool s = (bool)dataGridView.CurrentCell.Value;
                    //bool isSelected = (bool)this.dataGridView.Rows[rowIndex].Cells[1].EditedFormattedValue;
                    //string name = this.dataGridView.Rows[rowIndex].Cells["DPTName"].Value.ToString();
                    //if (isSelected && name.Length <= 0)
                    //{
                    //SendKeys.Send("{ENTER}");
                    //}
                    this.dataGridView.CurrentCell = this.dataGridView.Rows[rowIndex].Cells[2];
                }
            }
        }
        #endregion

        private void cbbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView.CurrentCell.Value = ((ComboBox)sender).Text;
        }

        private void tvDPTName_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DatapointType selectedNode = e.Node as DatapointType;
            this.dataGridView.CurrentCell.Value = selectedNode.Text;
            this.tvDPTName.Visible = false;
        }

    }
}
