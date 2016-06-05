﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueSpeed
{
    class ValueSpeedNode:Types4OctetFloatValueNode
    {
        public ValueSpeedNode()
        {
            this.KNXSubNumber = DPST_65;
            this.Name = "speed (m/s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueSpeedNode nodeType = new ValueSpeedNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
