using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ImageAttributes
{
    public class ImageDimentionAttribute : ValidationAttribute
    {
        private int height;
        private int width;
        
        public ImageDimentionAttribute(int width, int height)
        {
            this.width = width;
            this.height = height;
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
                    if ((image != null) && (image.Width > width) && (image.Height > height))
                    {
                        return true;
                    }
                }
                catch { }
            }

            return false;
        }
    }
}