namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProfileUserValue
    {
        #region Properties

        public virtual string Pkid
        {
            get; set;
        }

        public virtual string VarName
        {
            get; set;
        }

        public virtual string VarValue
        {
            get; set;
        }

        #endregion Properties
    }
}