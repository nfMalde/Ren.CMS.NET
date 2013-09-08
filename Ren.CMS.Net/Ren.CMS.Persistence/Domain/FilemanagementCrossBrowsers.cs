namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FilemanagementCrossBrowsers
    {
        #region Properties

        public virtual string browserFullName
        {
            get; set;
        }

        public virtual string browserID
        {
            get; set;
        }

        public virtual string FileFormat
        {
            get; set;
        }

        public virtual string FileType
        {
            get; set;
        }

        public virtual System.Int32 Id
        {
            get; set;
        }

        #endregion Properties
    }
}