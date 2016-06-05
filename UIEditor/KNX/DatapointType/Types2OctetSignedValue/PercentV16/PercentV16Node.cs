using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.PercentV16
{
    class PercentV16Node:Types2OctetSignedValueNode
    {
        public PercentV16Node()
        {
            this.KNXSubNumber = DPST_10;
            this.Name = "percentage difference (%)";
        }

        public static TreeNode GetTypeNode()
        {
            PercentV16Node nodeType = new PercentV16Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
