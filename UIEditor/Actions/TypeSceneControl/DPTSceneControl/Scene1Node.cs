using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions.TypeSceneControl.DPTSceneControl
{
    public class Scene1Node : DPTSceneControlNode
    {
        public Scene1Node() {
            action = new KNXDatapointAction();
            action.Name = Text = "场面1";
            action.Value = 0;
        }
    }
}
