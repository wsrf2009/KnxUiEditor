using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesCharacterSet.CharASCII
{
    class CharASCIINode:TypesCharacterSetNode
    {
        public CharASCIINode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "character (ASCII)";
        }

        public static TreeNode GetTypeNode()
        {
            CharASCIINode nodeType = new CharASCIINode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
