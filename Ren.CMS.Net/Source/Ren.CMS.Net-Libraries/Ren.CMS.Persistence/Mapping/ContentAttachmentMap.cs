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
    public class ContentAttachmentMap : ClassMapping<ContentAttachment>
    {
        #region Constructors

        public ContentAttachmentMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix  +"Content_Attachment");
            Schema("dbo");
            Lazy(false);
            //Property(x => x.Pkid, map => map.NotNullable(true));
            Id<Guid>(x => x.Pkid, map =>
                {
                    map.Generator(Generators.Guid);
                    map.Column("PKID");

                });

 
            Property(x => x.Contentid, map => map.NotNullable(true));
            Property(x => x.AttachmentTypeId, map => { map.Column("attachment_type_id"); map.NotNullable(true); });
            Property(x => x.Filepath, map => map.NotNullable(true));
            Property(x => x.Thumnailpath);
            Property(x => x.Usage);
            Property(x => x.Title);


            ManyToOne(x => x.AttachmentType, m =>
            {

                m.Column(c => c.Name("attachment_type_id"));
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
                m.Lazy(LazyRelation.NoLazy);

            });

            ManyToOne(x => x.Role, m =>
            {

                m.Column(c => c.Name("usage"));
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
                m.Lazy(LazyRelation.NoLazy);

            });
        }

        #endregion Constructors
    }
}