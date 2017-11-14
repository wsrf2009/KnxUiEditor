using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using UIEditor.Entity;
using UIEditor.Component;
using Utils;

namespace UIEditor.PropertyGridEditor
{
    public class PropertyGridStringImageEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                ViewNode vNode = context.Instance as ViewNode;
                if (null == vNode)
                {
                    return null;
                }

                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = MyConst.PicFilter;
                    ofd.InitialDirectory = MyCache.ProjectResImgDir;
                    if (DialogResult.OK == ofd.ShowDialog())
                    {
                        value = ProjResManager.CopyImageSole(ofd.FileName);
                        //if (ofd.SafeFileName != null)
                        //{
                        //    string selectedFile = ofd.SafeFileName;
                        //    //if (string.IsNullOrEmpty(selectedFile))
                        //    //{
                        //    //    value = null;
                        //    //}
                        //    //else
                        //    //{
                        //        //value = FileHelper.CopyFile(ofd.FileName, MyCache.ProjImgPath);
                        //        string name = Path.GetFileNameWithoutExtension(ofd.FileName).Trim();
                        //        string suffix = Path.GetExtension(ofd.FileName).Trim();

                        //        //string imageFile = Path.Combine(vNode.ImagePath, selectedFile);
                        //        string desFile = Path.Combine(MyCache.ProjImgPath, selectedFile);

                        //        if (File.Exists(desFile))
                        //        {
                        //            //string f1md5 = FileHelper.MD5(imageFile);
                        //            //string f2md5 = FileHelper.MD5(ofd.FileName);
                        //            //if (File.Equals(f1md5, f2md5))
                        //            //{
                        //            //    value = selectedFile;
                        //            //}
                        //            bool isSame = ImageHelper.IsSameImage(ofd.FileName, desFile);
                        //            if (isSame)
                        //            {
                        //                value = selectedFile;
                        //                Console.WriteLine("已存在相同的图片 " + value);
                        //            }
                        //            else if (DialogResult.Yes == MessageBox.Show(string.Format(UIResMang.GetString("Message55"), selectedFile),
                        //            UIResMang.GetString("Message4"), MessageBoxButtons.YesNo))
                        //            {
                        //                try
                        //                {
                        //                    //selectedFile = name + "_1" + suffix;
                        //                    //// 覆写图片文件到资源目录
                        //                    //File.Copy(ofd.FileName, Path.Combine(Application.StartupPath, Path.Combine(vNode.ImagePath, selectedFile)), true);
                        //                    //value = selectedFile;
                        //                    value = FileHelper.CopyFile(ofd.FileName, MyCache.ProjImgPath);
                        //                    Console.WriteLine("已存在同名文件，复制后的文件名 " + value);
                        //                }
                        //                catch (Exception e)
                        //                {
                        //                    MessageBox.Show(string.Format(e.Message, selectedFile), UIResMang.GetString("Message6"), MessageBoxButtons.OK);
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            // 复制图片文件到资源目录
                        //            //File.Copy(ofd.FileName, Path.Combine(Application.StartupPath, imageFile));

                        //            //value = selectedFile;
                        //            value = FileHelper.CopyFile(ofd.FileName, MyCache.ProjImgPath);
                        //            Console.WriteLine("没有同名文件，直接拷贝 " + value);
                        //        }
                        //    //}
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("PropertyGridImageEditor Error : " + ex.Message);
                return value;
            }
            return value;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            base.PaintValue(e);

            ViewNode vNode = e.Context.Instance as ViewNode;
            if (null == vNode)
            {
                return;
            }

            Graphics g = e.Graphics;

            try
            {
                string imageFile = Path.Combine(MyCache.ProjImgPath, (string)e.Value);
                //string imageFile = Path.Combine(vNode.ImagePath, (string)e.Value);
                Image img = ImageHelper.GetDiskImage(imageFile);
                g.DrawImage(img, new Rectangle(1, 1, 20, 14));
            }
            catch (Exception ex)
            {
                Console.WriteLine("PropertyGridImageEditor Error:" + ex.Message);
            }
        }
    }
}
