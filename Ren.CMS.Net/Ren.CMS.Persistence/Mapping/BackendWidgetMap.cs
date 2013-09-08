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
    public class BackendWidgetMap : ClassMapping<BackendWidget>
    {
        #region Constructors

        public BackendWidgetMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Widgets");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
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