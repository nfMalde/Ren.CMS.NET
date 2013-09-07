using System;
using System.Text;
using System.Collections.Generic;

namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class BackendMenu {
        public virtual int Id { get; set; }
        public virtual string MenuTextLang { get; set; }
        public virtual string IconUrl { get; set; }
        public virtual string Action { get; set; }
        public virtual string NeededPermission { get; set; }
        public virtual System.Nullable<int> HeadID { get; set; }
    }
}
