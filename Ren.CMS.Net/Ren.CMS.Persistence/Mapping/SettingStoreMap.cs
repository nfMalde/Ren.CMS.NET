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
    public class SettingStoreMap : ClassMapping<SettingStore>
    {
        #region Constructors

        public SettingStoreMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"SettingStores");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Sid, map => map.NotNullable(true));
            Property(x => x.Val, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}