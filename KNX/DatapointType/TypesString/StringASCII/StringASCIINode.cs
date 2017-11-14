using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesString.StringASCII
{
    class StringASCIINode:TypesStringNode
    {
        public StringASCIINode()
        {
            this.KNXSubNumber = DPST_0;
            this.DPTName = "Character String (ASCII)";
        }

        public static TreeNode GetTypeNode()
        {
            StringASCIINode nodeType = new StringASCIINode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
