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
    public class InternalProContraMap : ClassMapping<InternalProContra>
    {
        #region Constructors

        public InternalProContraMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Internal_Pro_Contra");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.Refid, map => map.NotNullable(true));
            Property(x => x.PType, map => map.NotNullable(true));
            Property(x => x.PText, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}