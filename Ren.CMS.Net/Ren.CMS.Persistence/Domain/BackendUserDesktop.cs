namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BackendUserDesktop
    {
        #region Properties

        public virtual string Icon
        {
            get; set;
        }

        public virtual int IconID
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string Userid
        {
            get; set;
        }

        public virtual float XPos
        {
            get; set;
        }

        public virtual float YPos
        {
            get; set;
        }

        #endregion Properties
    }
}