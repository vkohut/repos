using Directory_browse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Directory_browse.Managers
{
    public class BrowseManager
    {
        public BrowseManager()
        {

        }
        public Directory GetLogicalDrives()
        {
            Directory dir;
            List<DirectoryData> drData = new List<DirectoryData>();

            List<string> drives = System.Environment.GetLogicalDrives().ToList<string>();
            foreach (var dr in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(dr);
                if (di.IsReady)
                {
                    drData.Add(new DirectoryData() { Name = di.Name, FullName = "...", IsFolder = 1, Size = di.TotalSize });
                }
            }
            dir = new Directory() { path = "...", data = drData };

            return dir;
        }

        public Directory getFolderTree(string path)
        {
            Directory dir;
            List<DirectoryData> drData = new List<DirectoryData>();

            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(path);

            try
            {
                var directories = dirInfo.GetDirectories();
                var files = dirInfo.GetFiles();
                foreach (var directory in directories)
                {
                    drData.Add(new DirectoryData() { Name = directory.Name, FullName = directory.FullName, IsFolder = 1, Size = null });
                }
                foreach (var file in files)
                {
                    drData.Add(new DirectoryData() { Name = file.Name, FullName = null, IsFolder = 0, Size = file.Length });
                }
                dir = new Directory() { path = dirInfo.FullName, data = drData };
            }
            catch (Exception ex)
            {
                return null;
            }

            return dir;
        }
    }
}