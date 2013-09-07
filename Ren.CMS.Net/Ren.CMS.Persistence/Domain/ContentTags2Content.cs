using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentTags2Content {
        public virtual int Id { get; set; }
        public virtual int ContentID { get; set; }
        public virtual int TagID { get; set; }
    }
}
