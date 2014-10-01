using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Domain
{
    public virtual class TbFiletype2MIME
    {
        public virtual int Id { get; set; }

        public virtual int FileTypeId { get; set; }

        public virtual int MimeId { get; set; }

        public virtual RegisteredMIMEType Mime { get; set; }
    }
}
