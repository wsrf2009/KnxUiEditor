using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.ADAType
{
    class ADATypeNode:TypesN8Node
    {
        public ADATypeNode()
        {
            this.KNXSubNumber = DPST_120;
            this.Name = "ADA type";
        }

        public static TreeNode GetTypeNode()
        {
            ADATypeNode nodeType = new ADATypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
