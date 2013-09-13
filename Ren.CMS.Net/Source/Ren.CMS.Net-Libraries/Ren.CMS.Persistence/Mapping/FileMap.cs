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
    public class FileMap : ClassMapping<File>
    {
        #region Constructors

        public FileMap()
        {
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

        #endregion Constructors
    }
}