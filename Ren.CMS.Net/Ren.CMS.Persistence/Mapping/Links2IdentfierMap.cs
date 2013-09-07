using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class Links2IdentfierMap : ClassMapping<Links2Identfier> {
        
        public Links2IdentfierMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Links2Identfiers");
			Schema("dbo");
			Lazy(true);
			Property(x => x.LinkID, map => map.NotNullable(true));
			Property(x => x.IdentifierID, map => map.NotNullable(true));
        }
    }
}
