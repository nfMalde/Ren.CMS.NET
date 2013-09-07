using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class InternalRating {
        public virtual string Topic { get; set; }
        public virtual int Refid { get; set; }
        public virtual int Stars { get; set; }
        public virtual int Id { get; set; }
    }
}
