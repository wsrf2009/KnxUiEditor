using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.ValueLux
{
    class ValueLuxNode:Types2OctetFloatValueNode
    {
        public ValueLuxNode()
        {
            this.KNXSubNumber = DPST_4;
            this.Name = "lux (Lux)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueLuxNode nodeType = new ValueLuxNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
