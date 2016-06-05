using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueWsp
{
    class ValueWspNode:Types2OctetFloatValueNode
    {
        public ValueWspNode()
        {
            this.KNXSubNumber = DPST_5;
            this.Name = "speed (m/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueWspNode nodeType = new ValueWspNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
