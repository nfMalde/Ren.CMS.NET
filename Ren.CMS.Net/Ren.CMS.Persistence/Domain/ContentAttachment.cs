using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentAttachment {
        public virtual System.Guid Pkid { get; set; }
        public virtual int Nid { get; set; }
        public virtual string AttachmentType { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FPath { get; set; }
        public virtual string FName { get; set; }
        public virtual string ThumpNail { get; set; }
        public virtual string AttachmentArgument { get; set; }
        public virtual string ATitle { get; set; }
        public virtual string AttachmentRemarks { get; set; }
    }
}
