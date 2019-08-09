using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ArcaneAPI.Models.CustomModels
{
    public class Request
    {
        public List<string> Collection()
        {
            List<string> list = new List<string>();
            foreach (string file in HttpContext.Current.Request.Files)
            {
                list.Add(file);
            }
            return list;
        }

        public string SaveFile(string fileName)
        {
            var postedFile = HttpContext.Current.Request.Files[fileName];
            if (postedFile == null)
            {
                throw new Exception(@"а файл где?");
            }
            string filePath = GetPath() + Guid.NewGuid().ToString().Trim() + Path.GetExtension(postedFile.FileName);
            if (filePath == null)
            {
                throw new Exception(@"а файл где?");
            }
            postedFile.SaveAs(filePath);
            filePath = filePath.Replace(AppPath(), string.Empty);
            filePath = filePath.Replace("\\", "/");
            return filePath;
        }

        public string GetPath()
        {
            return HttpContext.Current.Server.MapPath("~/PostedFiles/");
        }

        public string AppPath()
        {
            return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
        }

        public string GetFileName(string fileName)
        {
            return HttpContext.Current.Request.Files[fileName].FileName;
        }

        public byte[] GetBytes(string fileName)
        {
            var postedFile = HttpContext.Current.Request.Files[fileName];
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(postedFile.InputStream))
            {
                fileData = binaryReader.ReadBytes(postedFile.ContentLength);
            }
            
            return fileData;
        }

        public int FileCount()
        {
            return HttpContext.Current.Request.Files.Count;
        }
    }
}