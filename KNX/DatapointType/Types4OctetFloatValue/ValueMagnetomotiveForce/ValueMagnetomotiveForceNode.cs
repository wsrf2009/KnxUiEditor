using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueMagnetomotiveForce
{
    class ValueMagnetomotiveForceNode:Types4OctetFloatValueNode
    {
        public ValueMagnetomotiveForceNode()
        {
            this.KNXSubNumber = DPST_50;
            this.DPTName = "magnetomotive force (A)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagnetomotiveForceNode nodeType = new ValueMagnetomotiveForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
