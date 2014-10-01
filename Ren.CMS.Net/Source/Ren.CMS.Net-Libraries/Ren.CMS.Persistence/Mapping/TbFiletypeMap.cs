using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Mapping
{

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class TbFiletypeMap : ClassMapping<TbFileType>
    {
        public TbFiletypeMap()
         {
             Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Filetype");
             Schema("dbo");
             Id(e => e.Id, map => map.Generator(Generators.Identity));
             Property(e => e.Physical);
             Property(e => e.ProfileID);
             Property(e => e.TypeName);
             ManyToOne(e => e.Profile, map => {
                map.Cascade(Cascade.None);
                map.Fetch(FetchKind.Join);
                map.Column("ProfileID");
                map.Insert(false);
                map.Update(false);
             });

             Set(x => x.AllowedMIMETypes, mapping =>
             {
                 mapping.Lazy(CollectionLazy.NoLazy);
                 mapping.Key(k =>
                 {
                     k.Column("FileTypeId");
                 });
                 mapping.Inverse(true);
                 mapping.Cascade(Cascade.None);

             },
              r => r.OneToMany());

         }
    }
}
