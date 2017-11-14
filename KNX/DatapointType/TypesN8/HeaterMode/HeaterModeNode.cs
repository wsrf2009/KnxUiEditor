using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.HeaterMode
{
    class HeaterModeNode:TypesN8Node
    {
        public HeaterModeNode()
        {
            this.KNXSubNumber = DPST_110;
            this.DPTName = "heater mode";
        }

        public static TreeNode GetTypeNode()
        {
            HeaterModeNode nodeType = new HeaterModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
