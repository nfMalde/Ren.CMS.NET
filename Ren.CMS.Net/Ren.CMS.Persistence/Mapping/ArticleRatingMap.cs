namespace Ren.CMS.CORE.nhibernate.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.CORE.nhibernate.Domain;

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class ArticleRatingMap : ClassMapping<ArticleRating>
    {
        #region Constructors

        public ArticleRatingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Article_Rating");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.ArticleID, map => map.NotNullable(true));
            Property(x => x.RatingID, map => map.NotNullable(true));
            Property(x => x.Stars, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}