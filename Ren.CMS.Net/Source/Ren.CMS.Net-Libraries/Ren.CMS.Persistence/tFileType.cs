using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence
{
    public virtual class tFileType
    {
        public virtual int Id { get; set; }

        public virtual string TypeName { get; set; }

        public virtual List<MimeTypes> AllowedMIMETypes { get; set; }

        public virtual bool Physical { get; set; }

        public virtual FileManagementProfile Profile { get; set; }
        public virtual int ProfileId { get; set; }
    }
}
