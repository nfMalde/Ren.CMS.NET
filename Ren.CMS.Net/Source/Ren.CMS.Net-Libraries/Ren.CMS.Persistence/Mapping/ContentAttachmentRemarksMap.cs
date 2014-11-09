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
    public class ContentAttachmentRemarksMap : ClassMapping<ContentAttachmentRemarks>
    {
        public ContentAttachmentRemarksMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Content_Attachment_Remarks");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Remarktext);
            Lazy(false);
            ManyToOne(x => x.RemarkType, map =>
            {
                map.Column("Remarktype");
                map.Cascade(Cascade.None);
                map.Insert(true);
                map.Update(true);
                map.Fetch(FetchKind.Select);
                map.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Attachment, m =>
            {
                m.Column("AttachmentId");
                m.Cascade(Cascade.None);
                m.Fetch(FetchKind.Select);
                m.Lazy(LazyRelation.NoLazy);
                m.Update(true);
                m.Insert(true);

            });
        }
    }
}
