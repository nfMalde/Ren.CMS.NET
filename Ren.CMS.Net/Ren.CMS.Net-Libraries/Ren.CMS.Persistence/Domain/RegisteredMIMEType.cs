namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RegisteredMIMEType
    {
        #region Properties

        public virtual string FileExstension
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string Mimetype
        {
            get; set;
        }

        #endregion Properties
    }
}