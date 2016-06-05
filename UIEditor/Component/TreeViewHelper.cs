using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Component
{
    public class TreeViewHelper
    {
        /// <summary>
        /// 设置TreeView选中节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectStr">选中节点文本</param>
        public static void SelectNode2Level(TreeView treeView, string selectStr)
        {
            treeView.CollapseAll();
            treeView.Visible = true;
            treeView.Focus();
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                if (treeView.Nodes[i].Text == selectStr)
                {
                    treeView.SelectedNode = treeView.Nodes[i];//选中
                    treeView.SelectedNode.Checked = true;
                    return;
                }

                for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                {
                    if (treeView.Nodes[i].Nodes[j].Text == selectStr)
                    {
                        treeView.SelectedNode = treeView.Nodes[i].Nodes[j];//选中
                        treeView.SelectedNode.Checked = true;
                        treeView.Nodes[i].Expand();//展开父级
                        return;
                    }
                }
            }
        }
    }
}
