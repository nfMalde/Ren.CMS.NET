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
using Ren.CMS.Persistence.Domain;
using Ren.CMS.CORE.Settings;
using System.Net;

namespace Ren.CMS.Filemanagement
{
    public static class Filemanager
    {

        private static FileRepository Repo = new FileRepository();
      
     
        /// <summary>
        /// Getting an File by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static nFile GetFile(int Id)
        {
            Ren.CMS.Persistence.Domain.File _file = Repo.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(x => x.Id == Id));
            if(_file != null)
                return new nFile(_file);

            return null;
        }

        /// <summary>
        /// Getting an file by aliasName
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public static nFile GetFile(string aliasName)
        {
            Ren.CMS.Persistence.Domain.File _file = Repo.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(x => x.AliasName == aliasName));
            if(_file != null)
                return new nFile(_file);

            return null;
        }
      
      /// <summary>
      /// Generates an unique alias name
      /// </summary>
      /// <param name="aliasName"></param>
      /// <param name="compareId"></param>
      /// <returns></returns>
      private static string getUniqueAliasName(string aliasName, int compareId = 0)
      {
            nFile fAlias = GetFile(aliasName);

           if(fAlias != null && fAlias.Id != compareId)
           {
               bool taken = true;
               int x = 1;
               do
               {
                   string name = Path.GetFileNameWithoutExtension(aliasName);
                   string ext = Path.GetExtension(aliasName);
                   if(!ext.StartsWith("."))
                       ext = "."+ ext;

                   string newAliasName = name + "(" + x + ")" + ext;
                   nFile check = GetFile(newAliasName);
                   if(check == null)
                   {
                        aliasName = newAliasName;
                        taken = false;

                   }
                   else
                   {
                       x++;
                   }
               }
               while (taken);


           }

          return aliasName;
      }

       
       public static nFile SaveFile(nFile file)
       {
            if(GetFile(file.Id) == null)
            {
                throw new Exception("Save File Infos is not Possible for new files. Please Call Method CreateFile ");
            }

            file.AliasName = getUniqueAliasName(file.AliasName, file.Id);

            Ren.CMS.Persistence.Domain.File Entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(e => e.Id == file.Id));
            Entity.AliasName = file.AliasName;
            Entity.isActive = file.isActive;
            if (file.ReferencedFiles.Count > 0 && file.FileReference > 0)
                throw new Exception("Cannot save file. Reason: file has FileReferenceId and ReferencedFiles. Delete Referenced Files or set FileReference to 0");
            if (file.FileReference > 0)
                Entity.FileReference = file.FileReference;

           if (file.ReferencedFiles.Count > 0)
           {
             

               foreach(nFile refFile in file.ReferencedFiles)
               {
                   Ren.CMS.Persistence.Domain.File RefEntity = Repo.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(e => e.Id == refFile.Id));
                    if (RefEntity == null)
                        continue;
                    if (RefEntity.FileReference != Entity.Id)
                    {
                        RefEntity.FileReference = Entity.Id;
                        
                    }
                    if (RefEntity.ReferencedFiles.Count > 0)
                        continue;
                    RefEntity.AliasName = getUniqueAliasName( refFile.AliasName , RefEntity.Id);
                    RefEntity.isActive = refFile.isActive;
                    if (!Entity.ReferencedFiles.Any(e => e.Id == RefEntity.Id))
                    {
                        Entity.ReferencedFiles.Add(RefEntity);
                    }
                   Repo.Update(RefEntity);
               }
           }

           Repo.Update(Entity);
           var many = Repo.GetMany(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(x => x.FileReference == Entity.Id));
           foreach (Ren.CMS.Persistence.Domain.File hit in many)
           {
               if (!file.ReferencedFiles.Any(e => e.Id == hit.Id))
               {
                   Repo.Delete(hit);
                 
               }
           }
           return GetFile(Entity.Id);
       }
        private static string  getStoragePath()
       {
           Ren.CMS.CORE.Settings.GlobalSettings GLBS = new CORE.Settings.GlobalSettings();
           nSetting setting = GLBS.getSetting("STORAGE_PATH");
           string path = HttpContext.Current.Server.MapPath("~/Storage/");
           if (setting != null && setting.ID > 0)
           {
               path = HttpContext.Current.Server.MapPath(setting.Value.ToString());
           }

           return path;
       }

        private static string GetAndPrepareTarget(string subPath)
        {

            string target = getStoragePath();
            if (!target.EndsWith("\\"))
                target += "\\";

            if (subPath.Contains("/"))
            {
                int ix = subPath.Count(e => e == '/');
                for (int i = 0; i < ix; i++)
                {
                    subPath = subPath.Replace('/', '\\');
                }
            }
            if (subPath.StartsWith("\\"))
                subPath = subPath.Substring(1);
            target += subPath;

            if (!target.EndsWith("\\"))
                target += "\\";
            //create file structure
            MkdirR(target);

            return target;
        }

        private static string GenerateFileName(string Origfilename, int ContentLength, string target)
        {

            string oldExtension = Path.GetExtension(Origfilename);
            int fs = Directory.GetFiles(target).Length;

            Ren.CMS.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            string fileName = ContentLength + "_" + Origfilename + "_" + fs + 1;
            fileName = Crypto.ConvertToSHA1(fileName);

            if (fs > 0)
            {
                fileName += "_" + fs + 1;
            }

            if (!oldExtension.StartsWith("."))
                oldExtension = "." + oldExtension;

            fileName = fileName + oldExtension;
            target += fileName;

            return target;
        }

        public static nFile CreateFile(HttpPostedFile postedFile, string subPath = "Files", bool active = true, int fileReference = 0)
        {

            string target = GetAndPrepareTarget(subPath);
            target = GenerateFileName(postedFile.FileName, postedFile.ContentLength, target);
            postedFile.SaveAs(target);

            Ren.CMS.Persistence.Domain.File NewFile = new Persistence.Domain.File();
            NewFile.Physical = true;
            NewFile.isActive = active;
            NewFile.AliasName = getUniqueAliasName(Path.GetFileName(postedFile.FileName));
            NewFile.FilePath = target;
            if (fileReference > 0)
                NewFile.FileReference = fileReference;


            return AddFile(NewFile);
        }

        public static nFile CreateFile(HttpPostedFileBase postedFile, string subPath = "Files", bool active = true, int fileReference = 0)
        {

            string target = GetAndPrepareTarget(subPath);
            target = GenerateFileName(postedFile.FileName, postedFile.ContentLength, target);
            postedFile.SaveAs(target);

            Ren.CMS.Persistence.Domain.File NewFile = new Persistence.Domain.File();
            NewFile.Physical = true;
            NewFile.isActive = active;
            NewFile.AliasName = getUniqueAliasName(Path.GetFileName(postedFile.FileName));
            NewFile.FilePath = target;
            if (fileReference > 0)
                NewFile.FileReference = fileReference;


            return AddFile(NewFile);
        }

        private static  bool RemoteFileExists(Uri url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
       
        public static nFile CreateFile(Uri url, bool active, int fileReference = 0)
        {
            
                //Test by webrequest
                if (!RemoteFileExists(url))
                    throw new Exception("Remote File " + url.ToString() + " does not exists!");
                Ren.CMS.Persistence.Domain.File NewFile = new Persistence.Domain.File();
                NewFile.Physical = false;
                NewFile.isActive = active;
                NewFile.AliasName = getUniqueAliasName(Path.GetFileName(url.ToString()));
                NewFile.FilePath = url.ToString();
                if (fileReference > 0)
                    NewFile.FileReference = fileReference;

                return AddFile(NewFile);
        }

        public static nFile CreateFile(string url, bool active, int fileReference = 0)
        {

            //Test by webrequest
            if (!RemoteFileExists(new Uri(url)))
                throw new Exception("Remote File " + url.ToString() + " does not exists!");
            Ren.CMS.Persistence.Domain.File NewFile = new Persistence.Domain.File();
            NewFile.Physical = false;
            NewFile.isActive = active;
            NewFile.AliasName = getUniqueAliasName(Path.GetFileName(url.ToString()));
            NewFile.FilePath = url.ToString();
            if (fileReference > 0)
                NewFile.FileReference = fileReference;

            return AddFile(NewFile);
        }

        

        private static nFile AddFile(Ren.CMS.Persistence.Domain.File NewFile)
        {
            int id = (int)Repo.AddAndGetId(NewFile);
            Ren.CMS.Persistence.Domain.File eFile = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.File>(e => e.Id == id));

            if (eFile != null)
                return new nFile(eFile);

            return null;
        }

        private static void MkdirR(string path)
        {
            string rPath = HttpContext.Current.Server.MapPath("~/");
            path = path.Replace(HttpContext.Current.Server.MapPath("~/"), "");
            string[] segments = path.Split('\\');
            foreach(string piece in segments)
            {
                 if(!rPath.EndsWith("\\"))
                 {
                     rPath += "\\";
                 }
                 rPath += piece;
                 if (!Directory.Exists(rPath))
                     Directory.CreateDirectory(rPath);

            }
        }
      
        public static bool DeleteFile(nFile file)
        {
            if(file.ReferencedFiles.Count > 0)
            {
                file.ReferencedFiles.Clear();
            }

            SaveFile(file);
            Ren.CMS.Persistence.Domain.File eFile = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.File>(e => e.Id == file.Id));
            Repo.Delete(eFile);
            Ren.CMS.Persistence.Domain.File eFile2 = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.File>(e => e.Id == file.Id));
            if (eFile2 == null)
                return true;
            else
                return false;

            
        }
  
      
    }

}
