using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Types4OctetSignedValue.ReactiveEnergykVARh
{
    class ReactiveEnergykVARhNode:Types4OctetSignedValueNode
    {
        public ReactiveEnergykVARhNode()
        {
            this.KNXSubNumber = DPST_15;
            this.DPTName = "reactive energy (kVARh)";
        }

        public static TreeNode GetTypeNode()
        {
            ReactiveEnergykVARhNode nodeType = new ReactiveEnergykVARhNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
