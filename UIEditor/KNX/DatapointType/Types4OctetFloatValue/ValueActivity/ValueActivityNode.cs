using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueActivity
{
    class ValueActivityNode:Types4OctetFloatValueNode
    {
        public ValueActivityNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "radioactive activity (1/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueActivityNode nodeType = new ValueActivityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
