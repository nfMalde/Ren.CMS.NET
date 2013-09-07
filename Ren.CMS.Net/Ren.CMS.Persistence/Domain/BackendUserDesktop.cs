using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class BackendUserDesktop {
        public virtual int Id { get; set; }
        public virtual int IconID { get; set; }
        public virtual string Icon { get; set; }
        public virtual float XPos { get; set; }
        public virtual float YPos { get; set; }
        public virtual string Userid { get; set; }
    }
}
