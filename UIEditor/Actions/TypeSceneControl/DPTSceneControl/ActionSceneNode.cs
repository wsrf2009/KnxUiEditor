using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.TypeSceneControl.DPTSceneControl;

namespace UIEditor.Actions.TypeSceneControl.DPTSceneControl
{
    public class ActionSceneNode : DPTSceneControlNode
    {
        public ActionSceneNode() {
            Text = "场面";

            this.Nodes.Add(new Scene1Node());
            this.Nodes.Add(new Scene2Node());
            this.Nodes.Add(new Scene3Node());
        }
    }
}
