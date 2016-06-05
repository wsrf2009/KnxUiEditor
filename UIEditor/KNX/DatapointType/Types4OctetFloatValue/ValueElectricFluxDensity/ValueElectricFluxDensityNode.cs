﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueElectricFluxDensity
{
    class ValueElectricFluxDensityNode:Types4OctetFloatValueNode
    {
        public ValueElectricFluxDensityNode()
        {
            this.KNXSubNumber = DPST_25;
            this.Name = "electric flux density (C/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueElectricFluxDensityNode nodeType = new ValueElectricFluxDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
