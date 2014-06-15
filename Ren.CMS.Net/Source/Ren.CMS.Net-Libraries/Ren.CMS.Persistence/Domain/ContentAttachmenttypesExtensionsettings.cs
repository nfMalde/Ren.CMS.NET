using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class ContentAttachmenttypesExtensionsettings {
        public virtual int Id { get; set; }
        public virtual int Attachmenttypeid { get; set; }
        public virtual string Extension { get; set; }
        public virtual long Maxfilesize { get; set; }
        public virtual bool Convertfile { get; set; }

        public virtual ContentAttachmenttypes AttType { get; set; }
    }
}
