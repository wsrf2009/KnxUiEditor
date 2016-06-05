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
            //this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        //void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    this.Close();//执行完之后，直接关闭页面
        //}

        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.labelText.Text = (null != e.UserState) ? e.UserState.ToString() : "";
            //this.progressBar1.Value = e.ProgressPercentage;
            //this.textBox1.AppendText(e.UserState.ToString());//主窗体传过来的值，通过e.UserState.ToString()来接受
        }
    }
}
