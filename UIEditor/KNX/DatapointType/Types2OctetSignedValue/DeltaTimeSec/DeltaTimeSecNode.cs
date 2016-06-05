﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTimeSec
{
    class DeltaTimeSecNode:Types2OctetSignedValueNode
    {
        public DeltaTimeSecNode()
        {
            this.KNXSubNumber = DPST_5;
            this.Name = "time lag (s)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTimeSecNode nodeType = new DeltaTimeSecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
