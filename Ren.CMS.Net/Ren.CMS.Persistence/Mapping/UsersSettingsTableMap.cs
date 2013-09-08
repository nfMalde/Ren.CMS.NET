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
    public class UsersSettingsTableMap : ClassMapping<UsersSettingsTable>
    {
        #region Constructors

        public UsersSettingsTableMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Users_Settings_Table");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.SettingName, map => map.NotNullable(true));
            Property(x => x.SettingDefaultVal);
            Property(x => x.SettingLangLine);
            Property(x => x.SettingLongDescription);
            Property(x => x.SettingOrder);
            Property(x => x.DataType);
            Property(x => x.SType, map => map.Column("s_type"));
        }

        #endregion Constructors
    }
}