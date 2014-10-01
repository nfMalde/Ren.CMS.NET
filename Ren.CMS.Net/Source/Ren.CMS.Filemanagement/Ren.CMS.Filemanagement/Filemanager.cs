using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Ren.CMS.Persistence.Repositories;

namespace Ren.CMS.Filemanagement
{
    public static class Filemanager
    {

        private static FileRepository Repo = new FileRepository();


        public static bool CheckMimeType(this nFile file, nMimeType Mime)
        {
            return MimeMapping.GetMimeMapping(file.FilePath) == Mime.MIME;
        }
         
        public static nFile GetFile(int Id)
        {
            var c = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.File>(e => e.Id == Id));
            if(c != null)
            {
                List<nMimeType> l = new List<nMimeType>();
                foreach(var m  in c.FileType.AllowedMIMETypes)
                {
                    l.Add(new nMimeType(){ ID = m.Mime.Id, MIME = m.Mime.Mimetype, nFileTypeID = c.FileType.Id});
                }

                List<nFileSetting> s = new List<nFileSetting>();
                foreach(var S in c.FileType.Profile.Settings)
                    s.Add(new nFileSetting(){
                         Id = S.Setting.Id,
                          SettingName = S.Setting.SettingName,
                           Value = (S.Setting.Value.Any(e => e.ProfileID == c.FileType.Profile.Id && e.SettingID == S.Setting.Id) ? S.Setting.Value.First(e => e.ProfileID == c.FileType.Profile.Id && e.SettingID == S.Setting.Id).SettingValue : null),
                            ValueId =(S.Setting.Value.Any(e => e.ProfileID == c.FileType.Profile.Id && e.SettingID == S.Setting.Id) ? S.Setting.Value.First(e => e.ProfileID == c.FileType.Profile.Id && e.SettingID == S.Setting.Id).Id : 0)
                    }  );


                return new nFile() { 
                 AliasName = c.AliasName,
                  FilePath = c.FilePath,
                 FileType = new nFileType()
                 {
                      Id = c.FileType.Id,
                       AllowedMIMETypes = l,
                        Physical = c.FileType.Physical,
                      Profile = new nFileProfile()
                      {
                           Id = c.FileType.Profile.Id,
                            ProfileName = c.FileType.Profile.ProfileName,
                             Settings = s
                      },
                       TypeName = c.FileType.TypeName
                 },
                  Id = c.Id,
                   isActive = c.isActive,
                    TypeID = c.TypeID,
                     VirtualPath = c.VirtualPath
                
                };
            }

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
