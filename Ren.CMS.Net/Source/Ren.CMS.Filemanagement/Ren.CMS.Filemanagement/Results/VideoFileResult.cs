﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.MimeMapping;
using System.IO;
namespace Ren.CMS.Filemanagement.Results
{
    public class VideoFileResult : ActionResult
    {
        private string _Path = null;
        private string _ContentType = null;

        public VideoFileResult(string filePath, string contentType = null)
       {

       }

       public override void ExecuteResult(ControllerContext context)
       {
           //Step 1 Get File
           //   Check if Path is Absolute

           if(this._Path.StartsWith("~/"))
           {
               this._Path = HttpContext.Current.Server.MapPath(this._Path);
           }


           if (!File.Exists(this._Path))
               throw new Exception(this._Path + " does not exists");

           //Detect content Type
           
           string mime = MimeMapping.GetMimeMapping(this._Path);
           if (mime.ToLower() != this._ContentType.ToLower() && this._ContentType != null)
               throw new Exception("Incorrect Mime Format:  Expected type " + this._ContentType + " got " + mime);

           if (!mime.ToLower().StartsWith("video/"))
               throw new Exception("File is not an video.");

           var bytes = File.ReadAllBytes(this._Path);
           context.HttpContext.Response.ContentType = mime;
           context.HttpContext.Response.BinaryWrite(bytes);

       }
    } 
}
