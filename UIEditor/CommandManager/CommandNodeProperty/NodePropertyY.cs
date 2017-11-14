using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertyY:IUndoCommand
    {
        private ViewNode node;
        private int OldY;
        private int NewY;

        public NodePropertyY(ViewNode node, int o, int n)
        {
            this.node = node;
            this.OldY = o;
            this.NewY = n;
        }

        public void Execute()
        {
            //node.Y = NewY;
            node.Location = new Point(node.Location.X, NewY);
        }

        public void Undo()
        {
            //node.Y = OldY;
            node.Location = new Point(node.Location.X, OldY);
        }
    }
}
