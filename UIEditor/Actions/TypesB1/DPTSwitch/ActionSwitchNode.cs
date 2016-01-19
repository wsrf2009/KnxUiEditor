using Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Switch.TypesB1.DPTSwitch;

namespace UIEditor.Actions.TypesB1.DPTSwitch
{
    public class ActionSwitchNode : DPTSwitchNode
    {
        public ActionSwitchNode()
        {
            Text = "开关";

            this.Nodes.Add(new SwitchOpenNode());
            this.Nodes.Add(new SwitchCloseNode());
        }
    }
}
