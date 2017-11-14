using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIEditor.Component;

namespace UIEditor.CommandManager
{
    public class CommandQuene : ICommandManager
    {
        #region 变量定义
        private Stack undoStack = new Stack();
        private Stack reverseStack = new Stack();
        #endregion

        #region 通知定义
        public delegate void CommandQueueChangedEvent(object sender, CommandEventArgs e);
        public event CommandQueueChangedEvent UndoStateChanged;
        public event CommandQueueChangedEvent ReverseUndoStateChanged;
        #endregion

        #region 私有方法
        #region 事件通知
        private void UndoStateChangedNotify()
        {
            if (null != this.UndoStateChanged)
            {
                CommandEventArgs e = new CommandEventArgs();
                e.Remain = this.undoStack.Count;
                if (this.undoStack.Count > 0)
                {
                    e.Valid = true;
                }
                else
                {
                    e.Valid = false;
                }
                this.UndoStateChanged(this, e);
            }
        }

        private void ReverseUndoStateChangedNotify()
        {
            if (null != this.ReverseUndoStateChanged)
            {
                CommandEventArgs e = new CommandEventArgs();
                e.Remain = this.reverseStack.Count;
                if (this.reverseStack.Count > 0)
                {
                    e.Valid = true;
                }
                else
                {
                    e.Valid = false;
                }
                this.ReverseUndoStateChanged(this, e);
            }
        }
        #endregion

        #region Undo Stack
        private void ClearUndoStack()
        {
            this.undoStack.Clear();
            UndoStateChangedNotify();
        }

        private void UndoStackPushCommand(IUndoCommand command)
        {
            if (null != command)
            {
                this.undoStack.Push(command);

                UndoStateChangedNotify();
            }
        }

        private IUndoCommand UndoStatckPopCommand()
        {
            if (this.undoStack.Count > 0)
            {
                IUndoCommand command = (IUndoCommand)this.undoStack.Pop();
                UndoStateChangedNotify();
                return command;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region ReverseUndoStack
        private void ClearReverseStack()
        {
            this.reverseStack.Clear();

            ReverseUndoStateChangedNotify();
        }

        private void ReverseStackPushCommand(IUndoCommand command)
        {
            this.reverseStack.Push(command);
            ReverseUndoStateChangedNotify();
        }

        private IUndoCommand ReverseStackPopCommand()
        {
            if (this.reverseStack.Count > 0)
            {
                IUndoCommand command = (IUndoCommand)this.reverseStack.Pop();
                ReverseUndoStateChangedNotify();
                return command;
            }
            else
            {
                return null;
            }
        }
        #endregion
        #endregion

        #region 公共方法
        public void AddCommand(IUndoCommand command)
        {
            ClearReverseStack();
            UndoStackPushCommand(command);
        }

        public void ExecuteCommand(IUndoCommand command)
        {
            command.Execute();
            AddCommand(command);
        }

        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                IUndoCommand command = UndoStatckPopCommand();
                if (command == null)
                {
                    return;
                }

                command.Undo();

                ReverseStackPushCommand(command);
            }
        }

        public void ReverseUndo()
        {
            if (this.reverseStack.Count > 0)
            {
                IUndoCommand command = ReverseStackPopCommand();
                if (command == null)
                {
                    return;
                }

                command.Execute();

                UndoStackPushCommand(command);
            }
        }

        public void Clear()
        {
            ClearUndoStack();
            ClearReverseStack();
        }

        public bool UndoIsValid()
        {
            if (this.undoStack.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReverseIsValid()
        {
            if (this.reverseStack.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    public class CommandEventArgs : EventArgs
    {
        public bool Valid;
        public int Remain;
    }
}
