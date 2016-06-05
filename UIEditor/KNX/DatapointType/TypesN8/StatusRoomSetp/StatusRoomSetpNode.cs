﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.StatusRoomSetp
{
    class StatusRoomSetpNode:TypesN8Node
    {
        public StatusRoomSetpNode()
        {
            this.KNXSubNumber = DPST_113;
            this.Name = "status room setpoint";
        }

        public static TreeNode GetTypeNode()
        {
            StatusRoomSetpNode nodeType = new StatusRoomSetpNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
