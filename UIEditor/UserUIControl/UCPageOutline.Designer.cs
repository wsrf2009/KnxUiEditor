namespace UIEditor.UserUIControl
{
    partial class UCPageOutline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPageOutline));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbExpand = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddGroupBox = new System.Windows.Forms.ToolStripButton();
            this.tsbAddBlinds = new System.Windows.Forms.ToolStripButton();
            this.tsbAddLabel = new System.Windows.Forms.ToolStripButton();
            this.tsbAddSceneButton = new System.Windows.Forms.ToolStripButton();
            this.tsbAddSliderSwitch = new System.Windows.Forms.ToolStripButton();
            this.tsbAddSwitch = new System.Windows.Forms.ToolStripButton();
            this.tsbAddValueDisplay = new System.Windows.Forms.ToolStripButton();
            this.tsbAddTimer = new System.Windows.Forms.ToolStripButton();
            this.tsbAddDigitalAdjustment = new System.Windows.Forms.ToolStripButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tvPage = new UIEditor.Drawing.STTreeView();
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
            this.tableLayoutPanel1.Controls.Add(this.tvPage, 0, 2);
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
            this.tsbExpand,
            this.tsbCollapse,
            this.toolStripSeparator1,
            this.tsbMoveUp,
            this.tsbMoveDown,
            this.toolStripSeparator2,
            this.tsbAddGroupBox,
            this.tsbAddBlinds,
            this.tsbAddLabel,
            this.tsbAddSceneButton,
            this.tsbAddSliderSwitch,
            this.tsbAddSwitch,
            this.tsbAddValueDisplay,
            this.tsbAddTimer,
            this.tsbAddDigitalAdjustment});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // tsbExpand
            // 
            this.tsbExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExpand.Image = global::UIEditor.Properties.Resources.Expand_16x16;
            resources.ApplyResources(this.tsbExpand, "tsbExpand");
            this.tsbExpand.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tsbExpand.Name = "tsbExpand";
            this.tsbExpand.Click += new System.EventHandler(this.tsbExpand_Click);
            // 
            // tsbCollapse
            // 
            this.tsbCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollapse.Image = global::UIEditor.Properties.Resources.Collapse_16x16;
            resources.ApplyResources(this.tsbCollapse, "tsbCollapse");
            this.tsbCollapse.Name = "tsbCollapse";
            this.tsbCollapse.Click += new System.EventHandler(this.tsbCollapse_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Image = global::UIEditor.Properties.Resources.MoveUp_16x16;
            resources.ApplyResources(this.tsbMoveUp, "tsbMoveUp");
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Image = global::UIEditor.Properties.Resources.MoveDown_16x16;
            resources.ApplyResources(this.tsbMoveDown, "tsbMoveDown");
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbAddGroupBox
            // 
            this.tsbAddGroupBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddGroupBox.Image = global::UIEditor.Properties.Resources.GroupBox_16x16;
            resources.ApplyResources(this.tsbAddGroupBox, "tsbAddGroupBox");
            this.tsbAddGroupBox.Name = "tsbAddGroupBox";
            this.tsbAddGroupBox.Click += new System.EventHandler(this.tsbAddGroupBox_Click);
            // 
            // tsbAddBlinds
            // 
            this.tsbAddBlinds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddBlinds.Image = global::UIEditor.Properties.Resources.Blinds_16x16;
            resources.ApplyResources(this.tsbAddBlinds, "tsbAddBlinds");
            this.tsbAddBlinds.Name = "tsbAddBlinds";
            this.tsbAddBlinds.Click += new System.EventHandler(this.tsbAddBlinds_Click);
            // 
            // tsbAddLabel
            // 
            this.tsbAddLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddLabel.Image = global::UIEditor.Properties.Resources.Label_16x16;
            resources.ApplyResources(this.tsbAddLabel, "tsbAddLabel");
            this.tsbAddLabel.Name = "tsbAddLabel";
            this.tsbAddLabel.Click += new System.EventHandler(this.tsbAddLabel_Click);
            // 
            // tsbAddSceneButton
            // 
            this.tsbAddSceneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddSceneButton.Image = global::UIEditor.Properties.Resources.Scene_16x16;
            resources.ApplyResources(this.tsbAddSceneButton, "tsbAddSceneButton");
            this.tsbAddSceneButton.Name = "tsbAddSceneButton";
            this.tsbAddSceneButton.Click += new System.EventHandler(this.tsbAddSceneButton_Click);
            // 
            // tsbAddSliderSwitch
            // 
            this.tsbAddSliderSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddSliderSwitch.Image = global::UIEditor.Properties.Resources.SliderSwitch_16x16;
            resources.ApplyResources(this.tsbAddSliderSwitch, "tsbAddSliderSwitch");
            this.tsbAddSliderSwitch.Name = "tsbAddSliderSwitch";
            this.tsbAddSliderSwitch.Click += new System.EventHandler(this.tsbAddSliderSwitch_Click);
            // 
            // tsbAddSwitch
            // 
            this.tsbAddSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddSwitch.Image = global::UIEditor.Properties.Resources.Switch_16x16;
            resources.ApplyResources(this.tsbAddSwitch, "tsbAddSwitch");
            this.tsbAddSwitch.Name = "tsbAddSwitch";
            this.tsbAddSwitch.Click += new System.EventHandler(this.tsbAddSwitch_Click);
            // 
            // tsbAddValueDisplay
            // 
            this.tsbAddValueDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddValueDisplay.Image = global::UIEditor.Properties.Resources.ValueDisplay_16x16;
            resources.ApplyResources(this.tsbAddValueDisplay, "tsbAddValueDisplay");
            this.tsbAddValueDisplay.Name = "tsbAddValueDisplay";
            this.tsbAddValueDisplay.Click += new System.EventHandler(this.tsbAddValueDisplay_Click);
            // 
            // tsbAddTimer
            // 
            this.tsbAddTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddTimer.Image = global::UIEditor.Properties.Resources.Timer_16x16;
            resources.ApplyResources(this.tsbAddTimer, "tsbAddTimer");
            this.tsbAddTimer.Name = "tsbAddTimer";
            this.tsbAddTimer.Click += new System.EventHandler(this.tsbAddTimer_Click);
            // 
            // tsbAddDigitalAdjustment
            // 
            this.tsbAddDigitalAdjustment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddDigitalAdjustment.Image = global::UIEditor.Properties.Resources.DigitalAdjustment_16x16;
            resources.ApplyResources(this.tsbAddDigitalAdjustment, "tsbAddDigitalAdjustment");
            this.tsbAddDigitalAdjustment.Name = "tsbAddDigitalAdjustment";
            this.tsbAddDigitalAdjustment.Click += new System.EventHandler(this.tsbAddDigitalAdjustment_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "KNXPage");
            this.imageList.Images.SetKeyName(1, "KNXBlinds");
            this.imageList.Images.SetKeyName(2, "KNXDigitalAdjustment");
            this.imageList.Images.SetKeyName(3, "KNXGroupBox");
            this.imageList.Images.SetKeyName(4, "KNXImageButton");
            this.imageList.Images.SetKeyName(5, "KNXLabel");
            this.imageList.Images.SetKeyName(6, "KNXSceneButton");
            this.imageList.Images.SetKeyName(7, "KNXShutter");
            this.imageList.Images.SetKeyName(8, "KNXSliderSwitch");
            this.imageList.Images.SetKeyName(9, "KNXSwitch");
            this.imageList.Images.SetKeyName(10, "KNXTimerButton");
            this.imageList.Images.SetKeyName(11, "KNXValueDisplay");
            this.imageList.Images.SetKeyName(12, "KNXDimmer");
            this.imageList.Images.SetKeyName(13, "KNXWebCamer");
            this.imageList.Images.SetKeyName(14, "KNXMediaButton");
            this.imageList.Images.SetKeyName(15, "KNXAirCondition");
            this.imageList.Images.SetKeyName(16, "KNXHVAC");
            // 
            // tvPage
            // 
            this.tvPage.BackColor = System.Drawing.Color.White;
            this.tvPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tvPage, "tvPage");
            this.tvPage.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvPage.FullRowSelect = true;
            this.tvPage.HideSelection = false;
            this.tvPage.HotTracking = true;
            this.tvPage.ImageList = this.imageList;
            this.tvPage.Name = "tvPage";
            this.tvPage.SelectedCollapsed = ((System.Drawing.Image)(resources.GetObject("tvPage.SelectedCollapsed")));
            this.tvPage.SelectedExpanded = ((System.Drawing.Image)(resources.GetObject("tvPage.SelectedExpanded")));
            this.tvPage.UnselectedCollapsed = ((System.Drawing.Image)(resources.GetObject("tvPage.UnselectedCollapsed")));
            this.tvPage.UnselectedExpanded = ((System.Drawing.Image)(resources.GetObject("tvPage.UnselectedExpanded")));
            this.tvPage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvPage_MouseDown);
            // 
            // UCPageOutline
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCPageOutline";
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
        private System.Windows.Forms.ToolStripButton tsbExpand;
        private System.Windows.Forms.ToolStripButton tsbCollapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private Drawing.STTreeView tvPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAddGroupBox;
        private System.Windows.Forms.ToolStripButton tsbAddBlinds;
        private System.Windows.Forms.ToolStripButton tsbAddSceneButton;
        private System.Windows.Forms.ToolStripButton tsbAddLabel;
        private System.Windows.Forms.ToolStripButton tsbAddSliderSwitch;
        private System.Windows.Forms.ToolStripButton tsbAddSwitch;
        private System.Windows.Forms.ToolStripButton tsbAddValueDisplay;
        private System.Windows.Forms.ToolStripButton tsbAddTimer;
        private System.Windows.Forms.ToolStripButton tsbAddDigitalAdjustment;
        private System.Windows.Forms.ImageList imageList;

    }
}
