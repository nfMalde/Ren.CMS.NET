using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class SettingCategory {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CatRel { get; set; }
    }
}
