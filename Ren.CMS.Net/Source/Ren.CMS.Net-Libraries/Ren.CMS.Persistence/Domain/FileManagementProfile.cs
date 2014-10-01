namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileManagementProfile
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string ProfileName
        {
            get; set;
        }

        public virtual List<FileManagementProfiles2FileSetting> Settings { get; set;}


        #endregion Properties
    }
}