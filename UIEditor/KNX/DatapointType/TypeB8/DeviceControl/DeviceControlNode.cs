using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.DeviceControl
{
    class DeviceControlNode:TypeB8Node
    {
        public DeviceControlNode()
        {
            this.KNXSubNumber = DPST_2;
            this.Name = "device control";
        }

        public static TreeNode GetTypeNode()
        {
            DeviceControlNode nodeType = new DeviceControlNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
