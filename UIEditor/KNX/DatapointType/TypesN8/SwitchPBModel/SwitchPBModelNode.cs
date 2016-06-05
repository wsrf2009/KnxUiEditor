using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.SwitchPBModel
{
    class SwitchPBModelNode:TypesN8Node
    {
        public SwitchPBModelNode()
        {
            this.KNXSubNumber = DPST_605;
            this.Name = "PB switch mode";
        }

        public static TreeNode GetTypeNode()
        {
            SwitchPBModelNode nodeType = new SwitchPBModelNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
