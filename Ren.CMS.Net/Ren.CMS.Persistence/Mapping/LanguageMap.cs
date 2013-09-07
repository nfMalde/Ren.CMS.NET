using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class LanguageMap : ClassMapping<tbLanguage>
    {
        
        public LanguageMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Language");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Id(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Content, map => map.NotNullable(true));
			Property(x => x.Package, map => map.NotNullable(true));
			Property(x => x.Code, map => map.NotNullable(true));
        }
    }
}
