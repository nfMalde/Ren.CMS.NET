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
    public class PermissionGroupMap : ClassMapping<PermissionGroup>
    {
        #region Constructors

        public PermissionGroupMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"PermissionGroups");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.GroupName, map => map.NotNullable(true));
            Property(x => x.IsGuestGroup, map => map.NotNullable(true));
            Property(x => x.IsDefaultGroup, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}