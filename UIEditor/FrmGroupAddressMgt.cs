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

namespace UIEditor
{
    public partial class FrmGroupAddressMgt : Form
    {
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private class DisplayAddress
        {
            public DisplayAddress(GroupAddress address) {
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
                this.Actions = "";
                if (address.Actions != null)
                {
                    foreach (KNXDatapointAction action in address.Actions)
                    {
                        if (this.Actions.Length > 0)
                        {
                            this.Actions += "/" + action.Name;
                        }
                        else
                        {
                            this.Actions = action.Name;
                        }
                    }
                }
            }

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
            var data = new List<DisplayAddress>();

            foreach (var it in MyCache.GroupAddressTable)
            {
                var temp = new DisplayAddress(it);

                data.Add(temp);
            }

            // 排序
            var sortData = data.ToList();

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
                            //this.dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
                            LoadAllAddress();

                            _saved = false;
                        }

                        break;
                    }
                }
                //if (item != null)
                //{
                //    FrmGroupAddress frm = new FrmGroupAddress(DataStatus.Modify);

                //    frm.Address = item;
                //    var dlgResult = frm.ShowDialog(this);

                //    if (dlgResult == DialogResult.OK)
                //    {
                //        //this.dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
                //        LoadAllAddress();

                //        _saved = false;
                //    }
                //}
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

        private void DeleteAddress()
        {
            if (this.dgvGroupAddress.SelectedRows.Count > 0)
            {
                var item = this.dgvGroupAddress.SelectedRows[0].DataBoundItem as DisplayAddress;
                foreach (GroupAddress address in MyCache.GroupAddressTable)
                {
                    if (address.Id == item.Id)
                    {
                        MyCache.GroupAddressTable.Remove(address);
                        //this.dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
                        LoadAllAddress();

                        _saved = false;

                        break;
                    }
                }
                //if (item != null)
                //{
                //    MyCache.GroupAddressTable.Remove(item);
                //    //this.dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
                //    LoadAllAddress();

                //    _saved = false;
                //}
            }
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;
                var col = grid.Columns["Id"];
                col.Width = 5;
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["Name"];
                col.Width = 150;
                col.HeaderText = "名称";
                col.DisplayIndex = i++;

                col = grid.Columns["KnxAddress"];
                col.Width = 80;
                col.HeaderText = "地址";
                col.DefaultCellStyle.Format = "y";
                col.DisplayIndex = i++;

                col = grid.Columns["Type"];
                col.Width = 80;
                col.HeaderText = "数据类型";
                col.DisplayIndex = i++;

                //col = grid.Columns["KnxSize"];
                //col.Width = 100;
                //col.HeaderText = "数据大小";
                //col.DisplayIndex = i++;

                //col = grid.Columns["KnxDPTName"];
                //col.Width = 80;
                //col.HeaderText = "数据点名称";
                //col.DisplayIndex = i++;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = "默认值";
                col.DisplayIndex = i++;

                col = grid.Columns["Priority"];
                col.Width = 80;
                col.HeaderText = "优先级";
                col.DisplayIndex = i++;

                col = grid.Columns["ReadTimespan"];
                col.Width = 80;
                col.HeaderText = "读间隔时间";
                col.DisplayIndex = i++;

                col = grid.Columns["WireNumber"];
                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.Width = 80;
                col.HeaderText = "电缆编号";
                col.DisplayIndex = i++;

                col = grid.Columns["IsCommunication"];
                col.Width = 50;
                col.HeaderText = "通讯";
                col.DisplayIndex = i++;

                col = grid.Columns["IsRead"];
                col.Width = 50;
                col.HeaderText = "读";
                col.DisplayIndex = i++;

                col = grid.Columns["IsWrite"];
                col.Width = 50;
                col.HeaderText = "写";
                col.DisplayIndex = i++;

                col = grid.Columns["IsTransmit"];
                col.Width = 50;
                col.HeaderText = "传送";
                col.DisplayIndex = i++;

                col = grid.Columns["IsUpgrade"];
                col.Width = 50;
                col.HeaderText = "更新";
                col.DisplayIndex = i++;

                col = grid.Columns["Tip"];
                col.Width = 150;
                col.HeaderText = "操控提示";
                col.DisplayIndex = i++;

                col = grid.Columns["Actions"];
                col.Width = 150;
                col.HeaderText = "组地址行为";
                col.DisplayIndex = i++;

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

        private void dgvGroupAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvGroupAddress.Focused == true && e.KeyCode == Keys.F2)
            {
                ModifyAddress();
            }
        }

        private void dgvGroupAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ModifyAddress();
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
            //this.dgvGroupAddress.DataSource = MyCache.GroupAddressTable;
            LoadAllAddress();

            FormatGrid(dgvGroupAddress);
        }

        private void dgvGroupAddress_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {



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

        private void dgvGroupAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }

}
