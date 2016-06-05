﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeHrs
{
    class DeltaTimeHrsNode:Types2OctetSignedValueNode
    {
        public DeltaTimeHrsNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "time lag (h)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTimeHrsNode nodeType = new DeltaTimeHrsNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
