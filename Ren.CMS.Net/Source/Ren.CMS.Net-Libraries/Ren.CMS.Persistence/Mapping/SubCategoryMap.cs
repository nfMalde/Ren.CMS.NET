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
    public class SubCategoryMap : ClassMapping<SubCategory>
    {
        #region Constructors

        public SubCategoryMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Sub_Categories");
            Schema("dbo");
            Lazy(true);
            Property(x => x.Pkid, map => map.NotNullable(true));
            Property(x => x.Cid);
            Property(x => x.Ref, map => map.NotNullable(true));
            Property(x => x.ShortName, map => map.NotNullable(true));
            Property(x => x.LongName, map => map.NotNullable(true));
            Property(x => x.LangCode, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}