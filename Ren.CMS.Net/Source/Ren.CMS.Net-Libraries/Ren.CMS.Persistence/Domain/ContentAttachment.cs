namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachment
    {
        public virtual System.Guid Pkid { get; set; }
        public virtual int Contentid { get; set; }
        public virtual int AttachmentTypeId { get; set; }
        public virtual string Filepath { get; set; }
        public virtual string Thumnailpath { get; set; }
        public virtual int Usage { get; set; }
        public virtual string Title { get; set; }

        public virtual ContentAttachmenttypes AttachmentType { get; set; }
        public virtual ContentAttachmentRole Role { get; set; }
    }
}