using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class BackendDesktopBackground {
        public virtual string Userid { get; set; }
        public virtual string BackgroundImage { get; set; }
        public virtual string BackgroundColor { get; set; }
        public virtual string BackgroundAlign { get; set; }
        public virtual string BackgroundRepeat { get; set; }
    }
}
