using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class User2SettingvalueMap : ClassMapping<User2Settingvalue> {
        
        public User2SettingvalueMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"User2Settingvalues");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.Sid, map => map.NotNullable(true));
			Property(x => x.Uid, map => map.NotNullable(true));
			Property(x => x.Vid, map => map.NotNullable(true));
        }
    }
}
