using Ren.CMS.Content.ContentAttachmentHandlers;
using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence.Domain;
using Ren.CMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content
{
    public class nContentAttachment
    {
        public List<nContentAttachmentTexts> Texts { get; set; }
        public Guid AttachmentID { get; set; }
        public nFile File { get; set; }
        public nContentAttachmenType AttachmentType { get; set; }
        public nAttachmentRole Role { get; set; }
        public nAttachmentArgument Argument { get; set; }
        public int ContentId { get; set; }

        public nContentAttachment(Persistence.Domain.ContentAttachment attachment)
        {
            this.Texts = new List<nContentAttachmentTexts>();
            this.AttachmentID = attachment.Pkid;
            this.File = new nFile(attachment.File);
            this.AttachmentType = new nContentAttachmenType(attachment.AttachmentType);
            this.Role = new nAttachmentRole(attachment.Role);
            this.Argument = new nAttachmentArgument(attachment.Argument);
            foreach (var t in attachment.Texts)
                this.Texts.Add(new nContentAttachmentTexts(t));

            this.ContentId = attachment.Contentid;

        }

        public nContentAttachment(int contentId, nFile file, nContentAttachmenType type, nAttachmentRole role, nAttachmentArgument argument, List<nContentAttachmentTexts> Texts = null)
        {
            this.Texts = (Texts != null ? Texts : new List<nContentAttachmentTexts>());
            this.AttachmentID = Guid.NewGuid();
            this.File = file;
            this.Role = role;
            this.Argument = argument;
            this.ContentId = contentId;
            this.AttachmentType = type;
        }
    }
}

