using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class PermissionGroup {
        public virtual int Id { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string IsGuestGroup { get; set; }
        public virtual string IsDefaultGroup { get; set; }
    }
}
