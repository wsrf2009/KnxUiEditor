﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetFloatValue.PowerDensity
{
    class PowerDensityNode:Types2OctetFloatValueNode
    {
        public PowerDensityNode()
        {
            this.KNXSubNumber = DPST_22;
            this.Name = "power denisity (W/m²)";
        }

        public static TreeNode GetTypeNode()
        {
            PowerDensityNode nodeType = new PowerDensityNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
