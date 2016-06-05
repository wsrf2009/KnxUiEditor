using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.FanMode
{
    class FanModeNode:TypesN8Node
    {
        public FanModeNode()
        {
            this.KNXSubNumber = DPST_111;
            this.Name = "fan mode";
        }

        public static TreeNode GetTypeNode()
        {
            FanModeNode nodeType = new FanModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
