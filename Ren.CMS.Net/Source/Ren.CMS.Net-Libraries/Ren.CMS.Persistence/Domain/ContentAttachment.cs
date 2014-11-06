namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachment
    {
        public virtual System.Guid Pkid { get; set; }
        //FKs
        public virtual int Contentid { get; set; }
        public virtual int AttachmentTypeId { get; set; }
        public virtual int FileId { get; set;}
        public virtual int RoleId { get; set; }
        public virtual int? ArgumentId { get; set; }


       
        public virtual ContentAttachmenttypes AttachmentType { get; set; }
        public virtual ContentAttachmentRole Role { get; set; }
        public virtual ContentAttachmentArgument Argument { get; set; }
        public virtual ICollection<ContentAttachmentRemarks> Remarks { get; set; }
        public virtual File File { get; set; }
        public virtual IList<ContentAttachmentTexts> Texts { get; set; }
        public virtual TContent Content { get; set; }
    }
}