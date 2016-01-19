using System;
using System.Drawing;
using System.Windows.Forms;
using Structure;
using UIEditor.Entity;
using UIEditor.Entity.Control;

namespace UIEditor
{
    public partial class FrmPreview : Form
    {
        /// <summary>
        /// 屏幕尺寸
        /// </summary>
        public Size ScreenSize { get; set; }

        /// <summary>
        /// 需要显示的 page node
        /// </summary>
        public PageNode SelectedNode { get; set; }

        public FrmPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 创建一个例子按钮
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private Button CreateBlock(string title, ContentAlignment textAlign = ContentAlignment.MiddleCenter)
        {
            var block = new Button();
            block.Size = new Size(80, 25);
            block.Text = title;
            block.FlatStyle = FlatStyle.Flat;
            block.BackColor = Color.Silver;
            block.TextAlign = textAlign;
            block.Dock = DockStyle.Fill;

            return block;
        }

        /// <summary>
        /// 添加控件到 TablePanel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="node"></param>
        private void AddControls(TableLayoutPanel panel, TreeNode node)
        {
            var baseNode = node as ControlBaseNode;

            if (baseNode != null)
            {
                var knxControlBase = new KNXControlBase();
                baseNode.ToKnx(knxControlBase);
                Button control;
                if (node.Name == MyConst.Controls.KnxLabelType)
                {
                    var textAlign = ContentAlignment.MiddleLeft;
                    var labelNode = node as LabelNode;
                    if (labelNode != null)
                    {
                        textAlign = labelNode.TextAlign;
                    }
                    control = CreateBlock(node.Text, textAlign);
                }
                else
                {
                    control = CreateBlock(node.Text);
                }
                panel.Controls.Add(control, baseNode.Column - 1, baseNode.Row - 1);
                panel.SetColumnSpan(control, knxControlBase.ColumnSpan);
                panel.SetRowSpan(control, knxControlBase.RowSpan);
            }
        }

        /// <summary>
        /// 根据行列创建一个 TablePanel
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        private TableLayoutPanel CreateTablePanel(int rowCount, int columnCount)
        {
            var tlpLayout = new TableLayoutPanel();

            tlpLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpLayout.Dock = DockStyle.Fill;
            tlpLayout.RowCount = rowCount;
            tlpLayout.ColumnCount = columnCount;

            // 设置行高
            for (int i = 0; i < rowCount; i++)
            {
                tlpLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }

            // 设置列宽
            for (int i = 0; i < columnCount; i++)
            {
                tlpLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }

            //设置 tablelayoutpanel 控件的 DoubleBuffered 属性为 true，这样可以减少或消除由于不断重绘所显示图面的某些部分而导致的闪烁
            tlpLayout.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tlpLayout, true, null);

            return tlpLayout;
        }
        private void FrmPreview_Load(object sender, EventArgs e)
        {
            // 调整窗体大小
            if (ScreenSize.Height > 300 && ScreenSize.Width > 400)
            {
                this.Height = ScreenSize.Height;
                this.Width = ScreenSize.Width;
            }

            BuilderControles();
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPreview_Activated(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void BuilderControles()
        {
            // 
            if (SelectedNode != null && SelectedNode.Name == MyConst.View.KnxPageType)
            {
                this.panLayout.SuspendLayout();

                var page = SelectedNode.ToKnx();
                var pagePanel = CreateTablePanel(page.RowCount, page.ColumnCount);

                if (SelectedNode.Nodes.Count > 0)
                {
                    // 添加 grid
                    foreach (TreeNode item1 in SelectedNode.Nodes)
                    {
                        if (MyConst.View.KnxGridType == item1.Name)
                        {
                            var gridNode = item1 as GridNode;
                            if (gridNode != null)
                            {
                                var grid = gridNode.ToKnx();
                                var tablePanel = CreateTablePanel(grid.RowCount, grid.ColumnCount);
                                tablePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
                                pagePanel.Controls.Add(tablePanel, grid.Column, grid.Row);
                                pagePanel.SetRowSpan(tablePanel, grid.RowSpan);
                                pagePanel.SetColumnSpan(tablePanel, grid.ColumnSpan);

                                if (item1.Nodes.Count > 0)
                                {
                                    // 添加控件
                                    foreach (TreeNode item2 in item1.Nodes)
                                    {
                                        AddControls(tablePanel, item2);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 添加控件
                            AddControls(pagePanel, item1);
                        }
                    }
                }

                this.panLayout.Controls.Add(pagePanel);

                this.panLayout.ResumeLayout();
            }
        }

        public void RefreshPreview()
        {
            this.panLayout.Controls.Clear();
            BuilderControles();
        }



    }
}

