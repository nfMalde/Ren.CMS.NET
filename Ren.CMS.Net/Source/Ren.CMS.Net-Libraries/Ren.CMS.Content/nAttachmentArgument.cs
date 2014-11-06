using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nAttachmentArgument
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ArgumentName { get; set; }
        public string Argumentlangline { get; set; }
        public string Argumentlangpackage { get; set; }
        public nAttachmentArgument()
        {

        }
        public nAttachmentArgument(Persistence.Domain.ContentAttachmentArgument Argument)
        {
            this.Id = Argument.Id;
            this.RoleId = Argument.RoleId;
            this.ArgumentName = Argument.ArgumentName;
            this.Argumentlangline = Argument.Argumentlangline;
            this.Argumentlangpackage = Argument.Argumentlangpackage;

        }


        public nAttachmentArgument(int roleid, string name, string langline, string package)
        {
            this.RoleId = roleid;
            this.ArgumentName = name;
            this.Argumentlangline = langline;
            this.Argumentlangpackage = package;
        }
    }
}
