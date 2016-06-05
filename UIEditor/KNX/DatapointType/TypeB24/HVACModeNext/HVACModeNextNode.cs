using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB24.HVACModeNext
{
    class HVACModeNextNode:TypeB24Node
    {
        public HVACModeNextNode()
        {
            this.KNXSubNumber = DPST_100;
            this.Name = "time delay & HVAC mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACModeNextNode nodeType = new HVACModeNextNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
