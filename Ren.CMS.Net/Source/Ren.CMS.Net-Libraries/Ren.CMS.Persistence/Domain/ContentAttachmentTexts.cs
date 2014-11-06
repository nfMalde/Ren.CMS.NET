namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachmentTexts
    {
        //PK
        public virtual int id { get; set; }
        //FK
        public virtual Guid AttachmentId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string LangCode { get; set; }

        public virtual ContentAttachment Attachment { get; set;}
        
    }
}