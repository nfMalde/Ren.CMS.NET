namespace ThumpnailModule.Thumpnail.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    using Ren.CMS.CORE.Config;
    using Ren.CMS.Persistence;

    using ThumpnailModule.Thumpnail.Domain;

    [Ren.CMS.Persistence.Base.PersistenceMapping]
    public class ThumpnailsModuleMap : ClassMapping<TBThumpnailsModule>
    {
        #region Constructors

        public ThumpnailsModuleMap()
        {
            Table(RenConfig.DB.Prefix.Replace("dbo.","") +"Thumpnails_Module");
            Schema("dbo");
            Lazy(true);
            Id<int>(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.AtID, map => map.NotNullable(true));
            Property(x => x.LastModification, map => map.NotNullable(true));
            Property(x => x.Path);
            Property(x => x.Width);
            Property(x => x.Height);
        }

        #endregion Constructors
    }
}