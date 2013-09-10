namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachment
    {
        #region Properties

        public virtual string ATitle
        {
            get; set;
        }

        public virtual string AttachmentArgument
        {
            get; set;
        }

        public virtual string AttachmentRemarks
        {
            get; set;
        }

        public virtual string AttachmentType
        {
            get; set;
        }

        public virtual string ContentType
        {
            get; set;
        }

        public virtual string FName
        {
            get; set;
        }

        public virtual string FPath
        {
            get; set;
        }

        public virtual int Nid
        {
            get; set;
        }

        public virtual System.Guid Pkid
        {
            get; set;
        }

        public virtual string ThumpNail
        {
            get; set;
        }

        #endregion Properties
    }
}