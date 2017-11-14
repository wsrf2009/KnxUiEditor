using Structure;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.Entity;
using UIEditor.Entity.Control;
using Utils;

namespace UIEditor.Component
{
    public class CollectionHelper
    {
        #region 常量
        public static string TemplateExt = ".tpl";
        public static string TemplateFilter = "Template File (*" + TemplateExt + ")" + "|*" + TemplateExt + "|All Files (*.*)|*.*";
        public static string CollectionExt = ".clc";
        public static string CollectionFilter = "Collection File (*" + CollectionExt + ")" + "|*" + CollectionExt + "|All Files (*.*)|*.*";
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        public static void ExportToCollections(List<ViewNode> nodes, string path)
        {
            string name = Path.GetFileNameWithoutExtension(path); // 目标文件的名称，不含扩展名
            string tempDir = Path.Combine(MyCache.ProjTempCollFolder, name); // 收藏文件临时目录

            /* 清空临时目录 */
            FileHelper.DeleteFolder(tempDir);
            Directory.CreateDirectory(tempDir);

            PageNode pageNode = ViewNode.GetPageNodeFromParent(nodes[0]);
            int width = pageNode.Size.Width;
            int height = pageNode.Size.Height;

            /* 在控件的原始位置绘制 */
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            foreach (ViewNode node in nodes)
            {
                node.DrawAt(g, 1.0f, true); // 绘制控件
            }

            Rectangle rect = ViewNode.GetMinimumCommonRectangleInPage(nodes);
            int newWidth = rect.Width;
            int newHeight = rect.Height;
            int Side = newWidth > newHeight ? newWidth : newHeight;
            Side += 10;
            Bitmap img = new Bitmap(Side, Side);
            Graphics graphics = Graphics.FromImage(img);
            graphics.DrawImage(bm, (Side - newWidth) / 2, (Side - newHeight) / 2, rect, GraphicsUnit.Pixel);

            /* 保存预览图片 */
            string preview = Path.Combine(tempDir, MyConst.TemplatePreviewFile);  // 模板预览图片
            ImageHelper.SaveImageAsPNG(img, preview);

            /* 创建图片资源临时存放目录 */
            string tempImgDir = Path.Combine(tempDir, MyConst.ImgFolder);
            Directory.CreateDirectory(tempImgDir);

            /* 创建模板的界面元文件，并导出图片资源 */
            TemplateMeta template = new TemplateMeta();
            foreach (ViewNode node in nodes)
            {
                /* 添加控件到界面元文件 */
                KNXView knx = EntityHelper.ExportViewNode(null, node, tempImgDir, rect.Location);
                template.Views.Add(knx);
            }

            /* 保存界面元文件 */
            string metaFile = Path.Combine(tempDir, MyConst.TemplateMetaFile);  // 界面元文件
            AppStorage.SaveAsFile(template, metaFile); // 保存模板文件

            VersionStorage.SaveTemplateVersionFile(Path.Combine(tempDir, MyConst.TemplateVersion)); // 保存模板文件的版本信息。

            ZipHelper.ZipDir(tempDir, path); // 压缩为最终的收藏文件
        }

        public static string GetDefaultCollectionName()
        {
            return UIResMang.GetString("MyCollection") + (index++).ToString();
        }

        public static List<Template> LoadAllTemplates()
        {
            List<Template> templates = new List<Template>();

            string ClcDir = MyCache.DefatultKnxCollectionFolder; // 默认的收藏文件夹
            if (Directory.Exists(ClcDir))
            {
                foreach (string file in Directory.GetFiles(ClcDir)) // 遍历收藏文件下的文件
                {
                    string ext = Path.GetExtension(file);
                    if (TemplateExt == ext) // 若为模板文件格式（*.tpl）
                    {
                        string name = Path.GetFileNameWithoutExtension(file); // 获取模板文件的名字
                        string tempDir = Path.Combine(MyCache.ProjTempCollFolder, name); // 模板文件解压出来的文件存放的路径

                        /* 清空工作目录 */
                        FileHelper.DeleteFolder(tempDir);
                        Directory.CreateDirectory(tempDir);

                        ZipHelper.UnZipDir(file, tempDir); // 解压收藏文件


                        string tempImgDir = Path.Combine(tempDir, MyConst.ImgFolder); // 图片资源目录

                        string templateVersion = Path.Combine(tempDir, MyConst.TemplateVersion);
                        KNXVersion version = VersionStorage.LoadTemplateVersionFile(templateVersion);

                        string preview = Path.Combine(tempDir, MyConst.TemplatePreviewFile);  // 模板预览图片
                        Image TempPre = ImageHelper.GetDiskImage(preview);

                        string metaFile = Path.Combine(tempDir, MyConst.TemplateMetaFile); // 界面元文件
                        TemplateMeta TplMeta = AppStorage.Import(metaFile);

                        Template template = new Template();
                        template.Views = TplMeta.Views;
                        template.Name = name;
                        template.Preview = TempPre;
                        template.ResImgDir = tempImgDir;
                        template.Version = version;

                        templates.Add(template);
                    }
                }
            }

            return templates;
        }

        public static List<ViewNode> ImportTemplate(Template tpl)
        {
            MyCache.VersionOfImportedFile = tpl.Version;

            List<ViewNode> list = new List<ViewNode>();
            
            foreach (KNXView knx in tpl.Views)
            {
                ViewNode node = EntityHelper.ImportKNXView(null, knx as KNXControlBase, tpl.ResImgDir);
                list.Add(node);
            }

            return list;
        }
    }
}
