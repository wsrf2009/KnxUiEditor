using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeDatapointTypeVersion.Version
{
    class VersionNode:TypeDatapointTypeVersionNode
    {
        public VersionNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "DPT version";
        }

        public static TreeNode GetTypeNode()
        {
            VersionNode nodeType = new VersionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
