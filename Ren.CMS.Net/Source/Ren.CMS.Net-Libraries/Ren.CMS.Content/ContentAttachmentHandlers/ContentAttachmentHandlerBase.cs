using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content.ContentAttachmentHandlers
{
    public class ContentAttachmentHandlerBase
    {
        public ContentAttachmentHandlerBase()
        {

        }



        private nContentAttachment register (  nFile RegisteredFile, nContentAttachment AttachmentModel)
        {
            Persistence.Repositories.ContentAttachmentRepository Repo = new Persistence.Repositories.ContentAttachmentRepository();
            var farg = new Ren.CMS.Persistence.Base.BaseRepository<ContentAttachmentArgument>().GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentArgument>(e => e.Id ==  AttachmentModel.Argument.Id ));
            var ffile = new Ren.CMS.Persistence.Base.BaseRepository<File>().GetOne(NHibernate.Criterion.Expression.Where<File>(e => e.Id == RegisteredFile.Id ));
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

        public virtual void Convert()
        {

        }


        public virtual void Delete()
        {

        }


        public virtual void Update()
        {

        }

        public virtual Uri GetUrl()
        {

            return null;
        }
    }
}
