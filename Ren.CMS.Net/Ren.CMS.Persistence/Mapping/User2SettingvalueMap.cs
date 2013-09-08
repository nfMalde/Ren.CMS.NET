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
    public class User2SettingvalueMap : ClassMapping<User2Settingvalue>
    {
        #region Constructors

        public User2SettingvalueMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"User2Settingvalues");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Sid, map => map.NotNullable(true));
            Property(x => x.Uid, map => map.NotNullable(true));
            Property(x => x.Vid, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}