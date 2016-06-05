using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesB1.WindowDoor
{
    class WindowDoorNode : TypesB1Node
    {
        public WindowDoorNode()
        {
            this.KNXSubNumber = DPST_19;
            this.Name = "window/door";
        }

        public static TreeNode GetTypeNode()
        {
            WindowDoorNode nodeType = new WindowDoorNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
