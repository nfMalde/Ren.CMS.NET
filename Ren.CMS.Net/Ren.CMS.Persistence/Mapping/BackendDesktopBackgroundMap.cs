using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class BackendDesktopBackgroundMap : ClassMapping<BackendDesktopBackground> {
        
        public BackendDesktopBackgroundMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Desktop_Backgrounds");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Userid, map => map.NotNullable(true));
			Property(x => x.BackgroundImage);
			Property(x => x.BackgroundColor);
			Property(x => x.BackgroundAlign);
			Property(x => x.BackgroundRepeat);
        }
    }
}
