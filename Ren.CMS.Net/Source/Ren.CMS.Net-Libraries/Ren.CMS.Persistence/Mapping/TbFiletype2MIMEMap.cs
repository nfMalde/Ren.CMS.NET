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
    public class TbFiletype2MIMEMap : ClassMapping<TbFiletype2MIME>
    {
        public TbFiletype2MIMEMap()
        {
            Table(Ren.CMS.CORE.Config.RenConfig.DB.Prefix + "Filetype2MIME");
            Schema("dbo");
            Id(x => x.Id);
            Property(x => x.FileTypeId);
            Property(x => x.MimeId);
            ManyToOne(x => x.Mime, map =>
            {
                map.Cascade(Cascade.None);
                map.Fetch(FetchKind.Join);
                map.Column("MimeId");
                map.Insert(false);
                map.Update(false);
            });
        }
    }
}
