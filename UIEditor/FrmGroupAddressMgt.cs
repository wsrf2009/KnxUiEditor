using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NLog;
using UIEditor.Component;
using UIEditor.Controls;
using UIEditor.ETS;
using Structure;
using Structure.ETS;
using System.Drawing;

namespace UIEditor
{
    public partial class FrmGroupAddressMgt : Form
    {
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private List<DisplayAddress> addressList = new List<DisplayAddress>();
        //private CheckBox cbSelectAll = new CheckBox(); 
        private bool IsSelectAll = false;

        private class DisplayAddress
        {
            public DisplayAddress(GroupAddress address) {
                this.IsSelected = false;
                this.Id = address.Id;
                this.Name = address.Name;
                this.KnxAddress = address.KnxAddress;
                //this.KnxSize = address.KnxSize;
                //this.KnxDPTName = address.KnxDPTName;
                this.Type = address.Type;
                this.Priority = address.Priority;
                this.DefaultValue = address.DefaultValue;
                this.IsCommunication = address.IsCommunication;
                this.IsRead = address.IsRead;
                this.IsWrite = address.IsWrite;
                this.IsTransmit = address.IsTransmit;
                this.IsUpgrade = address.IsUpgrade;
                this.WireNumber = address.WireNumber;
                this.ReadTimeSpan = address.ReadTimeSpan;
                this.Tip = address.Tip;
                //this.Actions = address.Actions;
                this.Actions = "";
                if (address.Actions != null)
                {
                    foreach (KNXDatapointAction action in address.Actions)
                    {
                        if (this.Actions.Length > 0)
                        {
                            this.Actions += "/" + action;
                        }
                        else
                        {
                            this.Actions = action.ToString();
                        }
                    }
                }
            }

            /// <summary>
            /// 是否选中该地址
            /// </summary>
            public bool IsSelected { get; set; }

            /// <summary>
            /// ETS中设备的ID， ETS自动分配
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// ETS中设备用户指定的名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// ETS 写入地址
            /// </summary>
            public string KnxAddress { get; set; }

            public KNXDataType Type { get; set; }

            /// <summary>
            /// ETS 数据大小
            /// </summary>
            //public string KnxSize { get; set; }

            /// <summary>
            /// ETS数据类型
            /// </summary>
            //public string KnxDPTName { get; set; }

            /// <summary>
            /// 优先级
            /// </summary>
            public KNXPriority Priority { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string DefaultValue { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsCommunication { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsRead { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsWrite { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsTransmit { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool IsUpgrade { get; set; }

            /// <summary>
            /// 电缆编号
            /// </summary>
            public string WireNumber { get; set; }

            /// <summary>
            /// 读取时间间隔，单位毫秒
            /// </summary>
            public int ReadTimeSpan { get; set; }

            /// <summary>
            /// 给最终用户控制该组地址时提供一些提示
            /// </summary>
            public string Tip { get; set; }

            /// <summary>
            /// ETS中该设备可执行的动作
            /// </summary>
            public string Actions { get; set; }
            //public List<KNXDatapointAction> Actions;
        }

        #region 模块变量

        //
        private const string EtsFilter = "ETS project files (*.knxproj)|*.knxproj|All files (*.*)|*.*";
        private const string XmlFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        // 当前是否保存
        private bool _saved = true;

        #endregion

        #region 构造函数
        public FrmGroupAddressMgt()
        {
            InitializeComponent();

            //cbSelectAll.CheckedChanged += cbSelectAll_CheckedChanged;

            //this.cbSelectAll.Visible = false;
            //this.cbSelectAll.Text = "选择";
            //this.dgvGroupAddress.Controls.Add(cbSelectAll); 

            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(0, "批量设置"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(1, "设置组地址名"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(2, "设置组地址"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(3, "设置数据类型"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(4, "设置默认值"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(5, "设置优先级"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(6, "设置读取时间间隔"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(7, "设置线缆编号"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(8, "设置是否通讯"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(9, "设置是否读"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(10, "设置是否写"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(11, "设置是否传输"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(12, "设置是否更新"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(13, "设置提示"));
            //this.cbbBatchOperation.Items.Add(new ComboBoxItem(14, "设置行为"));

            this.cmsDgvGroupAddress.Items.Add(tsmiSetGroupAddressName());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetGroupAddress());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetDataType());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetDefaultValue());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetPriority());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetReadTimeSpan());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetWireNumber());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetIsCommunication());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetIsRead());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetIsWrite());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetIsTransmit());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetIsUpgrade());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetTip());
            this.cmsDgvGroupAddress.Items.Add(tsmiSetActions());
        }
        #endregion

        #region 右键菜单 cmsDgvGroupAddress

        private ToolStripMenuItem tsmiSetGroupAddressName() 
        {
            ToolStripMenuItem setGroupAddressName = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setGroupAddressName.Name = "tsmiSetGroupAddressName";
            //setGroupAddressName.Size = new System.Drawing.Size(152, 22);
            setGroupAddressName.Text = "设置组地址名";
            setGroupAddressName.Click += SetGroupAddressName_Click;

            return setGroupAddressName;
        }

        private ToolStripMenuItem tsmiSetGroupAddress()
        {
            ToolStripMenuItem setGroupAddress = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setGroupAddress.Name = "tsmiSetGroupAddress";
            //setGroupAddress.Size = new System.Drawing.Size(152, 22);
            setGroupAddress.Text = "设置组地址";
            setGroupAddress.Click += SetGroupAddress_Click;

            return setGroupAddress;
        }

        private ToolStripMenuItem tsmiSetDataType()
        {
            ToolStripMenuItem seDataType = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            seDataType.Name = "tsmiSetDataType";
            //seDataType.Size = new System.Drawing.Size(152, 22);
            seDataType.Text = "设置数据类型";
            seDataType.Click += SetDataType_Click;

            return seDataType;
        }

        private ToolStripMenuItem tsmiSetDefaultValue()
        {
            ToolStripMenuItem setDefaultValue = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setDefaultValue.Name = "tsmiSetDefaultValue";
            //setDefaultValue.Size = new System.Drawing.Size(152, 22);
            setDefaultValue.Text = "设置默认值";
            setDefaultValue.Click += SetDefaultValue_Click;

            return setDefaultValue;
        }

        private ToolStripMenuItem tsmiSetPriority()
        {
            ToolStripMenuItem setPriority = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setPriority.Name = "tsmiSetPriority";
            //setPriority.Size = new System.Drawing.Size(152, 22);
            setPriority.Text = "设置优先级";
            setPriority.Click += SetPriority_Click;

            return setPriority;
        }

        private ToolStripMenuItem tsmiSetReadTimeSpan()
        {
            ToolStripMenuItem setReadTimeSpan = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setReadTimeSpan.Name = "tsmiSetReadTimeSpan";
            //setReadTimeSpan.Size = new System.Drawing.Size(152, 22);
            setReadTimeSpan.Text = "设置读取时间间隔";
            setReadTimeSpan.Click += SetReadTimeSpan_Click;

            return setReadTimeSpan;
        }

        private ToolStripMenuItem tsmiSetWireNumber()
        {
            ToolStripMenuItem setWireNumber = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setWireNumber.Name = "tsmiSetWireNumber";
            //setWireNumber.Size = new System.Drawing.Size(152, 22);
            setWireNumber.Text = "设置线缆编号";
            setWireNumber.Click += SetWireNumber_Click;

            return setWireNumber;
        }

        private ToolStripMenuItem tsmiSetIsCommunication()
        {
            ToolStripMenuItem setIsCommunication = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setIsCommunication.Name = "tsmiSetIsCommunication";
            //setIsCommunication.Size = new System.Drawing.Size(152, 22);
            setIsCommunication.Text = "设置是否通讯";
            setIsCommunication.Click += SetIsCommunication_Click;

            return setIsCommunication;
        }

        private ToolStripMenuItem tsmiSetIsRead()
        {
            ToolStripMenuItem setIsRead = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setIsRead.Name = "tsmiSetIsRead";
            //setIsRead.Size = new System.Drawing.Size(152, 22);
            setIsRead.Text = "设置是否读";
            setIsRead.Click += SetIsRead_Click;

            return setIsRead;
        }

        private ToolStripMenuItem tsmiSetIsWrite()
        {
            ToolStripMenuItem setIsWrite = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setIsWrite.Name = "tsmiSetIsWrite";
            //setIsWrite.Size = new System.Drawing.Size(152, 22);
            setIsWrite.Text = "设置是否写";
            setIsWrite.Click += SetIsWrite_Click;

            return setIsWrite;
        }

        private ToolStripMenuItem tsmiSetIsTransmit()
        {
            ToolStripMenuItem setIsTransmit = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setIsTransmit.Name = "tsmiSetIsTransmit";
            //setIsTransmit.Size = new System.Drawing.Size(152, 22);
            setIsTransmit.Text = "设置是否传输";
            setIsTransmit.Click += SetIsTransmit_Click;

            return setIsTransmit;
        }

        private ToolStripMenuItem tsmiSetIsUpgrade()
        {
            ToolStripMenuItem setIsUpgrade = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setIsUpgrade.Name = "tsmiSetIsUpgrade";
            //setIsUpgrade.Size = new System.Drawing.Size(152, 22);
            setIsUpgrade.Text = "设置是否更新";
            setIsUpgrade.Click += SetIsUpgrade_Click;

            return setIsUpgrade;
        }

        private ToolStripMenuItem tsmiSetTip()
        {
            ToolStripMenuItem setTip = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setTip.Name = "tsmiSetTip";
            //setTip.Size = new System.Drawing.Size(152, 22);
            setTip.Text = "设置提示";
            setTip.Click += SettsmiSetTip_Click;

            return setTip;
        }

        private ToolStripMenuItem tsmiSetActions()
        {
            ToolStripMenuItem setActions = new ToolStripMenuItem();
            //setGroupAddressName.Image = Properties.Resources.Copy_16x16;
            setActions.Name = "tsmiSetActions";
            //setActions.Size = new System.Drawing.Size(152, 22);
            setActions.Text = "设置行为";
            setActions.Click += SetActions_Click;

            return setActions;
        }

        private void SetGroupAddressName_Click(object sender, EventArgs e)
        {
            Console.Write("设置组地址名 ==> " + e.ToString());
        }

        private void SetGroupAddress_Click(object sender, EventArgs e)
        {
            Console.Write("设置组地址 ==> " + e.ToString());
        }

        private void SetDataType_Click(object sender, EventArgs e)
        {
            Console.Write("设置数据类型 ==> " + e.ToString());
        }

        private void SetDefaultValue_Click(object sender, EventArgs e)
        {
            Console.Write("设置默认值 ==> " + e.ToString());
        }

        private void SetPriority_Click(object sender, EventArgs e)
        {
            Console.Write("设置优先级 ==> " + e.ToString());
        }

        private void SetReadTimeSpan_Click(object sender, EventArgs e)
        {
            Console.Write("设置读取时间间隔 ==> " + e.ToString());
        }

        private void SetWireNumber_Click(object sender, EventArgs e)
        {
            Console.Write("设置线缆编号 ==> " + e.ToString());
        }

        private void SetIsCommunication_Click(object sender, EventArgs e)
        {
            Console.Write("设置是否通讯 ==> " + e.ToString());
        }

        private void SetIsRead_Click(object sender, EventArgs e)
        {
            Console.Write("设置是否读 ==> " + e.ToString());
        }

        private void SetIsWrite_Click(object sender, EventArgs e)
        {
            Console.Write("设置是否写 ==> " + e.ToString());
        }

        private void SetIsTransmit_Click(object sender, EventArgs e)
        {
            Console.Write("设置是否传输 ==> " + e.ToString());
        }

        private void SetIsUpgrade_Click(object sender, EventArgs e)
        {
            Console.Write("设置是否更新 ==> " + e.ToString());
        }

        private void SettsmiSetTip_Click(object sender, EventArgs e)
        {
            Console.Write("设置提示 ==> " + e.ToString());
        }

        private void SetActions_Click(object sender, EventArgs e)
        {
            Console.Write("设置行为 ==> " + e.ToString());
        }

        #endregion

        #region 用户方法
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

        /// <summary>
        /// 根据 ReadAddress 合并地址列表
        /// </summary>
        /// <param name="orgData"></param>
        /// <param name="importData"></param>
        /// <returns></returns>
        private static void Merge(List<GroupAddress> importData)
        {
            if (importData != null && importData.Any())
            {
                var dictionary = MyCache.GroupAddressTable.ToDictionary(it => it.Id);

                foreach (var it in importData)
                {
                    dictionary[it.Id] = it;
                }

                var list = dictionary.Values.ToList();
                // 
                MyCache.GroupAddressTable = new BindingList<GroupAddress>(list);
            }

        }

        private void LoadAllAddress()
        {
            addressList.Clear();

            foreach (var it in MyCache.GroupAddressTable)
            {
                var temp = new DisplayAddress(it);

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
            var sortData = addressList.ToList();

            this.dgvGroupAddress.DataSource = new BindingList<DisplayAddress>(sortData);

            FormatGrid(dgvGroupAddress);
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
                    // ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofd.Filter = EtsFilter;
                    ofd.FilterIndex = 1;
                    ofd.DefaultExt = "knxproj";
                    ofd.RestoreDirectory = true;

                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        ClearProjTempFolder();

                        if (Directory.Exists(MyCache.ProjTempFolder))
                        {
                            // 存放ETS文件，解压并解析xml
                            string etsProjectFile = Path.Combine(MyCache.ProjTempFolder, ofd.SafeFileName);
                            File.Copy(ofd.FileName, etsProjectFile);

                            // 导入的地址表
                            var importAddress = ETSImport.ParseEtsProjectFile(etsProjectFile);

                            Merge(importAddress);

                            //this.dgvGroupAddress.DataSource = new BindingList<DisplayAddress>(MyCache.GroupAddressTable.ToList());
                            LoadAllAddress();

                            _saved = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "组地址文件格式不兼容！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                            Merge(importAddress);
                        }

                        //this.dgvGroupAddress.DataSource = new BindingList<GroupAddress>(MyCache.GroupAddressTable);
                        LoadAllAddress();

                        _saved = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "组地址文件格式不兼容！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
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
                var item = this.dgvGroupAddress.SelectedRows[0].DataBoundItem as DisplayAddress;
                foreach (GroupAddress address in MyCache.GroupAddressTable)
                {
                    if (address.Id == item.Id)
                    {
                        FrmGroupAddress frm = new FrmGroupAddress(DataStatus.Modify);
                        frm.Address = address;
                        var dlgResult = frm.ShowDialog(this);

                        if (dlgResult == DialogResult.OK)
                        {
                            /* 刷新修改的行 */
                            DisplayAddress disAddress = new DisplayAddress(frm.Address);
                            //addressList[rowIndex] = disAddress;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[1].Value = disAddress.Id;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[2].Value = disAddress.Name;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[3].Value = disAddress.KnxAddress;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[4].Value = disAddress.Type;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[6].Value = disAddress.DefaultValue;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[5].Value = disAddress.Priority;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[13].Value = disAddress.ReadTimeSpan;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[12].Value = disAddress.WireNumber;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[7].Value = disAddress.IsCommunication;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[8].Value = disAddress.IsRead;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[9].Value = disAddress.IsWrite;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[10].Value = disAddress.IsTransmit;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[11].Value = disAddress.IsUpgrade;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[14].Value = disAddress.Tip;
                            this.dgvGroupAddress.Rows[rowIndex].Cells[15].Value = disAddress.Actions;

                            this.dgvGroupAddress.EndEdit();

                            _saved = false;
                        }

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

            foreach(var item in addressList)
            {
                foreach (GroupAddress address in MyCache.GroupAddressTable)
                {
                    if (address.Id == item.Id)
                    {
                        address.Name = item.Name;
                        address.KnxAddress = item.KnxAddress;
                        address.Type = item.Type;
                        address.Priority = item.Priority;
                        address.DefaultValue = item.DefaultValue;
                        address.IsCommunication = item.IsCommunication;
                        address.IsRead = item.IsRead;
                        address.IsWrite = item.IsWrite;
                        address.IsTransmit = item.IsTransmit;
                        address.IsUpgrade = item.IsUpgrade;
                        address.WireNumber = item.WireNumber;
                        address.ReadTimeSpan = item.ReadTimeSpan;
                        address.Tip = item.Tip;

                        break;
                    }
                }
            }

            try
            {
                // 保存缓存中的数据到 JSON 文件
                GroupAddressStorage.Save();

                // 
                var formName = typeof(FrmMain).Name;
                if (Application.OpenForms[formName] != null)
                {
                    var frm = Application.OpenForms[formName] as FrmMain;
                    if (frm != null)
                    {
                        frm.Saved = false;
                    }
                }

                _saved = true;
            }
            catch (Exception ex)
            {
                string errorMsg = "组地址保存失败！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void NewAddress()
        {
            var frm = new FrmGroupAddress(DataStatus.Add) { Address = new GroupAddress() };
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
                        _saved = false;
                    }
                    else
                    {
                        MessageBox.Show("组地址冲突，地址添加失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private bool CheckUnique(BindingList<GroupAddress> addressList, GroupAddress address)
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
                DisplayAddress disAddress = this.dgvGroupAddress.Rows[i].DataBoundItem as DisplayAddress;
                if (disAddress.IsSelected)
                {
                    addressList.Remove(disAddress);
                    this.dgvGroupAddress.Rows.RemoveAt(i);
                    foreach (GroupAddress address in MyCache.GroupAddressTable)
                    {
                        if (address.Id == disAddress.Id)
                        {
                            MyCache.GroupAddressTable.Remove(address);
                            //LoadAllAddress();

                            _saved = false;

                            break;
                        }
                    }
                }
            }

            //refreshDataTable();
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;

                var col = grid.Columns["IsSelected"];
                col.HeaderText = "选择";
                col.Width = 60;
                col.DisplayIndex = i++;
                col.ReadOnly = false;

                col = grid.Columns["Id"];
                col.Width = 5;
                col.Visible = false;
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Name"];
                col.Width = 150;
                col.HeaderText = "名称";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["KnxAddress"];
                col.Width = 80;
                col.HeaderText = "地址";
                col.DefaultCellStyle.Format = "y";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Type"];
                col.Width = 80;
                col.HeaderText = "数据类型";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = "默认值";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Priority"];
                col.Width = 80;
                col.HeaderText = "优先级";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["ReadTimespan"];
                col.Width = 80;
                col.HeaderText = "读间隔时间";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["WireNumber"];
                col.Width = 80;
                col.HeaderText = "电缆编号";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsCommunication"];
                col.Width = 50;
                col.HeaderText = "通讯";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsRead"];
                col.Width = 50;
                col.HeaderText = "读";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsWrite"];
                col.Width = 50;
                col.HeaderText = "写";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsTransmit"];
                col.Width = 50;
                col.HeaderText = "传送";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["IsUpgrade"];
                col.Width = 50;
                col.HeaderText = "更新";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Tip"];
                col.Width = 150;
                col.HeaderText = "操控提示";
                col.DisplayIndex = i++;
                col.ReadOnly = true;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = "组地址行为";
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
            // 当前数据有没有保存
            if (_saved == false)
            {
                var result = MessageBox.Show("是否保存当前修改", "KNX 地址保存提示", MessageBoxButtons.YesNoCancel);

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
            SaveAddressList();
        }

        /// <summary>
        /// 导入ETS项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportETSProject_Click(object sender, EventArgs e)
        {
            DatapointTypesStorage.Load();
            ImportEtsProject();
        }

        private void btnImportETSAddressXML_Click(object sender, EventArgs e)
        {
            ImportEtsAddressXml();
        }

        private void FrmGroupAddressMgt_Load(object sender, EventArgs e)
        {
            LoadAllAddress();

            FormatGrid(dgvGroupAddress);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void SearchAddress()
        {
            var searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                var data = from x in MyCache.GroupAddressTable
                           where x.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1
                           select x;
                var groupAddresses = data as IList<GroupAddress> ?? data.ToList();
                //this.dgvGroupAddress.DataSource = new BindingList<GroupAddress>(groupAddresses);
                LoadAllAddress();
            }
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



        #region DataGridView dgvGroupAddress事件

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
            Console.Write("\nrow index:"+e.RowIndex+"\tcolumn index:"+e.ColumnIndex);

            if (e.ColumnIndex == 0)
            {
                if (this.IsSelectAll)
                {
                    this.IsSelectAll = false;

                    this.dgvGroupAddress.CurrentCell = null;

                    for (int i = 0; i < this.dgvGroupAddress.RowCount; i++)
                    {
                        this.dgvGroupAddress.Rows[i].Cells[0].Value = false;

                    }
                }
                else
                {
                    this.IsSelectAll = true;

                    this.dgvGroupAddress.CurrentCell = null;

                    for (int i = 0; i < this.dgvGroupAddress.RowCount; i++)
                    {
                        this.dgvGroupAddress.Rows[i].Cells[0].Value = true;
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
                    anySelected = (bool)row.Cells[0].Value;
                    communicationIsChecked = (bool)row.Cells[7].Value;
                    readIsChecked = (bool)row.Cells[8].Value;
                    writeIsChecked = (bool)row.Cells[9].Value;
                    transmitIsChecked = (bool)row.Cells[10].Value;
                    upgradeIsChecked = (bool)row.Cells[11].Value;
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
                    else if(2 == e.ColumnIndex) // 名称
                    {
                        //var frm = new FrmSetWireNumber();
                        //frm.typeName = "名称";
                        //var result = frm.ShowDialog();
                        //if (DialogResult.OK == frm.DialogResult)
                        //{
                        //    string number = frm.value;
                        //    FrmSetWireNumber.CodingMode mode = frm.mode;
                        //    if (0 < number.Length)
                        //    {
                        //        int res = 0;
                        //        int count = number.Length;
                        //        bool isNum = false;
                        //        while ((!isNum) && (0 < count))
                        //        {
                        //            string lastChar = number.Substring(number.Length - count, count);
                        //            isNum = isNumberic(lastChar, out res);
                        //            count--;
                        //        }

                        //        if (isNum)
                        //        {
                        //            count++;
                        //            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        //            {
                        //                bool isSelected = (bool)row.Cells[0].Value;
                        //                if (isSelected)
                        //                {
                        //                    row.Cells[2].Value = string.Format("{0}{1}", number.Substring(0, number.Length - count), res);

                        //                    if (FrmSetWireNumber.CodingMode.Decrease == mode)
                        //                    {
                        //                        res--;
                        //                    }
                        //                    else if (FrmSetWireNumber.CodingMode.Increment == mode)
                        //                    {
                        //                        res++;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        //            {
                        //                bool isSelected = (bool)row.Cells[0].Value;
                        //                if (isSelected)
                        //                {
                        //                    row.Cells[2].Value = number;
                        //                }
                        //            }
                        //        }
                        //    }

                        //}
                    }
                    else if (3 == e.ColumnIndex) // 地址
                    {
                        //var frm = new FrmSetWireNumber();
                        //frm.typeName = "地址";
                        //var result = frm.ShowDialog();
                        //if (DialogResult.OK == frm.DialogResult)
                        //{
                        //    string number = frm.value;
                        //    FrmSetWireNumber.CodingMode mode = frm.mode;
                        //    if (0 < number.Length)
                        //    {
                        //        int res = 0;
                        //        int count = number.Length;
                        //        bool isNum = false;
                        //        while ((!isNum) && (0 < count))
                        //        {
                        //            string lastChar = number.Substring(number.Length - count, count);
                        //            isNum = isNumberic(lastChar, out res);
                        //            count--;
                        //        }

                        //        if (isNum)
                        //        {
                        //            count++;
                        //            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        //            {
                        //                bool isSelected = (bool)row.Cells[0].Value;
                        //                if (isSelected)
                        //                {
                        //                    row.Cells[3].Value = string.Format("{0}{1}", number.Substring(0, number.Length - count), res);

                        //                    if (FrmSetWireNumber.CodingMode.Decrease == mode)
                        //                    {
                        //                        res--;
                        //                    }
                        //                    else if (FrmSetWireNumber.CodingMode.Increment == mode)
                        //                    {
                        //                        res++;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        //            {
                        //                bool isSelected = (bool)row.Cells[0].Value;
                        //                if (isSelected)
                        //                {
                        //                    row.Cells[3].Value = number;
                        //                }
                        //            }
                        //        }
                        //    }

                        //}
                    }
                    else if (4 == e.ColumnIndex) 
                    {
                        //var frm = new FrmSetDataType();
                        //var result = frm.ShowDialog();
                        //if (DialogResult.OK == result)
                        //{
                        //    KNXDataType type = frm.type;
                        //    foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        //    {
                        //        bool isSelected = (bool)row.Cells[0].Value;
                        //        if (isSelected)
                        //        {
                        //            row.Cells[4].Value = type;
                        //        }
                        //    }
                        //}
                    }
                    else if (5 == e.ColumnIndex)
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
                                    row.Cells[5].Value = priority;
                                }
                            }

                            _saved = false;
                        }
                    }
                    else if (6 == e.ColumnIndex)
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
                                    row.Cells[6].Value = value;
                                }
                            }

                            _saved = false;
                        }
                    }
                    else if (7 == e.ColumnIndex) // 是否通讯
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if(communicationIsChecked) 
                                {
                                    row.Cells[7].Value = false;
                                }
                                else
                                {
                                    row.Cells[7].Value = true;
                                }
                            }
                        }

                        _saved = false;
                    }
                    else if (8 == e.ColumnIndex) // 是否读
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (readIsChecked)
                                {
                                    row.Cells[8].Value = false;
                                }
                                else
                                {
                                    row.Cells[8].Value = true;
                                }
                            }
                        }

                        _saved = false;
                    }
                    else if (9 == e.ColumnIndex) // 是否写
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (writeIsChecked)
                                {
                                    row.Cells[9].Value = false;
                                }
                                else
                                {
                                    row.Cells[9].Value = true;
                                }
                            }
                        }

                        _saved = false;
                    }
                    else if (10 == e.ColumnIndex) // 是否传输
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (transmitIsChecked)
                                {
                                    row.Cells[10].Value = false;
                                }
                                else
                                {
                                    row.Cells[10].Value = true;
                                }
                            }
                        }

                        _saved = false;
                    }
                    else if (11 == e.ColumnIndex) // 是否更新
                    {
                        foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                        {
                            bool isSelected = (bool)row.Cells[0].Value;
                            if (isSelected)
                            {
                                if (upgradeIsChecked)
                                {
                                    row.Cells[11].Value = false;
                                }
                                else
                                {
                                    row.Cells[11].Value = true;
                                }
                            }
                        }

                        _saved = false;
                    }
                    else if (12 == e.ColumnIndex)
                    {
                        var frm = new FrmSetWireNumber();
                        frm.typeName = "线缆编号";
                        var result = frm.ShowDialog();
                        if (DialogResult.OK == frm.DialogResult)
                        {
                            string number = frm.value;
                            FrmSetWireNumber.CodingMode type = frm.mode;
                            if (0 < number.Length)
                            {
                                int res = 0;
                                int count = number.Length;
                                bool isNum  = false;
                                while ((!isNum) && (0 <count))
                                {
                                    string lastChar = number.Substring(number.Length - count, count);
                                    isNum = isNumberic(lastChar, out res);
                                    count--;
                                }

                                if (isNum)
                                {
                                    count++;
                                    foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                                    {
                                        bool isSelected = (bool)row.Cells[0].Value;
                                        if (isSelected)
                                        {
                                            row.Cells[12].Value = string.Format("{0}{1}", number.Substring(0, number.Length - count), res);

                                            if (FrmSetWireNumber.CodingMode.Decrease == type)
                                            {
                                                res--;
                                            }
                                            else if(FrmSetWireNumber.CodingMode.Increment == type)
                                            {
                                                res++;
                                            }
                                        }
                                    }

                                    
                                }
                                else
                                {
                                    foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                                    {
                                        bool isSelected = (bool)row.Cells[0].Value;
                                        if (isSelected)
                                        {
                                            row.Cells[12].Value = number;
                                        }
                                    }
                                }
                            }

                            _saved = false;
                        }
                    }
                    else if (13 == e.ColumnIndex)
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
                                    row.Cells[13].Value = value;
                                }
                            }

                            _saved = false;
                        }
                    }
                    else if (14 == e.ColumnIndex) // 操控提示
                    {
                        var frm = new FrmSetOperationTips();
                        var result = frm.ShowDialog();
                        if (DialogResult.OK == result)
                        {
                            string tip = frm.tip;
                            foreach (DataGridViewRow row in this.dgvGroupAddress.Rows)
                            {
                                bool isSelected = (bool)row.Cells[0].Value;
                                if (isSelected)
                                {
                                    row.Cells[14].Value = tip;
                                }
                            }

                            _saved = false;
                        }
                    }

                    this.dgvGroupAddress.EndEdit();
                }
            }
        }

        private void dgvGroupAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1)
            //{
            //    if (this.dgvGroupAddress.Rows[e.RowIndex].Cells[0].EditedFormattedValue.Equals(true))
            //    {
            //        this.dgvGroupAddress.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
            //    }
            //    else
            //    {
                    //if (0 == (e.RowIndex % 2)) {
                    //    this.dgvGroupAddress.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;
                    //}
                    //else
                    //{
                    //    this.dgvGroupAddress.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Lavender;
                    //}
            //    }
            //}

            //if (4 == e.ColumnIndex)
            //{
            //    DataGridViewComboBoxCell cel = new DataGridViewComboBoxCell();
            //    cel.Style.BackColor = Color.Cornsilk;
            //    foreach (var elem in Enum.GetNames(typeof(KNXDataType)))
            //    {
            //        cel.Items.Add(elem);
            //    }
            //    dgvGroupAddress.Rows[e.RowIndex].Cells[e.ColumnIndex] = cel;
            //}
        }
        
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupAddress_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
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
                for (int i = 0; i < this.dgvGroupAddress.Rows.Count; i++ )
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

        //private void cbSelectAll_CheckedChanged(object send, System.EventArgs e)
        //{
        //    for (int i = 0; i <= this.dgvGroupAddress.RowCount - 1; i++)
        //    {
        //        this.dgvGroupAddress.Rows.SharedRow(i).SetValues(cbSelectAll.Checked);
        //    }
        //}

        //private void dgvGroupAddress_CellPainting(object sender, System.Windows.Forms.DataGridViewCellPaintingEventArgs e)
        //{
            //if (e.RowIndex == -1 & e.ColumnIndex == 0)
            //{
            //    Point p = this.dgvGroupAddress.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            //    p.Offset(this.dgvGroupAddress.Left, this.dgvGroupAddress.Top);

            //    //p.Offset(left, top);
            //    this.cbSelectAll.Location = p;
            //    this.cbSelectAll.Size = this.dgvGroupAddress.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Size;
            //    this.cbSelectAll.Visible = true;
            //    this.cbSelectAll.BringToFront();
            //}
        //} 

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

    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Index { get; set; }

        public ComboBoxItem(int index, string text)
        {
            Text = text;
            Index = index;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class DataGridViewComboEditBoxColumn : DataGridViewComboBoxColumn
    {
        public DataGridViewComboEditBoxColumn()
        {
            DataGridViewComboEditBoxCell obj = new DataGridViewComboEditBoxCell();
            CellTemplate = obj;
        }

        public override sealed DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set { base.CellTemplate = value; }
        }
    }

    public class DataGridViewComboEditBoxCell : DataGridViewComboBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            ComboBox comboBox = (ComboBox)DataGridView.EditingControl;

            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox.Validating += ComboBoxValidating;
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value != null)
            {
                if (value.ToString().Trim() != string.Empty)
                {
                    if (Items.IndexOf(value) == -1)
                    {
                        Items.Add(value);
                        DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)OwningColumn;
                        col.Items.Add(value);
                    }
                }
            }
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        protected static void ComboBoxValidating(object sender, CancelEventArgs e)
        {
            DataGridViewComboBoxEditingControl cbo = (DataGridViewComboBoxEditingControl)sender;
            if (cbo.Text.Trim() == string.Empty) return;

            DataGridView grid = cbo.EditingControlDataGridView;
            object value = cbo.Text;

            // Add value to list if not there
            if (cbo.Items.IndexOf(value) == -1)
            {
                DataGridViewComboBoxColumn cboCol = (DataGridViewComboBoxColumn)grid.Columns[grid.CurrentCell.ColumnIndex];
                // Must add to both the current combobox as well as the template, to avoid duplicate entries
                cbo.Items.Add(value);
                cboCol.Items.Add(value);
                grid.CurrentCell.Value = value;
            }
        }
    }

    public class DataGridViewMoneyTextBoxColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewMoneyTextBoxColumn()
        {
            this.CellTemplate = new DataGridViewMoneyCell();
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                DataGridViewMoneyCell cell = value as DataGridViewMoneyCell;
                if (value != null && cell == null)
                {
                    throw new InvalidCastException("Value provided for CellTemplate must be of type TEditNumDataGridViewCell or derive from it.");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewMoneyCell : DataGridViewTextBoxCell
    {
        public DataGridViewMoneyCell()
        {

        }
        int decimalLength;

        private static Type defaultEditType = typeof(MoneyTextBoxDataGridViewEditingControl);
        private static Type defaultValueType = typeof(System.Decimal);

        public int DecimalLength
        {
            get { return decimalLength; }
            set { decimalLength = value; }
        }

        public override Type EditType
        {
            get { return defaultEditType; }
        }

        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;
                if (valueType != null)
                {
                    return valueType;
                }
                return defaultValueType;
            }
        }
    }
    public class MoneyTextBoxDataGridViewEditingControl : TextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;  // grid owning this editing control
        private bool valueChanged;  // editing control's value has changed or not
        private int rowIndex;  //  row index in which the editing control resides

        #region IDataGridViewEditingControl 成员

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
                // The NumericUpDown control does not support transparent back colors
                Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
                this.BackColor = opaqueBackColor;
                this.dataGridView.EditingPanel.BackColor = opaqueBackColor;
            }
            else
            {
                this.BackColor = dataGridViewCellStyle.BackColor;
            }
            this.ForeColor = dataGridViewCellStyle.ForeColor;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get { return this.valueChanged; }
            set { this.valueChanged = value; }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.End:
                case Keys.Delete:
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                this.SelectAll();
            }
            else
            {
                this.SelectionStart = this.Text.Length;
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion
    }

}
