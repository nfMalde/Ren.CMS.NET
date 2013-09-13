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
            Property(x => x.Id, map => map.NotNullable(true));
            Property(x => x.AType, map => map.NotNullable(true));
            Property(x => x.RoleName, map => map.NotNullable(true));
            Property(x => x.RoleLangLine);
        }

        #endregion Constructors
    }
}