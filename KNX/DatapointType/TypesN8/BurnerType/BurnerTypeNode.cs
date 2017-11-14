using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.BurnerType
{
    class BurnerTypeNode:TypesN8Node
    {
        public BurnerTypeNode(){
            this.KNXSubNumber = DPST_101;
            this.DPTName = "burner type";
        }

        public static TreeNode GetTypeNode()
        {
            BurnerTypeNode nodeType = new BurnerTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
