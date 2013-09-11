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
    public class ContentTypeMap : ClassMapping<ContentType>
    {
        #region Constructors

        public ContentTypeMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Types");
            Schema("dbo");
            Lazy(true);
            Id<string>(x => x.Name, map => map.Generator(Generators.Assigned));
            //Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.Controller, map => map.NotNullable(true));
            Property(x => x.Actionpath, map => map.NotNullable(true));
            Property(x => x.CreatePartial);
            Property(x => x.EditPartial);
        }

        #endregion Constructors
    }
}