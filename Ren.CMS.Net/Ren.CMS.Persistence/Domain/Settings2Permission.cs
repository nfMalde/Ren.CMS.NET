using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class Settings2Permission {
        public virtual int Id { get; set; }
        public virtual string FrontEndPM { get; set; }
        public virtual string BackEndPM { get; set; }
        public virtual System.Nullable<int> Sid { get; set; }
    }
}
