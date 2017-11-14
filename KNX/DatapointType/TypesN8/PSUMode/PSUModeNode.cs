using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.PSUMode
{
    class PSUModeNode:TypesN8Node
    {
        public PSUModeNode()
        {
            this.KNXSubNumber = DPST_8;
            this.DPTName = "PSU mode";
        }

        public static TreeNode GetTypeNode()
        {
            PSUModeNode nodeType = new PSUModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
