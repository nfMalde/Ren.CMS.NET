using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Domain
{
    public virtual class MimeTypes
    {
       
            public virtual int ID { get; set; }

            public virtual string MIME { get; set; }

            public virtual int nFileTypeID { get; set; }
     
    }
}
