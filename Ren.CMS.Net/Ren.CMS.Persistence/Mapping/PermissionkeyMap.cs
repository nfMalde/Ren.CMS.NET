using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class PermissionkeyMap : ClassMapping<Permissionkey> {
        
        public PermissionkeyMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Permissionkeys");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Pkey, map => map.NotNullable(true));
			Property(x => x.DefaultVal, map => map.NotNullable(true));
			Property(x => x.LangLine, map => map.NotNullable(true));
        }
    }
}
