using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.Trigger
{
    class TriggerNode : TypesB1Node
    {
        public TriggerNode()
        {
            this.KNXSubNumber = DPST_17;
            this.DPTName = "trigger";
        }

        public static TreeNode GetTypeNode()
        {
            TriggerNode nodeType = new TriggerNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
