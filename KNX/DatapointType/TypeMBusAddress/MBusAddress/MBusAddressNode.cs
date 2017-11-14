using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.TypeMBusAddress.MBusAddress
{
    class MBusAddressNode:TypeMBusAddressNode
    {
        public MBusAddressNode()
        {
            this.KNXSubNumber = DPST_1000;
            this.DPTName = "MBus address";
        }

        public static TreeNode GetTypeNode()
        {
            MBusAddressNode nodeType = new MBusAddressNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
