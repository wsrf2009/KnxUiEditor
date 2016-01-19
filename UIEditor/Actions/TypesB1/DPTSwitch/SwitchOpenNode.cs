using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Switch.TypesB1.DPTSwitch;

namespace UIEditor.Actions.TypesB1.DPTSwitch
{
    public class SwitchOpenNode : DPTSwitchNode
    {
        public SwitchOpenNode() {
            action = new KNXDatapointAction();
            action.Name = Text = "开";
            action.Value = 1;
        }
    }
}
