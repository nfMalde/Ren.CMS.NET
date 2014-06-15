using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class ContentAttachmentRemarkTypes {
        public virtual int Id { get; set; }
        public virtual string Remarkname { get; set; }
        public virtual string Remarklocalline { get; set; }
        public virtual string Remarklocalpackage { get; set; }

        public List<ContentAttachmentRemarks> Remarks { get; set; }
    }
}
