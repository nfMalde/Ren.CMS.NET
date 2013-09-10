namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BackendWidget
    {
        #region Properties

        public virtual System.Nullable<int> DefinedHeight
        {
            get; set;
        }

        public virtual System.Nullable<int> DefinedWidth
        {
            get; set;
        }

        public virtual string Icon
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string NeededPermission
        {
            get; set;
        }

        public virtual string WidgetName
        {
            get; set;
        }

        public virtual string WidgetPartialView
        {
            get; set;
        }

        #endregion Properties
    }
}