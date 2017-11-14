using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.ErrorClassSystem
{
    class ErrorClassSystemNode:TypesN8Node
    {
        public ErrorClassSystemNode()
        {
            this.KNXSubNumber = DPST_11;
            this.DPTName = "system error class";
        }

        public static TreeNode GetTypeNode()
        {
            ErrorClassSystemNode nodeType = new ErrorClassSystemNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
