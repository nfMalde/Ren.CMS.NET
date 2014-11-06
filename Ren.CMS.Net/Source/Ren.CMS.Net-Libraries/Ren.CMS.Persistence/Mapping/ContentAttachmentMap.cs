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

            
            //Property(x => x.Contentid, map => map.NotNullable(true));
 
            //Property(x => x.FileId, map => map.NotNullable(true));

            ManyToOne(x => x.AttachmentType, m =>
            {

                m.Column("AttachmentTypeId");
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(true);
                m.Update(true);
                m.Lazy(LazyRelation.NoLazy);

            });

            ManyToOne(x => x.Role, m =>
            {
                
                m.Column("RoleId");
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(true);
                m.Update(true);
                m.Lazy(LazyRelation.NoLazy);

            });

            ManyToOne(x => x.Argument, m =>
            {
                m.Column("ArgumentId");
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.None);
                m.Insert(true);
                m.Update(true);
                m.Lazy(LazyRelation.NoLazy);
            });


            ManyToOne(x => x.File, m =>
            {
                m.Column("FileId");
                m.Fetch(FetchKind.Join);
                m.Cascade(Cascade.All);
                m.Insert(true);
                m.Update(true);
                m.Lazy(LazyRelation.NoLazy);
            });


            ManyToOne(x => x.Content, m =>
            {
                m.Column("ContentId");
                m.Fetch(FetchKind.Select);
                m.Cascade(Cascade.None);
                m.Insert(true);
                m.Update(true);
                m.Lazy(LazyRelation.NoLazy);
            });


            Set(x => x.Remarks, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("Attachmentid");
                });
                mapping.Inverse(true);
                mapping.Cascade(Cascade.All);

            },
               r => r.OneToMany());
 

            Bag(x => x.Texts, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("AttachmentId");

                });
                mapping.Inverse(true);
                mapping.Cascade(Cascade.All);

                mapping.Fetch(CollectionFetchMode.Select);

            },
              r => r.OneToMany());
        }

        #endregion Constructors
    }
}