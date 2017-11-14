using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertyBound:IUndoCommand
    {
        private ViewNode node;
        private Rectangle OldRectangle;
        private Rectangle NewRectangle;

        public NodePropertyBound(ViewNode node, Rectangle or, Rectangle nr)
        {
            this.node = node;
            this.OldRectangle = or;
            this.NewRectangle = nr;
        }

        public void Execute()
        {
            //this.node.X = this.NewRectangle.X;
            //this.node.Y = this.NewRectangle.Y;
            //this.node.Width = this.NewRectangle.Width;
            //this.node.Height = this.NewRectangle.Height;
            this.node.Location = this.NewRectangle.Location;
            this.node.Size = this.NewRectangle.Size;
        }

        public void Undo()
        {
            //this.node.X = this.OldRectangle.X;
            //this.node.Y = this.OldRectangle.Y;
            //this.node.Width = this.OldRectangle.Width;
            //this.node.Height = this.OldRectangle.Height;
            this.node.Location = this.OldRectangle.Location;
            this.node.Size = this.OldRectangle.Size;
        }
    }
}
