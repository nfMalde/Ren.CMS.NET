namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentAttachmentRole
    {
        #region Properties

        public virtual string AType
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string RoleLangLine
        {
            get; set;
        }

        public virtual string RoleName
        {
            get; set;
        }

        #endregion Properties
    }
}