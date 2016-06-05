using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetF163;
using UIEditor.KNX.DatapointType.Type3x2ByteFloatValue.TempRoomSetpSetShiftF163;

namespace UIEditor.KNX.DatapointType.Type3x2ByteFloatValue
{
    class Type3x2ByteFloatValueNode:DatapointType
    {
        public Type3x2ByteFloatValueNode(){
            this.KNXMainNumber = DPT_222;
            this.Name = "3x 2-byte float value";
        }

        public static TreeNode GetAllTypeNode()
        {
            Type3x2ByteFloatValueNode nodeType = new Type3x2ByteFloatValueNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            nodeType.Nodes.Add(TempRoomSetpSetF163Node.GetTypeNode());
            nodeType.Nodes.Add(TempRoomSetpSetShiftF163Node.GetTypeNode());

            return nodeType;
        }
    }
}
