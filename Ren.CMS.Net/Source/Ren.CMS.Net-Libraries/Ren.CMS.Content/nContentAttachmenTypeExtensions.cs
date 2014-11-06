using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nContentAttachmenTypeExtensions
    {
        public int Id { get; set; }
        public int Attachmenttypeid { get; set; }
        public string Extension { get; set; }
        public long Maxfilesize { get; set; }
        public bool Convertfile { get; set; }
        public string TargetExtension { get; set; }
        public nContentAttachmenTypeExtensions(Persistence.Domain.ContentAttachmenttypesExtensionsettings Setting)
        {
            this.Id = Setting.Id;
            this.Maxfilesize = Setting.Maxfilesize;
            this.Extension = Setting.Extension;
            this.Convertfile = Setting.Convertfile;
            this.Attachmenttypeid = Setting.Attachmenttypeid;
            if (Setting.ConvertToExt == null)
                this.TargetExtension = this.Extension;
            else
                this.TargetExtension = Setting.ConvertToExt.ToString();
        }

        public nContentAttachmenTypeExtensions(int attachmenttypeId, string ext, long maxsize, bool convert, string targetExt = null)
        {
            this.Attachmenttypeid = attachmenttypeId;
            this.Extension = ext;
            this.Maxfilesize = maxsize;
            this.Convertfile = convert;
            if (targetExt == null)
                this.TargetExtension = this.Extension;
            else
               this.TargetExtension = targetExt;
        }
    }
}
