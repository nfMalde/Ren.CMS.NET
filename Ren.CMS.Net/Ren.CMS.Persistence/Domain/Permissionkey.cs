using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class Permissionkey {
        public virtual string Pkey { get; set; }
        public virtual string DefaultVal { get; set; }
        public virtual string LangLine { get; set; }
    }
}
