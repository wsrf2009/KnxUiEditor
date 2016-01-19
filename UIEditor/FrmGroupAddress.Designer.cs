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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.treeViewDefaultActions = new System.Windows.Forms.TreeView();
            this.textBoxTip = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxActionName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonHelper = new System.Windows.Forms.Button();
            this.comboBoxActionValue = new System.Windows.Forms.ComboBox();
            this.buttonDeleteAction = new System.Windows.Forms.Button();
            this.listBoxActios = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
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
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWireNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtWriteAddress = new System.Windows.Forms.TextBox();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flpCommands.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(174, 3);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0, 3, 50, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // flpCommands
            // 
            this.flpCommands.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flpCommands.Controls.Add(this.btnCancel);
            this.flpCommands.Controls.Add(this.btnOK);
            this.flpCommands.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpCommands.Location = new System.Drawing.Point(0, 525);
            this.flpCommands.Name = "flpCommands";
            this.flpCommands.Size = new System.Drawing.Size(394, 32);
            this.flpCommands.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAddAction);
            this.groupBox1.Controls.Add(this.treeViewDefaultActions);
            this.groupBox1.Controls.Add(this.textBoxTip);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.buttonDeleteAction);
            this.groupBox1.Controls.Add(this.listBoxActios);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(0, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 267);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操控行为";
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Image = global::UIEditor.Properties.Resources.add;
            this.buttonAddAction.Location = new System.Drawing.Point(175, 76);
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.Size = new System.Drawing.Size(54, 25);
            this.buttonAddAction.TabIndex = 71;
            this.buttonAddAction.UseVisualStyleBackColor = true;
            this.buttonAddAction.Click += new System.EventHandler(this.buttonAddAction_Click);
            // 
            // treeViewDefaultActions
            // 
            this.treeViewDefaultActions.Location = new System.Drawing.Point(6, 73);
            this.treeViewDefaultActions.Name = "treeViewDefaultActions";
            this.treeViewDefaultActions.Size = new System.Drawing.Size(162, 149);
            this.treeViewDefaultActions.TabIndex = 70;
            this.treeViewDefaultActions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDefaultActions_AfterSelect);
            // 
            // textBoxTip
            // 
            this.textBoxTip.Location = new System.Drawing.Point(89, 18);
            this.textBoxTip.Multiline = true;
            this.textBoxTip.Name = "textBoxTip";
            this.textBoxTip.Size = new System.Drawing.Size(299, 49);
            this.textBoxTip.TabIndex = 69;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxActionName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonHelper, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxActionValue, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 230);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(385, 32);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // textBoxActionName
            // 
            this.textBoxActionName.Location = new System.Drawing.Point(3, 3);
            this.textBoxActionName.Name = "textBoxActionName";
            this.textBoxActionName.Size = new System.Drawing.Size(131, 21);
            this.textBoxActionName.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(277, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(74, 24);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "添加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonHelper
            // 
            this.buttonHelper.Image = global::UIEditor.Properties.Resources.Help_16x16;
            this.buttonHelper.Location = new System.Drawing.Point(357, 3);
            this.buttonHelper.Name = "buttonHelper";
            this.buttonHelper.Size = new System.Drawing.Size(25, 23);
            this.buttonHelper.TabIndex = 5;
            this.buttonHelper.UseVisualStyleBackColor = true;
            this.buttonHelper.Click += new System.EventHandler(this.buttonHelper_Click);
            // 
            // comboBoxActionValue
            // 
            this.comboBoxActionValue.FormattingEnabled = true;
            this.comboBoxActionValue.Location = new System.Drawing.Point(140, 3);
            this.comboBoxActionValue.Name = "comboBoxActionValue";
            this.comboBoxActionValue.Size = new System.Drawing.Size(131, 20);
            this.comboBoxActionValue.TabIndex = 6;
            // 
            // buttonDeleteAction
            // 
            this.buttonDeleteAction.Image = global::UIEditor.Properties.Resources.remove;
            this.buttonDeleteAction.Location = new System.Drawing.Point(175, 123);
            this.buttonDeleteAction.Name = "buttonDeleteAction";
            this.buttonDeleteAction.Size = new System.Drawing.Size(54, 24);
            this.buttonDeleteAction.TabIndex = 1;
            this.buttonDeleteAction.UseVisualStyleBackColor = true;
            this.buttonDeleteAction.Click += new System.EventHandler(this.buttonDeleteAction_Click);
            // 
            // listBoxActios
            // 
            this.listBoxActios.FormattingEnabled = true;
            this.listBoxActios.ItemHeight = 12;
            this.listBoxActios.Location = new System.Drawing.Point(235, 75);
            this.listBoxActios.Name = "listBoxActios";
            this.listBoxActios.Size = new System.Drawing.Size(153, 148);
            this.listBoxActios.TabIndex = 0;
            this.listBoxActios.SelectedIndexChanged += new System.EventHandler(this.listBoxActios_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 68;
            this.label8.Text = "操控时的提示";
            // 
            // cbxUpgrade
            // 
            this.cbxUpgrade.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxUpgrade.AutoSize = true;
            this.cbxUpgrade.Location = new System.Drawing.Point(165, 3);
            this.cbxUpgrade.Name = "cbxUpgrade";
            this.cbxUpgrade.Size = new System.Drawing.Size(48, 16);
            this.cbxUpgrade.TabIndex = 17;
            this.cbxUpgrade.Text = "更新";
            this.cbxUpgrade.UseVisualStyleBackColor = true;
            // 
            // cbxTransmit
            // 
            this.cbxTransmit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxTransmit.AutoSize = true;
            this.cbxTransmit.Location = new System.Drawing.Point(3, 3);
            this.cbxTransmit.Name = "cbxTransmit";
            this.cbxTransmit.Size = new System.Drawing.Size(48, 16);
            this.cbxTransmit.TabIndex = 16;
            this.cbxTransmit.Text = "传送";
            this.cbxTransmit.UseVisualStyleBackColor = true;
            // 
            // cbxCommunication
            // 
            this.cbxCommunication.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxCommunication.AutoSize = true;
            this.cbxCommunication.Location = new System.Drawing.Point(219, 3);
            this.cbxCommunication.Name = "cbxCommunication";
            this.cbxCommunication.Size = new System.Drawing.Size(48, 16);
            this.cbxCommunication.TabIndex = 13;
            this.cbxCommunication.Text = "通讯";
            this.cbxCommunication.UseVisualStyleBackColor = true;
            // 
            // cbxRead
            // 
            this.cbxRead.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxRead.AutoSize = true;
            this.cbxRead.Location = new System.Drawing.Point(57, 3);
            this.cbxRead.Name = "cbxRead";
            this.cbxRead.Size = new System.Drawing.Size(36, 16);
            this.cbxRead.TabIndex = 14;
            this.cbxRead.Text = "读";
            this.cbxRead.UseVisualStyleBackColor = true;
            // 
            // cbxWrite
            // 
            this.cbxWrite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxWrite.AutoSize = true;
            this.cbxWrite.Location = new System.Drawing.Point(111, 3);
            this.cbxWrite.Name = "cbxWrite";
            this.cbxWrite.Size = new System.Drawing.Size(36, 16);
            this.cbxWrite.TabIndex = 15;
            this.cbxWrite.Text = "写";
            this.cbxWrite.UseVisualStyleBackColor = true;
            // 
            // txtReadTimespan
            // 
            this.txtReadTimespan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtReadTimespan.Location = new System.Drawing.Point(98, 163);
            this.txtReadTimespan.Name = "txtReadTimespan";
            this.txtReadTimespan.Size = new System.Drawing.Size(120, 21);
            this.txtReadTimespan.TabIndex = 67;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 66;
            this.label4.Text = "读间隔时间：";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDefaultValue.Location = new System.Drawing.Point(98, 113);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(120, 21);
            this.txtDefaultValue.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 65;
            this.label7.Text = "默认值：";
            // 
            // cmbPriority
            // 
            this.cmbPriority.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(98, 138);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(120, 20);
            this.cmbPriority.TabIndex = 11;
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(39, 141);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(53, 12);
            this.lblPriority.TabIndex = 63;
            this.lblPriority.Text = "优先级：";
            // 
            // cmbType
            // 
            this.cmbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(98, 88);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(120, 20);
            this.cmbType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "数据类型：";
            // 
            // txtWireNumber
            // 
            this.txtWireNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWireNumber.Location = new System.Drawing.Point(98, 188);
            this.txtWireNumber.Name = "txtWireNumber";
            this.txtWireNumber.Size = new System.Drawing.Size(273, 21);
            this.txtWireNumber.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "电缆编号：";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(98, 13);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(273, 21);
            this.txtID.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "地址：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(98, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(273, 21);
            this.txtName.TabIndex = 6;
            // 
            // txtWriteAddress
            // 
            this.txtWriteAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWriteAddress.Location = new System.Drawing.Point(98, 63);
            this.txtWriteAddress.Name = "txtWriteAddress";
            this.txtWriteAddress.Size = new System.Drawing.Size(273, 21);
            this.txtWriteAddress.TabIndex = 7;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpTop.Controls.Add(this.txtWriteAddress, 1, 2);
            this.tlpTop.Controls.Add(this.txtName, 1, 1);
            this.tlpTop.Controls.Add(this.label1, 0, 0);
            this.tlpTop.Controls.Add(this.label2, 0, 1);
            this.tlpTop.Controls.Add(this.label3, 0, 2);
            this.tlpTop.Controls.Add(this.txtID, 1, 0);
            this.tlpTop.Controls.Add(this.label6, 0, 7);
            this.tlpTop.Controls.Add(this.txtWireNumber, 1, 7);
            this.tlpTop.Controls.Add(this.label5, 0, 3);
            this.tlpTop.Controls.Add(this.cmbType, 1, 3);
            this.tlpTop.Controls.Add(this.lblPriority, 0, 5);
            this.tlpTop.Controls.Add(this.cmbPriority, 1, 5);
            this.tlpTop.Controls.Add(this.label7, 0, 4);
            this.tlpTop.Controls.Add(this.txtDefaultValue, 1, 4);
            this.tlpTop.Controls.Add(this.label4, 0, 6);
            this.tlpTop.Controls.Add(this.txtReadTimespan, 1, 6);
            this.tlpTop.Controls.Add(this.tableLayoutPanel2, 1, 8);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Margin = new System.Windows.Forms.Padding(5);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.Padding = new System.Windows.Forms.Padding(3, 10, 20, 5);
            this.tlpTop.RowCount = 9;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTop.Size = new System.Drawing.Size(394, 243);
            this.tlpTop.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.cbxTransmit, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxRead, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxWrite, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxUpgrade, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxCommunication, 4, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(98, 213);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(273, 22);
            this.tableLayoutPanel2.TabIndex = 68;
            // 
            // FrmGroupAddress
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 556);
            this.Controls.Add(this.flpCommands);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tlpTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGroupAddress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组地址编辑器";
            this.Load += new System.EventHandler(this.FrmGroupAddress_Load);
            this.flpCommands.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox textBoxTip;
        private System.Windows.Forms.Label label8;
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
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWireNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtWriteAddress;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAddAction;
        private System.Windows.Forms.TreeView treeViewDefaultActions;
        private System.Windows.Forms.ComboBox comboBoxActionValue;
    }
}