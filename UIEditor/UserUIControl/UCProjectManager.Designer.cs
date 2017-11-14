namespace UIEditor.UserUIControl
{
    partial class UCProjectManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCProjectManager));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsrBtnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrBtnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrBtnAddArea = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddRoom = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddPage = new System.Windows.Forms.ToolStripButton();
            this.tvProject = new UIEditor.Drawing.STTreeView();
            this.imageListTreeViewProject = new System.Windows.Forms.ImageList(this.components);
            this.cmsEditControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsGroupBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tvProject, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsrBtnExpandAll,
            this.tsrBtnCollapseAll,
            this.toolStripSeparator1,
            this.tsrBtnMoveUp,
            this.tsrBtnMoveDown,
            this.toolStripSeparator2,
            this.tsrBtnAddArea,
            this.tsrBtnAddRoom,
            this.tsrBtnAddPage});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // tsrBtnExpandAll
            // 
            this.tsrBtnExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnExpandAll.Image = global::UIEditor.Properties.Resources.Expand_16x16;
            this.tsrBtnExpandAll.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsrBtnExpandAll.Name = "tsrBtnExpandAll";
            resources.ApplyResources(this.tsrBtnExpandAll, "tsrBtnExpandAll");
            this.tsrBtnExpandAll.Click += new System.EventHandler(this.tsrBtnExpandAll_Click);
            // 
            // tsrBtnCollapseAll
            // 
            this.tsrBtnCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnCollapseAll.Image = global::UIEditor.Properties.Resources.Collapse_16x16;
            this.tsrBtnCollapseAll.Name = "tsrBtnCollapseAll";
            resources.ApplyResources(this.tsrBtnCollapseAll, "tsrBtnCollapseAll");
            this.tsrBtnCollapseAll.Click += new System.EventHandler(this.tsrBtnCollapseAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsrBtnMoveUp
            // 
            this.tsrBtnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnMoveUp.Image = global::UIEditor.Properties.Resources.MoveUp_16x16;
            this.tsrBtnMoveUp.Name = "tsrBtnMoveUp";
            resources.ApplyResources(this.tsrBtnMoveUp, "tsrBtnMoveUp");
            this.tsrBtnMoveUp.Click += new System.EventHandler(this.tsrBtnMoveUp_Click);
            // 
            // tsrBtnMoveDown
            // 
            this.tsrBtnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnMoveDown.Image = global::UIEditor.Properties.Resources.MoveDown_16x16;
            this.tsrBtnMoveDown.Name = "tsrBtnMoveDown";
            resources.ApplyResources(this.tsrBtnMoveDown, "tsrBtnMoveDown");
            this.tsrBtnMoveDown.Click += new System.EventHandler(this.tsrBtnMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsrBtnAddArea
            // 
            this.tsrBtnAddArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddArea.Image = global::UIEditor.Properties.Resources.Area_16x16;
            resources.ApplyResources(this.tsrBtnAddArea, "tsrBtnAddArea");
            this.tsrBtnAddArea.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.tsrBtnAddArea.Name = "tsrBtnAddArea";
            this.tsrBtnAddArea.Click += new System.EventHandler(this.tsrBtnAddArea_Click);
            // 
            // tsrBtnAddRoom
            // 
            this.tsrBtnAddRoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddRoom.Image = global::UIEditor.Properties.Resources.Room_16x16;
            resources.ApplyResources(this.tsrBtnAddRoom, "tsrBtnAddRoom");
            this.tsrBtnAddRoom.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.tsrBtnAddRoom.Name = "tsrBtnAddRoom";
            this.tsrBtnAddRoom.Click += new System.EventHandler(this.tsrBtnAddRoom_Click);
            // 
            // tsrBtnAddPage
            // 
            this.tsrBtnAddPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddPage.Image = global::UIEditor.Properties.Resources.Page_16x16;
            resources.ApplyResources(this.tsrBtnAddPage, "tsrBtnAddPage");
            this.tsrBtnAddPage.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.tsrBtnAddPage.Name = "tsrBtnAddPage";
            this.tsrBtnAddPage.Click += new System.EventHandler(this.tsrBtnAddPage_Click);
            // 
            // tvProject
            // 
            this.tvProject.BackColor = System.Drawing.Color.White;
            this.tvProject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tvProject, "tvProject");
            this.tvProject.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvProject.FullRowSelect = true;
            this.tvProject.HideSelection = false;
            this.tvProject.HotTracking = true;
            this.tvProject.ImageList = this.imageListTreeViewProject;
            this.tvProject.Name = "tvProject";
            this.tvProject.SelectedCollapsed = ((System.Drawing.Image)(resources.GetObject("tvProject.SelectedCollapsed")));
            this.tvProject.SelectedExpanded = ((System.Drawing.Image)(resources.GetObject("tvProject.SelectedExpanded")));
            this.tvProject.UnselectedCollapsed = ((System.Drawing.Image)(resources.GetObject("tvProject.UnselectedCollapsed")));
            this.tvProject.UnselectedExpanded = ((System.Drawing.Image)(resources.GetObject("tvProject.UnselectedExpanded")));
            this.tvProject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvProject_KeyUp);
            this.tvProject.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvProject_MouseDoubleClick);
            this.tvProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProject_MouseDown);
            // 
            // imageListTreeViewProject
            // 
            this.imageListTreeViewProject.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeViewProject.ImageStream")));
            this.imageListTreeViewProject.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeViewProject.Images.SetKeyName(0, "KNXApp");
            this.imageListTreeViewProject.Images.SetKeyName(1, "KNXArea");
            this.imageListTreeViewProject.Images.SetKeyName(2, "KNXRoom");
            this.imageListTreeViewProject.Images.SetKeyName(3, "KNXPage");
            this.imageListTreeViewProject.Images.SetKeyName(4, "KNXGroupBox");
            this.imageListTreeViewProject.Images.SetKeyName(5, "KNXBlinds");
            this.imageListTreeViewProject.Images.SetKeyName(6, "KNXLabel");
            this.imageListTreeViewProject.Images.SetKeyName(7, "KNXSceneButton");
            this.imageListTreeViewProject.Images.SetKeyName(8, "KNXSliderSwitch");
            this.imageListTreeViewProject.Images.SetKeyName(9, "KNXSwitch");
            this.imageListTreeViewProject.Images.SetKeyName(10, "KNXTimerButton");
            this.imageListTreeViewProject.Images.SetKeyName(11, "KNXValueDisplay");
            this.imageListTreeViewProject.Images.SetKeyName(12, "KNXDigitalAdjustment");
            // 
            // cmsEditControl
            // 
            this.cmsEditControl.Name = "cmsEditControl";
            resources.ApplyResources(this.cmsEditControl, "cmsEditControl");
            // 
            // cmsGroupBox
            // 
            this.cmsGroupBox.Name = "cmsGroupBox";
            resources.ApplyResources(this.cmsGroupBox, "cmsGroupBox");
            // 
            // cmsControl
            // 
            this.cmsControl.Name = "cmsControl";
            resources.ApplyResources(this.cmsControl, "cmsControl");
            // 
            // UCProjectManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCProjectManager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsrBtnExpandAll;
        private System.Windows.Forms.ToolStripButton tsrBtnCollapseAll;
        private System.Windows.Forms.ToolStripButton tsrBtnMoveUp;
        private System.Windows.Forms.ToolStripButton tsrBtnMoveDown;
        private System.Windows.Forms.ContextMenuStrip cmsEditControl;
        private System.Windows.Forms.ContextMenuStrip cmsGroupBox;
        private System.Windows.Forms.ContextMenuStrip cmsControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsrBtnAddArea;
        private System.Windows.Forms.ToolStripButton tsrBtnAddRoom;
        private System.Windows.Forms.ToolStripButton tsrBtnAddPage;
        private System.Windows.Forms.ImageList imageListTreeViewProject;
        private Drawing.STTreeView tvProject;
    }
}
