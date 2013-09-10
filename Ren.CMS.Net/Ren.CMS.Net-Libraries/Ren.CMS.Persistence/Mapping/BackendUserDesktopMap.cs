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
    public class BackendUserDesktopMap : ClassMapping<BackendUserDesktop>
    {
        #region Constructors

        public BackendUserDesktopMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_User_Desktops");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.IconID, map => map.NotNullable(true));
            Property(x => x.Icon);
            Property(x => x.XPos, map => map.NotNullable(true));
            Property(x => x.YPos, map => map.NotNullable(true));
            Property(x => x.Userid, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}