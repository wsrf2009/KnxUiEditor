using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeMsec
{
    class DeltaTimeMsecNode:Types2OctetSignedValueNode
    {
        public DeltaTimeMsecNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "time lag (ms)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTimeMsecNode nodeType = new DeltaTimeMsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
