using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.Persistence.Domain;


namespace Ren.CMS.Persistence.Mapping {

        [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class ContentAttachmenttypesMap : ClassMapping<ContentAttachmenttypes> {

        public ContentAttachmenttypesMap()
        {
			Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix  +"Content_AttachmentTypes");
			Schema("dbo");
			Lazy(true);
			//Property(x => x.Id, map => map.NotNullable(true));
            Id(e => e.Id, map => map.Generator(Generators.Identity));
			Property(x => x.Typename, map => map.NotNullable(true));
			Property(x => x.Storagepath, map => map.NotNullable(true));
            Property(e => e.HandlerNamespace);
            Set(x => x.AttachmentExtensions, mapping =>
            {
                mapping.Lazy(CollectionLazy.NoLazy);
                mapping.Key(k =>
                {
                    k.Column("attachmentTypeId");
                });
                mapping.Inverse(true);
                mapping.Cascade(Cascade.None);

            },
             r => r.OneToMany());
        }
    }
}
