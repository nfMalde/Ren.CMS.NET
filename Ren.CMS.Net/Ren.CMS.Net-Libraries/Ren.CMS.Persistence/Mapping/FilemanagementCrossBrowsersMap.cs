namespace Ren.CMS.Persistence.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class FilemanagementCrossBrowsersMap : ClassMapping<FilemanagementCrossBrowsers>
    {
        #region Constructors

        public FilemanagementCrossBrowsersMap()
        {
            Lazy(true);
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Filemanagement_Crossbrowsers");
            Schema("dbo");
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
            Property<string>(x => x.browserFullName, map => {
                map.NotNullable(true);

            });

            Property<string>(x => x.browserID, map =>
            {
                map.NotNullable(true);
            });

            Property<string>(x => x.FileFormat, map =>  {
                map.NotNullable(true);
                map.Length(5);

            });

            Property<string>(x => x.FileType, map =>
            {
                map.NotNullable(true);

            });
        }

        #endregion Constructors
    }
}