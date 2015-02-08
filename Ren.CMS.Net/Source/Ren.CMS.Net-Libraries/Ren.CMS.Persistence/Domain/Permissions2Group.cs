namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Permissions2Group
    {
        #region Properties

        public virtual int Id { get; set; }

        public virtual string GroupID
        {
            get; set;
        }

        public virtual string Pk
        {
            get; set;
        }

        public virtual bool Val
        {
            get; set;
        }

        #endregion Properties
    }
}