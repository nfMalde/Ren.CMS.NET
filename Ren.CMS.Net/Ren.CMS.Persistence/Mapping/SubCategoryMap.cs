using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class SubCategoryMap : ClassMapping<SubCategory> {
        
        public SubCategoryMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Sub_Categories");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Pkid, map => map.NotNullable(true));
			Property(x => x.Cid);
			Property(x => x.Ref, map => map.NotNullable(true));
			Property(x => x.ShortName, map => map.NotNullable(true));
			Property(x => x.LongName, map => map.NotNullable(true));
			Property(x => x.LangCode, map => map.NotNullable(true));
        }
    }
}
