using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Switch.TypesB1.DPTSwitch;

namespace UIEditor.Actions.TypesB1.DPTSwitch
{
    public class SwitchCloseNode : DPTSwitchNode
    {
        public SwitchCloseNode() {
            action = new KNXDatapointAction();
            action.Name = Text = "关";
            action.Value = 0;
        }
    }
}
