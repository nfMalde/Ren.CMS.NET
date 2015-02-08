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
    public class Settings2PermissionMap : ClassMapping<Settings2Permission>
    {
        #region Constructors

        public Settings2PermissionMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Settings2Permissions");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.FrontEndPM, map => map.NotNullable(true));
            Property(x => x.BackEndPM, map => map.NotNullable(true));
            Property(x => x.Sid);
        }

        #endregion Constructors
    }
}