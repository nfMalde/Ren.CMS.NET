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
    public class Permissions2UserMap : ClassMapping<Permissions2User>
    {
        #region Constructors

        public Permissions2UserMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Permissions2Users");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Pk);
            Property(x => x.GroupID);
            Property(x => x.Val);
            Property(x => x.Usr);
        }

        #endregion Constructors
    }
}