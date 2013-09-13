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
    public class CategoryMap : ClassMapping<Category>
    {
        #region Constructors

        public CategoryMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Categories");
            Schema("dbo");
            Lazy(true);
            Id<Guid>(e => e.Pkid, map =>
                {
                    map.Generator(Generators.Guid);

                });
            //Property(x => x.Pkid, map => map.NotNullable(true));
            Property(x => x.ContentType, map => map.NotNullable(true));
            Property(x => x.ShortName, map => map.NotNullable(true));
            Property(x => x.LongName, map => map.NotNullable(true));
            Property(x => x.SubFrom);
        }

        #endregion Constructors
    }
}