namespace ThumpnailModule.Thumpnail.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Ren.CMS.CORE.nhibernate;
    using Ren.CMS.CORE.nhibernate.Base;

    using ThumpnailModule.Thumpnail.Domain;
    using ThumpnailModule.Thumpnail.Mapping;

    public class ThumpnailRepository : BaseRepository<TBThumpnailsModule>
    {
        #region Constructors

        public ThumpnailRepository()
            : base()
        {
        }

        #endregion Constructors
    }
}