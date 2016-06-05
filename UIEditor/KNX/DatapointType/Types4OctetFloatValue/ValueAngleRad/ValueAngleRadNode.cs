using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAngleRad
{
    class ValueAngleRadNode:Types4OctetFloatValueNode
    {
        public ValueAngleRadNode()
        {
            this.KNXSubNumber = DPST_6;
            this.Name = "angle (radiant)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAngleRadNode nodeType = new ValueAngleRadNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
