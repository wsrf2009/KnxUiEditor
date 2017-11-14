using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetFloatValue.ValueWeight
{
    class ValueWeightNode:Types4OctetFloatValueNode
    {
        public ValueWeightNode(){
            this.KNXSubNumber = DPST_78;
            this.DPTName = "weight (N)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueWeightNode nodeType = new ValueWeightNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
