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
    public class ContentAttachmentRoleMap : ClassMapping<ContentAttachmentRole>
    {
        #region Constructors

        public ContentAttachmentRoleMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix.Replace("dbo.", "") +"Content_Attachment_Roles");
            Schema("dbo");
            Lazy(true);
            Id(e => e.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Subpath, map => map.NotNullable(true));
            Property(x => x.Rolename, map => map.NotNullable(true));
            Property(x => x.Rolelangline);
            Property(x => x.Rolelangpackage);
        }

        #endregion Constructors
    }
}