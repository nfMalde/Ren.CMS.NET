namespace ThumpnailModule.Thumpnail.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TBThumpnailsModule
    {
        #region Properties

        public virtual System.Guid AtID
        {
            get; set;
        }

        public virtual System.Nullable<int> Height
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual System.DateTime LastModification
        {
            get; set;
        }

        public virtual string Path
        {
            get; set;
        }

        public virtual System.Nullable<int> Width
        {
            get; set;
        }

        #endregion Properties
    }
}