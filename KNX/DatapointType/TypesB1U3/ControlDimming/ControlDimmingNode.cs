using KNX.DatapointAction;
using System.Windows.Forms;

namespace KNX.DatapointType.TypesB1U3.ControlDimming
{
    class ControlDimmingNode : TypesB1U3Node
    {
        public ControlDimmingNode()
        {
            this.KNXSubNumber = DPST_7;
            this.DPTName = "dimming control";
        }

        public static TreeNode GetTypeNode()
        {
            ControlDimmingNode nodeType = new ControlDimmingNode();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            return nodeType;
        }

        public static TreeNode GetActionNode()
        {
            ControlDimmingNode nodeAction = new ControlDimmingNode();
            nodeAction.Text = nodeAction.KNXMainNumber + "." + nodeAction.KNXSubNumber + " " + nodeAction.DPTName;

            DatapointActionNode actionIncreasing1per = new DatapointActionNode();
            actionIncreasing1per.ActionName = actionIncreasing1per.Text = KNXResMang.GetString("Increasing1per");
            actionIncreasing1per.Value = 0x0F;

            DatapointActionNode actionIncreasing3per = new DatapointActionNode();
            actionIncreasing3per.ActionName = actionIncreasing3per.Text = KNXResMang.GetString("Increasing3per");
            actionIncreasing3per.Value = 0x0E;

            DatapointActionNode actionIncreasing6per = new DatapointActionNode();
            actionIncreasing6per.ActionName = actionIncreasing6per.Text = KNXResMang.GetString("Increasing6per");
            actionIncreasing6per.Value = 0x0D;

            DatapointActionNode actionIncreasing12per = new DatapointActionNode();
            actionIncreasing12per.ActionName = actionIncreasing12per.Text = KNXResMang.GetString("Increasing12per");
            actionIncreasing12per.Value = 0x0C;

            DatapointActionNode actionIncreasing25per = new DatapointActionNode();
            actionIncreasing25per.ActionName = actionIncreasing25per.Text = KNXResMang.GetString("Increasing25per");
            actionIncreasing25per.Value = 0x0B;

            DatapointActionNode actionIncreasing50per = new DatapointActionNode();
            actionIncreasing50per.ActionName = actionIncreasing50per.Text = KNXResMang.GetString("Increasing50per");
            actionIncreasing50per.Value = 0x0A;

            DatapointActionNode actionIncreasing100per = new DatapointActionNode();
            actionIncreasing100per.ActionName = actionIncreasing100per.Text = KNXResMang.GetString("Increasing100per");
            actionIncreasing100per.Value = 0x09;

            DatapointActionNode actionDecreasing1per = new DatapointActionNode();
            actionDecreasing1per.ActionName = actionDecreasing1per.Text = KNXResMang.GetString("Decreasing1per");
            actionDecreasing1per.Value = 0x07;

            DatapointActionNode actionDecreasing3per = new DatapointActionNode();
            actionDecreasing3per.ActionName = actionDecreasing3per.Text = KNXResMang.GetString("Decreasing3per");
            actionDecreasing3per.Value = 0x06;

            DatapointActionNode actionDecreasing6per = new DatapointActionNode();
            actionDecreasing6per.ActionName = actionDecreasing6per.Text = KNXResMang.GetString("Decreasing6per");
            actionDecreasing6per.Value = 0x05;

            DatapointActionNode actionDecreasing12per = new DatapointActionNode();
            actionDecreasing12per.ActionName = actionDecreasing12per.Text = KNXResMang.GetString("Decreasing12per");
            actionDecreasing12per.Value = 0x05;

            DatapointActionNode actionDecreasing25per = new DatapointActionNode();
            actionDecreasing25per.ActionName = actionDecreasing25per.Text = KNXResMang.GetString("Decreasing25per");
            actionDecreasing25per.Value = 0x03;

            DatapointActionNode actionDecreasing50per = new DatapointActionNode();
            actionDecreasing50per.ActionName = actionDecreasing50per.Text = KNXResMang.GetString("Decreasing50per");
            actionDecreasing50per.Value = 0x02;

            DatapointActionNode actionDecreasing100per = new DatapointActionNode();
            actionDecreasing100per.ActionName = actionDecreasing100per.Text = KNXResMang.GetString("Decreasing100per");
            actionDecreasing100per.Value = 0x01;

            nodeAction.Nodes.Add(actionIncreasing1per);
            nodeAction.Nodes.Add(actionIncreasing3per);
            nodeAction.Nodes.Add(actionIncreasing6per);
            nodeAction.Nodes.Add(actionIncreasing12per);
            nodeAction.Nodes.Add(actionIncreasing25per);
            nodeAction.Nodes.Add(actionIncreasing50per);
            nodeAction.Nodes.Add(actionIncreasing100per);
            nodeAction.Nodes.Add(actionDecreasing1per);
            nodeAction.Nodes.Add(actionDecreasing3per);
            nodeAction.Nodes.Add(actionDecreasing6per);
            nodeAction.Nodes.Add(actionDecreasing12per);
            nodeAction.Nodes.Add(actionDecreasing25per);
            nodeAction.Nodes.Add(actionDecreasing50per);
            nodeAction.Nodes.Add(actionDecreasing100per);

            return nodeAction;
        }
    }
}
