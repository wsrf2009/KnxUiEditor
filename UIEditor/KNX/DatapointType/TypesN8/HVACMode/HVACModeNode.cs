using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.HVACMode
{
    class HVACModeNode:TypesN8Node
    {
        public HVACModeNode()
        {
            this.KNXSubNumber = DPST_102;
            this.Name = "HVAC mode";
        }

        public static TreeNode GetTypeNode()
        {
            HVACModeNode nodeType = new HVACModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
