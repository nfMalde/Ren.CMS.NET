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
    public class UsersSettingMap : ClassMapping<UsersSetting>
    {
        #region Constructors

        public UsersSettingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Users_Settings");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Upkid, map => map.NotNullable(true));
            Property(x => x.SettingName, map => map.NotNullable(true));
            Property(x => x.SettingValue, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}