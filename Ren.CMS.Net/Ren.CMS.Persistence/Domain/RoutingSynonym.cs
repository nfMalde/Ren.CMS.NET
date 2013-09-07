using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class RoutingSynonym {
        public virtual string Name { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual string Rpath { get; set; }
    }
}
