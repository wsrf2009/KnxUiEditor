using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesString.StringASCII
{
    class StringASCIINode:TypesStringNode
    {
        public StringASCIINode()
        {
            this.KNXSubNumber = DPST_0;
            this.Name = "Character String (ASCII)";
        }

        public static TreeNode GetTypeNode()
        {
            StringASCIINode nodeType = new StringASCIINode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
