using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentAttachmentRole {
        public virtual int Id { get; set; }
        public virtual string AType { get; set; }
        public virtual string RoleName { get; set; }
        public virtual string RoleLangLine { get; set; }
    }
}
