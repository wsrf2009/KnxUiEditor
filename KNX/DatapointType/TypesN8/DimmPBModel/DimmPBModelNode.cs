using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.DimmPBModel
{
    class DimmPBModelNode:TypesN8Node
    {
        public DimmPBModelNode()
        {
            this.KNXSubNumber = DPST_607;
            this.DPTName = "PB dimm mode";
        }

        public static TreeNode GetTypeNode()
        {
            DimmPBModelNode nodeType = new DimmPBModelNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
