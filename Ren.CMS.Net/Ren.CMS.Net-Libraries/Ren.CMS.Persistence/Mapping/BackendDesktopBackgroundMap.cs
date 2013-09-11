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
    public class BackendDesktopBackgroundMap : ClassMapping<BackendDesktopBackground>
    {
        #region Constructors

        public BackendDesktopBackgroundMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Backend_Desktop_Backgrounds");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Userid, map => map.NotNullable(true));
            Property(x => x.BackgroundImage);
            Property(x => x.BackgroundColor);
            Property(x => x.BackgroundAlign);
            Property(x => x.BackgroundRepeat);
        }

        #endregion Constructors
    }
}