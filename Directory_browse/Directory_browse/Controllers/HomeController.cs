using Directory_browse.Managers;
using Directory_browse.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Directory_browse.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public JObject getDrivers()
        {
            BrowseManager browse = new BrowseManager();
            Directory_browse.Models.Directory dir = browse.GetLogicalDrives();

            JObject dirJSON = JObject.FromObject(new
            {
                Dir = dir
            });
            return dirJSON;
        }
        [HttpPost]
        public JObject getFolderTree(DirectoryData d)
        {
            BrowseManager browse = new BrowseManager();
            Directory_browse.Models.Directory dir = browse.getFolderTree(d.FullName);

            JObject dirJSON = JObject.FromObject(new
            {
                Dir = dir
            });
            return dirJSON;
        }
    }
}
