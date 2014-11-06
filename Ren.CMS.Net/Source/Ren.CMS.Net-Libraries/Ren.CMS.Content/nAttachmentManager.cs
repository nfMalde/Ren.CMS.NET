using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content
{
    public class nAttachmentManager
    {
        private int contentId = 0;

        public nAttachmentManager(int ContentId)
        {
            this.contentId = ContentId;

        }

        //Adding
        public nContentAttachment AddAttachment(HttpPostedFile file, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts);


            return cType.Handler.Upload(file, NewAttachment);
        }

        public nContentAttachment AddAttachment(HttpPostedFileBase file, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts);


            return cType.Handler.Upload(file, NewAttachment);
        }

        public nContentAttachment AddAttachment(Uri url, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts);


            return cType.Handler.AddExternal(url, NewAttachment);
        }

        public nContentAttachment AddAttachment(string url, nContentAttachmenType cType, nAttachmentRole Role, nAttachmentArgument Argument, List<nContentAttachmentTexts> Texts = null)
        {
            nContentAttachment NewAttachment = new nContentAttachment(this.contentId, null, cType, Role, Argument, Texts);


            return cType.Handler.AddExternal(url, NewAttachment);
        }
    }
}
