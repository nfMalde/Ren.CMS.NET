using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class Category {
        public virtual System.Guid Pkid { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string LongName { get; set; }
        public virtual string SubFrom { get; set; }
    }
}
