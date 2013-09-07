using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Domain
{
    public class FilemanagementCrossBrowsers
    {
        public virtual System.Int32 Id { get; set; }
        public virtual string browserID { get; set; }
        public virtual string browserFullName { get; set; }
        public virtual string FileFormat { get; set; }
        public virtual string FileType { get; set; }


    }
}
