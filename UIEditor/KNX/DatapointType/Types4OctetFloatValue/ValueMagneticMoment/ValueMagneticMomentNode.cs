﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticMoment
{
    class ValueMagneticMomentNode:Types4OctetFloatValueNode
    {
        public ValueMagneticMomentNode()
        {
            this.KNXSubNumber = DPST_47;
            this.Name = "magnetic moment (Am²)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticMomentNode nodeType = new ValueMagneticMomentNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
