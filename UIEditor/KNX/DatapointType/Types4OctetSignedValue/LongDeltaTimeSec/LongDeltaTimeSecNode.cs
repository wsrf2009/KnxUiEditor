using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetSignedValue.LongDeltaTimeSec
{
    class LongDeltaTimeSecNode:Types4OctetSignedValueNode
    {
        public LongDeltaTimeSecNode()
        {
            this.KNXSubNumber = DPST_100;
            this.Name = "time lag (s)";
        }

        public static TreeNode GetTypeNode()
        {
            LongDeltaTimeSecNode nodeType = new LongDeltaTimeSecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
