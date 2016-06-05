using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.RFFilterInfo
{
    class RFFilterInfoNode:TypeB8Node
    {
        public RFFilterInfoNode()
        {
            this.KNXSubNumber = DPST_1001;
            this.Name = "cEMI server supported RF filtering modes";
        }

        public static TreeNode GetTypeNode()
        {
            RFFilterInfoNode nodeType = new RFFilterInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
