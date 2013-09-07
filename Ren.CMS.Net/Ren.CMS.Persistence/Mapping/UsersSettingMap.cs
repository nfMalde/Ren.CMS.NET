using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class UsersSettingMap : ClassMapping<UsersSetting> {
        
        public UsersSettingMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Users_Settings");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.Upkid, map => map.NotNullable(true));
			Property(x => x.SettingName, map => map.NotNullable(true));
			Property(x => x.SettingValue, map => map.NotNullable(true));
        }
    }
}
