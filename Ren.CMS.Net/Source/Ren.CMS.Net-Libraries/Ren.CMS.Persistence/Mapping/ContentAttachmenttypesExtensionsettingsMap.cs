using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using Ren.CMS.Persistence.Domain;


namespace Ren.CMS.Persistence.Mapping {

      [Ren.CMS.Persistence.Base.PersistenceMapping]  
    public class ContentAttachmenttypesExtensionsettingsMap : ClassMapping<ContentAttachmenttypesExtensionsettings> {

        public ContentAttachmenttypesExtensionsettingsMap()
        {
			Table( Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Content_AttachmentTypes_ExtensionSettings");
			Schema("dbo");
			Lazy(true);
			//Id(x => x.Id, map => map.Generator(Generators.Identity));
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Attachmenttypeid, map => map.NotNullable(true));
			Property(x => x.Extension, map => map.NotNullable(true));
			Property(x => x.Maxfilesize);
			Property(x => x.Convertfile, map => map.NotNullable(true));

 
        }
    }
}
