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
    public class ContentAttachmentRemarkTypesMap : ClassMapping<ContentAttachmentRemarkTypes>
    {
        public ContentAttachmentRemarkTypesMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Content_Attachment_Remark_Types");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Remarklocalline);
            Property(x => x.Remarklocalpackage);
            Property(x => x.Remarkname);
            
       
        }
    }
}
