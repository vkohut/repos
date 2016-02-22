using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Directory_browse.Models
{
    public class Directory
    {
        public string path { get; set; }
        public List<DirectoryData> data;
    }
    public class DirectoryData
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public int IsFolder { get; set; }
        public long? Size { get; set; }
    }
}