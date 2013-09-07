using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class InternalRatingMap : ClassMapping<InternalRating> {
        
        public InternalRatingMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Internal_Rating");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Topic, map => map.NotNullable(true));
			Property(x => x.Refid, map => map.NotNullable(true));
			Property(x => x.Stars, map => map.NotNullable(true));
			Property(x => x.Id, map => map.NotNullable(true));
        }
    }
}
