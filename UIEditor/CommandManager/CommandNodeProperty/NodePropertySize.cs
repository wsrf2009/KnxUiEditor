using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    class NodePropertySize:IUndoCommand
    {
        private ViewNode node;
        private Size OldSize;
        private Size NewSize;

        public NodePropertySize(ViewNode node, Size os, Size ns)
        {
            this.node = node;
            this.OldSize = os;
            this.NewSize = ns;
        }

        public void Execute()
        {
            //this.node.Width = this.NewSize.Width;
            //this.node.Height = this.NewSize.Height;
            this.node.Size = this.NewSize;
        }

        public void Undo()
        {
            //this.node.Width = this.OldSize.Width;
            //this.node.Height = this.OldSize.Height;
            this.node.Size = this.OldSize;
        }
    }
}
