using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class CategoryMap : ClassMapping<Category> {
        
        public CategoryMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Categories");
			Schema("dbo");
			Lazy(true);
            Id<Guid>(e => e.Pkid, map =>
                {
                    map.Generator(Generators.Guid);

                });
			//Property(x => x.Pkid, map => map.NotNullable(true));
			Property(x => x.ContentType, map => map.NotNullable(true));
			Property(x => x.ShortName, map => map.NotNullable(true));
			Property(x => x.LongName, map => map.NotNullable(true));
			Property(x => x.SubFrom);
        }
    }
}
