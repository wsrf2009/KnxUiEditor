﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueResistivity
{
    class ValueResistivityNode:Types4OctetFloatValueNode
    {
        public ValueResistivityNode()
        {
            this.KNXSubNumber = DPST_61;
            this.Name = "resistivity (Ωm)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueResistivityNode nodeType = new ValueResistivityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
