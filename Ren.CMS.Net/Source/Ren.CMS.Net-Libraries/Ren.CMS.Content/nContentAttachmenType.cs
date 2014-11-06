using Ren.CMS.Content.ContentAttachmentHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class nContentAttachmenType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoragePath { get; set; }
        public ContentAttachmentHandlerBase Handler { get; set; }
        public List<nContentAttachmenTypeExtensions> Settings { get; set; }
        public nContentAttachmenType() { }
        public nContentAttachmenType(Persistence.Domain.ContentAttachmenttypes Entity)
        {
           
            this.Name = Entity.Typename;
            this.Settings = new List<nContentAttachmenTypeExtensions>();
            foreach (Persistence.Domain.ContentAttachmenttypesExtensionsettings setting in Entity.AttachmentExtensions)
                this.Settings.Add(new nContentAttachmenTypeExtensions(setting));
            this.StoragePath = Entity.Storagepath;
            this.Id = Entity.Id;
            this.Handler = Activator.CreateInstance(Type.GetType(Entity.HandlerNamespace)) as ContentAttachmentHandlerBase;
        }
    }
}
