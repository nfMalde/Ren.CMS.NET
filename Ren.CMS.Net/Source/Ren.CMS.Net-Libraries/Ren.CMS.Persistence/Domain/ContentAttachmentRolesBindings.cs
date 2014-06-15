using System;
using System.Text;
using System.Collections.Generic;


namespace Ren.CMS.Persistence.Domain {
    
    public class ContentAttachmentRolesBindings {
        public virtual int Id { get; set; }
        public virtual int Roleid { get; set; }
        public virtual string Bindingtype { get; set; }
        public virtual string Bindingvalue { get; set; }

        public virtual ContentAttachmentRole Role { get; set; }
    }
}
