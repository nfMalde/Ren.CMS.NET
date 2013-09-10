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
    public class Links2IdentfierMap : ClassMapping<Links2Identfier>
    {
        #region Constructors

        public Links2IdentfierMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Links2Identfiers");
            Schema("dbo");
            Lazy(true);
            Property(x => x.LinkID, map => map.NotNullable(true));
            Property(x => x.IdentifierID, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}