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
    public class RatingMap : ClassMapping<Rating>
    {
        #region Constructors

        public RatingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Rating");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.GroupID, map => map.NotNullable(true));
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.LangCode, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}