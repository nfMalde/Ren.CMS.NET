using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nAttachmentRemark
    {
        public int Id { get; set; }
        public nAttachmentRemarkType Type { get; set; }
        public string Remarktext { get; set; }

        public nAttachmentRemark()
        {

        }

        public nAttachmentRemark(Persistence.Domain.ContentAttachmentRemarks Entity)
        {
            this.Id = Entity.Id;
            this.Remarktext = Entity.Remarktext;
            this.Type = new nAttachmentRemarkType(Entity.RemarkType);
        }

        public nAttachmentRemark(string Text, nAttachmentRemarkType type)
        {
            this.Remarktext = Text;
            this.Type = type;
        }
            
        
    }
}
