using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Filemanagement
{
    public class nFile
    {
        public int Id { get; set; }
        public int TypeID { get; set; }

        public string AliasName { get; set; }

        public string FilePath { get; set; }

        public bool isActive { get; set; }

        public string VirtualPath { get; set; }

        public nFileType FileType { get; set; }
    }
}
