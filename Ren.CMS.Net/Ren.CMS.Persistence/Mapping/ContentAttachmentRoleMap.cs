using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ContentAttachmentRoleMap : ClassMapping<ContentAttachmentRole> {
        
        public ContentAttachmentRoleMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Attachment_Roles");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.AType, map => map.NotNullable(true));
			Property(x => x.RoleName, map => map.NotNullable(true));
			Property(x => x.RoleLangLine);
        }
    }
}
