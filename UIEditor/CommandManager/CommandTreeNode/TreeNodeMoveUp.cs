using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.CommandManager.CommandTreeNode
{
    public class TreeNodeMoveUp : IUndoCommand
    {
        private TreeView tvRoot;
        private TreeNode ParentNode;
        private TreeNode ChildNode;
        private int Index;

        public TreeNodeMoveUp(TreeView tv, TreeNode c, int i)
        {
            this.tvRoot = tv;
            this.ParentNode = c.Parent;
            this.ChildNode = c;
            this.Index = i;
        }

        public void Execute()
        {
            this.ChildNode.Remove();
            this.ParentNode.Nodes.Insert(this.Index, this.ChildNode);
            this.tvRoot.SelectedNode = this.ChildNode;
        }

        public void Undo()
        {
            this.ChildNode.Remove();
            this.ParentNode.Nodes.Insert(this.Index + 1, this.ChildNode);
            this.tvRoot.SelectedNode = this.ChildNode;
        }
    }
}
