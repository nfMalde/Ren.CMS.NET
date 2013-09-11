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
    public class ContentClickCounterMap : ClassMapping<ContentClickCounter>
    {
        #region Constructors

        public ContentClickCounterMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_ClickCounter");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Ip);
            Property(x => x.Cid, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}