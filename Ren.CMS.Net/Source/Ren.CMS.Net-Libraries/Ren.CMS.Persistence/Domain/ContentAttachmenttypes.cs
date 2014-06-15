using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class ContentAttachmenttypes {
        public virtual int Id { get; set; }
        public virtual string Typename { get; set; }
        public virtual string Storagepath { get; set; }

        public virtual string HandlerNamespace { get; set; }
        public virtual List<ContentAttachmenttypesExtensionsettings> AttachmentExtensions { get; set; }
    }
}
