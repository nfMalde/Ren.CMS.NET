using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ContentTagMap : ClassMapping<ContentTag> {
        
        public ContentTagMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Tags");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.ContentType, map => map.NotNullable(true));
			Property(x => x.TagName, map => map.NotNullable(true));
			Property(x => x.EnableBrowsing, map => map.NotNullable(true));
			Property(x => x.TagNameSEO);
        }
    }
}
