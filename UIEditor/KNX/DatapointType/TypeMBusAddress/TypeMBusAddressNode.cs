using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.TypeMBusAddress.MBusAddress;

namespace UIEditor.KNX.DatapointType.TypeMBusAddress
{
    class TypeMBusAddressNode:DatapointType
    {
        public TypeMBusAddressNode()
        {
            this.KNXMainNumber = DPT_230;
            this.Name = "MBus address";
            this.Type = KNXDataType.Bit64;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeMBusAddressNode nodeType = new TypeMBusAddressNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(MBusAddressNode.GetTypeNode());

            return nodeType;
        }
    }
}
