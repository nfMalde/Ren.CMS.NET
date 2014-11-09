using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content
{
    public class nAttachmentManager
    {
        private int contentId = 0;
        private Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.ContentAttachment> Repo = new Persistence.Base.BaseRepository<Persistence.Domain.ContentAttachment>();
        public nAttachmentManager(int ContentId)
        {
            this.contentId = ContentId;

        }
        public List<nContentAttachment> GetAttachments(string RoleName=null, string ArgumentName = null, string TypeName = null)
        {
             ICriterion Where = null;
             if (RoleName != null && ArgumentName != null && TypeName != null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&  e.Role.Rolename == RoleName && e.Argument.ArgumentName == ArgumentName && e.AttachmentType.Typename == TypeName);
             else if (RoleName == null && ArgumentName != null && TypeName != null) 
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.Argument.ArgumentName == ArgumentName && e.AttachmentType.Typename == TypeName);
             else if (RoleName != null && ArgumentName == null && TypeName != null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.Role.Rolename == RoleName && e.AttachmentType.Typename == TypeName);
             else if (RoleName == null && ArgumentName == null && TypeName != null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.AttachmentType.Typename == TypeName);
             else if (RoleName != null && ArgumentName != null && TypeName == null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.Role.Rolename == RoleName && e.Argument.ArgumentName == ArgumentName);
             else if (RoleName != null && ArgumentName == null && TypeName == null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.Role.Rolename == RoleName);
             else if (RoleName == null && ArgumentName != null && TypeName == null)
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId &&e.Argument.ArgumentName == ArgumentName);
             else
                 Where = NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.ContentAttachment>(e => e.Content.Id == this.contentId);

             var list = Repo.GetMany(where: Where);

             List<nContentAttachment> List = new List<nContentAttachment>();

             foreach (var e in list)
                 List.Add(new nContentAttachment(e));
             return List;
        }


        //Adding
        public nContentAttachment AddAttachment(HttpPostedFile file, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null, List<nAttachmentRemark> Remarks = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts, Remarks);


            return cType.Handler.Upload(file, NewAttachment);
        }

        public nContentAttachment AddAttachment(HttpPostedFileBase file, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null, List<nAttachmentRemark> Remarks = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts, Remarks);


            return cType.Handler.Upload(file, NewAttachment);
        }

        public nContentAttachment AddAttachment(Uri url, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null, List<nAttachmentRemark> Remarks = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts, Remarks);


            return cType.Handler.AddExternal(url, NewAttachment);
        }

        public nContentAttachment AddAttachment(string url, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null, List<nAttachmentRemark> Remarks = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts, Remarks);


            return cType.Handler.AddExternal(url, NewAttachment);
        }

        public nContentAttachment AddAttachmentFromFile(string filePath, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null, List<nAttachmentRemark> Remarks = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts, Remarks);


            return cType.Handler.AddLocalFile(filePath, NewAttachment);
        }
    }
}
