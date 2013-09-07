using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class InternalProContra {
        public virtual int Id { get; set; }
        public virtual int Refid { get; set; }
        public virtual string PType { get; set; }
        public virtual string PText { get; set; }
    }
}
