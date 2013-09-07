using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using ThumpnailModule.Thumpnail.Domain;
using Ren.CMS.CORE.Config;
using Ren.CMS.CORE.nhibernate;

namespace ThumpnailModule.Thumpnail.Mapping {
    
    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ThumpnailsModuleMap : ClassMapping<TBThumpnailsModule> {
        
        public ThumpnailsModuleMap() {
			Table(RenConfig.DB.Prefix.Replace("dbo.","") +"Thumpnails_Module");
			Schema("dbo");
			Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));

			Property(x => x.AtID, map => map.NotNullable(true));
			Property(x => x.LastModification, map => map.NotNullable(true));
			Property(x => x.Path);
			Property(x => x.Width);
			Property(x => x.Height);
        }
    }
}
