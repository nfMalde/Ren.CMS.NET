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
    public class ContentAttachmenArgumenttMap : ClassMapping<ContentAttachmentArgument>
    {
        public ContentAttachmenArgumenttMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Content_Attachment_Argument");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.RoleId, map => map.NotNullable(true));
            Property(x => x.Argumentlangline);
            Property(x => x.Argumentlangpackage);
            
            Property(x => x.ArgumentName);
        }
    }
}
