using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertyHeight:IUndoCommand
    {
        private ViewNode node;
        private int OldHeight;
        private int NewHeight;

        public NodePropertyHeight(ViewNode node, int o, int n)
        {
            this.node = node;
            this.OldHeight = o;
            this.NewHeight = n;
        }

        public void Execute()
        {
            //node.Height = NewHeight;
            node.Size = new Size(node.Size.Width, NewHeight);
        }

        public void Undo()
        {
            //node.Height = OldHeight;
            node.Size = new Size(node.Size.Width, OldHeight);
        }
    }
}
