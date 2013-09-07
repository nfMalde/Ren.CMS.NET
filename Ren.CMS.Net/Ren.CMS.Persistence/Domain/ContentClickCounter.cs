using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentClickCounter {
        public virtual int Id { get; set; }
        public virtual string Ip { get; set; }
        public virtual int Cid { get; set; }
    }
}
