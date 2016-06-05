using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueTempa
{
    class ValueTempaNode:Types2OctetFloatValueNode
    {
        public ValueTempaNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "kelvin/hour (K/h)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTempaNode nodeType = new ValueTempaNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
