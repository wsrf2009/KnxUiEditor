﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types8BitUnsignedValue.Tariff
{
    class TariffNode:Types8BitUnsignedValueNode
    {
        public TariffNode()
        {
            this.KNXSubNumber = DPST_6;
            this.Name = "tariff (0..255)";
        }

        public static TreeNode GetTypeNode()
        {
            TariffNode nodeType = new TariffNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
