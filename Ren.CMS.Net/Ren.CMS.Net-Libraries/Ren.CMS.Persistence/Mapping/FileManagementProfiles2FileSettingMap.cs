namespace Ren.CMS.Persistence.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.Persistence.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class FileManagementProfiles2FileSettingMap : ClassMapping<FileManagementProfiles2FileSetting>
    {
        #region Constructors

        public FileManagementProfiles2FileSettingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FileManagementProfiles2FileSettings");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.ProfileID, map => map.NotNullable(true));
            Property(x => x.SettingID, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}