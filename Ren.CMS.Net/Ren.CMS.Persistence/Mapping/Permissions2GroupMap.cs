using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class Permissions2GroupMap : ClassMapping<Permissions2Group> {
        
        public Permissions2GroupMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Permissions2Groups");
			Schema("dbo");
			Lazy(true);
			Property(x => x.GroupID);
			Property(x => x.Val);
			Property(x => x.Pk);
        }
    }
}
