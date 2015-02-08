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
    public class ContentTagMap : ClassMapping<ContentTag>
    {
        #region Constructors

        public ContentTagMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Tags");
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.ContentType, map => map.NotNullable(true));
            Property(x => x.TagName, map => map.NotNullable(true));
            Property(x => x.EnableBrowsing, map => map.NotNullable(true));
            Property(x => x.TagNameSEO);
        }

        #endregion Constructors
    }
}