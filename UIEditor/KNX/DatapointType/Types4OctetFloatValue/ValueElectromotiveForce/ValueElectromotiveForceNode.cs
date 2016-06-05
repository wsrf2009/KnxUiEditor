using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectromotiveForce
{
    class ValueElectromotiveForceNode:Types4OctetFloatValueNode
    {
        public ValueElectromotiveForceNode()
        {
            this.KNXSubNumber = DPST_30;
            this.Name = "electromotive force (V)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectromotiveForceNode nodeType = new ValueElectromotiveForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
