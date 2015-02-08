namespace Ren.CMS.Persistence.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.Persistence.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class InternalRatingMap : ClassMapping<InternalRating>
    {
        #region Constructors

        public InternalRatingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Internal_Rating");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Topic, map => map.NotNullable(true));
            Property(x => x.Refid, map => map.NotNullable(true));
            Property(x => x.Stars, map => map.NotNullable(true));
            Id(x => x.Id, map => map.Generator(Generators.Identity));
        }

        #endregion Constructors
    }
}