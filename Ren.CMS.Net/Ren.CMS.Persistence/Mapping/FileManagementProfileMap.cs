using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class FileManagementProfileMap : ClassMapping<FileManagementProfile> {
        
        public FileManagementProfileMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FileManagementProfiles");
			Schema("dbo");
			Lazy(true); 
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
			Property(x => x.ProfileName, map => map.NotNullable(true));
        }
    }
}
