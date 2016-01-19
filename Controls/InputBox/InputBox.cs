using System.Windows.Forms;

namespace UIEditor.Controls
{
    /// <summary>
    /// Displays an input box
    /// </summary>
    public static class InputBox
    {
        /// <summary>
        /// Displays an input box with the specified text, caption and default value
        /// </summary>
        /// <param name="text">The text to display in the input box</param>
        /// <param name="caption">The text to display in the title bar of the input box</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="useSystemPasswordChar">True if user input should be masked with a password character</param>
        /// <param name="owner">The owner</param>
        /// <returns>A string entered by the user</returns>
        public static string Show(string text, string caption = "", string defaultValue = "", bool useSystemPasswordChar = false, IWin32Window owner = null)
        {
            using (InputBoxForm inputBoxForm = new InputBoxForm())
            {
                inputBoxForm.Prompt = text;
                inputBoxForm.Title = string.IsNullOrEmpty(caption) ? Application.ProductName : caption;
                inputBoxForm.UseSystemPasswordChar = useSystemPasswordChar;
                inputBoxForm.Value = defaultValue;

                return ((owner != null) ? inputBoxForm.ShowDialog(owner) : inputBoxForm.ShowDialog()) == DialogResult.Cancel ? string.Empty : inputBoxForm.Value;
            }
        }
    }
}
