using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeDatapointTypeVersion.Version
{
    class VersionNode:TypeDatapointTypeVersionNode
    {
        public VersionNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "DPT version";
        }

        public static TreeNode GetTypeNode()
        {
            VersionNode nodeType = new VersionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
