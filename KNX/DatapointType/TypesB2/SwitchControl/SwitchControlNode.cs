using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB2.SwitchControl
{
    class SwitchControlNode : TypesB2Node
    {
        public SwitchControlNode()
        {
            //this.Text = "2.001 switch control";
            this.KNXSubNumber = DPST_1;
            this.DPTName = "switch control";
        }

        public static TreeNode GetTypeNode()
        {
            SwitchControlNode nodeType = new SwitchControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
