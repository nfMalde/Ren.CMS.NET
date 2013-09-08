namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProfileVar
    {
        #region Properties

        public virtual string Active
        {
            get; set;
        }

        public virtual string LangLine
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        public virtual string Section
        {
            get; set;
        }

        public virtual string ShowInProfile
        {
            get; set;
        }

        public virtual string ViewName
        {
            get; set;
        }

        #endregion Properties
    }
}