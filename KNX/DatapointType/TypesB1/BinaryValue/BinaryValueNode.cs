using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.BinaryValue
{
    class BinaryValueNode : TypesB1Node
    {
        public BinaryValueNode()
        {
            //this.Text = "1.006 binary value";
            this.KNXSubNumber = DPST_6;
            this.DPTName = "binary value";
        }

        public static TreeNode GetTypeNode()
        {
            BinaryValueNode nodeType = new BinaryValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
