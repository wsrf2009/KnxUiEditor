using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1U3.ControlBlinds
{
    class ControlBlindsNode:TypesB1U3Node
    {
        public ControlBlindsNode()
        {
            this.KNXSubNumber = DPST_8;
            this.Name = "blind control";
        }

        public static TreeNode GetTypeNode()
        {
            ControlBlindsNode nodeType = new ControlBlindsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
