using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.LightApplicationMode
{
    class LightApplicationModeNode:TypesN8Node
    {
        public LightApplicationModeNode()
        {
            this.KNXSubNumber = DPST_5;
            this.DPTName = "light application mode";
        }

        public static TreeNode GetTypeNode()
        {
            LightApplicationModeNode nodeType = new LightApplicationModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
