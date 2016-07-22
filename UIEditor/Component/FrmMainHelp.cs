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
                                                                            ExportControls(page, it4);

                                                                            //if (MyConst.View.KnxGroupBoxType == it4.Name)
                                                                            //{
                                                                            //    #region 添加控件
                                                                            //    var gridNode = it4 as GroupBoxNode;
                                                                            //    if (gridNode != null)
                                                                            //    {
                                                                            //        var grid = gridNode.ToKnx();
                                                                            //        page.GroupBoxs.Add(grid);

                                                                            //        if (it4.Nodes.Count > 0)
                                                                            //        {
                                                                            //            // 添加控件
                                                                            //            foreach (TreeNode it5 in it4.Nodes)
                                                                            //            {
                                                                            //                ExportControls(grid, it5);
                                                                            //            }
                                                                            //        }
                                                                            //    }
                                                                            //    #endregion
                                                                            //}
                                                                            //else
                                                                            //{
                                                                            //    #region 添加控件
                                                                            //    ExportControls(page, it4);
                                                                            //    #endregion
                                                                            //}
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

                case MyConst.Controls.KnxLabelType:
                    var labelNode = node as LabelNode;
                    if (labelNode != null)
                    {
                        var knxLabel = labelNode.ToKnx();
                        view.Controls.Add(knxLabel);
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

                case MyConst.Controls.KnxSliderSwitchType:
                    var sliderSwitchNode = node as SliderSwitchNode;
                    if (sliderSwitchNode != null)
                    {
                        var knxSliderSwitch = sliderSwitchNode.ToKnx();
                        view.Controls.Add(knxSliderSwitch);
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

                case MyConst.Controls.KnxTimerButtonType:
                    var timerTaskButtonNode = node as TimerButtonNode;
                    if (timerTaskButtonNode != null)
                    {
                        var knxTimerTaskButton = timerTaskButtonNode.ToKnx();
                        view.Controls.Add(knxTimerTaskButton);
                    }
                    break;

                case MyConst.Controls.KnxDigitalAdjustment:
                    var digitalAdjustment = node as DigitalAdjustmentNode;
                    if (null != digitalAdjustment)
                    {
                        var knxDigitalAdjustment = digitalAdjustment.ToKnx();
                        view.Controls.Add(knxDigitalAdjustment);
                    }
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    var groupBox = node as GroupBoxNode;
                    if (null != groupBox)
                    {
                        var knxGroupBox = groupBox.ToKnx();
                        view.Controls.Add(knxGroupBox);

                        if (node.Nodes.Count > 0)
                        {
                            // 添加控件
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                ExportControls(knxGroupBox, childNode);
                            }
                        }

                    }
                    break;

                default:
                    MessageBox.Show(ResourceMng.GetString("Message38") + node.Name, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static AppNode ImportNode(KNXApp app/*, TreeView tv, UIEditor.Entity.ViewNode.PropertiesChangedDelegate proChangedDelegate*/)
        {
            AppNode appNode = null;

            if (app != null)
            {
                //tvwAppdata.BeginUpdate();
                //tvwAppdata.Nodes.Clear();

                appNode = new AppNode(app);

                //tvwAppdata.Nodes.Add(appNode);

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
                                        //pageNode.PropertiesChangedEvent += proChangedDelegate;
                                        roomNode.Nodes.Add(pageNode);

                                        // 给页面添加控件
                                        if (itemPage.Controls != null && itemPage.Controls.Count > 0)
                                        {
                                            foreach (var item in itemPage.Controls)
                                            {
                                                AddControlNode(pageNode, item/*, proChangedDelegate*/);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                //tvwAppdata.EndUpdate();
            }

            return appNode;
        }

        /// <summary>
        /// 按 KNX 控件类型，给树上添加控件节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="knxControl"></param>
        private static void AddControlNode(ContainerNode parentNode, KNXControlBase knxControl/*, UIEditor.Entity.ViewNode.PropertiesChangedDelegate proChangedDelegate*/)
        {
            switch (knxControl.GetType().Name)
            {
                case MyConst.Controls.KnxBlindsType:
                    var blindsNode = new BlindsNode(knxControl as KNXBlinds);
                    //blindsNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(blindsNode);
                    break;

                case MyConst.Controls.KnxLabelType:
                    var labelNode = new LabelNode(knxControl as KNXLabel);
                    //labelNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(labelNode);
                    break;

                case MyConst.Controls.KnxSceneButtonType:
                    var sceneButtonNode = new SceneButtonNode(knxControl as KNXSceneButton);
                    //sceneButtonNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(sceneButtonNode);
                    break;

                case MyConst.Controls.KnxSliderSwitchType:
                    var sliderSwitchNode = new SliderSwitchNode(knxControl as KNXSliderSwitch);
                    //sliderSwitchNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(sliderSwitchNode);
                    break;

                case MyConst.Controls.KnxSwitchType:
                    var switchNode = new SwitchNode(knxControl as KNXSwitch);
                    //switchNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(switchNode);
                    break;

                case MyConst.Controls.KnxValueDisplayType:
                    var valueDisplayNode = new ValueDisplayNode(knxControl as KNXValueDisplay);
                    //valueDisplayNode.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(valueDisplayNode);
                    break;

                case MyConst.Controls.KnxTimerButtonType:
                    var timerButton = new TimerButtonNode(knxControl as KNXTimerButton);
                    //timerButton.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(timerButton);
                    break;

                case MyConst.Controls.KnxDigitalAdjustment:
                    var digitalAdjustment = new DigitalAdjustmentNode(knxControl as KNXDigitalAdjustment);
                    //digitalAdjustment.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(digitalAdjustment);
                    break;

                case MyConst.Controls.KnxGroupBoxType:
                    var groupBox = new GroupBoxNode(knxControl as KNXGroupBox);
                    //groupBox.PropertiesChangedEvent += proChangedDelegate;
                    parentNode.Nodes.Add(groupBox);

                    KNXGroupBox knxGroupBox = knxControl as KNXGroupBox;
                    if (knxGroupBox.Controls != null && knxGroupBox.Controls.Count > 0)
                    {
                        foreach (var item in knxGroupBox.Controls)
                        {
                            AddControlNode(groupBox, item/*, proChangedDelegate*/);
                        }
                    }

                    break;

                default:
                    MessageBox.Show(ResourceMng.GetString("Message39") + knxControl.GetType().Name, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
