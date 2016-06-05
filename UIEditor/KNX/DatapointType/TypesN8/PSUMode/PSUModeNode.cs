using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.PSUMode
{
    class PSUModeNode:TypesN8Node
    {
        public PSUModeNode()
        {
            this.KNXSubNumber = DPST_8;
            this.Name = "PSU mode";
        }

        public static TreeNode GetTypeNode()
        {
            PSUModeNode nodeType = new PSUModeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
