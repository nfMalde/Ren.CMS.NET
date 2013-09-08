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
    public class Permissions2GroupMap : ClassMapping<Permissions2Group>
    {
        #region Constructors

        public Permissions2GroupMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Permissions2Groups");
            Schema("dbo");
            Lazy(true);
            Property(x => x.GroupID);
            Property(x => x.Val);
            Property(x => x.Pk);
        }

        #endregion Constructors
    }
}