using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB32.CombinedInfoOnOff
{
    class CombinedInfoOnOffNode : TypeB32Node
    {
        public CombinedInfoOnOffNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "bit-combined info on/off";
        }

        public static TreeNode GetTypeNode()
        {
            CombinedInfoOnOffNode nodeType = new CombinedInfoOnOffNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
