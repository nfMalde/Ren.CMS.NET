namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PermissionGroup
    {
        #region Properties

        public virtual string GroupName
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual bool IsDefaultGroup
        {
            get; set;
        }

        public virtual bool IsGuestGroup
        {
            get; set;
        }

        #endregion Properties
    }
}