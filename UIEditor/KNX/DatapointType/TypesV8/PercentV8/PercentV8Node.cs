using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesV8.PercentV8
{
    class PercentV8Node:TypesV8Node
    {
        public PercentV8Node()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "percentage (-128..127%)";
        }

        public static TreeNode GetTypeNode()
        {
            PercentV8Node nodeType = new PercentV8Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
