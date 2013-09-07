using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class SubCategory {
        public virtual System.Guid Pkid { get; set; }
        public virtual System.Nullable<System.Guid> Cid { get; set; }
        public virtual string Ref { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string LongName { get; set; }
        public virtual string LangCode { get; set; }
    }
}
