namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Permissionkey
    {
        #region Properties

        public virtual int Id { get; set; }

        public virtual string DefaultVal
        {
            get; set;
        }

        public virtual string LangLine
        {
            get; set;
        }

        public virtual string Pkey
        {
            get; set;
        }

        #endregion Properties
    }
}