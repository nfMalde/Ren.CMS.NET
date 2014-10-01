using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Domain
{
     public virtual class  TbFileType
    {
         public virtual int Id { get; set; }

         public string TypeName { get; set; }


         public bool Physical { get; set; }
         public virtual int ProfileID { get; set; }

         public List<TbFiletype2MIME> AllowedMIMETypes { get; set; }

         public FileManagementProfile Profile { get; set; }


    }
}
