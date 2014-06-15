using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content.ContentAttachmentHandlers
{
    public class ContentAttachmentHandlerBase
    {
        public ContentAttachmentHandlerBase(Ren.CMS.Content.nContent.nAttachment Attachment)
        {

        }

       
        public virtual void Upload( HttpPostedFileBase file)
        {

        }


        public virtual void Upload(HttpPostedFile file)
        {

        }

        public virtual void Convert()
        {

        }


        public virtual void Delete()
        {

        }


        public virtual void Update()
        {

        }

        public virtual Uri GetUrl()
        {

            return null;
        }
    }
}
