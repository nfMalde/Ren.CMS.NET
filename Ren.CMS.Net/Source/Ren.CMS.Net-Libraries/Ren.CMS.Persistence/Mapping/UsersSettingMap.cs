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
    public class UsersSettingMap : ClassMapping<UsersSetting>
    {
        #region Constructors

        public UsersSettingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Users_Settings");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Upkid, map => map.NotNullable(true));
            Property(x => x.SettingName, map => map.NotNullable(true));
            Property(x => x.SettingValue, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}