using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Structure;
using Structure.ETS;
using UIEditor.Entity;
using GroupAddress = UIEditor.ETS.GroupAddress;

namespace UIEditor.Controls
{
    /// <summary>
    /// Respresents a standard dialog that can be used as a base for custom dialog forms.
    /// </summary>
    public partial class FrmGroupAddressPick : Form
    {
        #region 界面显示用的组地址对象
        class DisplayAddress
        {
            public DisplayAddress(GroupAddress row)
            {
                Selected = false;
                this.Id = row.Id;
                this.Name = row.Name;
                this.KnxAddress = row.KnxAddress;
                //this.KnxDPTName = row.KnxDPTName;
                this.Type = row.Type.ToString();
                this.DefaultValue = row.DefaultValue;
                this.WireNumber = WireNumber;
                this.Tip = row.Tip;
                this.Actions = "";
                if (row.Actions != null)
                {
                    foreach (KNXDatapointAction action in row.Actions)
                    {
                        if (this.Actions.Length > 0)
                        {
                            this.Actions += "/"+action.Name;
                        }
                        else
                        {
                            this.Actions = action.Name;
                        }
                    }
                }
            }

            public bool Selected { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }

            public string KnxAddress { get; set; }

            // 数据点名称
            //public string KnxDPTName { get; set; }

            public string Type { get; set; }

            // 默认值
            public string DefaultValue { get; set; }

            //
            public string WireNumber { get; set; }

            /// <summary>
            /// 给最终用户控制该组地址时提供一些提示
            /// </summary>
            public string Tip { get; set; }

            public string Actions { get; set; }
        }
        #endregion
        #region 属性
        public Dictionary<string, KNXSelectedAddress> SelectedAddress { get; set; }


        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }


        private bool _search = false;
        private bool _multiSelect = false;

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
                MessageBox.Show("读地址只能设置一个地址，请重设置！");
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
                var groupAddress = dgvData.Rows[i].DataBoundItem as DisplayAddress;
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

            var data = new List<DisplayAddress>();

            foreach (var it in MyCache.GroupAddressTable)
            {
                var temp = new DisplayAddress(it);

                if (SelectedAddress != null && SelectedAddress.ContainsKey(temp.Id))
                {
                    temp.Selected = true;
                    temp.DefaultValue = SelectedAddress[temp.Id].DefaultValue;
                }

                data.Add(temp);
            }

            // 排序
            var sortData = (from i in data orderby i.Selected descending, i.KnxAddress select i).ToList();

            this.dgvData.DataSource = new BindingList<DisplayAddress>(sortData);

            FormatGrid(dgvData);
        }

        private void FormatGrid(DataGridView grid)
        {
            if (grid.Columns.Count > 1)
            {
                int i = 0;
                var col = grid.Columns["Id"];
                col.Visible = false;
                col.DisplayIndex = i++;

                col = grid.Columns["selected"];
                col.HeaderText = "选择";
                col.Width = 50;
                col.DisplayIndex = i++;

                col = grid.Columns["Name"];
                col.Width = 180;
                col.HeaderText = "名称";
                col.ReadOnly = true;
                col.DisplayIndex = i++;

                col = grid.Columns["KnxAddress"];
                col.Width = 80;
                col.HeaderText = "地址";
                col.ReadOnly = true;
                col.DisplayIndex = i++;

                col = grid.Columns["Type"];
                col.Width = 80;
                col.HeaderText = "数据类型";
                col.DisplayIndex = i++;

                //col = grid.Columns["KnxDPTName"];
                //col.Width = 80;
                //col.HeaderText = "数据点名称";
                //col.ReadOnly = true;
                //col.DisplayIndex = i++;

                col = grid.Columns["DefaultValue"];
                col.Width = 80;
                col.HeaderText = "默认值";
                col.DisplayIndex = i++;

                col = grid.Columns["WireNumber"];
                col.Width = 100;
                col.HeaderText = "电缆编号";
                col.ReadOnly = true;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SearchAddress();
        }

        private void SearchAddress()
        {
            _search = true;

            string searchText = this.txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var data = new List<DisplayAddress>();

                var filterAddress = from i in MyCache.GroupAddressTable where i.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 select i;

                foreach (var it in filterAddress)
                {
                    var temp = new DisplayAddress(it);

                    if (SelectedAddress != null && SelectedAddress.ContainsKey(temp.Id))
                    {
                        temp.Selected = true;
                    }

                    data.Add(temp);
                }

                // 排序
                var sortData = (from i in data orderby i.Selected descending, i.KnxAddress select i).ToList();

                this.dgvData.DataSource = new BindingList<DisplayAddress>(sortData);

                FormatGrid(dgvData);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadAllAddress();
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
    }
}
