using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.CommandManager.CommandTreeNode
{
    public class TreeNodeRemove :IUndoCommand
    {
        private TreeView tvRoot;
        private TreeNode ParentNode;
        private TreeNode ChildNode;
        private int Index;

        public TreeNodeRemove(TreeView tv, TreeNode node) 
        {
            this.tvRoot = tv;
            this.ParentNode = node.Parent;
            this.ChildNode = node;
            Index = node.Index;
        }

        public void Execute()
        {
            //this.tvRoot.BeginInvoke(
            //    new MethodInvoker(
            //        delegate
            //        {
            if (null != this.ChildNode)
            {
                this.ChildNode.Remove();
            }
            else
            {
                this.tvRoot.Nodes.Clear();
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
                        if (null != this.ParentNode)
                        {
                            this.ParentNode.Nodes.Insert(this.Index, this.ChildNode);
                            this.ParentNode.Expand();
                        }
                        else
                        {
                            this.tvRoot.Nodes.Add(this.ChildNode);
                            this.tvRoot.ExpandAll();
                        }

                        this.tvRoot.SelectedNode = this.ChildNode;
            //        }
            //    )
            //);
        }
    }
}
