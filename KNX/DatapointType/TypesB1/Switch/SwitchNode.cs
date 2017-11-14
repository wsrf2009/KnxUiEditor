using KNX.DatapointAction;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1.Switch
{
    class SwitchNode : TypesB1Node
    {
        public SwitchNode()
        {
            this.KNXSubNumber = DPST_1;
            this.DPTName = "switch";
        }

        public static TreeNode GetTypeNode()
        {
            SwitchNode nodeType = new SwitchNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }

        public static TreeNode GetActionNode(){
            SwitchNode nodeAction = new SwitchNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;
            
            DatapointActionNode actionOn = new DatapointActionNode();
            actionOn.ActionName = actionOn.Text = KNXResMang.GetString("On");
            actionOn.Value = 1;

            DatapointActionNode actionOff = new DatapointActionNode();
            actionOff.ActionName = actionOff.Text = KNXResMang.GetString("Off");
            actionOff.Value = 0;

            nodeAction.Nodes.Add(actionOn);
            nodeAction.Nodes.Add(actionOff);

            return nodeAction;
        }
    }
}
