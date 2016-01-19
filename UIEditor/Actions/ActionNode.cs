using Structure;
using Structure.ETS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Actions
{
    public class ActionNode : TreeNode
    {
        public int knxMainNumber;
        public int knxSubNumber;
        public KNXDataType type;
        public KNXDatapointAction action;

        public ActionNode()
        {
            
        }
    }
}
