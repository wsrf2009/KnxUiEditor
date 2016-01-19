using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UIEditor.Controls
{
    /// <summary>
    /// Respresents a standard dialog that can be used as a base for custom dialog forms.
    /// </summary>
    public partial class DialogForm : Form
    {
        #region Contructor
        /// <summary>
        /// Creates a new <see cref="DialogForm"/>
        /// </summary>
        public DialogForm()
        {
            InitializeComponent();

            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
        }
        #endregion

        #region Private Fields
        private DialogFormButtonDock _ButtonDock = DialogFormButtonDock.Bottom;
        private Padding _ButtonPadding = new Padding(0);
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating how to dock the buttons.
        /// </summary>
        public DialogFormButtonDock ButtonDock
        {
            get { return _ButtonDock; }
            set { _ButtonDock = value; SetupButtons(); }
        }

        /// <summary>
        /// Gets or sets additional padding around the buttons.
        /// </summary>
        public Padding ButtonPadding
        {
            get { return _ButtonPadding; }
            set { _ButtonPadding = value; SetupButtons(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool MaximizeBox
        {
            get { return base.MaximizeBox; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowIcon
        {
            get { return base.ShowIcon; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the form is displayed in the Windows taskbar.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowInTaskbar
        {
            get { return base.ShowInTaskbar; }
        }
        #endregion

        #region Private Methods
        private void SetupButtons()
        {
            tlpButtons.Controls.Clear();
            tlpButtons.ColumnCount = 0;
            tlpButtons.ColumnStyles.Clear();
            tlpButtons.RowCount = 0;
            tlpButtons.RowStyles.Clear();

            switch (ButtonDock)
            {
                case DialogFormButtonDock.Bottom:
                    {
                        tlpButtons.ColumnCount = 6;
                        tlpButtons.Dock = DockStyle.Bottom;
                        tlpButtons.Height = 42;
                        tlpButtons.RowCount = 3;

                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9 + ButtonPadding.Left));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9 + ButtonPadding.Right));

                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 9 + ButtonPadding.Top));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 9 + ButtonPadding.Bottom));

                        tlpButtons.Controls.Add(btnOk, 2, 1);
                        tlpButtons.Controls.Add(btnCancel, 4, 1);
                        break;
                    }
                case DialogFormButtonDock.Right:
                    {
                        tlpButtons.ColumnCount = 3;
                        tlpButtons.Dock = DockStyle.Right;
                        tlpButtons.RowCount = 6;
                        tlpButtons.Width = 93;

                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9 + ButtonPadding.Left));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                        tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9 + ButtonPadding.Right));

                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 9 + ButtonPadding.Top));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 9));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 9 + ButtonPadding.Bottom));

                        tlpButtons.Controls.Add(btnOk, 1, 1);
                        tlpButtons.Controls.Add(btnCancel, 1, 3);
                        break;                        
                    }
            }
        }
        #endregion

        #region Event Handlers
        private void _ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            base.Close();
        }

        private void _ButtonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            base.Close();
        }
        #endregion

        #region Public Methods
        [Obsolete("A form that inherits from DialogForm can only use the ShowDialog() method!")]
        public new void Close()
        {
            throw new NotImplementedException("A form that inherits from DialogForm can only use the ShowDialog() method!");
        }

        [Obsolete("A form that inherits from DialogForm can only use the ShowDialog() method!")]
        public new void Hide()
        {
            throw new NotImplementedException("A form that inherits from DialogForm can only use the ShowDialog() method!");
        }

        [Obsolete("A form that inherits from DialogForm can only use the ShowDialog() method!")]
        public new void Show()
        {
            throw new NotImplementedException("A form that inherits from DialogForm can only use the ShowDialog() method!");
        }

        [Obsolete("A form that inherits from DialogForm can only use the ShowDialog() method!")]
        public new void Show(IWin32Window window)
        {
            throw new NotImplementedException("A form that inherits from DialogForm can only use the ShowDialog() method!");
        }
        #endregion
    }
}
