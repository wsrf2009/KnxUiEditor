using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetSignedValue.RotationAngle
{
    class RotationAngleNode:Types2OctetSignedValueNode
    {
        public RotationAngleNode()
        {
            this.KNXSubNumber = DPST_11;
            this.DPTName = "rotation angle (°)";
        }

        public static TreeNode GetTypeNode()
        {
            RotationAngleNode nodeType = new RotationAngleNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
