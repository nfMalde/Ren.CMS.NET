using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ProfileVar {
        public virtual string Name { get; set; }
        public virtual string LangLine { get; set; }
        public virtual string Section { get; set; }
        public virtual string ViewName { get; set; }
        public virtual string Active { get; set; }
        public virtual string ShowInProfile { get; set; }
    }
}
