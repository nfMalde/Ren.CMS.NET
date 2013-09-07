using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {
    
    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ArticleRatingMap : ClassMapping<ArticleRating> {
        
        public ArticleRatingMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Article_Rating");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.ArticleID, map => map.NotNullable(true));
			Property(x => x.RatingID, map => map.NotNullable(true));
			Property(x => x.Stars, map => map.NotNullable(true));
        }
    }
}
