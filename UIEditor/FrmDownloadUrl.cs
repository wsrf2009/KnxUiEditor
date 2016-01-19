using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor
{
    public partial class FrmDownloadUrl : Form
    {
        public string DownloadUri { get; set; }

        public FrmDownloadUrl()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void FrmDownloadUrl_Load(object sender, EventArgs e)
        {
            if (DownloadUri != null)
            {
                lklDownload.Text = DownloadUri;
            }
        }

        private void lklDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Explorer.OpenIE(DownloadUri);
        }
    }
}
