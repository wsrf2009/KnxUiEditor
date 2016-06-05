using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeLanguageCodeISO6391.LanguageCodeAlpha2ASCII
{
    class LanguageCodeAlpha2ASCIINode:TypeLanguageCodeISO6391Node
    {
        public LanguageCodeAlpha2ASCIINode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "language code (ASCII)";
        }

        public static TreeNode GetTypeNode()
        {
            LanguageCodeAlpha2ASCIINode nodeType = new LanguageCodeAlpha2ASCIINode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
