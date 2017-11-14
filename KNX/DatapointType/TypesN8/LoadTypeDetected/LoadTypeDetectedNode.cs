using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.LoadTypeDetected
{
    class LoadTypeDetectedNode:TypesN8Node
    {
        public LoadTypeDetectedNode()
        {
            this.KNXSubNumber = DPST_610;
            this.DPTName = "load type detection";
        }

        public static TreeNode GetTypeNode()
        {
            LoadTypeDetectedNode nodeType = new LoadTypeDetectedNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
