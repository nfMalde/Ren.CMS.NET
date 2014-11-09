using MediaToolkit;
using MediaToolkit.Model;
using Ren.CMS.CORE.Settings;
using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence.Base;
using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content.ContentAttachmentHandlers
{
    public class ContentAttachmentHandlerBase
    {
        private nContentAttachment Source = null;
        public ContentAttachmentHandlerBase()
        {

        }

        public ContentAttachmentHandlerBase(nContentAttachment attachment)
        {
            this.Source = attachment;
        }


        public bool Update(nContentAttachment attachment)
        {
            Persistence.Repositories.ContentAttachmentRepository Repo = new Persistence.Repositories.ContentAttachmentRepository();
            Persistence.Domain.ContentAttachment Attachment = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.ContentAttachment>(e => e.Pkid == attachment.AttachmentID));
            Attachment.Argument = new BaseRepository<ContentAttachmentArgument>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentArgument>(e => e.Id == attachment.Argument.Id));
            Attachment.Role = new BaseRepository<ContentAttachmentRole>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(e => e.Id == attachment.Role.Id));
            Attachment.Texts.Clear();
            foreach(var t in attachment.Texts)
            {
                Attachment.Texts.Add(new ContentAttachmentTexts(){ Attachment = Attachment, Description = t.Description, Title = t.Title, LangCode = t.LangCode, id = t.Id});

            }

            Attachment.Remarks.Clear();

            foreach(var r in attachment.Remarks)
            {
                Attachment.Remarks.Add(new ContentAttachmentRemarks() { Id = r.Id, Remarktext = r.Remarktext, RemarkType = new ContentAttachmentRemarkTypes() { Id = r.Type.Id, Remarkname = r.Type.Name, Remarklocalline = r.Type.Remarklocalline, Remarklocalpackage = r.Type.Remarklocalpackage } });

            }

            Repo.Update(Attachment);
            return true;
 
        }

        private nContentAttachment register (  nFile RegisteredFile, nContentAttachment AttachmentModel)
        {
            Persistence.Repositories.ContentAttachmentRepository Repo = new Persistence.Repositories.ContentAttachmentRepository();
            var farg = new Ren.CMS.Persistence.Base.BaseRepository<ContentAttachmentArgument>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentArgument>(e => e.Id ==  AttachmentModel.Argument.Id ));
            var ffile = new Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.File>().GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.File>(e => e.Id == RegisteredFile.Id ));
            var frole = new Ren.CMS.Persistence.Base.BaseRepository<ContentAttachmentRole>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(e => e.Id == AttachmentModel.Role.Id));
            int typeId = AttachmentModel.AttachmentType.Id;
            var ftype = new Ren.CMS.Persistence.Base.BaseRepository<ContentAttachmenttypes>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(e => e.Id == typeId));
            var fcontent = new Ren.CMS.Persistence.Base.BaseRepository<TContent>().GetOne(NHibernate.Criterion.Expression.Where<TContent>(e => e.Id == AttachmentModel.ContentId));
            
            Persistence.Domain.ContentAttachment Attachment = new Persistence.Domain.ContentAttachment();
            Attachment.File = ffile;
            Attachment.Argument = farg;
            Attachment.Role = frole;
            Attachment.AttachmentType = ftype;
            Attachment.Content = fcontent;
            //Attachment.FileId = RegisteredFile.Id;
            //Attachment.ArgumentId = AttachmentModel.Argument.Id;
            //Attachment.RoleId = AttachmentModel.Role.Id;
            Attachment.Pkid = Guid.NewGuid();
            if(Attachment.Texts == null)
                Attachment.Texts = new List<ContentAttachmentTexts>();
            if(AttachmentModel.Texts != null)
            {
                foreach(nContentAttachmentTexts text in AttachmentModel.Texts)
                {
                    Attachment.Texts.Add(new ContentAttachmentTexts() { LangCode = text.LangCode, Title = text.Title, Description = text.Description, Attachment = Attachment });
                }
            }
            // Add Remarks
            Attachment.Remarks = new List<ContentAttachmentRemarks>();
            BaseRepository<ContentAttachmentRemarkTypes> RemarkTypes = new BaseRepository<ContentAttachmentRemarkTypes>();
          
            foreach(var remark in AttachmentModel.Remarks)
            {
                if(remark.Type == null || remark.Type.Id == 0)
                {
                    throw new Exception("Undefined RemarkType "+ remark.Type.Name);
                }
                var rType = RemarkTypes.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarkTypes>(e => e.Id == remark.Type.Id));
                if (rType != null)
                    Attachment.Remarks.Add(new ContentAttachmentRemarks()
                    {
                        Attachment = Attachment,
                        Remarktext = remark.Remarktext,
                        RemarkType = rType
                    });
                else
                    throw new Exception("Remark Type not Found " + remark.Type.Name);
            }



            Persistence.Base.BaseRepository<Persistence.Domain.ContentAttachmentTexts> RepoTexts = new Persistence.Base.BaseRepository<Persistence.Domain.ContentAttachmentTexts>();
            string lang = Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage;
            foreach (nContentAttachmentTexts t in AttachmentModel.Texts)
            {

                if (!Attachment.Texts.Any(e => e.LangCode == t.LangCode))
                    Attachment.Texts.Add(new Persistence.Domain.ContentAttachmentTexts() { Description = t.Description, Title = t.Title, LangCode = t.LangCode });
            }

            if (Attachment.Texts.Count == 0)
            {
                Attachment.Texts.Add(new Persistence.Domain.ContentAttachmentTexts() { LangCode = lang, Title = RegisteredFile.AliasName, Description = RegisteredFile.AliasName });

            }


            Guid pkid = (Guid)Repo.AddAndGetId(Attachment);
            Persistence.Domain.ContentAttachment AttachmentNew = Repo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.ContentAttachment>(e => e.Pkid == pkid));
            if (AttachmentNew != null)
            {
                return new nContentAttachment(AttachmentNew);
            }
            return null;

        }
        public virtual nContentAttachment Upload(HttpPostedFileBase file, nContentAttachment AttachmentModel)
        {

            Persistence.Base.BaseRepository<Persistence.Domain.TContent> contentRepo = new Persistence.Base.BaseRepository<Persistence.Domain.TContent>();
            Persistence.Domain.TContent _c = contentRepo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.TContent>(e => e.Id == AttachmentModel.ContentId));
            if (_c == null)
                throw new Exception("ContentAttachmentHandler Exception: Cannot Add Attachment to not existing content with Id#" + AttachmentModel.ContentId);

            nFile RegisteredFile = Filemanager.CreateFile(file, "eContent\\"+ _c.ContentType);
            return register(RegisteredFile, AttachmentModel);
        }


        public virtual nContentAttachment Upload(HttpPostedFile file, nContentAttachment AttachmentModel)
        {
            Persistence.Base.BaseRepository<Persistence.Domain.TContent> contentRepo = new Persistence.Base.BaseRepository<Persistence.Domain.TContent>();
            Persistence.Domain.TContent _c = contentRepo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.TContent>(e => e.Id == AttachmentModel.ContentId));
            if (_c == null)
                throw new Exception("ContentAttachmentHandler Exception: Cannot Add Attachment to not existing content with Id#" + AttachmentModel.ContentId);

            nFile RegisteredFile = Filemanager.CreateFile(file, "eContent\\" + _c.ContentType);
            return register(RegisteredFile, AttachmentModel);
        }

        public virtual nContentAttachment AddLocalFile(string file, nContentAttachment AttachmentModel)
        {
            Persistence.Base.BaseRepository<Persistence.Domain.TContent> contentRepo = new Persistence.Base.BaseRepository<Persistence.Domain.TContent>();
            Persistence.Domain.TContent _c = contentRepo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.TContent>(e => e.Id == AttachmentModel.ContentId));
            if (_c == null)
                throw new Exception("ContentAttachmentHandler Exception: Cannot Add Attachment to not existing content with Id#" + AttachmentModel.ContentId);

            nFile RegisteredFile = Filemanager.CreateFileFromFile(file, "eContent\\" + _c.ContentType);
            return register(RegisteredFile, AttachmentModel);
        }

        public virtual nContentAttachment AddExternal(Uri url, nContentAttachment AttachmentModel)
        {

            Persistence.Base.BaseRepository<Persistence.Domain.TContent> contentRepo = new Persistence.Base.BaseRepository<Persistence.Domain.TContent>();
            Persistence.Domain.TContent _c = contentRepo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.TContent>(e => e.Id == AttachmentModel.ContentId));
            if (_c == null)
                throw new Exception("ContentAttachmentHandler Exception: Cannot Add Attachment to not existing content with Id#" + AttachmentModel.ContentId);

            nFile RegisteredFile = Filemanager.CreateFile(url, true);
            return register(RegisteredFile, AttachmentModel);
        }

        public virtual nContentAttachment AddExternal(string url, nContentAttachment AttachmentModel)
        {
            Persistence.Base.BaseRepository<Persistence.Domain.TContent> contentRepo = new Persistence.Base.BaseRepository<Persistence.Domain.TContent>();
            Persistence.Domain.TContent _c = contentRepo.GetOne(NHibernate.Criterion.Expression.Where<Persistence.Domain.TContent>(e => e.Id == AttachmentModel.ContentId));
            if (_c == null)
                throw new Exception("ContentAttachmentHandler Exception: Cannot Add Attachment to not existing content with Id#" + AttachmentModel.ContentId);

            nFile RegisteredFile = Filemanager.CreateFile(url, true);
            return register(RegisteredFile, AttachmentModel);
        }

        public virtual void Convert(string targetExt)
        {
            if (this.Source == null)
                throw new Exception("AttachmentHandlerBase - No Source Attachment set.");
            //Get Main File
            nFile mainFile = this.Source.File;
            //Get Old Name
            string fileName = mainFile.AliasName;
            string filePath = mainFile.FilePath;

            if (targetExt.StartsWith("."))
                targetExt = targetExt.Substring(1);
            //Set Targets
            string targetFileName = Path.GetFileNameWithoutExtension(fileName) + "." + targetExt.ToLower();
            
            //Get Path to TempDir
            Ren.CMS.CORE.Settings.GlobalSettings GLBS = new CORE.Settings.GlobalSettings();
            nSetting setting = GLBS.getSetting("STORAGE_PATH");
            string path = HttpContext.Current.Server.MapPath("~/Storage/");
            if (setting != null && setting.ID > 0)
            {
                path = HttpContext.Current.Server.MapPath(setting.Value.ToString());
            }

            if (path.EndsWith("/"))
                path = path.Remove(path.LastIndexOf("/"));

            path += "/__TEMP/";
            
 
            string RealFileName = Path.GetFileNameWithoutExtension(filePath) + "." + targetExt.ToLower();

            string TargetPath = path + RealFileName;



            //TODO: Add Static File Method for adding a file
            //Call Converter
            MediaFile xSource = new MediaFile(filePath);
            MediaFile xTarget = new MediaFile(TargetPath);
            using (var engine = new Engine())
            {
                engine.Convert(xSource, xTarget);
            }

            //Register Referenced File
            Filemanager.CreateFileFromFile(TargetPath, "Reference_" + mainFile.Id, true, mainFile.Id);
        }


        public virtual void Delete()
        {
            if (this.Source == null)
                throw new Exception("AttachmentHandlerBase - No Source Attachment set.");

            
            
        }


     

        public void SetSource(nContentAttachment source)
        {
            this.Source = source;
        }

  
    }
}
