using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SharpUtility.Core.Drawing
{
    /// <summary>
    ///     Class contaning method to resize an image and save in JPEG format
    /// </summary>
    public static class ImageHandler
    {
        public static Bitmap ResizeCrop(this Bitmap image, int width, int height)
        {
            var ratio = image.Width > image.Height ? height / (double)image.Height : width / (double)image.Width;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var resiziedImage = image.ResizeKeepAspectRatio(newWidth, newHeight);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = (int)(-(newWidth - width) / 2d);
            var y = (int)(-(newHeight - height) / 2d);

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

        public static Bitmap ResizeFill(this Bitmap image, int width, int height, Brush fillColor)
        {
            var resiziedImage = image.ResizeKeepAspectRatio(width, height);
            // Convert other formats (including CMYK) to RGB.
            var newImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            var x = width / 2 - resiziedImage.Width / 2;
            var y = height / 2 - resiziedImage.Height / 2;

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

        public static Bitmap ResizeKeepAspectRatio(this Bitmap image, int maxWidth, int maxHeight)
        {
            // Get the image's original width and height
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // To preserve the aspect ratio
            var ratioX = maxWidth / (float)originalWidth;
            var ratioY = maxHeight / (float)originalHeight;
            var ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            var newWidth = (int)(originalWidth * ratio);
            var newHeight = (int)(originalHeight * ratio);

            return new Bitmap(image, newWidth, newHeight);
        }
    }
}