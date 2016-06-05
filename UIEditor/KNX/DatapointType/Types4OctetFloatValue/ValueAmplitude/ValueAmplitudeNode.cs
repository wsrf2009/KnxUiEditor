using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueAmplitude
{
    class ValueAmplitudeNode:Types4OctetFloatValueNode
    {
        public ValueAmplitudeNode()
        {
            this.KNXSubNumber = DPST_5;
            this.Name = "amplitude";
        }

        public static TreeNode GetTypeNode()
        {
            ValueAmplitudeNode nodeType = new ValueAmplitudeNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
