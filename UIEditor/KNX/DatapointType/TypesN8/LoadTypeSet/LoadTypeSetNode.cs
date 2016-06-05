﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.TypesN8.LoadTypeSet
{
    class LoadTypeSetNode:TypesN8Node
    {
        public LoadTypeSetNode()
        {
            this.KNXSubNumber = DPST_609;
            this.Name = "load type";
        }

        public static TreeNode GetTypeNode()
        {
            LoadTypeSetNode nodeType = new LoadTypeSetNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
