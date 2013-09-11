namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LinkIdentifier
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string IdentiferName
        {
            get; set;
        }

        public virtual string Theme
        {
            get; set;
        }

        #endregion Properties
    }
}