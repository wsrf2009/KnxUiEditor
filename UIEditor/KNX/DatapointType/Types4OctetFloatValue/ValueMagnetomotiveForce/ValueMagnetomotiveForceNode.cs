using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagnetomotiveForce
{
    class ValueMagnetomotiveForceNode:Types4OctetFloatValueNode
    {
        public ValueMagnetomotiveForceNode()
        {
            this.KNXSubNumber = DPST_50;
            this.Name = "magnetomotive force (A)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagnetomotiveForceNode nodeType = new ValueMagnetomotiveForceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
