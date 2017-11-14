using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.InputSource
{
    class InputSourceNode : TypesB1Node
    {
        public InputSourceNode()
        {
            this.KNXSubNumber = DPST_14;
            this.DPTName = "input source";
        }

        public static TreeNode GetTypeNode()
        {
            InputSourceNode nodeType = new InputSourceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
