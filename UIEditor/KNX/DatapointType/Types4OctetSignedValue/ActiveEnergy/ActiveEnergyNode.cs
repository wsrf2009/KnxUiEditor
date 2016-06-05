﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetSignedValue.ActiveEnergy
{
    class ActiveEnergyNode:Types4OctetSignedValueNode
    {
        public ActiveEnergyNode()
        {
            this.KNXSubNumber = DPST_10;
            this.Name = "active energy (Wh)";
        }

        public static TreeNode GetTypeNode()
        {
            ActiveEnergyNode nodeType = new ActiveEnergyNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
