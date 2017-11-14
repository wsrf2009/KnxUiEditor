using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.SSSBMode
{
    class SSSBModeNode:TypesN8Node
    {
        public SSSBModeNode(){
            this.KNXSubNumber = DPST_803;
            this.DPTName = "SSSB mode";
        }

        public static TreeNode GetTypeNode()
        {
            SSSBModeNode nodeType = new SSSBModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
