using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetUnsignedValue.PropDataType
{
    class PropDataTypeNode:Types2OctetUnsignedValueNode
    {
        public PropDataTypeNode()
        {
            this.KNXSubNumber = DPST_10;
            this.DPTName = "property data type";
        }

        public static TreeNode GetTypeNode()
        {
            PropDataTypeNode nodeType = new PropDataTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
