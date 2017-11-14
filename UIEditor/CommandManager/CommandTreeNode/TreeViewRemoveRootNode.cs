using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.CommandManager.CommandTreeNode
{
    class TreeViewRemoveRootNode:IUndoCommand
    {
        private TreeView tvRoot;
        private TreeNode RootNode;
        private int Index;

        public TreeViewRemoveRootNode(TreeView tv, TreeNode n, int i)
        {
            this.tvRoot = tv;
            this.RootNode = n;
            this.Index = i;
        }

        public void Execute()
        {
            this.RootNode.Remove();
        }

        public void Undo()
        {
            if (this.Index >= 0)
            {
                this.tvRoot.Nodes.Insert(this.Index, this.RootNode);
            }
            else
            {
                this.tvRoot.Nodes.Add(this.RootNode);
            }

            this.tvRoot.ExpandAll();
            this.tvRoot.SelectedNode = this.RootNode;
        }
    }
}
