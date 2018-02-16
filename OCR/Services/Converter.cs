using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Services
{
    public static class Converter
    {
        public static string ConvertSize(float byteSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
                //{ "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes", "Petabytes" };
            int value = 0;
            while (byteSize >= 1024 && value < sizes.Length - 1)
            {
                value++;
                byteSize /= 1024;
            }

            return String.Format("{0:0.##} {1}", byteSize, sizes[value]);
        }

        public static string ConvertTime(double secSize)
        {
            TimeSpan span = TimeSpan.FromSeconds((double)secSize);

            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                span.Hours,
                span.Minutes,
                span.Seconds,
                span.Milliseconds);
        }
    }
}
