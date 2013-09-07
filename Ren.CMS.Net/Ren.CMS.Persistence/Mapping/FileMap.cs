using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class FileMap : ClassMapping<File> {
        
        public FileMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Files");
			Schema("dbo");
			Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Fpath, map => map.NotNullable(true));
			Property(x => x.AliasName, map => map.NotNullable(true));
			Property(x => x.NeedPermission, map => map.NotNullable(true));
			Property(x => x.Active, map => map.NotNullable(true));
			Property(x => x.FileSize);
			Property(x => x.ProfileID);
        }
    }
}
