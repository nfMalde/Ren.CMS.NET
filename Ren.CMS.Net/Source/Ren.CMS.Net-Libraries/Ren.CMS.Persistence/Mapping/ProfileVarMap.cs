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
    public class ProfileVarMap : ClassMapping<ProfileVar>
    {
        #region Constructors

        public ProfileVarMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Profile_Vars");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Name);
            Property(x => x.LangLine);
            Property(x => x.Section);
            Property(x => x.ViewName);
            Property(x => x.Active);
            Property(x => x.ShowInProfile);
        }

        #endregion Constructors
    }
}