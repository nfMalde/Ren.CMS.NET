using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class BackendWidgetMap : ClassMapping<BackendWidget> {
        
        public BackendWidgetMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Widgets");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.WidgetName, map => map.NotNullable(true));
			Property(x => x.WidgetPartialView, map => map.NotNullable(true));
			Property(x => x.NeededPermission, map => map.NotNullable(true));
			Property(x => x.DefinedWidth);
			Property(x => x.DefinedHeight);
			Property(x => x.Icon);
        }
    }
}
