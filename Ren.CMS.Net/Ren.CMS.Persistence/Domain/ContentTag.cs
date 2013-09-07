using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ContentTag {
        public virtual int Id { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string TagName { get; set; }
        public virtual int EnableBrowsing { get; set; }
        public virtual string TagNameSEO { get; set; }
    }
}
