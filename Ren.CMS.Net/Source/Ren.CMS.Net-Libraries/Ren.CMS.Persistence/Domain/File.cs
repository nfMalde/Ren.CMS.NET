namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class File
    {
        #region Properties
        //identity
        public virtual int Id { get; set; }

        public virtual int? FileReference { get; set; }

        //Columns
        public virtual string AliasName { get; set; }

        public virtual string FilePath { get; set; }

        public virtual bool isActive { get; set; }

        public virtual bool Physical { get; set; }

        public virtual long FileSize { get; set; }
        
        //Referenced Files
        public virtual ICollection<File> ReferencedFiles { get; set; }
        public virtual File MainFile { get; set; }
 
        #endregion Properties
    }
}