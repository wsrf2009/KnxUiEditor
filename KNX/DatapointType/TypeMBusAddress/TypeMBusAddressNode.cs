using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeMBusAddress.MBusAddress;

namespace KNX.DatapointType.TypeMBusAddress
{
    class TypeMBusAddressNode:DatapointType
    {
        public TypeMBusAddressNode()
        {
            this.KNXMainNumber = DPT_230;
            this.DPTName = "MBus address";
            this.Type = KNXDataType.Bit64;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeMBusAddressNode nodeType = new TypeMBusAddressNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(MBusAddressNode.GetTypeNode());

            return nodeType;
        }
    }
}
