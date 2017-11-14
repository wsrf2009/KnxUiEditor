using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertyLocation:IUndoCommand
    {
        private ViewNode node;
        private Point OldLocation;
        private Point NewLocation;

        public NodePropertyLocation(ViewNode node, Point ol, Point nl)
        {
            this.node = node;
            this.OldLocation = ol;
            this.NewLocation = nl;
        }

        public void Execute()
        {
            //this.node.X = this.NewLocation.X;
            //this.node.Y = this.NewLocation.Y;
            this.node.Location = this.NewLocation;
        }

        public void Undo()
        {
            //this.node.X = this.OldLocation.X;
            //this.node.Y = this.OldLocation.Y;
            this.node.Location = this.OldLocation;
        }
    }
}
