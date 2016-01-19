using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEditor.Actions.Types8BitUnsignedValue
{
    public class Types8BitUnsignedValueNode : ActionNode
    {
        public Types8BitUnsignedValueNode()
        {
            knxMainNumber = 5;
            type = KNXDataType.Bit8;
        }
    }
}
