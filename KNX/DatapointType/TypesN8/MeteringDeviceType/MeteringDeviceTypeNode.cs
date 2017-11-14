using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesN8.MeteringDeviceType
{
    class MeteringDeviceTypeNode:TypesN8Node
    {
        public MeteringDeviceTypeNode()
        {
            this.KNXSubNumber = DPST_114;
            this.DPTName = "metering device type";
        }

        public static TreeNode GetTypeNode()
        {
            MeteringDeviceTypeNode nodeType = new MeteringDeviceTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
