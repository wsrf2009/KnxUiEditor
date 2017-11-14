using Structure;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity.Control;

namespace UIEditor.Entity
{
    public class EntityHelper
    {
        public static List<ViewNode> CopyControls(List<ViewNode> nodes)
        {
            var list = new List<ViewNode>();

            Rectangle rect = ViewNode.GetMinimumCommonRectangleInPage(nodes);
            int newWidth = rect.Width;
            int newHeight = rect.Height;

            foreach (ViewNode node in nodes)
            {
                ViewNode nodeCopy = node.Copy() as ViewNode;
                nodeCopy.Location = new Point(nodeCopy.LocationInPageFact.X - rect.X, nodeCopy.LocationInPageFact.Y - rect.Y);
                list.Add(nodeCopy);
            }

            return list;
        }

        public static bool IsControlNodeAndNotChildNode(string name)
        {
            bool result = false;

            try
            {
                switch (name)
                {
                    case MyConst.Controls.KnxBlindsType:
                    case MyConst.Controls.KnxDigitalAdjustmentType:
                    case MyConst.Controls.KnxLabelType:
                    case MyConst.Controls.KnxSceneButtonType:
                    case MyConst.Controls.KnxSliderSwitchType:
                    case MyConst.Controls.KnxSwitchType:
                    case MyConst.Controls.KnxTimerButtonType:
                    case MyConst.Controls.KnxValueDisplayType:
                    case MyConst.Controls.KnxImageButtonType:
                    case MyConst.Controls.KnxShutterType:
                    case MyConst.Controls.KnxDimmerType:
                    case MyConst.Controls.KnxWebCamViewerType:
                    case MyConst.Controls.KnxMediaButtonType:
                    case MyConst.Controls.KnxAirConditionType:
                    case MyConst.Controls.KnxHVACType:
                        result = true;
                        break;

                    default:
                        result = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public static bool IsControlNode(string name)
        {
            bool result = false;

            try
            {
                if (MyConst.Controls.KnxGroupBoxType == name)
                {
                    result = true;
                }
                else if (IsControlNodeAndNotChildNode(name))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        #region 导出
        #region 导出到工程文件 .knxuie
        /// <summary>
        /// 导出树形结构为json文件
        /// </summary>
        /// <param name="tvwAppdata"></param>
        /// <param name="fileName"></param>
        public static KNXApp ExportAppNodeAndResources(AppNode appNode, BackgroundWorker worker)
        {
            KNXApp app = new KNXApp();

            if (appNode != null)
            {
                app = appNode.ToKnx(worker);
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
                                var area = areaNode.ToKnx(worker);
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
                                                var room = roomNode.ToKnx(worker);
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
                                                                PageNode mPageNode = pageNode.GetTwinsPageNode();
                                                                if (null != mPageNode)
                                                                {
                                                                    var page = mPageNode.ToKnx(worker);
                                                                    room.Pages.Add(page);

                                                                    if (mPageNode.Nodes.Count > 0)
                                                                    {
                                                                        // 添加 grid
                                                                        foreach (TreeNode it4 in mPageNode.Nodes)
                                                                        {
                                                                            ExportControls(page, it4, worker);
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
            }

            return app;
        }

        /// <summary>
        /// 按树节点的类型，转为相应的KNX对象
        /// </summary>
        /// <param name="view"></param>
        /// <param name="node"></param>
        private static KNXControlBase ExportControls(KNXContainer view, TreeNode node, BackgroundWorker worker)
        {
            KNXControlBase knx = null;

            // 节点的类型
            switch (node.Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    knx = (node as BlindsNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxLabelType:
                    knx = (node as LabelNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    knx = (node as SceneButtonNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    knx = (node as SliderSwitchNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    knx = (node as SwitchNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    knx = (node as ValueDisplayNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    knx = (node as TimerButtonNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxDigitalAdjustmentType:
                    knx = (node as DigitalAdjustmentNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    knx = (node as ImageButtonNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxShutterType:
                    knx = (node as ShutterNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxDimmerType:
                    knx = (node as DimmerNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    knx = (node as WebCamerNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    knx = (node as MediaButtonNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxAirConditionType:
                    knx = (node as AirConditionNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxHVACType:
                    knx = (node as HVACNode).ToKnx(worker);
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    knx = (node as GroupBoxNode).ToKnx(worker);

                    if (node.Nodes.Count > 0)
                    {
                        // 添加控件
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            ExportControls(knx as KNXContainer, childNode, worker);
                        }
                    }
                    break;

                default:
                    MessageBox.Show(UIResMang.GetString("Message38") + node.Name, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            if ((null != knx) && (null != view))
            {
                view.Controls.Add(knx);
            }

            return knx;
        }
        #endregion

        #region 导出到模板文件 .tpl
        public static KNXView ExportViewNode(BackgroundWorker worker, ViewNode node, string ImgResDir, Point RelPoint)
        {
            KNXView knx = null;


            if (MyConst.View.KnxAppType == node.Name)
            {
                knx = ExportApp(worker, node as AppNode, ImgResDir);
            }
            else if (MyConst.View.KnxAreaType == node.Name)
            {
                knx = ExportArea(worker, null, node as AreaNode, ImgResDir);
            }
            else if (MyConst.View.KnxRoomType == node.Name)
            {
                knx = ExportRoom(worker, null, node as RoomNode, ImgResDir);
            }
            else if (MyConst.View.KnxPageType == node.Name)
            {
                knx = ExportPage(worker, null, node as PageNode, ImgResDir);
            }
            else if (IsControlNode(node.Name))
            {
                knx = ExportControl(worker, null, node, ImgResDir, RelPoint);
            }

            return knx;
        }

        private static KNXApp ExportApp(BackgroundWorker worker, AppNode node, string ImgResDir)
        {
            KNXApp app = node.ExportTo(worker, ImgResDir);
            foreach (AreaNode cNode in node.Nodes)
            {
                ExportArea(worker, app, cNode, ImgResDir);
            }

            return app;
        }

        private static KNXArea ExportArea(BackgroundWorker worker, KNXApp app, AreaNode node, string ImgResDir)
        {
            KNXArea area = node.ExportTo(worker, ImgResDir);
            foreach (RoomNode cNode in node.Nodes)
            {
                ExportRoom(worker, area, cNode, ImgResDir);
            }

            if (null != app)
            {
                app.Areas.Add(area);
            }

            return area;
        }

        private static KNXRoom ExportRoom(BackgroundWorker worker, KNXArea area, RoomNode node, string ImgResDir)
        {
            KNXRoom room = node.ExportTo(worker, ImgResDir);
            foreach (PageNode cNode in node.Nodes)
            {
                ExportPage(worker, room, cNode, ImgResDir);
            }

            if (null != area)
            {
                area.Rooms.Add(room);
            }

            return room;
        }

        private static KNXPage ExportPage(BackgroundWorker worker, KNXRoom room, PageNode node, string ImgResDir)
        {
            KNXPage page = node.ExportTo(worker, ImgResDir);
            foreach (ViewNode cNode in node.Nodes)
            {
                ExportControl(worker, page, cNode, ImgResDir, Point.Empty);
            }

            if (null != room)
            {
                room.Pages.Add(page);
            }

            return page;
        }

        /// <summary>
        /// 将ViewNode对象转为KNXControlBase对象
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static KNXControlBase ExportControl(BackgroundWorker worker, KNXContainer parent, ViewNode node, string ImgResDir, Point RelPoint)
        {
            KNXControlBase knx = null;

            // 节点的类型
            switch (node.Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    knx = (node as BlindsNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxLabelType:
                    knx = (node as LabelNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    knx = (node as SceneButtonNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    knx = (node as SliderSwitchNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    knx = (node as SwitchNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    knx = (node as ValueDisplayNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    knx = (node as TimerButtonNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxDigitalAdjustmentType:
                    knx = (node as DigitalAdjustmentNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    knx = (node as ImageButtonNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxShutterType:
                    knx = (node as ShutterNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxDimmerType:
                    knx = (node as DimmerNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    knx = (node as WebCamerNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    knx = (node as MediaButtonNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxAirConditionType:
                    knx = (node as AirConditionNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxHVACType:
                    knx = (node as HVACNode).ExportTo(worker, ImgResDir, RelPoint);
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    knx = (node as GroupBoxNode).ExportTo(worker, ImgResDir, RelPoint);

                    if (node.Nodes.Count > 0)
                    {
                        // 添加控件
                        foreach (ViewNode childNode in node.Nodes)
                        {
                            ExportControl(worker, knx as KNXContainer, childNode, ImgResDir, node.LocationInPageFact);
                        }
                    }
                    break;

                default:
                    MessageBox.Show(UIResMang.GetString("Message38") + node.Name, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            if ((null != knx) && (null != parent))
            {
                parent.Controls.Add(knx);
            }

            return knx;
        }
        #endregion
        #endregion

        #region 导入
        #region 从工程文件 .knxuie 导入
        /// <summary>
        /// 导入 JSON 文件，生成 Treeview 节点
        /// </summary>
        /// <param name="app"></param>
        /// <param name="tvwAppdata"></param>
        public static AppNode ImportNode(KNXApp app, BackgroundWorker worker)
        {
            AppNode appNode = null;
            if (app != null)
            {
                appNode = new AppNode(app, worker);
                if (app.Areas != null && app.Areas.Count > 0)
                {
                    foreach (KNXArea itemArea in app.Areas)
                    {
                        var areaNode = new AreaNode(itemArea, worker);
                        appNode.Nodes.Add(areaNode);

                        if (itemArea.Rooms != null && itemArea.Rooms.Count > 0)
                        {
                            foreach (KNXRoom itemRoom in itemArea.Rooms)
                            {
                                var roomNode = new RoomNode(itemRoom, worker);
                                areaNode.Nodes.Add(roomNode);

                                if (itemRoom.Pages != null && itemRoom.Pages.Count > 0)
                                {
                                    foreach (KNXPage itemPage in itemRoom.Pages)
                                    {
                                        var pageNode = new PageNode(itemPage, worker);
                                        roomNode.Nodes.Add(pageNode);

                                        PageNode pageNodeClone = pageNode.CreateTwinsPageNode();

                                        // 给页面添加控件
                                        if (itemPage.Controls != null && itemPage.Controls.Count > 0)
                                        {
                                            foreach (var item in itemPage.Controls)
                                            {
                                                AddControlNode(pageNodeClone, item, worker);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return appNode;
        }

        /// <summary>
        /// 按 KNX 控件类型，给树上添加控件节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="knxControl"></param>
        private static ViewNode AddControlNode(ContainerNode parentNode, KNXControlBase knxControl, BackgroundWorker worker)
        {
            ViewNode node = null;

            switch (knxControl.GetType().Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    node = new BlindsNode(knxControl as KNXBlinds, worker);
                    break;

                case MyConst.Controls.KnxLabelType:
                    node = new LabelNode(knxControl as KNXLabel, worker);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    node = new SceneButtonNode(knxControl as KNXSceneButton, worker);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    node = new SliderSwitchNode(knxControl as KNXSliderSwitch, worker);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    node = new SwitchNode(knxControl as KNXSwitch, worker);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    node = new ValueDisplayNode(knxControl as KNXValueDisplay, worker);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    node = new TimerButtonNode(knxControl as KNXTimerButton, worker);
                    break;

                case MyConst.Controls.KnxDigitalAdjustmentType:
                    node = new DigitalAdjustmentNode(knxControl as KNXDigitalAdjustment, worker);
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    node = new ImageButtonNode(knxControl as KNXImageButton, worker);
                    break;

                case MyConst.Controls.KnxShutterType:
                    node = new ShutterNode(knxControl as KNXShutter, worker);
                    break;

                case MyConst.Controls.KnxDimmerType:
                    node = new DimmerNode(knxControl as KNXDimmer, worker);
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    node = new WebCamerNode(knxControl as KNXWebCamer, worker);
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    node = new MediaButtonNode(knxControl as KNXMediaButton, worker);
                    break;

                case MyConst.Controls.KnxAirConditionType:
                    node = new AirConditionNode(knxControl as KNXAirCondition, worker);
                    break;

                case MyConst.Controls.KnxHVACType:
                    node = new HVACNode(knxControl as KNXHVAC, worker);
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    node = new GroupBoxNode(knxControl as KNXGroupBox, worker);

                    KNXGroupBox knxGroupBox = knxControl as KNXGroupBox;
                    if (knxGroupBox.Controls != null && knxGroupBox.Controls.Count > 0)
                    {
                        foreach (var item in knxGroupBox.Controls)
                        {
                            AddControlNode(node as ContainerNode, item, worker);
                        }
                    }

                    break;

                default:
                    MessageBox.Show(UIResMang.GetString("Message39") + knxControl.GetType().Name, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            parentNode.Nodes.Add(node);

            return node;
        }
        #endregion

        #region 从模板文件 .tpl 导入
        public static ViewNode ImportKNXView(BackgroundWorker worker, KNXView knx, string ResImgDir)
        {
            ViewNode node = null;


            if (MyConst.View.KnxAppType == knx.GetType().Name)
            {
                node = ImportAppNode(worker, knx as KNXApp, ResImgDir);
            }
            else if (MyConst.View.KnxAreaType == knx.GetType().Name)
            {
                node = ImportAreaNode(worker, null, knx as KNXArea, ResImgDir);
            }
            else if (MyConst.View.KnxRoomType == knx.GetType().Name)
            {
                node = ImportRoomNode(worker, null, knx as KNXRoom, ResImgDir);
            }
            else if (MyConst.View.KnxPageType == knx.GetType().Name)
            {
                node = ImportPageNode(worker, null, knx as KNXPage, ResImgDir);
            }
            else if (IsControlNode(knx.GetType().Name))
            {
                node = ImportControlNode(worker, null, knx as KNXControlBase, ResImgDir);
            }

            return node;
        }

        private static AppNode ImportAppNode(BackgroundWorker worker, KNXApp app, string ResImgDir)
        {
            AppNode appNode = new AppNode(app, worker, ResImgDir);
            foreach (KNXArea knx in app.Areas)
            {
                ImportAreaNode(worker, appNode, knx, ResImgDir);
            }
            return appNode;
        }

        private static AreaNode ImportAreaNode(BackgroundWorker worker, AppNode appNode, KNXArea area, string ResImgDir)
        {
            AreaNode areaNode = new AreaNode(area, worker, ResImgDir);
            foreach (KNXRoom knx in area.Rooms)
            {
                ImportRoomNode(worker, areaNode, knx, ResImgDir);
            }
            if (null != appNode)
            {
                appNode.Nodes.Add(areaNode);
            }

            return areaNode;
        }

        private static RoomNode ImportRoomNode(BackgroundWorker worker, AreaNode areaNode, KNXRoom room, string ResImgDir)
        {
            RoomNode roomNode = new RoomNode(room, worker, ResImgDir);
            foreach (KNXPage knx in room.Pages)
            {
                ImportPageNode(worker, roomNode, knx, ResImgDir);
            }
            if (null != areaNode)
            {
                areaNode.Nodes.Add(roomNode);
            }

            return roomNode;
        }

        private static PageNode ImportPageNode(BackgroundWorker worker, RoomNode roomNode, KNXPage page, string ResImgDir)
        {
            PageNode pageNode = new PageNode(page, worker, ResImgDir);
            foreach (KNXControlBase knx in page.Controls)
            {
                ImportControlNode(worker, pageNode, knx, ResImgDir);
            }

            if (null != roomNode)
            {
                roomNode.Nodes.Add(pageNode);
            }

            return pageNode;
        }

        private static ViewNode ImportControlNode(BackgroundWorker worker, ContainerNode parentNode, KNXControlBase knx, string ResImgDir)
        {
            ViewNode node = null;

            switch (knx.GetType().Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    node = new BlindsNode(knx as KNXBlinds, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxLabelType:
                    node = new LabelNode(knx as KNXLabel, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    node = new SceneButtonNode(knx as KNXSceneButton, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    node = new SliderSwitchNode(knx as KNXSliderSwitch, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    node = new SwitchNode(knx as KNXSwitch, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    node = new ValueDisplayNode(knx as KNXValueDisplay, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    node = new TimerButtonNode(knx as KNXTimerButton, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxDigitalAdjustmentType:
                    node = new DigitalAdjustmentNode(knx as KNXDigitalAdjustment, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxImageButtonType:
                    node = new ImageButtonNode(knx as KNXImageButton, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxShutterType:
                    node = new ShutterNode(knx as KNXShutter, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxDimmerType:
                    node = new DimmerNode(knx as KNXDimmer, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxWebCamViewerType:
                    node = new WebCamerNode(knx as KNXWebCamer, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxMediaButtonType:
                    node = new MediaButtonNode(knx as KNXMediaButton, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxAirConditionType:
                    node = new AirConditionNode(knx as KNXAirCondition, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxHVACType:
                    node = new HVACNode(knx as KNXHVAC, worker, ResImgDir);
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    node = new GroupBoxNode(knx as KNXGroupBox, worker, ResImgDir);

                    KNXGroupBox knxGroupBox = knx as KNXGroupBox;
                    if (knxGroupBox.Controls != null && knxGroupBox.Controls.Count > 0)
                    {
                        foreach (var item in knxGroupBox.Controls)
                        {
                            ImportControlNode(worker, node as ContainerNode, item, ResImgDir);
                        }
                    }

                    break;

                default:
                    MessageBox.Show(UIResMang.GetString("Message39") + knx.GetType().Name, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            if (null != parentNode)
            {
                parentNode.Nodes.Add(node);
            }

            return node;
        }
        #endregion
        #endregion
    }
}
