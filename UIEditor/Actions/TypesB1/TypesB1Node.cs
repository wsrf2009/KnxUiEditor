using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEditor.Actions.TypesB1
{
    public class TypesB1Node : ActionNode
    {
        public TypesB1Node()
        {
            knxMainNumber = 1;
            type = KNXDataType.Bit1;
        }
    }
}
