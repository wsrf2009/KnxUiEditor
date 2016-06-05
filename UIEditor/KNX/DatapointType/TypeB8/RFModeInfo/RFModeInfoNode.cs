﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypeB8.RFModeInfo
{
    class RFModeInfoNode:TypeB8Node
    {
        public RFModeInfoNode()
        {
            this.KNXSubNumber = DPST_1000;
            this.Name = "RF communication mode info";
        }

        public static TreeNode GetTypeNode()
        {
            RFModeInfoNode nodeType = new RFModeInfoNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
