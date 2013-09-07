using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class BackendDesktopIcon {
        public virtual int Id { get; set; }
        public virtual string LangLine { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Action { get; set; }
    }
}
