using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.KNX.DatapointAction;

namespace UIEditor.KNX.DatapointType.TypesB1.Switch
{
    class SwitchNode : TypesB1Node
    {
        public SwitchNode()
        {
            this.KNXSubNumber = DPST_1;
            this.Name = "switch";
        }

        public static TreeNode GetTypeNode()
        {
            SwitchNode nodeType = new SwitchNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.Name;

            return nodeType;
        }

        public static TreeNode GetActionNode(){
            SwitchNode nodeAction = new SwitchNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.Name;
            
            DatapointActionNode actionOn = new DatapointActionNode();
            actionOn.Name = actionOn.Text = ResourceMng.GetString("On");
            actionOn.Value = 1;

            DatapointActionNode actionOff = new DatapointActionNode();
            actionOff.Name = actionOff.Text = ResourceMng.GetString("Off");
            actionOff.Value = 0;

            nodeAction.Nodes.Add(actionOn);
            nodeAction.Nodes.Add(actionOff);

            return nodeAction;
        }
    }
}
