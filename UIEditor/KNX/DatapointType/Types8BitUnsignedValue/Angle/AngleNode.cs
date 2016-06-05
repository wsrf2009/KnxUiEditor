using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Angle
{
    class AngleNode:Types8BitUnsignedValueNode
    {
        public AngleNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "angle (degrees)";
        }

        public static TreeNode GetTypeNode()
        {
            AngleNode nodeType = new AngleNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
