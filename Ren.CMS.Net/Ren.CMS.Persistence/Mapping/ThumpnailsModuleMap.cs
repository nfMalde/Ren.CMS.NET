using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ThumpnailsModuleMap : ClassMapping<ThumpnailsModule> {
        
        public ThumpnailsModuleMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Thumpnails_Module");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.AtID, map => map.NotNullable(true));
			Property(x => x.LastModification, map => map.NotNullable(true));
			Property(x => x.Path);
			Property(x => x.Width);
			Property(x => x.Height);
        }
    }
}
