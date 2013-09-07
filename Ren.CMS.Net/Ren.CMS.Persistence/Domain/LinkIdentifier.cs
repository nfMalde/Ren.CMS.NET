using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class LinkIdentifier {
        public virtual int Id { get; set; }
        public virtual string IdentiferName { get; set; }
        public virtual string Theme { get; set; }
    }
}
