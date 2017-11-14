using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    public class NodePropertyX : IUndoCommand
    {
        private ViewNode node;
        private int OldX;
        private int NewX;

        public NodePropertyX(ViewNode node, int o, int n)
        {
            this.node = node;
            this.OldX = o;
            this.NewX = n;
        }

        public void Execute()
        {
            //node.X = NewX;
            node.Location = new Point(NewX, node.Location.Y);
        }

        public void Undo()
        {
            //node.X = OldX;
            node.Location = new Point(OldX, node.Location.Y);
        }
    }
}
