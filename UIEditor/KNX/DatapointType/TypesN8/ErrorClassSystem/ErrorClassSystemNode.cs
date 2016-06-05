using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ErrorClassSystem
{
    class ErrorClassSystemNode:TypesN8Node
    {
        public ErrorClassSystemNode()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "system error class";
        }

        public static TreeNode GetTypeNode()
        {
            ErrorClassSystemNode nodeType = new ErrorClassSystemNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
