using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.BurnerType
{
    class BurnerTypeNode:TypesN8Node
    {
        public BurnerTypeNode(){
            this.KNXSubNumber = DPST_101;
            this.Name = "burner type";
        }

        public static TreeNode GetTypeNode()
        {
            BurnerTypeNode nodeType = new BurnerTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
