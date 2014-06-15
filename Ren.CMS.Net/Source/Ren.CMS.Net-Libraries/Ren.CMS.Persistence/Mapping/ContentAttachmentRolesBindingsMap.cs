using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.Persistence.Domain;


namespace Ren.CMS.Persistence.Mapping {
    
    
    public class ContentAttachmentRolesBindingsMap : ClassMapping<ContentAttachmentRolesBindings> {
        
        public ContentAttachmentRolesBindingsMap() {
			Table("nfcms_Content_Attachment_Roles_Bindings");
			Schema("dbo");
			Lazy(true);
            Id(e => e.Id, map => map.Generator(Generators.Identity));
 
			Property(x => x.Roleid, map => map.NotNullable(true));
			Property(x => x.Bindingtype, map => map.NotNullable(true));
			Property(x => x.Bindingvalue, map => map.NotNullable(true));
        }
    }
}
