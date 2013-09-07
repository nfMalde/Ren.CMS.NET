using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class Rating {
        public virtual int Id { get; set; }
        public virtual int GroupID { get; set; }
        public virtual string Name { get; set; }
        public virtual string LangCode { get; set; }
    }
}
