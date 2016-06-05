using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetShiftF163
{
    class TempRoomSetpSetShiftF163Node:Type3x2ByteFloatValueNode
    {
        public TempRoomSetpSetShiftF163Node()
        {
            this.KNXSubNumber = DPST_101;
            this.Name = "room temperature setpoint shift";
        }

        public static TreeNode GetTypeNode()
        {
            TempRoomSetpSetShiftF163Node nodeType = new TempRoomSetpSetShiftF163Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
