using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertyWidth:IUndoCommand
    {
        private ViewNode node;
        private int OldWidth;
        private int NewWidth;

        public NodePropertyWidth(ViewNode node, int o, int n)
        {
            this.node = node;
            this.OldWidth = o;
            this.NewWidth = n;
        }

        public void Execute()
        {
            //node.Width = NewWidth;
            node.Size = new Size(NewWidth, node.Size.Height);
        }

        public void Undo()
        {
            //node.Width = OldWidth;
            node.Size = new Size(OldWidth, node.Size.Height);
        }
    }
}
