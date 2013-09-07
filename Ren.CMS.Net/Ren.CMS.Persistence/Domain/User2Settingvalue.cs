using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class User2Settingvalue {
        public virtual int Id { get; set; }
        public virtual int Sid { get; set; }
        public virtual string Uid { get; set; }
        public virtual int Vid { get; set; }
    }
}
