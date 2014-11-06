using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nAttachmentRole
    {
        public int Id { get; set; }
        public string Rolename { get; set; }
        public string Rolelangline { get; set; }
        public string Rolelangpackage { get; set; }
        public string GetDisplayName(string LangCode = "__USER__")
        {

            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language(LangCode, this.Rolelangpackage);

            return Lang.getLine(this.Rolelangline);
        }

        public List<nAttachmentArgument> Arguments { get; set; }
        public nAttachmentRole()
        {
            this.Arguments = new List<nAttachmentArgument>();
        }
        public nAttachmentRole(Persistence.Domain.ContentAttachmentRole Role)
        {
            this.Id = Role.Id;
            this.Rolename = Role.Rolename;
            this.Rolelangline = Role.Rolelangline;
            this.Rolelangpackage = Role.Rolelangpackage;
            this.Arguments = new List<nAttachmentArgument>();

            foreach(Persistence.Domain.ContentAttachmentArgument arg in Role.Arguments)
            {
                this.Arguments.Add(new nAttachmentArgument(arg));
            }
            
        }

 


    }
}
