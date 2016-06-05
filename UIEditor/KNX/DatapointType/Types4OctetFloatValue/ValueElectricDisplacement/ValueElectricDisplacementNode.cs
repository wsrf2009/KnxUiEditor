﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricDisplacement
{
    class ValueElectricDisplacementNode:Types4OctetFloatValueNode
    {
        public ValueElectricDisplacementNode()
        {
            this.KNXSubNumber = DPST_22;
            this.Name = "electric displacement (C/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricDisplacementNode nodeType = new ValueElectricDisplacementNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
