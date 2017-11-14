using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.CommandManager.CommandTreeNode
{
    public class TreeNodeAdd : IUndoCommand
    {
        private TreeView tvRoot;
        private TreeNode ParentNode;
        private TreeNode ChildNode;
        private int Index;
        delegate void SetTextCallback();

        public TreeNodeAdd(TreeView tv, TreeNode p, TreeNode c, int i)
        {
            this.tvRoot = tv;
            this.ParentNode = p;
            this.ChildNode = c;
            this.Index = i;
        }

        public void Execute()
        {
            //this.tvRoot.BeginInvoke(
            //    new MethodInvoker(
            //        delegate
            //        {
            if (null != this.ParentNode)
            {
                if (this.Index >= 0)
                {
                    this.ParentNode.Nodes.Insert(Index, this.ChildNode);
                }
                else
                {
                    this.ParentNode.Nodes.Add(this.ChildNode);
                }

                this.ParentNode.Expand();
                this.tvRoot.SelectedNode = this.ChildNode;
            }
            else
            {
                this.tvRoot.Nodes.Add(this.ChildNode);
            }
            //        }
            //    )
            //);
        }

        public void Undo()
        {
            //this.tvRoot.BeginInvoke(
            //    new MethodInvoker(
            //        delegate
            //        {
                        this.ChildNode.Remove();
            //        }
            //    )
            //);
        }
    }
}
