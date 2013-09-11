namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BackendMenu
    {
        #region Properties

        public virtual string Action
        {
            get; set;
        }

        public virtual System.Nullable<int> HeadID
        {
            get; set;
        }

        public virtual string IconUrl
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string MenuTextLang
        {
            get; set;
        }

        public virtual string NeededPermission
        {
            get; set;
        }

        #endregion Properties
    }
}