using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class BackendUserDesktopMap : ClassMapping<BackendUserDesktop> {
        
        public BackendUserDesktopMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_User_Desktops");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.IconID, map => map.NotNullable(true));
			Property(x => x.Icon);
			Property(x => x.XPos, map => map.NotNullable(true));
			Property(x => x.YPos, map => map.NotNullable(true));
			Property(x => x.Userid, map => map.NotNullable(true));
        }
    }
}
