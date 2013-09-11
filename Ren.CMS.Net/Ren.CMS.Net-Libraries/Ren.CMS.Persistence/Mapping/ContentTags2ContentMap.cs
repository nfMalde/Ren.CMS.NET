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
    public class ContentTags2ContentMap : ClassMapping<ContentTags2Content>
    {
        #region Constructors

        public ContentTags2ContentMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Tags2Content");
            Schema("dbo");
            Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));
             			Property(x => x.ContentID, map => map.NotNullable(true));
            Property(x => x.TagID, map => map.NotNullable(true));
        }

        #endregion Constructors
    }
}