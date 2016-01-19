using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Actions.Types8BitUnsignedValue.DPTScaling;
using UIEditor.Actions.TypesB1.DPTSwitch;
using UIEditor.Actions.TypesB1U3.DPTControlDimming;
using UIEditor.Actions.TypeSceneControl.DPTSceneControl;

namespace UIEditor.Actions
{
    public class DefaultActions
    {
        public List<TreeNode> getDefalutActions() {
            TreeNode node1 = new ActionSwitchNode();
            TreeNode node2 = new ActionRelativeDimmingNode();
            TreeNode node3 = new ActionAbsoluteAdjustmentPercentNode();
            TreeNode node4 = new ActionSceneNode();

            List<TreeNode> list = new List<TreeNode>();
            list.Add(node1);
            list.Add(node2);
            list.Add(node3);
            list.Add(node4);

            return list;
        }
        
    }
}
