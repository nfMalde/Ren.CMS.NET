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
    public class ContentAttachmentTextsMap : ClassMapping<ContentAttachmentTexts>
    {
        #region Constructors

        public ContentAttachmentTextsMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix  +"Content_Attachment_Texts");
            Schema("dbo");
            Lazy(false);
            //Property(x => x.Pkid, map => map.NotNullable(true));
             Id(x => x.id, map => map.Generator(Generators.Identity));
             

            
            Property(x => x.Title, map => map.NotNullable(true));
            Property(x => x.Description, map => map.NotNullable(false));
            Property(x => x.LangCode, map => map.NotNullable(true));
           // Property(x => x.AttachmentId, map => map.NotNullable(true));
            ManyToOne(x => x.Attachment, m =>
            {
                m.Column("AttachmentId");
                m.Cascade(Cascade.None);
                m.Fetch(FetchKind.Select);
                m.Update(true);
                m.Insert(true);

            });
        }

        #endregion Constructors
    }
}