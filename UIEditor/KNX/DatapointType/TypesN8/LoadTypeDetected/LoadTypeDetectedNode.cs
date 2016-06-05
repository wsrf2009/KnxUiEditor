using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.LoadTypeDetected
{
    class LoadTypeDetectedNode:TypesN8Node
    {
        public LoadTypeDetectedNode()
        {
            this.KNXSubNumber = DPST_610;
            this.Name = "load type detection";
        }

        public static TreeNode GetTypeNode()
        {
            LoadTypeDetectedNode nodeType = new LoadTypeDetectedNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
