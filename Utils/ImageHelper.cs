using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Utils
{
    public static class ImageHelper
    {
        private static Dictionary<string, Bitmap> _Images;
        private static Dictionary<string, Bitmap> Images
        {
            get
            {
                if (null == _Images)
                {
                    _Images = new Dictionary<string, Bitmap>();
                }

                return _Images;
            }
            set
            {
                _Images = value;
            }
        }

        private static Size iconSize = new Size(16, 16);

        public static Image Crop(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        /// <summary>
        /// 对给定的Image重新设置大小
        /// </summary>
        /// <param name="imgToResize">需要更改Size的Image</param>
        /// <param name="size">新的Size</param>
        /// <param name="aspectRatio">是否要遵循Image原有的长宽比</param>
        /// <returns>返回更改Size后的Image</returns>
        public static Image Resize(Image imgToResize, Size size, bool aspectRatio)
        {
            int destWidth = size.Width;
            int destHeight = size.Height;

            if (aspectRatio)
            {
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                }
                else
                {
                    nPercent = nPercentW;
                }

                destWidth = (int)(sourceWidth * nPercent);
                destHeight = (int)(sourceHeight * nPercent);

                //Console.WriteLine("destWidth:" + destWidth + " destHeight:" + destHeight);
            }

            //Console.WriteLine("destWidth:" + destWidth);
            //Console.WriteLine("destHeight:" + destHeight);

            if (destWidth > 0 && destHeight > 0)
            {
                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                return (Image)b;
            }
            else
            {
                return null;
            }
        }


        public static Image LoadImage(string filename)
        {
            if (null == filename)
            {
                return null;
            }

            if (File.Exists(filename))
            {
                var vimage = Image.FromFile(filename);
                return Resize(vimage, iconSize, true);
            }

            return null;
        }

        public static Image CreateImage(string colorValue)
        {
            if (null == colorValue)
            {
                return null;
            }

            Bitmap image = new System.Drawing.Bitmap(iconSize.Width, iconSize.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);//建立这个Bitmap的Graphics.
            g.Clear(ColorTranslator.FromHtml(colorValue));//这里用指定的颜色刷新整个Bitmap. 

            return image;
        }

        public static Image CreateImage(Color c)
        {
            if (null == c)
            {
                return null;
            }

            Bitmap image = new System.Drawing.Bitmap(iconSize.Width, iconSize.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);//建立这个Bitmap的Graphics.
            g.Clear(c);//这里用指定的颜色刷新整个Bitmap. 

            return image;
        }

        public static Image CreateImage(Font font)
        {
            Bitmap image = new Bitmap(iconSize.Width, iconSize.Height);
            //创建Graphics
            Graphics g = Graphics.FromImage(image);
            //try
            //{
            //清空图片背景颜色
            g.Clear(Color.White);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.2f, true);
            g.DrawString("F", font, brush, 2, 2);
            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);
            //}
            //catch (Exception e) 
            //{
            //    g.Dispose();
            //    image.Dispose();
            //}

            return image;
        }

        public static void SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
            {
                return;
            }

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                {
                    return codecs[i];
                }
            }
            return null;
        }

        public static Bitmap GetDiskImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            try
            {
                if (File.Exists(path))
                {
                    if (Images.ContainsKey(path))
                    {
                        return Images[path];
                    }
                    else
                    {
                        Image img = Image.FromFile(path);

                        Bitmap cloneImage = new Bitmap(img);
                        img.Dispose();

                        return cloneImage;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static void SaveImageAsPNG(Image img, string name)
        {
            if ((null == img) || (string.IsNullOrEmpty(name)))
            {
                return;
            }

            try
            {
                img.Save(name, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool IsSameImg(Bitmap img, Bitmap bmp)
        {
            //大小一致
            if (img.Width == bmp.Width && img.Height == bmp.Height)
            {
                //将图片一锁定到内存
                BitmapData imgData_i = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_i = imgData_i.Scan0;
                int length_i = imgData_i.Width * imgData_i.Height * 3;
                byte[] imgValue_i = new byte[length_i];
                Marshal.Copy(ipr_i, imgValue_i, 0, length_i);
                img.UnlockBits(imgData_i);
                //将图片二锁定到内存
                BitmapData imgData_b = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_b = imgData_b.Scan0;
                int length_b = imgData_b.Width * imgData_b.Height * 3;
                byte[] imgValue_b = new byte[length_b];
                Marshal.Copy(ipr_b, imgValue_b, 0, length_b);
                img.UnlockBits(imgData_b);
                //长度不相同
                if (length_i != length_b)
                {
                    return false;
                }
                else
                {
                    //循环判断值
                    for (int i = 0; i < length_i; i++)
                    {
                        //不一致
                        if (imgValue_i[i] != imgValue_b[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ImageCompareString(Bitmap firstImage, Bitmap secondImage)
        {
            MemoryStream ms = new MemoryStream();
            firstImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String firstBitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;

            secondImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String secondBitmap = Convert.ToBase64String(ms.ToArray());

            if (firstBitmap.Equals(secondBitmap))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsSameImage(string path1, string path2)
        {
            if (null == path1 || null == path2)
            {
                return false;
            }

            if (!(File.Exists(path1)) || !(File.Exists(path2)))
            {
                return false;
            }

            return ImageCompareString(GetDiskImage(path1), GetDiskImage(path2));
        }
    }
}
