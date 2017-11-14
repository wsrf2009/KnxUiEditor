using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.CommandManager.CommandTreeNode
{
    class TreeViewClearNodes:IUndoCommand
    {
        private TreeView tvRoot;
        private List<TreeNode> ListChildNodes;

        public TreeViewClearNodes(TreeView tv)
        {
            this.tvRoot = tv;
            ListChildNodes = new List<TreeNode>();
            foreach (TreeNode node in tv.Nodes)
            {
                ListChildNodes.Add(node);
            }
        }

        public void Execute()
        {
            this.tvRoot.Nodes.Clear();
        }

        public void Undo()
        {
            foreach (TreeNode node in ListChildNodes)
            {
                this.tvRoot.Nodes.Add(node);
            }

            this.tvRoot.ExpandAll();
        }
    }
}
