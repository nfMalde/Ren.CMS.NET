namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachmentRole
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Subpath { get; set; }
        public virtual string Rolename { get; set; }
        public virtual string Rolelangline { get; set; }
        public virtual string Rolelangpackage { get; set; }

        public virtual List<ContentAttachmentRolesBindings> Bindings { get; set; }
        #endregion Properties
    }
}