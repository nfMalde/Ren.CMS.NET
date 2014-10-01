namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileManagementProfiles2FileSetting
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual int ProfileID
        {
            get; set;
        }

        public virtual int SettingID
        {
            get; set;
        }

        public virtual FileManagementFileSetting Setting { get; set; }
        #endregion Properties
    }
}