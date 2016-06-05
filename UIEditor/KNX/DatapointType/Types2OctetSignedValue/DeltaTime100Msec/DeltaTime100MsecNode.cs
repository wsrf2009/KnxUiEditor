using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTime100Msec
{
    class DeltaTime100MsecNode:Types2OctetSignedValueNode
    {
        public DeltaTime100MsecNode()
        {
            this.KNXSubNumber = DPST_4;
            this.Name = "time lag (100 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTime100MsecNode nodeType = new DeltaTime100MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
