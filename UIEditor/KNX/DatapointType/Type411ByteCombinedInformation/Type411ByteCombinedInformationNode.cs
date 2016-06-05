using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Type411ByteCombinedInformation.MeteringValue;

namespace UIEditor.KNX.DatapointType.Type411ByteCombinedInformation
{
    class Type411ByteCombinedInformationNode:DatapointType
    {
        public Type411ByteCombinedInformationNode()
        {
            this.KNXMainNumber = DPT_229;
            this.Name = "4-1-1 byte combined information";
            this.Type = Structure.KNXDataType.Bit48;
        }

        public static TreeNode GetAllTypeNode()
        {
            Type411ByteCombinedInformationNode nodeType = new Type411ByteCombinedInformationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(MeteringValueNode.GetTypeNode());

            return nodeType;
        }
    }
}
