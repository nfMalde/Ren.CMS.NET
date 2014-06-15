using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class ContentAttachmentRemarks {
        public virtual int Id { get; set; }
        public virtual int Remarktype { get; set; }
        public virtual string Remarkvarchar250 { get; set; }
        public virtual string Remarktext { get; set; }
        public virtual string Attachmentid { get; set; }

        public virtual ContentAttachment Attachment { get; set; }

        public virtual ContentAttachmentRemarkTypes RemarkTypes { get; set; }
    }
}
