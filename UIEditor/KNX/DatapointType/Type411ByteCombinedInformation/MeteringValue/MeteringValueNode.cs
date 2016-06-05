using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Type411ByteCombinedInformation.MeteringValue
{
    class MeteringValueNode:Type411ByteCombinedInformationNode
    {
        public MeteringValueNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "metering value (value,encoding,cmd)";
        }

        public static TreeNode GetTypeNode()
        {
            MeteringValueNode nodeType = new MeteringValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
