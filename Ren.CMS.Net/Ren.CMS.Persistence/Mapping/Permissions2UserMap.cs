using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class Permissions2UserMap : ClassMapping<Permissions2User> {
        
        public Permissions2UserMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Permissions2Users");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Pk);
			Property(x => x.GroupID);
			Property(x => x.Val);
			Property(x => x.Usr);
        }
    }
}
