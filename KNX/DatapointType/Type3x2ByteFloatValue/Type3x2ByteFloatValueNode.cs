using KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetF163;
using KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetShiftF163;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KNX.DatapointType.Type3x2ByteFloatValue
{
    class Type3x2ByteFloatValueNode:DatapointType
    {
        public Type3x2ByteFloatValueNode(){
            this.KNXMainNumber = DPT_222;
            this.DPTName = "3x 2-byte float value";
        }

        public static TreeNode GetAllTypeNode()
        {
            Type3x2ByteFloatValueNode nodeType = new Type3x2ByteFloatValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(TempRoomSetpSetF163Node.GetTypeNode());
            nodeType.Nodes.Add(TempRoomSetpSetShiftF163Node.GetTypeNode());

            return nodeType;
        }
    }
}
