using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueWork
{
    class ValueWorkNode : Types4OctetFloatValueNode
    {
        public ValueWorkNode()
        {
            this.KNXSubNumber = DPST_79;
            this.Name = "work (J)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueWorkNode nodeType = new ValueWorkNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
