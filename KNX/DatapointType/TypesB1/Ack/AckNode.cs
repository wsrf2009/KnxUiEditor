using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.Ack
{
    class AckNode : TypesB1Node
    {
        public AckNode()
        {
            this.KNXSubNumber = DPST_17;
            this.DPTName = "acknowledge";
        }

        public static TreeNode GetTypeNode()
        {
            AckNode nodeType = new AckNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
