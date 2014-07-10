using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Domain
{
    public virtual class MimeType2FileType
    {
        public virtual int Id { get; set; }

        public virtual int FileTypeId { get; set; }

        public virtual int MimeTypeId { get; set; }
    }
}
