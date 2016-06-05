using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeDPTAccessData.AccessData
{
    class AccessDataNode:TypeDPTAccessDataNode
    {
        public AccessDataNode()
        {
            this.KNXSubNumber = DPST_0;
            this.Name = "entrance access";
        }

        public static TreeNode GetTypeNode()
        {
            AccessDataNode nodeType = new AccessDataNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
