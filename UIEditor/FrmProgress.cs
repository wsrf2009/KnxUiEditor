using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor
{
    public partial class FrmProgress : Form
    {
        private BackgroundWorker backgroundWorker; //ProcessForm 窗体事件(进度条窗体) 

        public FrmProgress(BackgroundWorker worker)
        {
            InitializeComponent();

            this.backgroundWorker = worker;
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
        }

        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.labelText.Text = (null != e.UserState) ? e.UserState.ToString() : "";
        }
    }
}
