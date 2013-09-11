namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class tbLanguage
    {
        #region Properties

        public virtual string Code
        {
            get; set;
        }

        public virtual string Content
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        public virtual string Package
        {
            get; set;
        }

        #endregion Properties
    }
}