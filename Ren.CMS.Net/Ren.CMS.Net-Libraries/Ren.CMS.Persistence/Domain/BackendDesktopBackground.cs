namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BackendDesktopBackground
    {
        #region Properties

        public virtual string BackgroundAlign
        {
            get; set;
        }

        public virtual string BackgroundColor
        {
            get; set;
        }

        public virtual string BackgroundImage
        {
            get; set;
        }

        public virtual string BackgroundRepeat
        {
            get; set;
        }

        public virtual string Userid
        {
            get; set;
        }

        #endregion Properties
    }
}