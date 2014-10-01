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
    public class FileManagementFileSettingMap : ClassMapping<FileManagementFileSetting>
    {
        #region Constructors

        public FileManagementFileSettingMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FileManagementFileSettings");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.SettingName, map => map.NotNullable(true));
            Bag(x => x.Value, c => { c.Inverse(true); c.Key( e => e.Column("SettingID")); }, r => { r.OneToMany(); });
        }

        #endregion Constructors
    }
}