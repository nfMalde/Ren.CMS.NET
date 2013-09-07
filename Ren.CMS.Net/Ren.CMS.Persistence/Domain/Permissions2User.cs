using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class Permissions2User {
        public virtual string Pk { get; set; }
        public virtual string GroupID { get; set; }
        public virtual string Val { get; set; }
        public virtual string Usr { get; set; }
    }
}
