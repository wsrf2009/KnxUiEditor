using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace UIEditor.Component
{
    public static class ImageHelper
    {

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

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }


        public static Image LoadImage(string filename)
        {
            if (null == filename) {
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

        public static Image CreateImage(Color color)
        {
            Bitmap image = new System.Drawing.Bitmap(iconSize.Width, iconSize.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(image);//建立这个Bitmap的Graphics.
            g.Clear(color);//这里用指定的颜色刷新整个Bitmap. 

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
    }
}
