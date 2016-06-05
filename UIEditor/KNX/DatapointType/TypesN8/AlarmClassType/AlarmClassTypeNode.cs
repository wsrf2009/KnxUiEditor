﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.AlarmClassType
{
    class AlarmClassTypeNode:TypesN8Node
    {
        public AlarmClassTypeNode()
        {
            this.KNXSubNumber = DPST_7;
            this.Name = "alarm class";
        }

        public static TreeNode GetTypeNode()
        {
            AlarmClassTypeNode nodeType = new AlarmClassTypeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}