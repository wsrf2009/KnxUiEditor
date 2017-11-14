using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetSignedValue.PercentV16
{
    class PercentV16Node:Types2OctetSignedValueNode
    {
        public PercentV16Node()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "percentage difference (%)";
        }

        public static TreeNode GetTypeNode()
        {
            PercentV16Node nodeType = new PercentV16Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
