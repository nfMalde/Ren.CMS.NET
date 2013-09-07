using System;
using System.Text;
using System.Collections.Generic;


namespace ThumpnailModule.Thumpnail.Domain {
    
    public class TBThumpnailsModule {
        public virtual int Id { get; set; }
        public virtual System.Guid AtID { get; set; }
        public virtual System.DateTime LastModification { get; set; }
        public virtual string Path { get; set; }
        public virtual System.Nullable<int> Width { get; set; }
        public virtual System.Nullable<int> Height { get; set; }
    }
}
