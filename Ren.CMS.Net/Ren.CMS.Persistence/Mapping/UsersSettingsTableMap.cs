using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class UsersSettingsTableMap : ClassMapping<UsersSettingsTable> {
        
        public UsersSettingsTableMap() {
            
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
    }
}
