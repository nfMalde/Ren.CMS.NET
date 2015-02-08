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
    public class BackendMenuMap : ClassMapping<BackendMenu>
    {
        #region Constructors

        public BackendMenuMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Menu");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.MenuTextLang, map => map.NotNullable(true));
            Property(x => x.IconUrl);
            Property(x => x.Action);
            Property(x => x.NeededPermission, map => map.NotNullable(true));
            Property(x => x.HeadID);
        }

        #endregion Constructors
    }
}