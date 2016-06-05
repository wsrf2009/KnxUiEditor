﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueTime
{
    class ValueTimeNode:Types4OctetFloatValueNode
    {
        public ValueTimeNode()
        {
            this.KNXSubNumber = DPST_74;
            this.Name = "time (s)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueTimeNode nodeType = new ValueTimeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
