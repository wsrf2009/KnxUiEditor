using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.ADAType
{
    class ADATypeNode:TypesN8Node
    {
        public ADATypeNode()
        {
            this.KNXSubNumber = DPST_120;
            this.DPTName = "ADA type";
        }

        public static TreeNode GetTypeNode()
        {
            ADATypeNode nodeType = new ADATypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
