using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN2.UpDownAction
{
    class UpDownActionNode:TypesN2Node
    {
        public UpDownActionNode()
        {
            this.KNXSubNumber = DPST_3;
            this.DPTName = "up/down action";
        }

        public static TreeNode GetTypeNode()
        {
            UpDownActionNode nodeType = new UpDownActionNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
