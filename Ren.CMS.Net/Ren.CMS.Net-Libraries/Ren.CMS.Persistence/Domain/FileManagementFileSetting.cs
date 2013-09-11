namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileManagementFileSetting
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string SettingName
        {
            get; set;
        }

        #endregion Properties
    }
}