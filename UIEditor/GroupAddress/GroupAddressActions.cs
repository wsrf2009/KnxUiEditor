using KNX.DatapointAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIEditor.GroupAddress
{
    public class GroupAddressActions
    {
        #region 成员
        public List<DatapointActionNode> Actions { get; set; }
        #endregion

        #region 构造函数
        public GroupAddressActions()
        {
            this.Actions = new List<DatapointActionNode>();
        }

        public GroupAddressActions(List<DatapointActionNode> actions)
        {
            this.Actions = actions;
        }

        public GroupAddressActions(List<DatapointAction> actions)
            : this()
        {
            if (null != actions)
            {
                foreach (DatapointAction action in actions)
                {
                    this.Actions.Add(new DatapointActionNode(action));
                }
            }
        }
        #endregion

        #region 覆写函数
        public override string ToString()
        {
            string str = "";

            foreach (DatapointActionNode action in this.Actions)
            {
                if (str.Length > 0)
                {
                    str += ";" + action.ActionName;
                }
                else
                {
                    str = action.ToString();
                }
            }

            return str;
        }
        #endregion

        public List<DatapointAction> ToKnx()
        {
            List<DatapointAction> actions = new List<DatapointAction>();

            foreach (DatapointActionNode node in this.Actions)
            {
                actions.Add(node.ToKnx());
            }

            return actions;
        }

        public void AddActionNode(DatapointActionNode node)
        {
            this.Actions.Add(node);
        }

        public void RemoveActionNode(DatapointActionNode node)
        {
            this.Actions.Remove(node);
        }

        public DatapointActionNode GetActionAccrodingToActionName(string name)
        {
            if (null != name)
            {
                foreach (var action in this.Actions)
                {
                    if (name.Equals(action.ActionName, StringComparison.CurrentCulture))
                    {
                        return action;
                    }
                }
            }

            return null;
        }

        public string[] GetActionNames()
        {
            var list = new string[this.Actions.Count];

            for(int i =0; i < this.Actions.Count; i++)
            {
                list.SetValue(this.Actions[i].Name, i);
            }

            return list;
        }
    }
}
