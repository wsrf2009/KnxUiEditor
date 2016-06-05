using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.KNX.DatapointType.Types4OctetFloatValue.ValueMagneticPolarization
{
    class ValueMagneticPolarizationNode:Types4OctetFloatValueNode
    {
        public ValueMagneticPolarizationNode()
        {
            this.KNXSubNumber = DPST_48;
            this.Name = "magnetic polarization (T)";
        }

        public static TreeNode GetTypeNode()
        {
            ValueMagneticPolarizationNode nodeType = new ValueMagneticPolarizationNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }
    }
}
