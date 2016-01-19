using System;
using System.Drawing;
using System.Text;
using Ionic.Zip;
using Newtonsoft.Json;
using Structure;
using Structure.Control;
using Structure.ETS;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.Entity.Control;

namespace UIEditor
{
    public class FrmMainHelp
    {
        #region 导出
        /// <summary>
        /// 导出树形结构为json文件
        /// </summary>
        /// <param name="tvwAppdata"></param>
        /// <param name="fileName"></param>
        public static KNXApp ExportTreeView(TreeView tvwAppdata)
        {
            KNXApp app = new KNXApp();

            if (tvwAppdata.Nodes.Count > 0)
            {
                // 生成应用
                var appNode = tvwAppdata.Nodes[0] as AppNode;
                if (appNode != null)
                {
                    app = appNode.ToKnx();
                    if (appNode.Nodes.Count > 0)
                    {
                        foreach (TreeNode it1 in appNode.Nodes)
                        {
                            #region 添加区域
                            if (MyConst.View.KnxAreaType == it1.Name)
                            {
                                var areaNode = it1 as AreaNode;
                                if (areaNode != null)
                                {
                                    var area = areaNode.ToKnx();
                                    app.Areas.Add(area);

                                    if (it1.Nodes.Count > 0)
                                    {
                                        #region 添加房间
                                        foreach (TreeNode it2 in it1.Nodes)
                                        {
                                            if (MyConst.View.KnxRoomType == it2.Name)
                                            {
                                                var roomNode = it2 as RoomNode;
                                                if (roomNode != null)
                                                {
                                                    var room = roomNode.ToKNX();
                                                    area.Rooms.Add(room);

                                                    if (it2.Nodes.Count > 0)
                                                    {
                                                        #region 添加页面
                                                        foreach (TreeNode it3 in it2.Nodes)
                                                        {
                                                            if (MyConst.View.KnxPageType == it3.Name)
                                                            {
                                                                var pageNode = it3 as PageNode;
                                                                if (pageNode != null)
                                                                {
                                                                    #region 添加页面
                                                                    var page = pageNode.ToKnx();
                                                                    room.Pages.Add(page);

                                                                    if (it3.Nodes.Count > 0)
                                                                    {
                                                                        // 添加 grid
                                                                        foreach (TreeNode it4 in it3.Nodes)
                                                                        {
                                                                            if (MyConst.View.KnxGridType == it4.Name)
                                                                            {
                                                                                #region 添加控件
                                                                                var gridNode = it4 as GridNode;
                                                                                if (gridNode != null)
                                                                                {
                                                                                    var grid = gridNode.ToKnx();
                                                                                    page.Grids.Add(grid);

                                                                                    if (it4.Nodes.Count > 0)
                                                                                    {
                                                                                        // 添加控件
                                                                                        foreach (TreeNode it5 in it4.Nodes)
                                                                                        {
                                                                                            ExportControls(grid, it5);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            else
                                                                            {
                                                                                #region 添加控件
                                                                                ExportControls(page, it4);
                                                                                #endregion
                                                                            }
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                    }

                    //// 写 Json 文件
                    //var settings = new JsonSerializerSettings();
                    //settings.TypeNameHandling = TypeNameHandling.Auto;
                    //string json = JsonConvert.SerializeObject(app, Formatting.Indented, settings);
                    //File.WriteAllText(fileName, json);
                }
            }

            return app;
        }

        /// <summary>
        /// 按树节点的类型，转为相应的KNX对象
        /// </summary>
        /// <param name="view"></param>
        /// <param name="node"></param>
        private static void ExportControls(KNXContainer view, TreeNode node)
        {
            // 节点的类型
            switch (node.Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    var blindsNode = node as BlindsNode;
                    if (blindsNode != null)
                    {
                        var knxBlinds = blindsNode.ToKnx();
                        view.Controls.Add(knxBlinds);
                    }
                    break;

                case MyConst.Controls.KnxColorLightType:
                    var colorLightNode = node as ColorLightNode;
                    if (colorLightNode != null)
                    {
                        var knxColorLight = colorLightNode.ToKnx();
                        view.Controls.Add(knxColorLight);
                    }
                    break;

                case MyConst.Controls.KnxLabelType:
                    var labelNode = node as LabelNode;
                    if (labelNode != null)
                    {
                        var knxLabel = labelNode.ToKnx();
                        view.Controls.Add(knxLabel);
                    }
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    var mediaButtonNode = node as MediaButtonNode;
                    if (mediaButtonNode != null)
                    {
                        var knxMediaButton = mediaButtonNode.ToKnx();
                        view.Controls.Add(knxMediaButton);
                    }
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    var sceneButtonNode = node as SceneButtonNode;
                    if (sceneButtonNode != null)
                    {
                        var knxSceneButton = sceneButtonNode.ToKnx();
                        view.Controls.Add(knxSceneButton);
                    }
                    break;

                case MyConst.Controls.KnxSipCallType:
                    var sipCallNode = node as SIPCallNode;
                    if (sipCallNode != null)
                    {
                        var knxSIPCall = sipCallNode.ToKnx();
                        view.Controls.Add(knxSIPCall);
                    }
                    break;

                case MyConst.Controls.KnxSliderType:
                    var slidNode = node as SliderNode;
                    if (slidNode != null)
                    {
                        var knxSlide = slidNode.ToKnx();
                        view.Controls.Add(knxSlide);
                    }
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    var sliderSwitchNode = node as SliderSwitchNode;
                    if (sliderSwitchNode != null)
                    {
                        var knxSliderSwitch = sliderSwitchNode.ToKnx();
                        view.Controls.Add(knxSliderSwitch);
                    }
                    break;

                case MyConst.Controls.KnxSnapperType:
                    var snapperNode = node as SnapperNode;
                    if (snapperNode != null)
                    {
                        var knxSnapper = snapperNode.ToKnx();
                        view.Controls.Add(knxSnapper);
                    }
                    break;

                case MyConst.Controls.KnxSnapperSwitchType:
                    var snapperSwitchNode = node as SnapperSwitchNode;
                    if (snapperSwitchNode != null)
                    {
                        var knxSnapperSwitch = snapperSwitchNode.ToKnx();
                        view.Controls.Add(knxSnapperSwitch);
                    }
                    break;

                case MyConst.Controls.KnxSwitchType:
                    var switchNode = node as SwitchNode;
                    if (switchNode != null)
                    {
                        var knxSwitch = switchNode.ToKnx();
                        view.Controls.Add(knxSwitch);
                    }
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    var valueDisplayNode = node as ValueDisplayNode;
                    if (valueDisplayNode != null)
                    {
                        var knxValueDisplay = valueDisplayNode.ToKnx();
                        view.Controls.Add(knxValueDisplay);
                    }
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    var webCamViewerNode = node as WebcamViewerNode;
                    if (webCamViewerNode != null)
                    {
                        var knxWebCamViewer = webCamViewerNode.ToKnx();
                        view.Controls.Add(knxWebCamViewer);
                    }
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    var imageButtonNode = node as ImageButtonNode;
                    if (imageButtonNode != null)
                    {
                        var knxImageButton = imageButtonNode.ToKnx();
                        view.Controls.Add(knxImageButton);
                    }
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    var timerTaskButtonNode = node as TimerButtonNode;
                    if (timerTaskButtonNode != null)
                    {
                        var knxTimerTaskButton = timerTaskButtonNode.ToKnx();
                        view.Controls.Add(knxTimerTaskButton);
                    }
                    break;

                case MyConst.Controls.KnxTimerTaskListViewType:
                    var timerTaskListViewNode = node as TimerTaskListViewNode;
                    if (timerTaskListViewNode != null)
                    {
                        var knxTimerTaskListView = timerTaskListViewNode.ToKnx();
                        view.Controls.Add(knxTimerTaskListView);
                    }
                    break;

                default:
                    MessageBox.Show("转换树上的节点为 KNX 对象时出错, 找不到对应的 KNX 类型! 树节点类型： " + node.Name, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入 JSON 文件，生成 Treeview 节点
        /// </summary>
        /// <param name="app"></param>
        /// <param name="tvwAppdata"></param>
        public static void ImportNode(KNXApp app, TreeView tvwAppdata)
        {
            if (app != null)
            {
                tvwAppdata.BeginUpdate();
                tvwAppdata.Nodes.Clear();

                var appNode = new AppNode(app);

                tvwAppdata.Nodes.Add(appNode);

                if (app.Areas != null && app.Areas.Count > 0)
                {
                    foreach (KNXArea itemArea in app.Areas)
                    {
                        var areaNode = new AreaNode(itemArea);
                        appNode.Nodes.Add(areaNode);

                        if (itemArea.Rooms != null && itemArea.Rooms.Count > 0)
                        {
                            foreach (KNXRoom itemRoom in itemArea.Rooms)
                            {
                                var roomNode = new RoomNode(itemRoom);
                                areaNode.Nodes.Add(roomNode);

                                if (itemRoom.Pages != null && itemRoom.Pages.Count > 0)
                                {
                                    foreach (KNXPage itemPage in itemRoom.Pages)
                                    {
                                        var pageNode = new PageNode(itemPage);
                                        roomNode.Nodes.Add(pageNode);

                                        // 给网格添加控件
                                        if (itemPage.Grids != null && itemPage.Grids.Count > 0)
                                        {
                                            foreach (KNXGrid itemGrid in itemPage.Grids)
                                            {
                                                var gridNode = new GridNode(itemGrid);
                                                pageNode.Nodes.Add(gridNode);

                                                if (itemGrid.Controls != null && itemGrid.Controls.Count > 0)
                                                {
                                                    foreach (var item in itemGrid.Controls)
                                                    {
                                                        AddControlNode(gridNode, item);
                                                    }
                                                }
                                            }
                                        }
                                        // 给页面添加控件
                                        if (itemPage.Controls != null && itemPage.Controls.Count > 0)
                                        {
                                            foreach (var item in itemPage.Controls)
                                            {
                                                AddControlNode(pageNode, item);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                tvwAppdata.EndUpdate();
            }
        }

        /// <summary>
        /// 按 KNX 控件类型，给树上添加控件节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="knxControl"></param>
        private static void AddControlNode(ContainerNode parentNode, KNXControlBase knxControl)
        {
            switch (knxControl.GetType().Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    var blindsNode = new BlindsNode(knxControl as KNXBlinds);
                    parentNode.Nodes.Add(blindsNode);
                    break;

                case MyConst.Controls.KnxColorLightType:
                    var colorLightNode = new ColorLightNode(knxControl as KNXColorLight);
                    parentNode.Nodes.Add(colorLightNode);
                    break;

                case MyConst.Controls.KnxLabelType:
                    var labelNode = new LabelNode(knxControl as KNXLabel);
                    parentNode.Nodes.Add(labelNode);
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    var mediaButtonNode = new MediaButtonNode(knxControl as KNXMediaButton);
                    parentNode.Nodes.Add(mediaButtonNode);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    var sceneButtonNode = new SceneButtonNode(knxControl as KNXSceneButton);
                    parentNode.Nodes.Add(sceneButtonNode);
                    break;

                case MyConst.Controls.KnxSipCallType:
                    var sipCallNode = new SIPCallNode(knxControl as KNXSIPCall);
                    parentNode.Nodes.Add(sipCallNode);
                    break;

                case MyConst.Controls.KnxSliderType:
                    var sliderNode = new SliderNode(knxControl as KNXSlider);
                    parentNode.Nodes.Add(sliderNode);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    var sliderSwitchNode = new SliderSwitchNode(knxControl as KNXSliderSwitch);
                    parentNode.Nodes.Add(sliderSwitchNode);
                    break;

                case MyConst.Controls.KnxSnapperType:
                    var snapperNode = new SnapperNode(knxControl as KNXSnapper);
                    parentNode.Nodes.Add(snapperNode);
                    break;

                case MyConst.Controls.KnxSnapperSwitchType:
                    var snapperSwitchNode = new SnapperSwitchNode(knxControl as KNXSnapperSwitch);
                    parentNode.Nodes.Add(snapperSwitchNode);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    var switchNode = new SwitchNode(knxControl as KNXSwitch);
                    parentNode.Nodes.Add(switchNode);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    var valueDisplayNode = new ValueDisplayNode(knxControl as KNXValueDisplay);
                    parentNode.Nodes.Add(valueDisplayNode);
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    var webCamViewerNode = new WebcamViewerNode(knxControl as KNXWebcamViewer);
                    parentNode.Nodes.Add(webCamViewerNode);
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    var imageButton = new ImageButtonNode(knxControl as KNXImageButton);
                    parentNode.Nodes.Add(imageButton);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    var timerButton = new TimerButtonNode(knxControl as KNXTimerButton);
                    parentNode.Nodes.Add(timerButton);
                    break;

                case MyConst.Controls.KnxTimerTaskListViewType:
                    var timerTaskListView = new TimerTaskListViewNode(knxControl as KNXTimerTaskListView);
                    parentNode.Nodes.Add(timerTaskListView);
                    break;

                default:
                    MessageBox.Show("按照 KNX 结构创建树节点时出错， KNX 结构找不到对应的节点类型， KNX结构： " + knxControl.GetType().Name, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 颜色转换为字符串　＃ffffffff
        /// </summary>
        /// <param name="selectedColor"></param>
        /// <returns></returns>
        public static string ColorToHexStr(Color selectedColor)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("#");
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.R }));
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.G }));
            sb.Append(BitConverter.ToString(new byte[] { selectedColor.B }));
            return sb.ToString();
        }

        public static Color HexStrToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }

    }
}
