using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetSignedValue.LongDeltaTimeSec
{
    class LongDeltaTimeSecNode:Types4OctetSignedValueNode
    {
        public LongDeltaTimeSecNode()
        {
            this.KNXSubNumber = DPST_100;
            this.DPTName = "time lag (s)";
        }

        public static TreeNode GetTypeNode()
        {
            LongDeltaTimeSecNode nodeType = new LongDeltaTimeSecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
