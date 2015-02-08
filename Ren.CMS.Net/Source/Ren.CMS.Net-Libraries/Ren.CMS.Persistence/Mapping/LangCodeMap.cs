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
    public class LangCodeMap : ClassMapping<LangCode>
    {
        #region Constructors

        public LangCodeMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Lang_Codes");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
//            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Code, map => map.NotNullable(true));
            Property(x => x.Name, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}