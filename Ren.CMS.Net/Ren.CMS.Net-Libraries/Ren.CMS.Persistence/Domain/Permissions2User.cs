namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Permissions2User
    {
        #region Properties

        public virtual string GroupID
        {
            get; set;
        }

        public virtual string Pk
        {
            get; set;
        }

        public virtual string Usr
        {
            get; set;
        }

        public virtual string Val
        {
            get; set;
        }

        #endregion Properties
    }
}