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

        public virtual string IsDefaultGroup
        {
            get; set;
        }

        public virtual string IsGuestGroup
        {
            get; set;
        }

        #endregion Properties
    }
}