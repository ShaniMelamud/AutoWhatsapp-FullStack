using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class UploadedFilePathModel
    {
        public string FilePath { get; set; }

        public UploadedFilePathModel()
        {

        }

        public UploadedFilePathModel(string filePath)
        {
            FilePath = filePath;
        }
    }
}
