using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class ThumpnailsModule {
        public virtual int Id { get; set; }
        public virtual string AtID { get; set; }
        public virtual System.DateTime LastModification { get; set; }
        public virtual string Path { get; set; }
        public virtual System.Nullable<int> Width { get; set; }
        public virtual System.Nullable<int> Height { get; set; }
    }
}
