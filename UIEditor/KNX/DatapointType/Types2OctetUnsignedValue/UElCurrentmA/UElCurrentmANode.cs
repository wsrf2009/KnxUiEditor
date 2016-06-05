using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.UElCurrentmA
{
    class UElCurrentmANode:Types2OctetUnsignedValueNode
    {
        public UElCurrentmANode()
        {
            this.KNXSubNumber = DPST_12;
            this.Name = "current (mA)";
        }

        public static TreeNode GetTypeNode()
        {
            UElCurrentmANode nodeType = new UElCurrentmANode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
