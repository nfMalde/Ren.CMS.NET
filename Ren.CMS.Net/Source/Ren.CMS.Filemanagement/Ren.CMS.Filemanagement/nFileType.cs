using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Filemanagement
{
    public class nFileType
    {
        private List<nMimeType> allowed = new List<nMimeType>();
        public int Id { get; set; }

        public string TypeName { get; set; }

        public List<nMimeType> AllowedMIMETypes { get { return this.allowed; } set { this.allowed = value; } }

        public bool Physical { get; set; }

        public bool MimeIsAllowend(nMimeType mime)
        {

            return this.MimeIsAllowend(mime.MIME);
        }

        public bool MimeIsAllowend(string mime)
        {

            return this.AllowedMIMETypes != null && this.AllowedMIMETypes.Any(e => e.MIME.ToLower() == mime.ToLower());
        }


        public nFileProfile Profile { get; set; }
    }



    public class nMimeType
    {
        public int ID { get; set; }

        public string MIME { get; set; }

        public int nFileTypeID { get; set; }
    }
}
