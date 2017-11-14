using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesString.String88591
{
    class String88591Node:TypesStringNode
    {
        public String88591Node()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "Character String (ISO 8859-1)";
        }

        public static TreeNode GetTypeNode()
        {
            String88591Node nodeType = new String88591Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
