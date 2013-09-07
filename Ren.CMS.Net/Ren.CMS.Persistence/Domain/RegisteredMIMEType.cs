using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class RegisteredMIMEType {
        public virtual int Id { get; set; }
        public virtual string FileExstension { get; set; }
        public virtual string Mimetype { get; set; }
    }
}
