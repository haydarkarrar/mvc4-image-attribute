using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageAttributes
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private int sizeInBytes = int.MaxValue;
        public FileSizeAttribute(int sizeInBytes)
        {
            this.sizeInBytes = sizeInBytes;
        }
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            //should be validated by RequiredAttribute
            if (file == null)
            {
                return true;
            }

            if (file.ContentLength < sizeInBytes)
            {
                return true;
            }

            return false;
        }
    }
}