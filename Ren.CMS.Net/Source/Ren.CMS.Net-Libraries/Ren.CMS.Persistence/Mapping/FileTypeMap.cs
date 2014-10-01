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
    public class FileTypeMap : ClassMapping<TbFileType>
    {

        public FileTypeMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Physical);
            Property(x => x.ProfileID);
            Property(x => x.TypeName);
            ManyToOne(x => x.Profile, m =>
            {
                m.Column("ProdileID");
                m.Fetch(FetchKind.Select);
                m.Cascade(Cascade.None);
                m.Insert(false);
                m.Update(false);
            });

           
        }
    }
}
