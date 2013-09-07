using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.CORE.nhibernate.Domain {
    
    public class File {
        public virtual int Id { get; set; }
        public virtual string Fpath { get; set; }
        public virtual string AliasName { get; set; }
        public virtual string NeedPermission { get; set; }
        public virtual int Active { get; set; }
        public virtual System.Nullable<int> FileSize { get; set; }
        public virtual System.Nullable<int> ProfileID { get; set; }
    }
}
