﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetUnsignedValue.Brightness
{
    class BrightnessNode:Types2OctetUnsignedValueNode
    {
        public BrightnessNode()
        {
            this.KNXSubNumber = DPST_13;
            this.Name = "brightness (lux)";
        }

        public static TreeNode GetTypeNode()
        {
            BrightnessNode nodeType = new BrightnessNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
