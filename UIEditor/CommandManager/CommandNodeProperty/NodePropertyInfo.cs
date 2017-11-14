using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.CommandManager.CommandNodeProperty
{
    public class NodePropertyInfo:IUndoCommand
    {
        private object Parent;
        private PropertyInfo pi;
        private object OldValue;
        private object NewValue;

        public NodePropertyInfo(object parent, PropertyInfo pi, object oldVal, object newVal)
        {
            this.Parent = parent;
            this.pi = pi;
            this.OldValue = oldVal;
            this.NewValue = newVal;
        }

        public void Execute()
        {
            pi.SetValue(this.Parent, NewValue, null);
        }

        public void Undo()
        {
            pi.SetValue(this.Parent, OldValue, null);
        }
    }
}
