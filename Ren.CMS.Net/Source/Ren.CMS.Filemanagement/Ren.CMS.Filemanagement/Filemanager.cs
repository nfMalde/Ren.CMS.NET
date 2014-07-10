using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace Ren.CMS.Filemanagement
{
    public static class Filemanager
    {
       
         
        public static nFile GetFile(int Id)
        {

            return null;
        }


       public static nFile AddFile(nFile file)
       {

           return null;
       }

      public static Stream GetFileWithWaterMark(nFile file, int waterMarkId)
      {
          return null;
      }
      private static List<Exception> FileManagerExceptionList = new List<Exception>();

      public static bool HasExceptions { get { return (FileManagerExceptionList.Count > 0); } }

      public static Uri GetUrl(this nFile file)
      {

          if(file.FileType.Physical)
          {
              return PathHelper.GetUrlByVirtualPath(file.VirtualPath);


          }
          else
          {
              try {

                  return new Uri(file.FilePath);
              }
              catch(Exception e)
              {
                  FileManagerExceptionList.Add(e);
                
              }  
          }


          return null;
      }

      
    }

}
