using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SourceGrid;
using Structure;
using Structure.Control;
using UIEditor.Component;
using UIEditor.Controls;
using UIEditor.Entity;
using UIEditor.Entity.Control;

namespace UIEditor
{
    partial class FrmMain
    {
        #region 添加app，area 等节点

        /// <summary>
        /// 创建新项目
        /// </summary>
        private void AddAppNode(TreeView treeView)
        {
            if (treeView != null)
            {
                ProjectFile = MyConst.DefaultKnxUiProjectName;
                tsslblProjectName.Text = string.Format("Project Name: {0}", ProjectFile);

                var root = new AppNode();

                // 清除所有节点，添加根
                treeView.Nodes.Clear();
                treeView.Nodes.Add(root);

                CreateProjectFolder();

                // 复制默认图像到项目资源路径
                if (File.Exists(MyConst.DefaultIcon))
                {
                    File.Copy(MyConst.DefaultIcon, Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon));
                }

                // 
                ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
                SetButtonStatus(status);

                Saved = false;
            }
        }

        private void AddAreaNode(TreeNode appNode)
        {
            // 添加楼层
            if (appNode != null && appNode.Name == MyConst.View.KnxAppType)
            {
                var areaNode = new AreaNode();
                // 
                appNode.Nodes.Add(areaNode);
                appNode.Expand();

                Saved = false;
            }
        }

        private void AddRoomNode(TreeNode areaNode)
        {
            // 添加
            if (areaNode != null && areaNode.Name == MyConst.View.KnxAreaType)
            {
                var roomNode = new RoomNode();
                roomNode.ContextMenuStrip = cmsAddPage;
                areaNode.Nodes.Add(roomNode);
                areaNode.Expand();

                Saved = false;
            }
        }

        private void AddPageNode(TreeNode roomNode)
        {
            if (roomNode != null && roomNode.Name == MyConst.View.KnxRoomType)
            {
                var pageNode = new PageNode();
                pageNode.ContextMenuStrip = cmsAddGrid;
                roomNode.Nodes.Add(pageNode);
                roomNode.Expand();

                Saved = false;
            }
        }

        /// <summary>
        /// 创建界面布局控件
        /// </summary>
        /// <param name="pageNode"></param>
        private void AddGridNode(TreeNode pageNode)
        {
            if (pageNode != null && pageNode.Name == MyConst.View.KnxPageType)
            {
                var gridNode = new GridNode();
                gridNode.ContextMenuStrip = cmsAddControl;
                pageNode.Nodes.Add(gridNode);
                pageNode.Expand();

                Saved = false;
            }
        }

        #endregion

        #region 添加控件节点

        private void AddBlindsNode(TreeNode parentNode)
        {
            if (parentNode != null && (parentNode.Name == MyConst.View.KnxGridType || parentNode.Name == MyConst.View.KnxPageType))
            {
                var node = new BlindsNode();

                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddColorLightNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxGridType || parentNode.Name == MyConst.View.KnxPageType))
            {
                var node = new ColorLightNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddLabelNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new LabelNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddMediaButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new MediaButtonNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSceneButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SceneButtonNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSIPCallNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SIPCallNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSlideNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SliderNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSliderSwitchNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SliderSwitchNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSnapperNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SnapperNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSnapperSwitchNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SnapperSwitchNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddSwitchNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new SwitchNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddValueDisplayNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new ValueDisplayNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddWebcamViewerNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new WebcamViewerNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }


        private void AddImageButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
               (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new ImageButtonNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddTimerButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new TimerButtonNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        private void AddTimerTaskListViewNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.View.KnxGridType))
            {
                var node = new TimerTaskListViewNode();
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                Saved = false;
            }
        }

        #endregion

        #region 删除当前节点

        /// <summary>
        /// 删除当前选中的节点
        /// </summary>
        private void DeleteSelectedNode(TreeNode selectedNode)
        {
            if (selectedNode != null)
            {
                if (DialogResult.OK == MessageBox.Show("你是否要删除当前节点", "删除当前节点", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                {
                    selectedNode.Remove();

                    Saved = false;
                }
            }

            if (tvwAppdata.Nodes.Count == 0)
            {
                ToolBarStatus status = new ToolBarStatus();
                SetButtonStatus(status);
                tsslblProjectName.Text = "";

                InitGrid();
            }
        }

        #endregion
    }
}
