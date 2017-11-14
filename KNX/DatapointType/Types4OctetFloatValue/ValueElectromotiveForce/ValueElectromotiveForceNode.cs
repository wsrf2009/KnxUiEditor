using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueElectromotiveForce
{
    class ValueElectromotiveForceNode:Types4OctetFloatValueNode
    {
        public ValueElectromotiveForceNode()
        {
            this.KNXSubNumber = DPST_30;
            this.DPTName = "electromotive force (V)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectromotiveForceNode nodeType = new ValueElectromotiveForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
