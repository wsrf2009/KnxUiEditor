using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor
{
    public partial class FrmCollections : Form
    {
        #region 自定义数据类型
        private enum GroupBy
        {
            Type,
            None
        }
        #endregion

        #region 变量
        private List<ToolStripMenuItem> ViewStyleItems { get; set; }
        private ToolStripMenuItem SelectedStyleItem { get; set; }
        private List<ToolStripMenuItem> GroupByItems { get; set; }
        private GroupBy SelectedGroupBy { get; set; }
        private Template SelectedTemplate { get; set; }
        private ToolStripMenuItem RefreshMenuItem { get; set; }
        #endregion

        #region 构造函数
        public FrmCollections()
        {
            InitializeComponent();

            this.listView.ContextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem detailItem = CreateDetailMenuItem();
            ToolStripMenuItem smallIconItem = CreateSmallIconMenuItem();
            ToolStripMenuItem largeIconItem = CreateLargeIconMenuItem();
            this.ViewStyleItems = new List<ToolStripMenuItem>();
            this.ViewStyleItems.Add(detailItem);
            this.ViewStyleItems.Add(smallIconItem);
            this.ViewStyleItems.Add(largeIconItem);

            ToolStripMenuItem groupItem = CreateGroupByMenuItem();
            ToolStripMenuItem typeItem = CreateGroupByTypeMenuItem();
            ToolStripMenuItem noneItem = CreateGroupByNoneMenuItem();
            this.GroupByItems = new List<ToolStripMenuItem>();
            this.GroupByItems.Add(typeItem);
            this.GroupByItems.Add(noneItem);
            groupItem.DropDownItems.Add(typeItem);
            groupItem.DropDownItems.Add(noneItem);

            this.RefreshMenuItem = CreateRefreshMenuItem();

            this.listView.ContextMenuStrip.Items.Add(detailItem);
            this.listView.ContextMenuStrip.Items.Add(smallIconItem);
            this.listView.ContextMenuStrip.Items.Add(largeIconItem);
            this.listView.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            this.listView.ContextMenuStrip.Items.Add(groupItem);
            this.listView.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            this.listView.ContextMenuStrip.Items.Add(this.RefreshMenuItem);

            this.SelectedGroupBy = GroupBy.None;
            noneItem.Image = UIResMang.GetImage("CheckMark_128");
            this.SelectedStyleItem = largeIconItem;

            if (null == MyCache.Templates)
            {
                LoadTemplates();
            }
            else
            {
                this.SelectedStyleItem.PerformClick();
            }
        }
        #endregion

        #region ListView事件
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {

            }
            else if (MouseButtons.Right == e.Button)
            {

            }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = this.listView.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                var item = info.Item as ListViewItem;
                string name = item.Text;

                foreach (Template tpl in MyCache.Templates)
                {
                    if (tpl.Name == name)
                    {
                        this.SelectedTemplate = tpl;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    }
                }
            }
        }
        #endregion

        #region 后台任务 - 加载所有模板
        private void BGWLoadTemplates_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //MyCache.Templates = CollectionHelper.LoadAllTemplates();
                var list = CollectionHelper.LoadAllTemplates();
                //list.Sort(delegate(Template x, Template y)
                //{
                //    var b = x.Name.CompareTo(y.Name);
                //    return b;
                //});
                list.Sort((x, y) => x.Name.CompareTo(y.Name));
                //list.Sort();
                MyCache.Templates = list;
                //MyCache.Templates = (from i in list orderby i.Name ascending, i.Name select i).ToList();
                //MyCache.Templates = list.OrderBy(t => t.Name).ToList();
                //MyCache.Templates = (from i in list orderby i.Name ascending select i).ToList();
                //MyCache.Templates = (from i in MyCache.Templates orderby i.Name ascending, i.Name select i).ToList();
                //MyCache.Templates = MyCache.Templates.OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BGWLoadTemplates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.SelectedStyleItem.PerformClick();

            Cursor = Cursors.Default;
            this.RefreshMenuItem.Enabled = true;
        }
        #endregion

        #region 创建右键菜单项
        private ToolStripMenuItem CreateDetailMenuItem()
        {
            ToolStripMenuItem tsmiDetail = new ToolStripMenuItem();
            tsmiDetail.Text = UIResMang.GetString("Detail");
            tsmiDetail.Click += DetailMenuItem_Clicked;

            return tsmiDetail;
        }

        private ToolStripMenuItem CreateSmallIconMenuItem()
        {
            ToolStripMenuItem tsmiSamllIcon = new ToolStripMenuItem();
            tsmiSamllIcon.Text = UIResMang.GetString("SmallIcon");
            tsmiSamllIcon.Click += SmallIconMenuItem_Clicked;

            return tsmiSamllIcon;
        }

        private ToolStripMenuItem CreateLargeIconMenuItem()
        {
            ToolStripMenuItem tsmiLargeIcon = new ToolStripMenuItem();
            tsmiLargeIcon.Text = UIResMang.GetString("LargeIcon");
            tsmiLargeIcon.Click += LargeIconMenuItem_Clicked;

            return tsmiLargeIcon;
        }

        private ToolStripMenuItem CreateGroupByMenuItem()
        {
            ToolStripMenuItem tsmiGroup = new ToolStripMenuItem();
            tsmiGroup.Text = UIResMang.GetString("GroupBy");

            return tsmiGroup;
        }

        private ToolStripMenuItem CreateGroupByTypeMenuItem()
        {
            ToolStripMenuItem tsmiGroupByType = new ToolStripMenuItem();
            tsmiGroupByType.Text = UIResMang.GetString("Type");
            tsmiGroupByType.Click += GroupByTypeMenuItem_Clicked;

            return tsmiGroupByType;
        }

        private ToolStripMenuItem CreateGroupByNoneMenuItem()
        {
            ToolStripMenuItem tsmiGroupByNone = new ToolStripMenuItem();
            tsmiGroupByNone.Text = UIResMang.GetString("None");
            tsmiGroupByNone.Click += GroupByNoneMenuItem_Clicked;

            return tsmiGroupByNone;
        }

        private ToolStripMenuItem CreateRefreshMenuItem()
        {
            ToolStripMenuItem tsmiRefresh = new ToolStripMenuItem();
            tsmiRefresh.Text = UIResMang.GetString("Refresh");
            tsmiRefresh.Click += RefreshMenuItem_Clicked;

            return tsmiRefresh;
        }
        #endregion

        #region 右键菜单点击事件
        private void DetailMenuItem_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tsmi.Image = UIResMang.GetImage("CheckMark_128");

            this.SelectedStyleItem = tsmi;
            MarkSelectedStyle(this.SelectedStyleItem);

            DisplayAsDetails(MyCache.Templates);
        }

        private void SmallIconMenuItem_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tsmi.Image = UIResMang.GetImage("CheckMark_128");

            this.SelectedStyleItem = tsmi;
            MarkSelectedStyle(this.SelectedStyleItem);

            DisplayAsSamllIcon(MyCache.Templates);
        }

        private void LargeIconMenuItem_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tsmi.Image = UIResMang.GetImage("CheckMark_128");

            this.SelectedStyleItem = tsmi;
            MarkSelectedStyle(this.SelectedStyleItem);

            if (null != MyCache.Templates)
            {
                DisplayAsLargeIcon(MyCache.Templates);
            }
        }

        private void GroupByTypeMenuItem_Clicked(object sender, EventArgs e)
        {
            UnmarkSelectedGroupBy();

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tsmi.Image = UIResMang.GetImage("CheckMark_128");

            this.SelectedGroupBy = GroupBy.Type;
            this.SelectedStyleItem.PerformClick();
        }

        private void GroupByNoneMenuItem_Clicked(object sender, EventArgs e)
        {
            UnmarkSelectedGroupBy();

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tsmi.Image = UIResMang.GetImage("CheckMark_128");

            this.SelectedGroupBy = GroupBy.None;
            this.SelectedStyleItem.PerformClick();
        }

        private void RefreshMenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                LoadTemplates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 私有方法
        private void UpdateListViewItems(List<Template> templates)
        {
            if (GroupBy.Type == this.SelectedGroupBy)
            {
                this.listView.ShowGroups = true;

                ListViewGroup lvgGroupBox = new ListViewGroup();
                lvgGroupBox.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgGroupBox);

                ListViewGroup lvgBlinds = new ListViewGroup();
                lvgBlinds.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgBlinds);

                ListViewGroup lvgDigitalAdjustment = new ListViewGroup();
                lvgDigitalAdjustment.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgDigitalAdjustment);

                ListViewGroup lvgImageButton = new ListViewGroup();
                lvgImageButton.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgImageButton);

                ListViewGroup lvgLabel = new ListViewGroup();
                lvgLabel.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgLabel);

                ListViewGroup lvgSceneButton = new ListViewGroup();
                lvgSceneButton.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgSceneButton);

                ListViewGroup lvgSliderSwitch = new ListViewGroup();
                lvgSliderSwitch.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgSliderSwitch);

                ListViewGroup lvgSwitch = new ListViewGroup();
                lvgSwitch.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgSwitch);

                ListViewGroup lvgTimer = new ListViewGroup();
                lvgTimer.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgTimer);

                ListViewGroup lvgValueDisplay = new ListViewGroup();
                lvgValueDisplay.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgValueDisplay);

                ListViewGroup lvgUngroup = new ListViewGroup();
                lvgUngroup.HeaderAlignment = HorizontalAlignment.Center;
                this.listView.Groups.Add(lvgUngroup);

                foreach (Template tpl in templates)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = tpl.Name;

                    this.imageListSmall.Images.Add(tpl.Name, tpl.Preview);
                    this.imageListLarge.Images.Add(tpl.Name, tpl.Preview);
                    lvi.ImageKey = tpl.Name;

                    lvi.SubItems.Add(tpl.Version.LastModified);

                    if (1 == tpl.Views.Count)
                    {
                        KNXView view = tpl.Views[0];
                        switch (view.GetType().Name)
                        {
                            case MyConst.View.KnxAppType:
                                break;

                            case MyConst.View.KnxAreaType:
                                break;

                            case MyConst.View.KnxRoomType:
                                break;

                            case MyConst.View.KnxPageType:
                                break;

                            case MyConst.Controls.KnxGroupBoxType:
                                lvgGroupBox.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxBlindsType:
                                lvgBlinds.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxDigitalAdjustmentType:
                                lvgDigitalAdjustment.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxLabelType:
                                lvgLabel.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxSceneButtonType:
                                lvgSceneButton.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxSliderSwitchType:
                                lvgSliderSwitch.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxSwitchType:
                                lvgSwitch.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxTimerButtonType:
                                lvgTimer.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxValueDisplayType:
                                lvgValueDisplay.Items.Add(lvi);
                                break;
                            case MyConst.Controls.KnxImageButtonType:
                                lvgImageButton.Items.Add(lvi);
                                break;

                            default:
                                lvgUngroup.Items.Add(lvi);
                                break;
                        }
                    }
                    else if (tpl.Views.Count > 1)
                    {
                        lvgUngroup.Items.Add(lvi);
                    }

                    this.listView.Items.Add(lvi);
                }

                lvgGroupBox.Header = UIResMang.GetString("TextGroupBox") + "(" + lvgGroupBox.Items.Count + ")";
                lvgBlinds.Header = UIResMang.GetString("TextBlinds") + "(" + lvgBlinds.Items.Count + ")";
                lvgDigitalAdjustment.Header = UIResMang.GetString("TextDigitalAdjustment") + "(" + lvgDigitalAdjustment.Items.Count + ")";
                lvgImageButton.Header = UIResMang.GetString("TextImageButton") + "(" + lvgImageButton.Items.Count + ")";
                lvgLabel.Header = UIResMang.GetString("TextLabel") + "(" + lvgLabel.Items.Count + ")";
                lvgSceneButton.Header = UIResMang.GetString("TextSceneButton") + "(" + lvgSceneButton.Items.Count + ")";
                lvgSliderSwitch.Header = UIResMang.GetString("TextSliderSwitch") + "(" + lvgSliderSwitch.Items.Count + ")";
                lvgSwitch.Header = UIResMang.GetString("TextSwitch") + "(" + lvgSwitch.Items.Count + ")";
                lvgTimer.Header = UIResMang.GetString("TextTimer") + "(" + lvgTimer.Items.Count + ")";
                lvgValueDisplay.Header = UIResMang.GetString("TextValueDisplay") + "(" + lvgValueDisplay.Items.Count + ")";
                lvgUngroup.Header = UIResMang.GetString("Combination") + "(" + lvgUngroup.Items.Count + ")";
            }
            else
            {
                this.listView.ShowGroups = false;

                foreach (Template tpl in templates)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = tpl.Name;

                    this.imageListSmall.Images.Add(tpl.Name, tpl.Preview);
                    this.imageListLarge.Images.Add(tpl.Name, tpl.Preview);
                    lvi.ImageKey = tpl.Name;

                    lvi.SubItems.Add(tpl.Version.LastModified);

                    this.listView.Items.Add(lvi);
                }
            }

            this.Text = UIResMang.GetString("Collections") + "(" + this.listView.Items.Count + ")";
        }

        private void DisplayAsDetails(List<Template> templates)
        {
            this.listView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            this.listView.Clear();
            this.listView.Items.Clear();
            this.imageListLarge.Images.Clear();
            this.imageListSmall.Images.Clear();
            this.listView.Groups.Clear();

            this.listView.View = View.Details;

            this.listView.Columns.Add(UIResMang.GetString("Name"), 120, HorizontalAlignment.Center);
            this.listView.Columns.Add(UIResMang.GetString("LastModifiedTime"), 120, HorizontalAlignment.Center);

            UpdateListViewItems(templates);

            this.listView.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void DisplayAsSamllIcon(List<Template> templates)
        {
            this.listView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            this.listView.Clear();
            this.listView.Items.Clear();
            this.imageListLarge.Images.Clear();
            this.imageListSmall.Images.Clear();
            this.listView.Groups.Clear();

            this.listView.View = View.SmallIcon;

            UpdateListViewItems(templates);

            this.listView.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }

        private void DisplayAsLargeIcon(List<Template> templates)
        {
            this.listView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            this.listView.Clear();
            this.listView.Items.Clear();
            this.imageListLarge.Images.Clear();
            this.imageListSmall.Images.Clear();
            this.listView.Groups.Clear();

            this.listView.View = View.LargeIcon;

            UpdateListViewItems(templates);

            this.listView.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }

        private void MarkSelectedStyle(ToolStripMenuItem SItem)
        {
            foreach (ToolStripMenuItem item in this.ViewStyleItems)
            {
                item.Image = null;
            }

            SItem.Image = UIResMang.GetImage("CheckMark_128");
        }

        private void UnmarkSelectedGroupBy()
        {
            foreach (ToolStripMenuItem item in this.GroupByItems)
            {
                item.Image = null;
            }
        }

        private void LoadTemplates()
        {
            this.RefreshMenuItem.Enabled = false;
            Cursor = Cursors.WaitCursor;

            this.BGWLoadTemplates.RunWorkerAsync();
        }
        #endregion

        #region 公共方法
        public List<ViewNode> GetSelectedNodes()
        {
            if (null != this.SelectedTemplate)
            {
                return CollectionHelper.ImportTemplate(this.SelectedTemplate);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
