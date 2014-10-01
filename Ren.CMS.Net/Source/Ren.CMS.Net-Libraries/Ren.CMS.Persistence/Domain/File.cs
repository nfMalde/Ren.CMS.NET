namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public virtual class File
    {
        #region Properties
        public virtual int Id { get; set; }
        public virtual int TypeID { get; set; }

        public virtual string AliasName { get; set; }

        public virtual string FilePath { get; set; }

        public virtual bool isActive { get; set; }

        public virtual string VirtualPath { get; set; }

        public virtual TbFileType FileType { get; set; }
        #endregion Properties
    }
}