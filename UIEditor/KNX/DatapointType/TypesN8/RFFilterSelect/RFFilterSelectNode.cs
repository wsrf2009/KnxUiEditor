using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.RFFilterSelect
{
    class RFFilterSelectNode : TypesN8Node
    {
        public RFFilterSelectNode()
        {
            this.KNXSubNumber = DPST_1003;
            this.Name = "RF filter mode selection";
        }

        public static TreeNode GetTypeNode()
        {
            RFFilterSelectNode nodeType = new RFFilterSelectNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
