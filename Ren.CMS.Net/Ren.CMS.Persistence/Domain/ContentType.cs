using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentType {
        public virtual string Name { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Actionpath { get; set; }
        public virtual string CreatePartial { get; set; }
        public virtual string EditPartial { get; set; }
    }
}
