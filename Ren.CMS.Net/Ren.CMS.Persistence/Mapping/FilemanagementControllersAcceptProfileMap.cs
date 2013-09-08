namespace Ren.CMS.CORE.nhibernate.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.CORE.nhibernate.Domain;

    [Ren.CMS.CORE.nhibernate.Base.PersistenceMapping]
    public class FilemanagementControllersAcceptProfileMap : ClassMapping<FilemanagementControllersAcceptProfile>
    {
        #region Constructors

        public FilemanagementControllersAcceptProfileMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"FilemanagementControllersAcceptProfiles");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Pid);
            Property(x => x.Cid);
        }

        #endregion Constructors
    }
}