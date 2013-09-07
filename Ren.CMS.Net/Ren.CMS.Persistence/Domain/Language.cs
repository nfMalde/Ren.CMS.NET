using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class tbLanguage {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Content { get; set; }
        public virtual string Package { get; set; }
        public virtual string Code { get; set; }
    }
}
