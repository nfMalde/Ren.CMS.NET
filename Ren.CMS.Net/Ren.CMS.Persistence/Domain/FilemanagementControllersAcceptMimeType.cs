using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class FilemanagementControllersAcceptMimeType {
        public virtual int Id { get; set; }
        public virtual string MimeType { get; set; }
        public virtual System.Nullable<int> Cid { get; set; }
    }
}
