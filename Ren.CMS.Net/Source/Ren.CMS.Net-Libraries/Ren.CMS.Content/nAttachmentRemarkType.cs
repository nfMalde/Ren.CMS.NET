using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nAttachmentRemarkType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remarklocalline { get; set; }
        public string Remarklocalpackage { get; set; }

        public nAttachmentRemarkType()
        {

        }


        public nAttachmentRemarkType(Persistence.Domain.ContentAttachmentRemarkTypes Entity)
        {
            this.Id = Entity.Id;
            this.Name = Entity.Remarkname;
            this.Remarklocalline = Entity.Remarklocalline;
            this.Remarklocalpackage = Entity.Remarklocalpackage;
        }


        public nAttachmentRemarkType(string name, string localline, string localpackage)
        {
            this.Name = name;
            this.Remarklocalline = localline;
            this.Remarklocalpackage = localpackage;

        }
    }
}
