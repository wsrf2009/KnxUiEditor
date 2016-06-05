using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.RFModeSelect
{
    class RFModeSelectNode:TypesN8Node
    {
        public RFModeSelectNode()
        {
            this.KNXSubNumber = DPST_1002;
            this.Name = "RF mode selection";
        }

        public static TreeNode GetTypeNode()
        {
            RFModeSelectNode nodeType = new RFModeSelectNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
