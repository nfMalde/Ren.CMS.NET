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
    public class Links2IdentfierMap : ClassMapping<Links2Identfier>
    {
        #region Constructors

        public Links2IdentfierMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Links2Identfiers");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.LinkID, map => map.NotNullable(true));
            Property(x => x.IdentifierID, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}