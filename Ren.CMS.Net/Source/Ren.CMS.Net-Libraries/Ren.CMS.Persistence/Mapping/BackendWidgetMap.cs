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
    public class BackendWidgetMap : ClassMapping<BackendWidget>
    {
        #region Constructors

        public BackendWidgetMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Widgets");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.WidgetName, map => map.NotNullable(true));
            Property(x => x.WidgetPartialView, map => map.NotNullable(true));
            Property(x => x.NeededPermission, map => map.NotNullable(true));
            Property(x => x.DefinedWidth);
            Property(x => x.DefinedHeight);
            Property(x => x.Icon);
        }

        #endregion Constructors
    }
}