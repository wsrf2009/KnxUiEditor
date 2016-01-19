using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEditor.Actions.TypeSceneControl
{
    public class SceneControlNode : ActionNode
    {
        public SceneControlNode()
        {
            knxMainNumber = 18;
            type = KNXDataType.Bit8;
        }
    }
}
