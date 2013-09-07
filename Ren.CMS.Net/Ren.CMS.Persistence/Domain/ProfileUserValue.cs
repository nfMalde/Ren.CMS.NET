using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ProfileUserValue {
        public virtual string VarName { get; set; }
        public virtual string VarValue { get; set; }
        public virtual string Pkid { get; set; }
    }
}
