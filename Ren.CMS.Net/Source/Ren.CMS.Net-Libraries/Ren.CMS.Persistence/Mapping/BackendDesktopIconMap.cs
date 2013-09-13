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
    public class BackendDesktopIconMap : ClassMapping<BackendDesktopIcon>
    {
        #region Constructors

        public BackendDesktopIconMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Desktop_Icons");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.LangLine);
            Property(x => x.Icon);
            Property(x => x.Action, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}