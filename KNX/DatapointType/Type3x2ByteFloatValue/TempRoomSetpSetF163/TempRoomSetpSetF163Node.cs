using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetF163
{
    class TempRoomSetpSetF163Node:Type3x2ByteFloatValueNode
    {
        public TempRoomSetpSetF163Node()
        {
            this.KNXSubNumber = DPST_100;
            this.DPTName = "room temperature setpoint";
        }

        public static TreeNode GetTypeNode()
        {
            TempRoomSetpSetF163Node nodeType = new TempRoomSetpSetF163Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }
    }
}
