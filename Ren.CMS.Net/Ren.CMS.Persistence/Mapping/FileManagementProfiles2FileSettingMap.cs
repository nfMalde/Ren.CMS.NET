using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.CORE.nhibernate.Domain;


namespace Ren.CMS.CORE.nhibernate.Mapping {

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class FileManagementProfiles2FileSettingMap : ClassMapping<FileManagementProfiles2FileSetting> {
        
        public FileManagementProfiles2FileSettingMap() {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FileManagementProfiles2FileSettings");
			Schema("dbo");
			Lazy(true);
			Property(x => x.Id, map => map.NotNullable(true));
			Property(x => x.ProfileID, map => map.NotNullable(true));
			Property(x => x.SettingID, map => map.NotNullable(true));
        }
    }
}
