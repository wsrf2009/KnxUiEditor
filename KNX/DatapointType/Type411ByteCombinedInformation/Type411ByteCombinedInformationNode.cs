using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.Type411ByteCombinedInformation.MeteringValue;

namespace KNX.DatapointType.Type411ByteCombinedInformation
{
    class Type411ByteCombinedInformationNode:DatapointType
    {
        public Type411ByteCombinedInformationNode()
        {
            this.KNXMainNumber = DPT_229;
            this.DPTName = "4-1-1 byte combined information";
            this.Type = KNXDataType.Bit48;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type411ByteCombinedInformationNode nodeType = new Type411ByteCombinedInformationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(MeteringValueNode.GetTypeNode());

            return nodeType;
        }
    }
}
