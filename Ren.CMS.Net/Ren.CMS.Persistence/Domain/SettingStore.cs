using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class SettingStore {
        public virtual int Id { get; set; }
        public virtual int Sid { get; set; }
        public virtual string Val { get; set; }
    }
}
