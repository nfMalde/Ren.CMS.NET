namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SettingCategory
    {
        #region Properties

        public virtual string CatRel
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

        #endregion Properties
    }
}