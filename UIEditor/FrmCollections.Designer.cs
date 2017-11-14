namespace UIEditor
{
    partial class FrmCollections
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCollections));
            this.listView = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.BGWLoadTemplates = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.LargeImageList = this.imageListLarge;
            this.listView.Name = "listView";
            this.listView.SmallImageList = this.imageListSmall;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageListLarge, "imageListLarge");
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "KNXApp");
            this.imageListSmall.Images.SetKeyName(1, "KNXArea");
            this.imageListSmall.Images.SetKeyName(2, "KNXRoom");
            this.imageListSmall.Images.SetKeyName(3, "KNXPage");
            this.imageListSmall.Images.SetKeyName(4, "KNXBlinds");
            this.imageListSmall.Images.SetKeyName(5, "KNXDigitalAdjustment");
            this.imageListSmall.Images.SetKeyName(6, "KNXGroupBox");
            this.imageListSmall.Images.SetKeyName(7, "KNXLabel");
            this.imageListSmall.Images.SetKeyName(8, "KNXSceneButton");
            this.imageListSmall.Images.SetKeyName(9, "KNXSliderSwitch");
            this.imageListSmall.Images.SetKeyName(10, "KNXSwitch");
            this.imageListSmall.Images.SetKeyName(11, "KNXTimerButton");
            this.imageListSmall.Images.SetKeyName(12, "KNXValueDisplay");
            this.imageListSmall.Images.SetKeyName(13, "KNXImageButton");
            // 
            // BGWLoadTemplates
            // 
            this.BGWLoadTemplates.WorkerReportsProgress = true;
            this.BGWLoadTemplates.WorkerSupportsCancellation = true;
            this.BGWLoadTemplates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWLoadTemplates_DoWork);
            this.BGWLoadTemplates.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWLoadTemplates_RunWorkerCompleted);
            // 
            // FrmCollections
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCollections";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.ComponentModel.BackgroundWorker BGWLoadTemplates;
    }
}