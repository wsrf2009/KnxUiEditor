﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueStress
{
    class ValueStressNode:Types4OctetFloatValueNode
    {
        public ValueStressNode()
        {
            this.KNXSubNumber = DPST_66;
            this.Name = "stress (Pa)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueStressNode nodeType = new ValueStressNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
