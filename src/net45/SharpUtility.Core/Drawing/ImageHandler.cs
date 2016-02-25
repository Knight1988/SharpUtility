using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SharpUtility.Core.Drawing
{
    /// <summary>
    ///     Class contaning method to resize an image
    /// </summary>
    public static class ImageHandler
    {
        /// <summary>
        /// Create Oval image
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image OvalImage(this Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, img.Width, img.Height);
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(img, Point.Empty);
                }
            }
            return bmp;
        }

        /// <summary>
        /// Resize image and crop
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap ResizeCrop(this Bitmap image, int width, int height)
        {
            var ratio = image.Width > image.Height ? height/(double) image.Height : width/(double) image.Width;

            var newWidth = (int) (image.Width*ratio);
            var newHeight = (int) (image.Height*ratio);

            var resiziedImage = image.ResizeKeepAspectRatio(newWidth, newHeight);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = (int) (-(newWidth - width)/2d);
            var y = (int) (-(newHeight - height)/2d);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(resiziedImage, x, y, resiziedImage.Width, resiziedImage.Height);
            }
            return newImage;
        }

        /// <summary>
        /// Resize image and crop
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image ResizeCrop(this Image image, int width, int height)
        {
            var ratio = image.Width > image.Height ? height/(double) image.Height : width/(double) image.Width;

            var newWidth = (int) (image.Width*ratio);
            var newHeight = (int) (image.Height*ratio);

            var resiziedImage = image.ResizeKeepAspectRatio(newWidth, newHeight);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = (int) (-(newWidth - width)/2d);
            var y = (int) (-(newHeight - height)/2d);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(resiziedImage, x, y, resiziedImage.Width, resiziedImage.Height);
            }
            return newImage;
        }

        /// <summary>
        /// Resize and fill blank with color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fillColor"></param>
        /// <returns></returns>
        public static Bitmap ResizeFill(this Bitmap image, int width, int height, Brush fillColor)
        {
            var resiziedImage = image.ResizeKeepAspectRatio(width, height);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = width/2 - resiziedImage.Width/2;
            var y = height/2 - resiziedImage.Height/2;

            // Draws the image in the specified size with quality mode set to HighQuality
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.FillRectangle(fillColor, 0, 0, newImage.Width, newImage.Height);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(resiziedImage, x, y, resiziedImage.Width, resiziedImage.Height);
            }
            return newImage;
        }

        /// <summary>
        /// Resize and fill blank with color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fillColor"></param>
        /// <returns></returns>
        public static Image ResizeFill(this Image image, int width, int height, Brush fillColor)
        {
            var resiziedImage = image.ResizeKeepAspectRatio(width, height);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = width/2 - resiziedImage.Width/2;
            var y = height/2 - resiziedImage.Height/2;

            // Draws the image in the specified size with quality mode set to HighQuality
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.FillRectangle(fillColor, 0, 0, newImage.Width, newImage.Height);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(resiziedImage, x, y, resiziedImage.Width, resiziedImage.Height);
            }
            return newImage;
        }

        /// <summary>
        /// Resize but keep apect ratio
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static Bitmap ResizeKeepAspectRatio(this Bitmap image, int maxWidth, int maxHeight)
        {
            // Get the image's original width and height
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // To preserve the aspect ratio
            var ratioX = maxWidth/(float) originalWidth;
            var ratioY = maxHeight/(float) originalHeight;
            var ratio = System.Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            var newWidth = (int) (originalWidth*ratio);
            var newHeight = (int) (originalHeight*ratio);

            return new Bitmap(image, newWidth, newHeight);
        }

        /// <summary>
        /// Resize but keep apect ratio
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static Image ResizeKeepAspectRatio(this Image image, int maxWidth, int maxHeight)
        {
            // Get the image's original width and height
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // To preserve the aspect ratio
            var ratioX = maxWidth/(float) originalWidth;
            var ratioY = maxHeight/(float) originalHeight;
            var ratio = System.Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            var newWidth = (int) (originalWidth*ratio);
            var newHeight = (int) (originalHeight*ratio);

            return new Bitmap(image, newWidth, newHeight);
        }

        /// <summary>
        ///     Make image round corner
        /// </summary>
        /// <param name="startImage"></param>
        /// <param name="cornerRadius"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static Image RoundCorners(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            var roundedImage = new Bitmap(startImage.Width, startImage.Height);
            var g = Graphics.FromImage(roundedImage);
            g.Clear(backgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(startImage);
            var gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius,
                cornerRadius, 0, 90);
            gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return roundedImage;
        }

        /// <summary>
        /// Convert image to byte array
        /// </summary>
        /// <param name="imageIn"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Image imageIn, ImageFormat imageFormat)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, imageFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// Convert byte array to image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image ToImage(this byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}