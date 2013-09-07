using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class RoutingSynonymMap : ClassMapping<RoutingSynonym> {
        
        public RoutingSynonymMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Routing_Synonyms");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
			Property(x => x.Controller, map => map.NotNullable(true));
			Property(x => x.Action, map => map.NotNullable(true));
			Property(x => x.Rpath, map => map.NotNullable(true));
        }
    }
}
