using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.UElCurrentmA
{
    class UElCurrentmANode:Types2OctetUnsignedValueNode
    {
        public UElCurrentmANode()
        {
            this.KNXSubNumber = DPST_12;
            this.DPTName = "current (mA)";
        }

        public static TreeNode GetTypeNode()
        {
            UElCurrentmANode nodeType = new UElCurrentmANode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
