using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types2OctetFloatValue.KelvinPerPercent
{
    class KelvinPerPercentNode:Types2OctetFloatValueNode
    {
        public KelvinPerPercentNode()
        {
            this.KNXSubNumber = DPST_23;
            this.DPTName = "kelvin/percent (K/%)";
        }

        public static TreeNode GetTypeNode()
        {
            KelvinPerPercentNode nodeType = new KelvinPerPercentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
