using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.TypeSceneControl.DPTSceneControl;

namespace UIEditor.Actions.TypeSceneControl.DPTSceneControl
{
    public class Scene3Node : DPTSceneControlNode
    {
        public Scene3Node()
        {
            action = new KNXDatapointAction();
            action.Name = Text = "场景3";
            action.Value = 2;
        }
    }
}
