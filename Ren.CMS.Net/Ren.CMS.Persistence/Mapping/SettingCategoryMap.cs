using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class SettingCategoryMap : ClassMapping<SettingCategory> {
        
        public SettingCategoryMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"SettingCategories");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.Name, map => map.NotNullable(true));
			Property(x => x.CatRel, map => map.NotNullable(true));
        }
    }
}
