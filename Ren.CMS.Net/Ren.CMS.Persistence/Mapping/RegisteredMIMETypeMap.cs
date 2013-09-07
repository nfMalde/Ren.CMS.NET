using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class RegisteredMIMETypeMap : ClassMapping<RegisteredMIMEType> {
        
        public RegisteredMIMETypeMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"RegisteredMIMETypes");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.FileExstension, map => map.NotNullable(true));
			Property(x => x.Mimetype, map => map.NotNullable(true));
        }
    }
}
