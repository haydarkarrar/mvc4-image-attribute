using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ImageAttributes
{
    public enum ImageDimentionOption
    {
        SmallerThan,
        LargerThan,
        EqualTo
    }
    public class ImageDimentionAttribute : ValidationAttribute
    {
        private int height;
        private int width;
        private ImageDimentionOption imageDimentionOption;

        public ImageDimentionAttribute(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.imageDimentionOption = ImageDimentionOption.EqualTo;
        }

        public ImageDimentionAttribute(int width, int height, ImageDimentionOption option)
        {
            this.width = width;
            this.height = height;
            this.imageDimentionOption = option;
        }

        public override bool IsValid(object value)
        {
            //should be checked by RequiredAttribute
            if (value == null)
            {
                return true;
            }

            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                try
                {
                    var image = new Bitmap(file.InputStream);
                    if (image != null)
                    {
                        if ((this.imageDimentionOption == ImageDimentionOption.LargerThan) && (image.Width > width) && (image.Height > height))
                        {
                            return true;
                        }
                        else if ((this.imageDimentionOption == ImageDimentionOption.SmallerThan) && (image.Width < width) && (image.Height < height))
                        {
                            return true;
                        }
                        else if ((this.imageDimentionOption == ImageDimentionOption.EqualTo) && (image.Width == width) && (image.Height == height))
                        {
                            return true;
                        }
                    }
                }
                catch { }
            }

            return false;
        }
    }
}