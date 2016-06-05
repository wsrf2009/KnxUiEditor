﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueCapacitance
{
    class ValueCapacitanceNode:Types4OctetFloatValueNode
    {
        public ValueCapacitanceNode()
        {
            this.KNXSubNumber = DPST_11;
            this.Name = "capacitance (F)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueCapacitanceNode nodeType = new ValueCapacitanceNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
