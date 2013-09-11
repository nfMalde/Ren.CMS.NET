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
    public class RatingGroupMap : ClassMapping<RatingGroup>
    {
        #region Constructors

        public RatingGroupMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Rating_Groups");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.InternalName, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}