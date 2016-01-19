using System;
using System.Drawing;
using System.Windows.Forms;

namespace UIEditor.Controls
{
    public partial class InputBoxForm : Form
    {
        public InputBoxForm()
        {
            InitializeComponent();
        }

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set { _inputText = value; }
        }

        #region Public Properties
        /// <summary>
        /// Gets or sets the prompt
        /// </summary>
        public string Prompt
        {
            get { return labelTips.Text; }
            set
            {
                labelTips.Text = value;

                using (Graphics graphics = CreateGraphics())
                {
                    SizeF size = graphics.MeasureString(value, labelTips.Font, labelTips.Width);

                    if (size.Height > labelTips.Height)
                    {
                        Height += (int)size.Height - labelTips.Height;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating wether to use a password character or not
        /// </summary>
        public bool UseSystemPasswordChar
        {
            get { return textBoxInputData.UseSystemPasswordChar; }
            set { textBoxInputData.UseSystemPasswordChar = value; }
        }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value
        {
            get { return textBoxInputData.Text; }
            set
            {
                if (!value.Equals(textBoxInputData.Text))
                {
                    textBoxInputData.Text = value;
                    textBoxInputData.SelectionStart = 0;
                    textBoxInputData.SelectionLength = textBoxInputData.Text.Length;
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Fires the <see cref="Form.Shown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            BringToFront();
            Focus();
            textBoxInputData.Focus();
        }
        #endregion


        ////显示InputBox 
        //public DialogResult Show(string message, string caption = "", string defaultValue = "")
        //{
        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        this.labelTips.Text = message;
        //    }

        //    if (!string.IsNullOrEmpty(caption))
        //    {
        //        this.Text = caption;
        //    }

        //    if (!string.IsNullOrEmpty(defaultValue))
        //    {
        //        this.textBoxInputData.Text = defaultValue;
        //    }

        //    this.ShowDialog();

        //    return this.DialogResult;
        //}

        //private void buttonEnter_Click(object sender, EventArgs e)
        //{
        //    this.InputText = this.textBoxInputData.Text.Trim();
        //    this.Close();
        //}
    }
}
