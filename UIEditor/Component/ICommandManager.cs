using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEditor.Component
{
    public interface ICommandManager
    {
        void ExecuteCommand(IUndoCommand command);
        void Undo();
        void ReverseUndo();//反撤销

        //以下事件可用于控制撤销与反撤销图标的启用
        //event bool UndoStateChanged; //bool参数表明当前是否有可撤销的操作
        //event bool ReverseUndoStateChanged; //bool参数表明当前是否有可反撤销的操作

    }

    // 命令接口，所有能被编辑器接受命令都从这里继承  
    public interface ICommand
    {
        void Execute();
    }  

    // 可撤销的命令借口，所有可撤销的命令都从这里继承  
    public interface IUndoCommand : ICommand
    {
        void Undo();
    } 
}
