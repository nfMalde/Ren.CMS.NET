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
    public class ProfileUserValueMap : ClassMapping<ProfileUserValue>
    {
        #region Constructors

        public ProfileUserValueMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Profile_User_Values");
            Schema("dbo");
            Lazy(true);
            Property(x => x.VarName);
            Property(x => x.VarValue);
            Property(x => x.Pkid);
        }

        #endregion Constructors
    }
}