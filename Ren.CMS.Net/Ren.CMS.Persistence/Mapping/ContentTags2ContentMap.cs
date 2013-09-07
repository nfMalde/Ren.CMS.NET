using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ContentTags2ContentMap : ClassMapping<ContentTags2Content> {
        
        public ContentTags2ContentMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Tags2Content");
			Schema("dbo");
			Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
 			Property(x => x.ContentID, map => map.NotNullable(true));
			Property(x => x.TagID, map => map.NotNullable(true));
        }
    }
}
