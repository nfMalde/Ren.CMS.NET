using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ContentTypeMap : ClassMapping<ContentType> {
        
        public ContentTypeMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Types");
			Schema("dbo");
			Lazy(true);
            Id<string>(x => x.Name, map => map.Generator(Generators.Assigned));
			//Property(x => x.Name, map => map.NotNullable(true));
			Property(x => x.Controller, map => map.NotNullable(true));
			Property(x => x.Actionpath, map => map.NotNullable(true));
			Property(x => x.CreatePartial);
			Property(x => x.EditPartial);
        }
    }
}
