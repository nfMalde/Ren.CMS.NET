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
    public class RoutingSynonymMap : ClassMapping<RoutingSynonym>
    {
        #region Constructors

        public RoutingSynonymMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Routing_Synonyms");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.Controller, map => map.NotNullable(true));
            Property(x => x.Action, map => map.NotNullable(true));
            Property(x => x.Rpath, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}