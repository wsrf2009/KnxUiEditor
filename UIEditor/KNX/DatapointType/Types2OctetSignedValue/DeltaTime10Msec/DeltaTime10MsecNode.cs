﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types2OctetSignedValue.DeltaTime10Msec
{
    class DeltaTime10MsecNode:Types2OctetSignedValueNode
    {
        public DeltaTime10MsecNode()
        {
            this.KNXSubNumber = DPST_3;
            this.Name = "time lag(10 ms)";
        }

        public static TreeNode GetTypeNode()
        {
            DeltaTime10MsecNode nodeType = new DeltaTime10MsecNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
