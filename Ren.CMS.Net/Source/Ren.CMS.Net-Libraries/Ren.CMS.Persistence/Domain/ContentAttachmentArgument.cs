namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachmentArgument
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual int RoleId { get; set; }
        public virtual string ArgumentName { get; set; }
        public virtual string Argumentlangline { get; set; }
        public virtual string Argumentlangpackage { get; set; }
        public virtual ContentAttachmentRole Role { get; set; }

        #endregion Properties
    }
}