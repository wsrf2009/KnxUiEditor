using UIEditor.Component;
namespace UIEditor
{
    partial class FrmGroupAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGroupAddress));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.treeViewDefaultActions = new System.Windows.Forms.TreeView();
            this.buttonDeleteAction = new System.Windows.Forms.Button();
            this.listBoxActios = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonHelper = new System.Windows.Forms.Button();
            this.textBoxActionName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxActionValue = new System.Windows.Forms.ComboBox();
            this.cbxUpgrade = new System.Windows.Forms.CheckBox();
            this.cbxTransmit = new System.Windows.Forms.CheckBox();
            this.cbxCommunication = new System.Windows.Forms.CheckBox();
            this.cbxRead = new System.Windows.Forms.CheckBox();
            this.cbxWrite = new System.Windows.Forms.CheckBox();
            this.txtReadTimespan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtWriteAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnDPTName = new System.Windows.Forms.Button();
            this.flpCommands.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // flpCommands
            // 
            this.flpCommands.Controls.Add(this.btnCancel);
            this.flpCommands.Controls.Add(this.btnOK);
            resources.ApplyResources(this.flpCommands, "flpCommands");
            this.flpCommands.Name = "flpCommands";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAddAction);
            this.groupBox1.Controls.Add(this.treeViewDefaultActions);
            this.groupBox1.Controls.Add(this.buttonDeleteAction);
            this.groupBox1.Controls.Add(this.listBoxActios);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Image = global::UIEditor.Properties.Resources.add;
            resources.ApplyResources(this.buttonAddAction, "buttonAddAction");
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.UseVisualStyleBackColor = true;
            this.buttonAddAction.Click += new System.EventHandler(this.buttonAddAction_Click);
            // 
            // treeViewDefaultActions
            // 
            resources.ApplyResources(this.treeViewDefaultActions, "treeViewDefaultActions");
            this.treeViewDefaultActions.Name = "treeViewDefaultActions";
            this.treeViewDefaultActions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDefaultActions_AfterSelect);
            // 
            // buttonDeleteAction
            // 
            resources.ApplyResources(this.buttonDeleteAction, "buttonDeleteAction");
            this.buttonDeleteAction.Name = "buttonDeleteAction";
            this.buttonDeleteAction.UseVisualStyleBackColor = true;
            this.buttonDeleteAction.Click += new System.EventHandler(this.buttonDeleteAction_Click);
            // 
            // listBoxActios
            // 
            this.listBoxActios.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxActios, "listBoxActios");
            this.listBoxActios.Name = "listBoxActios";
            this.listBoxActios.SelectedIndexChanged += new System.EventHandler(this.listBoxActios_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.buttonHelper, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxActionName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxActionValue, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // buttonHelper
            // 
            resources.ApplyResources(this.buttonHelper, "buttonHelper");
            this.buttonHelper.Name = "buttonHelper";
            this.buttonHelper.UseVisualStyleBackColor = true;
            this.buttonHelper.Click += new System.EventHandler(this.buttonHelper_Click);
            // 
            // textBoxActionName
            // 
            resources.ApplyResources(this.textBoxActionName, "textBoxActionName");
            this.textBoxActionName.Name = "textBoxActionName";
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // comboBoxActionValue
            // 
            resources.ApplyResources(this.comboBoxActionValue, "comboBoxActionValue");
            this.comboBoxActionValue.FormattingEnabled = true;
            this.comboBoxActionValue.Name = "comboBoxActionValue";
            // 
            // cbxUpgrade
            // 
            resources.ApplyResources(this.cbxUpgrade, "cbxUpgrade");
            this.cbxUpgrade.Name = "cbxUpgrade";
            this.cbxUpgrade.UseVisualStyleBackColor = true;
            // 
            // cbxTransmit
            // 
            resources.ApplyResources(this.cbxTransmit, "cbxTransmit");
            this.cbxTransmit.Name = "cbxTransmit";
            this.cbxTransmit.UseVisualStyleBackColor = true;
            // 
            // cbxCommunication
            // 
            resources.ApplyResources(this.cbxCommunication, "cbxCommunication");
            this.cbxCommunication.Name = "cbxCommunication";
            this.cbxCommunication.UseVisualStyleBackColor = true;
            // 
            // cbxRead
            // 
            resources.ApplyResources(this.cbxRead, "cbxRead");
            this.cbxRead.Name = "cbxRead";
            this.cbxRead.UseVisualStyleBackColor = true;
            // 
            // cbxWrite
            // 
            resources.ApplyResources(this.cbxWrite, "cbxWrite");
            this.cbxWrite.Name = "cbxWrite";
            this.cbxWrite.UseVisualStyleBackColor = true;
            // 
            // txtReadTimespan
            // 
            resources.ApplyResources(this.txtReadTimespan, "txtReadTimespan");
            this.txtReadTimespan.Name = "txtReadTimespan";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtDefaultValue
            // 
            resources.ApplyResources(this.txtDefaultValue, "txtDefaultValue");
            this.txtDefaultValue.Name = "txtDefaultValue";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbPriority
            // 
            resources.ApplyResources(this.cmbPriority, "cmbPriority");
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Name = "cmbPriority";
            // 
            // lblPriority
            // 
            resources.ApplyResources(this.lblPriority, "lblPriority");
            this.lblPriority.Name = "lblPriority";
            // 
            // txtID
            // 
            resources.ApplyResources(this.txtID, "txtID");
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtWriteAddress
            // 
            resources.ApplyResources(this.txtWriteAddress, "txtWriteAddress");
            this.txtWriteAddress.Name = "txtWriteAddress";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.cbxRead, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxWrite, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxUpgrade, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxCommunication, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxTransmit, 3, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnDPTName
            // 
            resources.ApplyResources(this.btnDPTName, "btnDPTName");
            this.btnDPTName.Name = "btnDPTName";
            this.btnDPTName.UseVisualStyleBackColor = true;
            this.btnDPTName.Click += new System.EventHandler(this.btnDPTName_Click);
            // 
            // FrmGroupAddress
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnDPTName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReadTimespan);
            this.Controls.Add(this.txtWriteAddress);
            this.Controls.Add(this.flpCommands);
            this.Controls.Add(this.txtDefaultValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGroupAddress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGroupAddress_FormClosing);
            this.Load += new System.EventHandler(this.FrmGroupAddress_Load);
            this.flpCommands.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FlowLayoutPanel flpCommands;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDeleteAction;
        private System.Windows.Forms.ListBox listBoxActios;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxActionName;
        private System.Windows.Forms.Button buttonHelper;
        private System.Windows.Forms.CheckBox cbxUpgrade;
        private System.Windows.Forms.CheckBox cbxTransmit;
        private System.Windows.Forms.CheckBox cbxCommunication;
        private System.Windows.Forms.CheckBox cbxRead;
        private System.Windows.Forms.CheckBox cbxWrite;
        private System.Windows.Forms.TextBox txtReadTimespan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtWriteAddress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAddAction;
        private System.Windows.Forms.TreeView treeViewDefaultActions;
        private System.Windows.Forms.ComboBox comboBoxActionValue;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnDPTName;
    }
}